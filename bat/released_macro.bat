@echo off
rem リリース用バッチスクリプト
rem プロジェクトパス
set ProjectDir=D:\vs_app\balloonapp
rem リソースパス
set RES=%ProjectDir%\resource
rem Release出力先
set REL=%ProjectDir%\bin\Release\net7.0-windows
rem PakageDirコピー先
set OUTDIR=%ProjectDir%\pkg
dir /b %REL%
rem Resource
rem PakageDirコピー先存在チェック
if not exist %OUTDIR% (
   mkdir %OUTDIR%
)
rem コピー処理
copy /y %REL% %OUTDIR%
copy /y %RES% %OUTDIR%
rem マクロフォルダへコピー
set MACRODIR=C:\Users\davinci1958\AppData\Roaming\Hidemaruo\Hidemaru\Macro
copy /y %OUTDIR% %MACRODIR%
dir /O-D %MACRODIR%
pause
exit 0
