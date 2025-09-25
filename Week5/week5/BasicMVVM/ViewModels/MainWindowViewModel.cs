namespace BasicMVVM.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public SimpleViewModel SimpleVM => new();
        public ReactiveViewModel ReactiveVM => new();

    }
}
