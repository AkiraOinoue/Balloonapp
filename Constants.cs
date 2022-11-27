using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    /// <summary>
    /// 設定ファイル関係～既定値
    /// </summary>
    public static class SConstants
    {
        /// <summary>
        /// バルーンタイムアウト
        /// OS側の設定に依存する。
        /// </summary>
        public static readonly int BalloonTime = 30000;
        /// <summary>
        /// バルーンメッセージ種類
        /// </summary>
        public static readonly string IconKind = "info";
        /// <summary>
        /// バルーンメッセージ
        /// </summary>
        public static readonly string BalloonMessage = "あなた宛てにメールが届いています。";
        /// <summary>
        /// バルーンメッセージタイトル
        /// </summary>
        public static readonly string BalloonTitle = "新着メールのお知らせ";
        /// <summary>
        /// バルーンアイコンのチップテキスト
        /// </summary>
        public static readonly string BalloonIconTipText = "新着メールアイコン";
        /// <summary>
        /// 新着メール数表示有無
        /// </summary>
        public static readonly bool NewMailCount = true;
        /// <summary>
        /// 新着メール数表示フォーマット
        /// </summary>
        public static readonly string MailCountFrm = "新着メールは{0}件です。";
        /// <summary>
        /// 設定ファイルパス
        /// </summary>
        public static readonly string SettingsFileName = @"Settings.xml";
        /// <summary>
        /// バルーンアイコン(ICO)ファイル名
        /// </summary>
        public static readonly string BalloonFileName = @"bell.ico";
        /// <summary>
        /// バルーン表示で秀丸メール起動
        /// </summary>
        public static readonly bool BalloonTipShownAct = false;
        /// <summary>
        /// デバッグモード
        /// </summary>
        public static readonly bool BalloonDebugMode = false;
    }
    /// <summary>
    /// エラーメッセージフォーマット
    /// </summary>
    public static class SErrMsg
    {
        /// <summary>
        /// "{0}が存在しません。"
        /// </summary>
        public static readonly string E_settings = "{0}が存在しません。";
        /// <summary>
        /// "秀丸メールが存在しません: {0}"
        /// </summary>
        public static readonly string E_hidemail = "秀丸メールが存在しません: {0}";
        /// <summary>
        /// "バルーンアイコン(ICO)ファイルが存在しません: {0}"
        /// </summary>
        public static readonly string E_balloonico = "バルーンアイコン(ICO)ファイルが存在しません: {0}";
        /// <summary>
        /// "秀丸メールのパスが未設定です。<HideMailPath></HideMailPath>"
        /// </summary>
        public static readonly string E_hidepath = "秀丸メールのパスが未設定です。<HideMailPath></HideMailPath>";
        /// <summary>
        /// "設定ファイル読み込みエラー"
        /// </summary>
        public static readonly string E_dialog = "設定ファイル読み込みエラー";
        /// <summary>
        /// バルーンアイコン(ICO)ファイルが存在しません: {0}
        /// </summary>
        public static readonly string E_balloonNoicon =
            "バルーンアイコン(ICO)ファイルが存在しません: {0}\n" +
            "指定されたパスにファイルが存在する事を確認して下さい。";
    }
}
