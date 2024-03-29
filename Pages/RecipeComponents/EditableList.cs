﻿using Microsoft.AspNetCore.Components;
using RecipeAZ.Models;
using RecipeAZ.Interfaces;
using System.ComponentModel;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MudBlazor;

namespace RecipeAZ.Pages.RecipeComponents {
    public class EditableList<T> : ComponentBase where T : IEditableListItem<T>, new() {
        [Inject]
        public ISnackbar RecipeSnack { get; set; }
        [CascadingParameter(Name = "RecipeParam")]
        public Recipe? ItemRecipe { get; set; }

        [CascadingParameter(Name = "CanEditParam")]
        protected bool CanEdit { get; set; }
        [CascadingParameter(Name = "EditingParam")]
        protected bool Editing { get; set; }
        public T LastItem { get; set; } = new(); //LastItem is the last item in the list, which means it is the only one that could be in the process of being added as a new ingredient
        
        public bool CannotAddItem {
            get {
                return LastItem == null;
            }
        }
    }
}
