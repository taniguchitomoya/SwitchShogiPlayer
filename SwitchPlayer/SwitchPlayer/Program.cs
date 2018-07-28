using SwitchPlayer.USICommand;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.ReadLine()が標準では256文字までしか対応していない
            Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));

            USICommandHandler handler = USICommandHandlerBuilder.GetInstance();

            while (true)
            {
                string line = Console.ReadLine();
                handler.Request(line);
            }
        }
    }
}
