using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchPlayer.USICommand
{
    /// <summary>
    /// USIコマンド"usi"の処理を担当するクラスです。
    /// </summary>
    //TODO 各エンジンのsetoptionに相当する処理もこのクラスで行うべき？
    public class USI : USICommandHandler
    {
        int usiokCount = 0;

        protected override bool IsAvailable(string command)
        {
            return command == "usi";
        }

        protected override void RequestImpl(string command)
        {

            Console.WriteLine("id name SwitchPlayer");
            Console.WriteLine("id author Tomoya Taniguchi");

            usiokCount = 0;
            Engines engines = Engines.Instance;
            //コールバック用に登録する
            engines.Handler = this;
            engines.SendCommandToAllEngines("usi");
        }

        public override void Response(string res)
        {
            if (res == "usiok")
            {
                usiokCount++;
                if (usiokCount == Engines.Instance.EngineCount)
                {
                    //すべてのエンジンからusiokが返ってきた
                    Console.WriteLine("usiok");
                    //コールバックの解除
                    Engines.Instance.Handler = null;
                }
            }
            else if (res.FirstToken() == "option")
            {
                //複数のエンジンでコンフリクトが起こるがとりあえずそのまま流してみる
                Console.WriteLine(res);
            }
        }
    }
}
