using System.Windows.Forms;
using SharedStream;

namespace balloonapp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // 設定情報取得
            var settings = new Settings();
            settings.ReadSettings();
            // 多重起動防止
            string mutexName = "BalloonApp";
            Mutex mutex = new(true, mutexName, out bool createdNew);
            // 新規に起動
            if (createdNew)
            {
                LogFile.Write($"{mutexName}単体起動", settings.DebugMode);
                try
                {
                    // To customize application configuration such as set high DPI settings or default font,
                    // see https://aka.ms/applicationconfiguration.
                    ApplicationConfiguration.Initialize();
                    var obj = new Form1();
                    // コマンドライン取得
                    obj.FrmCtrl.Option(args);
                    var StrMailCount = obj.FrmCtrl.StrMailCount;
                    // 応答監視の仕組み
                    // 一定時間後にあるステータスの状況がfalseならば、ある処理をする。
                    LogFile.Write($"新着メール数:{StrMailCount}", settings.DebugMode);
                    Application.Run(obj);
                }
                catch (Exception ex)
                {
                    LogFile.Write(ex);
                }
                // 多重起動防止を解放
                mutex.ReleaseMutex();
            }
            // 既に起動中
            else
            {
                // 新着メール数をプロセス間メッセージとして送信する
                var pipeobj = new NamedPipe(
                    NamedPipe.PipeName ,
                    settings.DebugMode);
                try
                {
                    LogFile.Write($"{mutexName}多重起動", settings.DebugMode);
                    ApplicationConfiguration.Initialize();
                    var obj = new Form1();
                    // コマンドライン取得
                    obj.FrmCtrl.Option(args);
                    // 新着メール数を取得
                    var StrMailCount = obj.FrmCtrl.StrMailCount;
                    var msg = $"プロセス間メッセージとして新着メール数を送信する:{StrMailCount}";
                    LogFile.Write(msg, settings.DebugMode);

                    // バルーンチップ表示メソッド起動メッセージ送信
                    pipeobj.SendMessage($"{pipeobj.ExecThreadMessage} {StrMailCount}");
                    msg = $"バルーンチップ表示メソッド起動メッセージを送信した。{pipeobj.Message}";
                    LogFile.Write(msg, settings.DebugMode);
#if DEBUG
                    MessageBox.Show(pipeobj.Message);
                    msg = string.Format($"{StrMailCount}:{pipeobj.ExecThreadMessage}:{pipeobj.Message}");
                    MessageBox.Show(msg, "バルーンチップは起動中");
#endif              
                    // パイプクローズメッセージ送信
                    pipeobj.SendMessage(pipeobj.CloseThreadMessage);
                    msg = $"{pipeobj.Message} -> {mutexName}パイプクローズ";
                    LogFile.Write(msg, settings.DebugMode);
                }
                catch (Exception ex)
                {
                    LogFile.Write(ex);
                }
            }
            // 多重起動防止処理クローズ
            mutex.Close();
        }
    }
}