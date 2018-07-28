using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchPlayer.USICommand
{

    /// <summary>
    /// USIコマンドを処理するためのクラスです
    /// 
    /// 基本的に１つのクラスが一つの命令に対応します。
    /// 複数の命令が関連している部分については1つのクラスで複数の処理をすることがあります。
    /// 
    /// Chains of Responsibilityパターンによる実装
    /// </summary>
    public abstract class USICommandHandler
    {
        /// <summary>
        /// 
        /// see Chains of Responsibility パターン
        /// </summary>
        public USICommandHandler Next { get; set; }


        /// <summary>
        /// 引数で渡されたコマンドをこのオブジェクトが処理するかどうかを返します
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected abstract bool IsAvailable(string command);

        /// <summary>
        /// 時間のかかる処理の場合は、別スレッドで実行し、処理をすぐに返すコードを書く。
        /// 
        /// 処理の結果についてはResponseメソッドを呼び出す
        /// 
        /// </summary>
        /// <param name="command"></param>
        protected abstract void RequestImpl(string command);


        public void Request(string command)
        {
            if (IsAvailable(command))
            {
                RequestImpl(command);
            }
            else if (Next != null)
            {
                Next.Request(command);
            }
            else {
                //与えられたコマンドは処理できなかった

            }
        }

        /// <summary>
        /// プロセスからの返答を受け取るためのメソッド
        /// 何もしない。
        /// 子クラスにおいてoverrideして挙動を変える
        /// </summary>
        /// <param name="res"></param>
        public abstract void Response(string res);


        /// <summary>
        /// chainの末尾にHandlerを追加します
        /// </summary>
        /// <param name="setOption"></param>
        internal void AddHandler(USICommandHandler handler)
        {
            USICommandHandler h = this;
            while (h.Next != null) h = h.Next;
            h.Next = handler;
        }
    }
}
