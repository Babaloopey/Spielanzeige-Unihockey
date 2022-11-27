using System;

namespace Umsetzung_III.Controller
{
    public class LogoController
    {
        private readonly SpielanzeigeViewModel viewModel;

        public bool IsLogoVisible = true;

        public event Action OnLogoVisibilityChanged;

        public LogoController(SpielanzeigeViewModel spielanzeigeViewModel)
        {
            viewModel = spielanzeigeViewModel;
        }

        public void CheckIfLogoMustBeVisible()
        {
            if (!viewModel.GastTeamStrafeRunning && !viewModel.HeimTeamStrafeRunning)
            {
                IsLogoVisible = true;
                LogoVisibilityChanged();
            }
            else
            {
                IsLogoVisible = false;
                LogoVisibilityChanged();
            }

        }

        private void LogoVisibilityChanged()
        {
            OnLogoVisibilityChanged?.Invoke();
        }

    }
}
