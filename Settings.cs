using Microsoft.VisualBasic;
using Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using SharedStream;

namespace balloonapp
{
    /// <summary>
    /// バルーンアイコン処理の設定情報を取得する
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Settings()
        {
            // 実行APPのパス情報取得
            var AppPath = Application.StartupPath;
            // 設定ファイルパス初期化
            SettingsPath = Path.Combine(
                AppPath,
                SConstants.SettingsFileName
                );
            // 秀丸メールパス初期化
            HideMailPath = "";
            // 秀丸メール起動パラメータ
            HideMailParameter = "";
            // バルーンアイコン(ICO)ファイルパス初期化
            BallooiconFile = Path.Combine(
                AppPath,
                SConstants.BalloonFileName
                );
            // バルーン表示で秀丸メール起動選択初期化
            BalloonTipShownAct = SConstants.BalloonTipShownAct;
            // バルーンメッセージ表示タイムアウト 0秒
            BalloonTime = SConstants.BalloonTime;
            // バルーンメッセージ種類
            IconKind = SConstants.IconKind;
            // バルーンメッセージ
            BalloonMessage = SConstants.BalloonMessage;
            // バルーンメッセージタイトル
            BalloonTitle = SConstants.BalloonTitle;
            // バルーンアイコンのチップテキスト
            BalloonIconTipText = SConstants.BalloonIconTipText;
            // 新着メール数表示有無
            NewMailCount = SConstants.NewMailCount;
            // 新着メール数表示フォーマット
            MailCountFrm = SConstants.MailCountFrm;
            // デバッグモードの有無
            DebugMode = SConstants.BalloonDebugMode;
        }

