using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchPlayer.USICommand
{
    /// <summary>
    /// setoptionに対する処理です。
    /// 現状設定は別途テキストファイルから読み込むため何もしません。
    /// </summary>
    public class SetOption : USICommandHandler
    {
        protected override void RequestImpl(string command)
        {
            //evaldirなどすべてのエンジンに同一の値を送るのが適切ではない場合は何もしない
            if (command.Contains("EvalDir"))
                return;
            

            Engines.Instance.SendCommandToAllEngines(command);
        }

        protected override bool IsAvailable(string command)
        {
            string[] split = command.Split(' ');
            return split.Length > 0 && split[0] == "setoption";
        }

        public override void Response(string res)
        {
        }
    }
}