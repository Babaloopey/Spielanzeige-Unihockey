using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umsetzung_III.Services;

namespace Umsetzung_III.Commands
{
    public class BuzzerCommand : CommandBase
    {
        private BuzzerService buzzer = new BuzzerService();

        public BuzzerCommand() { }

        public override void Execute(object? parameter)
        {
            buzzer.Buzz();
        }
    }
}
