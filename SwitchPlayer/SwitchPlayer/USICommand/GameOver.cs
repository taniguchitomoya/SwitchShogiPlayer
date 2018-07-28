using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchPlayer.USICommand
{
    public class GameOver : USICommandHandler
    {
        public override void Response(string res)
        {
        }

        protected override bool IsAvailable(string command)
        {
            return command.FirstToken() == "gameover";
        }

        protected override void RequestImpl(string command)
        {
            //どのエンジンにgameoverを流せばよいのか自明でないので保留
            //すべてのエンジンに送る方法もあるだろうし、無視する手もある
        }
    }
}
