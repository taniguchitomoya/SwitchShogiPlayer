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

    /// <summary>
    /// 複数の将棋エンジンを管理するためのクラスです。
    /// 
    /// Singletonクラスです。インスタンスを取得したい場合はInstanceプロパティを使用してください。
    /// </summary>
    public class Engines
    {
        /// <summary>
        /// 登録されているエンジンの数を表します
        /// </summary>
        public int EngineCount {
            get
            {
                return processes.Count;
            }
        }

        /// <summary>
        /// エンジンからの標準出力を受け取りたいハンドラを登録する
        /// 
        /// (たぶんeventとして実装するのが正しいがとりあえずこれで困らなさそうなので仮実装）
        /// </summary>
        public USICommandHandler Handler { get; set; }


        /// <summary>
        /// 使用するエンジンのプロセスたち
        /// </summary>
        List<Process> processes = new List<Process>();

        /// <summary>
        /// 現在使用中のプロセス
        /// </summary>
        Process nowProcess = null;

        /// <summary>
        /// 何手目までi番目のエンジンを使用するか
        /// </summary>
        List<int> to = new List<int>();

        private static Engines instance = new Engines();

        public static Engines Instance
        {
            get
            {
                return instance;
            }
        }


        public Engines()
        {
            using (StreamReader sr = new StreamReader("engines.txt"))
            {
                int engineCount = int.Parse(sr.ReadLine());
                for (int i = 0;i < engineCount;i++)
                {
                    Process p = new Process();
                    p.StartInfo.WorkingDirectory = sr.ReadLine();
                    p.StartInfo.FileName = sr.ReadLine();
                    p.StartInfo.UseShellExecute = false;
                    p.OutputDataReceived += OutputDataReceived;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.Start();
                    p.BeginOutputReadLine();
                    processes.Add(p);
                    to.Add(int.Parse(sr.ReadLine()));
                }
            }
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Debug.WriteLine("from engine:" + e.Data);

            Process senderProcess = (Process)sender;
            processes.IndexOf(senderProcess);

            if (Handler != null)
            {
                Handler.Response(e.Data);
            }
        }

        /// <summary>
        /// ゲームの進行状況を通知するときに使用するメソッドです
        /// 
        /// 与えられた進行状況に応じてエンジンを切り替えます
        /// </summary>
        /// <param name="gamePly">ゲームの進行状況</param>
        public void SetProgress(int gamePly)
        {
            Process lastProcess = nowProcess;
            for (int i = 0; i < to.Count; i++)
                if (gamePly < to[i])
                {
                    nowProcess = processes[i];
                    break;
                }

            if (lastProcess != null && lastProcess != nowProcess) {
                //プロセスが変更されたので一つ前のプロセスの思考を停止する
                lastProcess.StandardInput.WriteLine("stop");
            }

        }


        /// <summary>
        /// すべてのエンジンに同一のコマンドを送信する
        /// </summary>
        /// <param name="command"></param>
        public void SendCommandToAllEngines(string command)
        {
            foreach (Process p in processes)
            {
                p.StandardInput.WriteLine(command);
            }
        }

        /// <summary>
        /// 現在使用中のエンジンにコマンドを送信する
        /// </summary>
        /// <param name="count"></param>
        /// <param name="command"></param>
        /// <param name="sender"></param>
        public void SendCommand(string command)
        {
            nowProcess.StandardInput.WriteLine(command);
        }
 
    }
}
