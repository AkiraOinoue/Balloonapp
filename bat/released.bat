@echo off
rem �����[�X�p�o�b�`�X�N���v�g
rem �v���W�F�N�g�p�X
set ProjectDir=D:\vs_app\balloonapp
rem ���\�[�X�p�X
set RES=%ProjectDir%\resource
rem Release�o�͐�
set REL=%ProjectDir%\bin\Release\net7.0-windows
rem PakageDir�R�s�[��
set OUTDIR=%ProjectDir%\pkg
dir /b %REL%
rem Resource
rem PakageDir�R�s�[�摶�݃`�F�b�N
if not exist %OUTDIR% (
   mkdir %OUTDIR%
)
rem �R�s�[����
copy /y %REL% %OUTDIR%
copy /y %RES% %OUTDIR%
echo %OUTDIR%
dir /O-D %OUTDIR%
pause
exit 0
