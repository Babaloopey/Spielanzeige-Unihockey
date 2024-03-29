﻿
using static Umsetzung_III.Model.Actions;

namespace Umsetzung_III
{
    // Command, der je Nach Strafe und Store beim jeweiligen Team eine Strafe beginnt
    public class StrafenCommand: CommandBase
    {
        private readonly Strafe _strafe;
        private readonly StrafenStore _strafenStore;

        public StrafenCommand(StrafenStore strafenStore, Strafe strafe)
        {
            _strafe = strafe;
            _strafenStore = strafenStore;
        }

        public override void Execute(object? parameter)
        {
            switch (_strafe)
            {
                case Strafe.Delete:
                    if(parameter != null)
                    {
                        _strafenStore.Delete(parameter);
                    }
                   
                    break;
                default:
                    _strafenStore.Create(_strafe);
                    break;
            }
        }
    }
}
