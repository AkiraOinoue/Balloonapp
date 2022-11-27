using Shared;
using SharedStream;
using System.Diagnostics;
using System.Windows.Forms;

namespace balloonapp
{
    public partial class Form1 : Form
    {
        // フォームコントロールクラス
        public FrmCtrl FrmCtrl;
        public Form1()
        {
            // コンテナ―生成
            // プログラム終了時に綺麗にバルーンチップを消去するために必要
            components = new System.ComponentModel.Container();
            // フォームコントロールクラスインスタンス生成
            FrmCtrl = new FrmCtrl(this, components);
            InitializeComponent();
        }
        /// <summary>
        /// フォームが起動する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // バルーンヒントクリックイベントプロパティ
            FrmCtrl.EventHandler_BalloonTipClicked_Prop = this.NotifyIcon1_BalloonTipClicked;
            // バルーンアイコンクリックイベントプロパティ
            FrmCtrl.EventHandler_BalloonIconClick_Prop = this.NotifyIcon1_Click;
            // バルーンヒント表示イベントプロパティ
            FrmCtrl.EventHandler_BalloonTipShown_Prop = this.NotifyIcon1_BalloonTipShown;
            // バルーンチップ表示設定
            FrmCtrl.BalloonSetting();
            // バルーンコントロール
            FrmCtrl.Balloon();
            // フォームを非表示にする
            this.Hide();
            try
            {
                // 非同期で受信応答
                FrmCtrl.NamedPipeObj.Execthread = true;
                FrmCtrl.NamedPipeObj.ReceiveMessage();
                // バルーンアイコン処理でエラー発生
                if (FrmCtrl.balloonCtrl.Except_erro == true)
                {
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogFile.Write(ex);
            }
        }
        #region イベント処理
        /// <summary>
        /// バルーンヒントクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon1_BalloonTipClicked(object? sender, EventArgs e)
        {
#if DEBUG
            MessageBox.Show("NotifyIcon1_BalloonTipClicked:秀丸メール起動");
#else
            // 秀丸メール起動
            FrmCtrl.ProcessStart(
                FrmCtrl.HideMailPath,
                FrmCtrl.HideMailParameter
                );
#endif
            // フォーム画面を閉じます。
            this.Dispose(true);
        }
        /// <summary>
        /// バルーンチップが表示された時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon1_BalloonTipShown(object? sender, EventArgs e)
        {
            // フラグで判定
            if (true == FrmCtrl.BalloonTipShownAct_Prop)
            {
#if DEBUG
                MessageBox.Show("NotifyIcon1_BalloonTipShown:秀丸メール起動");
#else
                // 秀丸メール起動
                FrmCtrl.ProcessStart(
                    FrmCtrl.HideMailPath,
                    FrmCtrl.HideMailParameter
                    );
#endif
                // フォーム画面を閉じます。
                this.Dispose(true);
            }
        }
        /// <summary>
        /// バルーンアイコンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// バルーンチップがタイムアウトで閉じた後に通知領域に表示されるアイコン
        /// </remarks>
        private void NotifyIcon1_Click(object? sender, EventArgs e)
        {
#if DEBUG
            MessageBox.Show("NotifyIcon1_Click:秀丸メール起動");
#else
            // 秀丸メール起動
            FrmCtrl.ProcessStart(
                FrmCtrl.HideMailPath,
                FrmCtrl.HideMailParameter
                );
#endif
            // フォーム画面を閉じます。
            this.Dispose(true);
        }
    }
    #endregion
}