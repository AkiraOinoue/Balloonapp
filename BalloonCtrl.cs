using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedStream;

namespace balloonapp
{
    /// <summary>
    /// バルーンメッセージの表示を制御するコントロール
    /// </summary>
    public class BalloonCtrl
    {
        #region メンバー
        internal bool m_except_error;
        /// <summary>
        /// 通知用バルーンアイコン
        /// </summary>
        public NotifyIcon notifyIcon;
        /// <summary>
        /// バルーンメッセージ
        /// </summary>
        internal string popupmsg="";
        /// <summary>
        /// バルーンアイコンのタイプ
        /// </summary>
        internal ToolTipIcon mIconType;
        internal string newmailcounter;
        /// <summary>
        /// バルーンアイコンのマップ
        /// </summary>
        private readonly Dictionary<string, ToolTipIcon> icon_map = new()
        {
            {"info", ToolTipIcon.Info},
            {"warn", ToolTipIcon.Warning},
            {"error", ToolTipIcon.Error},
            {"none", ToolTipIcon.None}
        };
        #endregion
        public BalloonCtrl(NotifyIcon para_notifyIcon)
        {
            // 新着メール件数
            this.newmailcounter = "";
            this.BalloonMessage = "";
            this.MailCountFrm = "";
            // バルーンアイコンのインスタンスを生成
            this.notifyIcon = para_notifyIcon;
            //アイコンを右クリックしたときに表示するコンテキストメニュー
            this.notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            // メンバーの初期化
            this.Initialmember();
        }
        #region メソッド
        /// <summary>
        /// バルーンヒントクリックイベント設定
        /// </summary>
        /// <param name="eventHandler"></param>
        public void BalloonTipClicked(EventHandler eventHandler)
        {
            this.notifyIcon.BalloonTipClicked += new EventHandler(eventHandler);
        }
        /// <summary>
        /// バルーンヒントクローズイベント設定
        /// </summary>
        /// <param name="eventHandler"></param>
        public void BalloonTipClosed(EventHandler eventHandler)
        {
            this.notifyIcon.BalloonTipClosed += new EventHandler(eventHandler);
        }
        /// <summary>
        /// バルーンアイコンクリック
        /// </summary>
        /// <param name="eventHandler"></param>
        public void BalloonIconClick(EventHandler eventHandler)
        {
            this.notifyIcon.Click += new EventHandler(eventHandler);
        }
        /// <summary>
        /// バルーンヒント表示イベント設定
        /// </summary>
        /// <param name="eventHandler"></param>
        public void BalloonIconShown(EventHandler eventHandler)
        {
            this.notifyIcon.BalloonTipShown += new EventHandler(eventHandler);
        }
        /// <summary>
        /// メンバーの初期化
        /// </summary>
        internal void Initialmember()
        {            
            //NotifyIconを表示する
            this.notifyIcon.Visible = true;
            // バルーンアイコンの種類
            this.mIconType = this.icon_map[SConstants.IconKind];
            // 例外エラーフラグ
            this.Except_erro = false;
        }
        /// <summary>
        /// バルーン表示（初期値：10秒間表示）
        /// </summary>
        /// <param name="timeout">
        /// 表示タイムアウト（ミリ秒：10000 <= 30000）
        /// 初期値：10秒
        /// </param>
        public void Balloon(int timeout, bool icon_visible = true)
        {
            try
            {
                // バルーンメッセージ
                //NotifyIconを表示制御
                this.notifyIcon.Visible = icon_visible;
                this.notifyIcon.BalloonTipText = this.BalloonText;
                this.notifyIcon.BalloonTipIcon = this.mIconType;
                this.notifyIcon.ShowBalloonTip(timeout);
            }
            catch (Exception ex)
            {
                this.Except_erro = true;
                LogFile.Write(ex);
            }
        }
        /// <summary>
        /// バルーンアイコンファイル設定
        /// </summary>
        /// <param name="iconfile">バルーンアイコンファイル（.ico）</param>
        public void IconFile(string iconfile)
        {
            this.notifyIcon.Icon = new Icon(iconfile);
        }
        /// <summary>
        /// バルーンアイコンタイプ設定
        /// </summary>
        /// <param name="icon_type">"info", "warn", "error", "none"</param>
        public void IconType(string icon_type)
        {
            this.mIconType = this.icon_map[icon_type];
        }
        #endregion
        #region プロパティ
        //バルーンメッセージフォーマット
        public string MailCountFrm { get; set; }
        //バルーンメッセージ
        public string BalloonMessage { get; set; }
        /// <summary>
        /// 新着メール数表示ON/OFF
        /// </summary>
        public bool NewMailCount { get; set; }
        /// <summary>
        /// バルーンメッセージ設定プロパティ
        /// </summary>
        public string BalloonText
        {
            set { this.popupmsg = value; }
            private get { return this.popupmsg; }
        }
        /// <summary>
        /// バルーンアイコンのチップテキストプロパティ
        /// </summary>
        public string Text
        {
            set { this.notifyIcon.Text = value; }
            private get { return this.notifyIcon.Text; }
        }
        /// <summary>
        /// バルーンタイトル設定プロパティ
        /// </summary>
        public string TipTitle
        {
            set{ this.notifyIcon.BalloonTipTitle = value; }
            private get{ return this.notifyIcon.BalloonTipTitle; }
        }
        /// <summary>
        /// 例外エラー発生フラグプロパティ
        /// </summary>
        public bool Except_erro
        {
            get { return m_except_error; }
            set { m_except_error = value; }
        }
        #endregion
    }
}
