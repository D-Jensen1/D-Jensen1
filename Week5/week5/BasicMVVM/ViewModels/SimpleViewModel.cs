using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BasicMVVM.ViewModels
{
    public class SimpleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string? _name = null;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    Debug.WriteLine($"Name changing from {_name} to {value}.");
                    _name = value;
                    
                    this.RaisePropertyChanged(nameof(Greeting)); // Triggers event to update Greetingm
                }
            }
        }

        public string Greeting { get => $"Hello {_name?? "(enter your name above)"}"; }

    }
}
