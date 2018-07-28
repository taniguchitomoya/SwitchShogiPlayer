using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchPlayer.USICommand
{
    public class USINewGame : USICommandHandler
    {

        protected override bool IsAvailable(string command)
        {
            return command == "usinewgame";
        }

        protected override void RequestImpl(string command)
        {
            Engines.Instance.SendCommandToAllEngines("usinewgame");
        }

        public override void Response(string res)
        {
            throw new NotImplementedException();
        }

    }
}
