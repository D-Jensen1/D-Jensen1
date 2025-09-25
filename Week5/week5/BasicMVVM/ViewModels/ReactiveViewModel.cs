using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicMVVM.ViewModels
{
    public class ReactiveViewModel : ReactiveObject
    {
        public ReactiveViewModel()
        {
            this.WhenPropertyChanged(me => me.Name)
                .Subscribe(property => this.RaisePropertyChanged(nameof(Greeting)));
        }
        private string? _Name; // This is our backing field for Name

        public string? Name
        {
            get
            {
                return _Name;
            }
            set
            {
                // We can use "RaiseAndSetIfChanged" to check if the value changed and automatically notify the UI
                this.RaiseAndSetIfChanged(ref _Name, value);
            }
        }

        // Greeting will change based on a Name.
        public string Greeting
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    // If no Name is provided, use a default Greeting
                    return "Hello World from Avalonia.Samples";
                }
                else
                {
                    // else greet the User.
                    return $"Hello {Name}";
                }
            }
        }
    }
}
