using System;
using System.Linq;

namespace SwitchPlayer.USICommand
{
    /// <summary>
    /// USIコマンド position、go、およびそれらに関連する命令をを処理するためのクラス
    /// </summary>
    public class PositionAndGo : USICommandHandler
    {

        protected override bool IsAvailable(string command)
        {
            string token = command.FirstToken();
            return new[]{ "position","go","stop","ponderhit"}.Contains(token);
        }

        protected override void RequestImpl(string command)
        {
            string token = command.FirstToken();
            if (token == "position")
                PositionCommand(command);
            else if (token == "go")
                GoCommand(command);
            else if (token == "stop" || token == "ponderhit")
                OtherCommand(command);
        }

        /// <summary>
        /// 思考局面の手数
        /// </summary>
        private int gamePly;

        /// <summary>
        /// "go"の処理
        /// </summary>
        private void GoCommand(string command)
        {
            Engines.Instance.Handler = this;
            Engines.Instance.SendCommand(command);
        }

        /// <summary>
        /// stop,ponderhitの処理
        /// </summary>
        /// <param name="command"></param>
        private void OtherCommand(string command)
        {
            Engines.Instance.SendCommand(command);
        }

        /// <summary>
        /// "position"の処理
        /// </summary>
        /// <param name="command"></param>
        private void PositionCommand(string command)
        {
            string[] split = command.Split(' ');

            string position = split[0]; //"position"であるはず
            int startCount = 1;//開始局面の手数 平手の開始局面なら1

            int pos = 2;

            if (split[1] == "sfen")
            {
                //split[4] == "w" || split[4] == "bs"
                //split[5] == "-"
                int.TryParse(split[6], out startCount);
                pos = 7;
            }

            gamePly = startCount;//現在の手数、とりあえず開始局面の手数を代入する。下でmovesの分だけ加算する。

            if (split.Length > pos && split[pos] == "moves")
            {
                gamePly += split.Length - pos;
            }

            //手数の進行をエンジンに通知して実際に動くエンジンを切り替える
            Engines.Instance.SetProgress(gamePly);

            //切り替わったあとのエンジンに対してコマンド送信
            Engines.Instance.SendCommand(command);
        }

        public override void Response(string res)
        {
            if (res.FirstToken() == "bestmove")
            {
                Engines.Instance.Handler = null;
                Console.WriteLine(res);
            }
            else if (res.FirstToken() == "info") {
                //bestmoveが返ってきたらコールバック用のハンドラを解除するので、
                //ponderのinfoはここで処理されないはず
                Console.WriteLine(res);
            }
        }

    }
}