        /// <summary>
        /// バルーンメッセージタイムアウト処理コード
        /// </summary>
        public enum BalloonIcnAct
        {
            Non = 0,　//   Non: なにもせずにバルーンアイコンを閉じる(default)
            New       //   New: 新着メールを表示
        }
        /// <summary>
        /// バルーンメッセージタイムアウト処理コードマップ
        /// </summary>
        private readonly Dictionary<string, BalloonIcnAct> BaloonActkeys = new()
        {
            { "non", BalloonIcnAct.Non },
            { "new", BalloonIcnAct.New }
        };
        /// <summary>
        /// 設定ファイルの要素コード
        /// </summary>
        public enum ElementNum
        {
            HideMailPath = 1000,    // 秀丸メールパス
            HideMailParameter,      // 秀丸メール起動パラメータ
            BalloonTipShownAct,     // バルーン表示で秀丸メール起動選択
            BalloonTime,            // バルーン表示タイムアウト
            BalloonIconFile,        // バルーンアイコン(ICO)ファイル名
            IconKind,               // バルーンアイコン種類
            BalloonIconTipText,     // バルーンアイコンのチップテキスト
            BalloonMessage,         // バルーンメッセージ
            BalloonTitle,           // バルーンタイトル
            NewMailCount,           // 新着メール数表示有無
            MailCountFrm,           // 新着メール数表示フォーマット
            DebugMode,              // デバッグモードの有無
        }
        /// <summary>
        /// 設定ファイルの要素名・コードテーブル
        /// </summary>
        private readonly Dictionary<string, ElementNum> Elemkeys = new()
        {
            { "HideMailPath",           ElementNum.HideMailPath},               // 秀丸メールパス
            { "HideMailParameter",      ElementNum.HideMailParameter },         // 秀丸メール起動パラメータ
            { "BalloonTipShownAct",     ElementNum.BalloonTipShownAct },        // バルーン表示で秀丸メール起動選択
            { "BalloonTime",            ElementNum.BalloonTime },               // バルーン表示タイムアウト
            { "BalloonIconFile",        ElementNum.BalloonIconFile },           // バルーンアイコン(ICO)ファイル名
            { "IconKind",               ElementNum.IconKind },                  // バルーンアイコン種類  
            { "BalloonIconTipText",     ElementNum.BalloonIconTipText },        // バルーンアイコンのチップテキスト
            { "BalloonMessage",         ElementNum.BalloonMessage },            // バルーンメッセージ
            { "BalloonTitle",           ElementNum.BalloonTitle },              // バルーンタイトル
            { "NewMailCount",           ElementNum.NewMailCount },              // 新着メール数表示有無
            { "MailCountFrm",           ElementNum.MailCountFrm },              // 新着メール数表示フォーマット
            { "DebugMode",              ElementNum.DebugMode },                 // デバッグモードの有無
        };
        /// <summary>
        /// デバッグモードの有無
        /// </summary>
        public bool DebugMode { get; set; }
        /// <summary>
        /// バルーンアイコン種類
        /// </summary>
        public string IconKind { get; set; }
        /// <summary>
        /// 設定ファイルパス
        /// </summary>
        public string SettingsPath { get; set; }
        /// <summary>
        /// 秀丸メールパス
        /// </summary>
        public string HideMailPath { get; set; }
        /// <summary>
        /// 秀丸メール起動パラメータ
        /// </summary>
        public string HideMailParameter { get; set; }
        /// <summary>
        /// バルーン表示で秀丸メール起動選択
        /// </summary>
        public bool BalloonTipShownAct { get; set; }
        /// <summary>
        /// バルーン表示タイムアウト
        /// </summary>
        public int BalloonTime { get; set; }
        /// <summary>
        /// バルーンアイコン(ICO)ファイルパス
        /// </summary>
        public string BallooiconFile { get; set; }
        /// <summary>
        /// バルーンアイコンのチップテキスト
        /// </summary>
        public string BalloonIconTipText { get; set; }
        /// <summary>
        /// バルーンメッセージ
        /// </summary>
        public string BalloonMessage { get; set; }
        /// <summary>
        /// バルーンタイトル
        /// </summary>
        public string BalloonTitle { get; set; }
        /// <summary>
        /// 新着メール数表示有無
        /// </summary>
        /// <remarks>true:件数表示、false:件数非表示</remarks>
        public bool NewMailCount { get; set; }
        /// <summary>
        /// 新着メール数表示フォーマット
        /// </summary>
        /// <remarks>"新着メールは{0}件です。"</remarks>
        public string MailCountFrm { get; set; }
        /// <summary>
        /// 設定ファイル情報読み込み
        /// </summary>
        public void ReadSettings()
        {
            // Settingsファイルの存在チェック
            if (false == File.Exists(this.SettingsPath))
            {
                LogFile.Write($"Settingsファイルがありません:{this.SettingsPath}");
                return;
            }
            try
            {
                // 設定ファイル読み込み
                var xmldata = XElement.Load(this.SettingsPath);
                // 各要素を取得
                IEnumerable<XElement> SettingsInfos = from item in xmldata.Elements() select item;
                // 要素毎に値を設定
                foreach (var info in SettingsInfos)
                {
                    // 要素名毎に値を設定
                    var item_name = info.Name.ToString();
                    // 値が未設定ならば次へスキップ
                    if (info.Value.Length == 0)
                    {
                        continue;
                    }
                    switch ((int)Elemkeys[item_name])
                    {                        
                        case (int)ElementNum.HideMailPath:
                            if (false == File.Exists(info.Value))
                            {
                                throw new System.Xml.XmlException(
                                    string.Format(
                                        SErrMsg.E_hidemail,
                                        info.Value
                                        )
                                    );
                            }
                            // 秀丸メールパス
                            HideMailPath = info.Value;
                            break;
                        case (int)ElementNum.HideMailParameter:
                            // 秀丸メール起動パラメータ
                            HideMailParameter = info.Value;
                            break;
                        case (int)ElementNum.BalloonTime:
                            //バルーン表示タイムアウト
                            BalloonTime = Int32.Parse(info.Value);
                            break;
                        case (int)ElementNum.BalloonTipShownAct:
                            // バルーン表示で秀丸メール起動選択
                            BalloonTipShownAct = ("true" == info.Value);
                            break;
                        case (int)ElementNum.BalloonIconFile:
                            // バルーンアイコン(ICO)ファイルパス
                            // フルパスに変換
                            var icon_fullpath = Path.GetFullPath(info.Value);
                            if (false == File.Exists(icon_fullpath))
                            {
                                throw new System.Xml.XmlException(
                                    string.Format(
                                        SErrMsg.E_balloonico,
                                        icon_fullpath
                                        )
                                    );
                            }
                            BallooiconFile = icon_fullpath;
                            break;
                        case (int)ElementNum.IconKind:
                            // バルーンアイコン種類
                            IconKind = info.Value;
                            break;
                        case (int)ElementNum.BalloonIconTipText:
                            // バルーンアイコンのチップテキスト
                            BalloonIconTipText = info.Value;
                            break;
                        case (int)ElementNum.BalloonMessage:
                            // バルーンメッセージ
                            BalloonMessage = info.Value;
                            break;
                        case (int)ElementNum.BalloonTitle:
                            // バルーンタイトル
                            BalloonTitle = info.Value;
                            break;
                        case (int)ElementNum.NewMailCount:
                            // 新着メール数表示有無
                            NewMailCount = ("true" == info.Value);
                            break;
                        case (int)ElementNum.MailCountFrm:
                            // 新着メール数表示フォーマット
                            MailCountFrm = info.Value;
                            break;
                        case (int)ElementNum.DebugMode:
                            // デバッグモードの有無
                            DebugMode = ("true" == info.Value);
                            break;
                        default:
                            // なにもしない
                            break;
                    }
                }
                // 秀丸メールのパス設定チェック
                if (HideMailPath.Length == 0)
                {
                    throw new System.Xml.XmlException(
                        SErrMsg.E_hidepath
                        );
                }
            }
            catch (System.Xml.XmlException ex)
            {
                LogFile.Write(ex);
                // 異常終了
                Environment.Exit(1);
            }
        }
    }
}
