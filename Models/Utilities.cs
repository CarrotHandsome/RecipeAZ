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
}
