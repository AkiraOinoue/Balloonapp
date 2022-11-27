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
            // �ݒ���擾
            var settings = new Settings();
            settings.ReadSettings();
            // ���d�N���h�~
            string mutexName = "BalloonApp";
            Mutex mutex = new(true, mutexName, out bool createdNew);
            // �V�K�ɋN��
            if (createdNew)
            {
                LogFile.Write($"{mutexName}�P�̋N��", settings.DebugMode);
                try
                {
                    // To customize application configuration such as set high DPI settings or default font,
                    // see https://aka.ms/applicationconfiguration.
                    ApplicationConfiguration.Initialize();
                    var obj = new Form1();
                    // �R�}���h���C���擾
                    obj.FrmCtrl.Option(args);
                    var StrMailCount = obj.FrmCtrl.StrMailCount;
                    // �����Ď��̎d�g��
                    // ��莞�Ԍ�ɂ���X�e�[�^�X�̏󋵂�false�Ȃ�΁A���鏈��������B
                    LogFile.Write($"�V�����[����:{StrMailCount}", settings.DebugMode);
                    Application.Run(obj);
                }
                catch (Exception ex)
                {
                    LogFile.Write(ex);
                }
                // ���d�N���h�~�����
                mutex.ReleaseMutex();
            }
            // ���ɋN����
            else
            {
                // �V�����[�������v���Z�X�ԃ��b�Z�[�W�Ƃ��đ��M����
                var pipeobj = new NamedPipe(
                    NamedPipe.PipeName ,
                    settings.DebugMode);
                try
                {
                    LogFile.Write($"{mutexName}���d�N��", settings.DebugMode);
                    ApplicationConfiguration.Initialize();
                    var obj = new Form1();
                    // �R�}���h���C���擾
                    obj.FrmCtrl.Option(args);
                    // �V�����[�������擾
                    var StrMailCount = obj.FrmCtrl.StrMailCount;
                    var msg = $"�v���Z�X�ԃ��b�Z�[�W�Ƃ��ĐV�����[�����𑗐M����:{StrMailCount}";
                    LogFile.Write(msg, settings.DebugMode);

                    // �o���[���`�b�v�\�����\�b�h�N�����b�Z�[�W���M
                    pipeobj.SendMessage($"{pipeobj.ExecThreadMessage} {StrMailCount}");
                    msg = $"�o���[���`�b�v�\�����\�b�h�N�����b�Z�[�W�𑗐M�����B{pipeobj.Message}";
                    LogFile.Write(msg, settings.DebugMode);
#if DEBUG
                    MessageBox.Show(pipeobj.Message);
                    msg = string.Format($"{StrMailCount}:{pipeobj.ExecThreadMessage}:{pipeobj.Message}");
                    MessageBox.Show(msg, "�o���[���`�b�v�͋N����");
#endif              
                    // �p�C�v�N���[�Y���b�Z�[�W���M
                    pipeobj.SendMessage(pipeobj.CloseThreadMessage);
                    msg = $"{pipeobj.Message} -> {mutexName}�p�C�v�N���[�Y";
                    LogFile.Write(msg, settings.DebugMode);
                }
                catch (Exception ex)
                {
                    LogFile.Write(ex);
                }
            }
            // ���d�N���h�~�����N���[�Y
            mutex.Close();
        }
    }
}