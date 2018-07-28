using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchPlayer.USICommand
{
    public class USICommandHandlerBuilder
    {
        public static USICommandHandler GetInstance()
        {
            USICommandHandler h = new USI();
            h.AddHandler(new SetOption());
            h.AddHandler(new PositionAndGo());
            h.AddHandler(new IsReady());
            h.AddHandler(new USINewGame());
            h.AddHandler(new Quit());
            h.AddHandler(new GameOver());

            return h;
        }

    }
}
