using ReactiveUI;
using System.Reactive;

namespace MortgageCalcAvaloniaUI.ViewModels;

public class MainViewModel : ViewModelBase
{
    private decimal _homeCost;
    private decimal _downPayment;
    private decimal _interestRate;
    private int _loanTerm;

    public decimal HomeCost
    {
        get => _homeCost;
        set => this.RaiseAndSetIfChanged(ref _homeCost, value);
    }

    public decimal DownPayment
    {
        get => _downPayment;
        set => this.RaiseAndSetIfChanged(ref _downPayment, value);
    }

    public decimal InterestRate
    {
        get => _interestRate;
        set => this.RaiseAndSetIfChanged(ref _interestRate, value);
    }

    public int LoanTerm
    {
        get => _loanTerm;
        set => this.RaiseAndSetIfChanged(ref _loanTerm, value);
    }


}
