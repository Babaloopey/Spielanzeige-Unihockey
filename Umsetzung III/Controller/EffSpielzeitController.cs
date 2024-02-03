using System;
using Umsetzung_III.Interface;

namespace Umsetzung_III.Controller
{
    public class EffSpielzeitController
    {
        public bool IsEffektiveSpielzeitVisible = false;
        private readonly SpielanzeigeViewModel viewModel;
        private readonly TimeDeliverer spielzeitStore;

        public event Action OnEffektiveSpielzeitVisibilityChanged;

        public EffSpielzeitController(SpielanzeigeViewModel spielanzeigeViewModel, TimeDeliverer spielzeitStore)
        {
            this.spielzeitStore = spielzeitStore;
            viewModel = spielanzeigeViewModel;
        }

        public void CheckIfEffektiveSpielzeitMustBeVisible()
        {
            if (spielzeitStore.GetActualSpielMinute() < 3 && viewModel.Halbzeit == 2)
            {
                IsEffektiveSpielzeitVisible = true;
                EffektiveSpielzeitVisibilityChanged();
            }
            else
            {
                IsEffektiveSpielzeitVisible = false;
                EffektiveSpielzeitVisibilityChanged();
            }
        }

        private void EffektiveSpielzeitVisibilityChanged()
        {
            OnEffektiveSpielzeitVisibilityChanged?.Invoke();
        }
    }
}
