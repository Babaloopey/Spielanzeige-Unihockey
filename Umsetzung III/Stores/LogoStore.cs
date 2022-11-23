using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umsetzung_III.Stores
{
    public class LogoStore
    {
        private readonly SpielanzeigeViewModel viewModel;

        public bool IsLogoVisible = true;

        public event Action OnLogoVisibilityChanged;

        public LogoStore(SpielanzeigeViewModel spielanzeigeViewModel)
        {
            viewModel = spielanzeigeViewModel;
        }

        public void CheckIfLogoMustBeVisible()
        {
               if(!viewModel.GastTeamStrafeRunning && !viewModel.HeimTeamStrafeRunning)
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
