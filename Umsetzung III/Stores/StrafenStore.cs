﻿using System;
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
        public int StrafenAnzeigeGroesse = 0;

        public event Action OnStrafenChanged;
        public event Action OnStrafenAnzeigeGroesseChanged;

        private int grosseSchrift = 100;
        private int mittlereSchrift = 70;
        private int kleineSchrift = 55;

        public StrafenStore(SpielanzeigeViewModel spielanzeigeviewModel)
        {
            _spielanzeigeViewModel = spielanzeigeviewModel;

        }
        public void CreateStrafe(Strafe strafe)
        {
            int _strafSekunde = _spielanzeigeViewModel.SpielSekunde;

            switch (strafe)
            {
                case Strafe.Zwei:
                    Strafen.Add(new AngezeigteStrafe(calculateStrafminute(2), _strafSekunde));
                    break;
                case Strafe.Vier:
                    Strafen.Add(new AngezeigteStrafe(calculateStrafminute(2), _strafSekunde));
                    Strafen.Add(new AngezeigteStrafe(calculateStrafminute(4), _strafSekunde));
                    break;
                case Strafe.Zehn:
                    Strafen.Add(new AngezeigteStrafe(calculateStrafminute(10), _strafSekunde));
                    break;
            }

            StrafenChanged();

        }

        private int calculateStrafminute(int strafzeit)
        {
            int strafMinute = _spielanzeigeViewModel.SpielMinute - strafzeit;

            if (strafMinute < 0)
            {
                strafMinute = 20 + strafMinute;
            }

            return strafMinute;
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

        public void AdjustStrafenAnzeigeGroesse()
        {
            if(Strafen.Count == 1)
            {
                StrafenAnzeigeGroesse = grosseSchrift;
            }
            else if(Strafen.Count == 2)
            {
                StrafenAnzeigeGroesse = mittlereSchrift;
            }
            else
            {
                StrafenAnzeigeGroesse = kleineSchrift;
            }
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
            AdjustStrafenAnzeigeGroesse();
            StrafenAnzeigeGroesseChanged();
           OnStrafenChanged?.Invoke();
        }

        private void StrafenAnzeigeGroesseChanged()
        {
            OnStrafenAnzeigeGroesseChanged?.Invoke();
        }
    }
}
