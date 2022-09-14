using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Umsetzung_III.Model;
using static Umsetzung_III.Actions;

namespace Umsetzung_III
{
    public class StrafenStore
    {
        private readonly SpielanzeigeViewModel _spielanzeigeViewModel;

        public ObservableCollection<AngezeigteStrafe> Strafen = new ObservableCollection<AngezeigteStrafe>();

        public bool StrafeIsRunning => Strafen.Count > 0 ? true : false;

        public event Action OnStrafenChanged;


        public StrafenStore(SpielanzeigeViewModel spielanzeigeviewModel)
        {
            _spielanzeigeViewModel = spielanzeigeviewModel;

        }
        public void CreateStrafe(Strafe strafe)
        {
            int _strafMinute = 0;
            int _strafSekunde = 0;
            switch (strafe)
            {
                case Strafe.Zwei:
                    _strafMinute = _spielanzeigeViewModel.SpielMinute - 2;
                    break;
                case Strafe.Fuenf:
                    _strafMinute = _spielanzeigeViewModel.SpielMinute - 5;
                    break;
                case Strafe.Zehn:
                    _strafMinute = _spielanzeigeViewModel.SpielMinute - 10;
                    break;
            }

            if (_strafMinute < 0)
            {
                _strafMinute = 20 + _strafMinute;
            }

            _strafSekunde = _spielanzeigeViewModel.SpielSekunde;

            Strafen.Add(new AngezeigteStrafe(_strafMinute, _strafSekunde));
            StrafenChanged();

        }

        public void CheckIfStrafeStillActive()
        {
            //    secondCounter++;
            //    if (secondCounter >= 10)
            //    {
            //        for (var i = Strafen.Count - 1; i >= 0; i--)
            //        {
            //            if (Strafen[i].minute == _spielanzeigeViewModel.SpielMinute && Strafen[i].second == _spielanzeigeViewModel.SpielSekunde)
            //            {
            //                RemoveStrafeByIndex(i);
            //            }
            //        }
            //        secondCounter = 0;
            //    }
        }

        public void Delete(object strafe)
        {
            if (strafe != null)
            {
                if (strafe.GetType() == typeof(AngezeigteStrafe))
                {
                    Strafen.Remove((AngezeigteStrafe)strafe);
                    StrafenChanged();
                }
            }
        }

        public void Reset()
        {
            Strafen.Clear();
            StrafenChanged();
        }

        private void StrafenChanged()
        {
           OnStrafenChanged?.Invoke();
        }
    }
}
