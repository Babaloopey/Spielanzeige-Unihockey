
namespace Umsetzung_III
{
    // Command, der im ViewModel alles zuruecksetzt
    public class ResetAllCommand : CommandBase
    {
        private readonly SpielanzeigeViewModel _viewModel;
        public ResetAllCommand(SpielanzeigeViewModel viewModel)
        {
            _viewModel = viewModel;

        }
        public override void Execute(object? parameter)
        {
            _viewModel.ResetViewModel();

        }
    }
}
