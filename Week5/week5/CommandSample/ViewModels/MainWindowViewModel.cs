using System.Collections.ObjectModel;

namespace CommandSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReactiveUICommandsViewModel ReactiveUICommandsViewModel { get; } = new ReactiveUICommandsViewModel();

    }
}
