using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedStream;
using balloonapp.Properties;

namespace balloonapp
{
    public class FrmCtrl
    {
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
        public FrmCtrl(
            Form fval,  // Formオブジェクト
            System.ComponentModel.IContainer pcontainer // コンテナ―
            )
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
        {
            FormProp = fval;
            // 秀丸メールのプログラムパス
            HideMailPath = "";
            // 秀丸メール起動パラメータ
            HideMailParameter = "";
            // 新着メール件数
            StrMailCount = "";
            // 新着メール件数
            IntMailCount = 0;
            // バルーンアイコンのインスタンスを生成
            NotifyIcon1 = new NotifyIcon(pcontainer);
            // バルーンアイコンコントロールインスタンス生成
            balloonCtrl = new BalloonCtrl(NotifyIcon1);
            // プロセス間通信のインスタンス（受信側）
            NamedPipeObj = new NamedPipe
            {
                // 受信時実行メソッド登録（デリゲート）
                ExeMethod = Balloon2
            };
            // ログファイルフラグ
            NamedPipeObj.LogFlag = true;
        }
        internal NamedPipe NamedPipeObj { get;  set; }
        // バルーンヒントクリックイベントプロパティ
        public EventHandler EventHandler_BalloonTipClicked_Prop
        { get; set; }
        // バルーンアイコンクリックイベントプロパティ
        public EventHandler EventHandler_BalloonIconClick_Prop
        { get; set; }
        // バルーンヒント表示イベントプロパティ
        public EventHandler EventHandler_BalloonTipShown_Prop
        { get; set; }
        // バルーンヒント表示イベント処理実行
        public bool BalloonTipShownAct_Prop
        { get; set; }
        /// <summary>
        /// フォームプロパティ
        /// </summary>
        public Form FormProp
            { get; set; }
        /// <summary>
        /// オプション処理
        /// </summary>
        /// <param name="args">コマンドライン引数</param>
        public void Option(string[] args)
        {
            bool mailcount_flg = false;
            foreach (var item in args)
            {
                if (item == "/C")
                {
                    mailcount_flg = true;
                    continue;
                }
                if (mailcount_flg == true)
                {
                    StrMailCount = item;
                    // 現在の値に加算する
                    IntMailCount += Int32.Parse(StrMailCount);                    
                    continue;
                }
            }
        }
        /// <summary>
        /// 新着メール件数（文字型）
        /// 秀丸メールから起動オプションで受け取る
        /// </summary>
        public string StrMailCount
        {
            get; set;
        }
        /// <summary>
        /// 新着メール件数（数値型）
        /// 秀丸メールから起動オプションで受け取る
        /// </summary>
        public int IntMailCount
        {
            get; set;
        }
        /// <summary>
        /// 秀丸メールのプログラムパス
        /// </summary>
        public string HideMailPath
        {
            get; set;
        }
        /// <summary>
        /// 秀丸メール起動パラメータ
        /// </summary>
        public string HideMailParameter
        {
            get; set;
        }
        // バルーンアイコン
        public readonly NotifyIcon NotifyIcon1;
        // バルーンアイコンコントロールクラス
        public BalloonCtrl balloonCtrl;
        /// <summary>
        /// プログラムを起動
        /// </summary>
        /// <param name="prg">プログラム本体パス</param>
        /// <param name="para">起動パラメータ</param>
        public void ProcessStart(
            string prg,
            string para
            )
        {
            var runprg = string.Format($"\"{prg}\"");
            if (para.Length > 0)
            {
                runprg += " " + para;
            }
            Process.Start(runprg);
        }
        #region バルーンコントロール
        /// <summary>
        /// バルーンチップ表示
        /// </summary>
        /// <param name="mailcount">追加の新着メール数</param>
        /// <remarks>
        /// このメソッドはThreadとして呼ばれます。
        /// </remarks>
        public async void Balloon2(string mailcount)
        {
            // 新着メール数を加算
            if (mailcount != null)
            {
                this.IntMailCount += Int32.Parse(mailcount);
            }
            //バルーンヒントに表示するメッセージ
            if (this.balloonCtrl.NewMailCount == true)
            {
                // バルーンメッセージ＋新着メール件数を設定
                this.balloonCtrl.BalloonText =
                    this.balloonCtrl.BalloonMessage + "\n" +
                    string.Format(this.balloonCtrl.MailCountFrm, 
                    this.IntMailCount.ToString());
            }
            else
            {   // バルーンメッセージ
                this.balloonCtrl.BalloonText = this.balloonCtrl.BalloonMessage;
            }
            //表示する時間をミリ秒で指定する
            this.balloonCtrl.Balloon(30000);
            await Task.Delay(1);
        }
        /// <summary>
        /// バルーンチップ表示
        /// </summary>
        public void Balloon()
        {
            //バルーンヒントを表示する
            //表示する時間をミリ秒で指定する
            this.balloonCtrl.Balloon(30000);
        }
        /// <summary>
        /// バルーンチップ表示設定
        /// </summary>
        public void BalloonSetting()
        {
            // 設定ファイル読み込み
            var settings = new Settings();
            try
            {
                settings.ReadSettings();
                if (false == File.Exists(settings.BallooiconFile))
                {
                    var msg = string.Format(
                            SErrMsg.E_balloonNoicon,
                            settings.BallooiconFile
                        );
                    throw new Exception(msg);
                }
            }
            catch (Exception ex)
            {
                LogFile.Write(ex);
                // 異常終了
                Environment.Exit(1);
            }
            // 秀丸メールのプログラムパス
            this.HideMailPath = settings.HideMailPath;
            // 秀丸メール起動パラメータ
            this.HideMailParameter = settings.HideMailParameter;
            // バルーンヒント表示イベント処理実行
            this.BalloonTipShownAct_Prop=settings.BalloonTipShownAct;
            //タスクトレイにアイコンを表示するための設定
            //一度設定すれば、バルーンヒントを表示するたびに設定しなおす必要はない
            //バルーンヒントのタイトル
            this.balloonCtrl.TipTitle = settings.BalloonTitle;
            //バルーンヒントに表示するメッセージ
            this.balloonCtrl.NewMailCount = settings.NewMailCount;
            //バルーンメッセージ
            this.balloonCtrl.BalloonMessage = settings.BalloonMessage;
            //バルーンメッセージフォーマット
            this.balloonCtrl.MailCountFrm = settings.MailCountFrm;
            //バルーンヒントに表示するメッセージ
            if (this.balloonCtrl.NewMailCount == true)
            {
                // バルーンメッセージ＋新着メール件数を設定
                this.balloonCtrl.BalloonText =
                    this.balloonCtrl.BalloonMessage + "\n" +
                    string.Format(this.balloonCtrl.MailCountFrm, IntMailCount.ToString());
            }
            else
            {   // バルーンメッセージ
                this.balloonCtrl.BalloonText = this.balloonCtrl.BalloonMessage;
            }
            //バルーンチップテキスト
            this.balloonCtrl.Text = settings.BalloonIconTipText;
            //バルーンアイコンファイル設定
            this.balloonCtrl.IconFile(settings.BallooiconFile);
            //バルーンアイコンタイプ設定
            this.balloonCtrl.IconType(settings.IconKind);
            ///////////////////////////////////////////////
            // イベントハンドラを追加する
            ///////////////////////////////////////////////
            // バルーンヒントクリック
            this.balloonCtrl.BalloonTipClicked(this.EventHandler_BalloonTipClicked_Prop);
            // バルーンアイコンクリック
            this.balloonCtrl.BalloonIconClick(this.EventHandler_BalloonIconClick_Prop);
            // バルーンヒント表示
            this.balloonCtrl.BalloonIconShown(this.EventHandler_BalloonTipShown_Prop);
        }
        #endregion
    }
}
