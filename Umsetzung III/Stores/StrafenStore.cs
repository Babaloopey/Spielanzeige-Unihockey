using System;
using System.Collections.ObjectModel;
using System.Linq;
using Umsetzung_III.Interface;
using Umsetzung_III.Model;
using Umsetzung_III.Services;
using static Umsetzung_III.Actions;

namespace Umsetzung_III
{

    public class StrafenStore
    {
        private readonly TimeDeliverer _spielzeitStore;

        public ObservableCollection<AngezeigteStrafe> Strafen = new ObservableCollection<AngezeigteStrafe>();

        public bool IsStrafeRunning => Strafen.Count > 0 ? true : false;

        public event Action OnStrafenChanged;
        public event Action OnStrafenAnzeigeGroesseChanged;

        public virtual Schrift StrafenAnzeigeGroesse { get; set; } = 0;

        public StrafenStore(TimeDeliverer spielzeitStore)
        {
            _spielzeitStore = spielzeitStore;

        }
        public void Create(Strafe strafe)
        {
            int _strafSekunde = _spielzeitStore.GetActualSpielSecond();

            switch (strafe)
            {
                case Strafe.Zwei:
                    Strafen.Add(new AngezeigteStrafe(CalculateStrafminute(2), _strafSekunde));
                    break;
                case Strafe.Vier:
                    Strafen.Add(new AngezeigteStrafe(CalculateStrafminute(2), _strafSekunde));
                    Strafen.Add(new AngezeigteStrafe(CalculateStrafminute(4), _strafSekunde));
                    break;
                case Strafe.Zehn:
                    Strafen.Add(new AngezeigteStrafe(CalculateStrafminute(10), _strafSekunde));
                    break;
            }

            StrafenChanged();

        }

        private int CalculateStrafminute(int strafzeit)
        {
            int strafMinute = _spielzeitStore.GetActualSpielMinute() - strafzeit;

            if (strafMinute < 0)
            {
                strafMinute = 20 + strafMinute;
            }

            return strafMinute;
        }

        public void AdjustStrafenAnzeigeGroesse()
        {
            if(Strafen.Count == 1)
            {
                StrafenAnzeigeGroesse = Schrift.gross;
            }
            else if(Strafen.Count == 2)
            {
                StrafenAnzeigeGroesse = Schrift.mittel;
            }
            else
            {
                StrafenAnzeigeGroesse = Schrift.klein;
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
