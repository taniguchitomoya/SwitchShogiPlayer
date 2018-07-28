using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SwitchPlayer.USICommand
{
    public class Quit : USICommandHandler
    {
        public override void Response(string res)
        {
        }

        protected override bool IsAvailable(string command)
        {
            return command.FirstToken() == "quit";
        }

        protected override void RequestImpl(string command)
        {
            Engines.Instance.SendCommandToAllEngines(command);
            Thread.Sleep(100);//各エンジンの終了処理を少しだけ待つ
            Environment.Exit(0);
        }
    }
}
