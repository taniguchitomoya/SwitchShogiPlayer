using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchPlayer.USICommand
{
    public class IsReady : USICommandHandler
    {
        int isReadyCount = 0;
        protected override bool IsAvailable(string command)
        {
            return command == "isready";
        }

        protected override void RequestImpl(string command)
        {
            Engines.Instance.Handler = this;
            isReadyCount = 0;
            Engines.Instance.SendCommandToAllEngines("isready");
        }

        public override void Response(string res)
        {
            if (res == "readyok")
            {
                isReadyCount++;
                if (isReadyCount == Engines.Instance.EngineCount)
                {
                    //すべてのエンジンからreadyokが返ってきた
                    Console.WriteLine("readyok");
                    //コールバックの解除
                    Engines.Instance.Handler = null;
                }
            }
        }
    }
}
