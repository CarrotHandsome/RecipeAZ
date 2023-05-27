using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RecipeAZ.Models {
    
    public class BoolWrapper {
        private bool _value;

        public bool Value {
            get => _value;
            set {
                if (_value != value) {
                    _value = value;
                    ValueChanged?.Invoke();
                }
            }
        }

        public event Action ValueChanged;
    }

    public class EditService {
        public event Action OnEditButtonClick;

        public void NotifyEditButtonClick() {
            Console.WriteLine("Edit button clicked");
            OnEditButtonClick?.Invoke();
        }
    }

    public class NavHelperService {
        private readonly NavigationManager _navigationManager;
        public NavHelperService(NavigationManager navigationManager) {
            _navigationManager = navigationManager;
        }
        public void NavigateToRecipe(string id) {
            Console.WriteLine(_navigationManager == null);
            Console.WriteLine("navigating to recipe " + id);
            _navigationManager.NavigateTo($"/recipe/{id}", true);
        }
        public void NavigateToUser(string id) {
            _navigationManager.NavigateTo($"/profile/{id}", true);
        }
    }
    public class TextProcessing {
        public List<Tuple<string, bool, string>> ProcessString(string input) {
            var result = new List<Tuple<string, bool, string>>();
            var matches = Regex.Matches(input, @"@[^@]*@|[^@]+");

            foreach (Match match in matches) {
                string matchValue = match.Value;
                bool isId = matchValue.StartsWith("@") && matchValue.EndsWith("@");
                string afterSlash = null;

                if (isId) {
                    // Strip the '@' characters from the start and end.
                    matchValue = matchValue.Substring(1, matchValue.Length - 2);

                    // Check for a '/' character in the ID.
                    int slashIndex = matchValue.IndexOf('/');
                    if (slashIndex != -1 && slashIndex < matchValue.Length - 1) {
                        afterSlash = matchValue.Substring(slashIndex + 1);
                        matchValue = matchValue.Substring(0, slashIndex);
                    }
                }

                // Add it to the result list
                result.Add(Tuple.Create(matchValue, isId, afterSlash));
            }

            return result;
        }
        
    }
    public class CursorSpan {
        public int Item1 { get; set; }
        public int Item2 { get; set; }
    }

    public class EditEntity {
        public string Title { get; set; }
        public int Lines { get; set; }
        public bool CanLinkToOtherRecipes { get; set; }
    }
}
