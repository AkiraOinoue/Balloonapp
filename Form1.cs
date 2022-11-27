using Shared;
using SharedStream;
using System.Diagnostics;
using System.Windows.Forms;

namespace balloonapp
{
    public partial class Form1 : Form
    {
        // �t�H�[���R���g���[���N���X
        public FrmCtrl FrmCtrl;
        public Form1()
        {
            // �R���e�i�\����
            // �v���O�����I�������Y��Ƀo���[���`�b�v���������邽�߂ɕK�v
            components = new System.ComponentModel.Container();
            // �t�H�[���R���g���[���N���X�C���X�^���X����
            FrmCtrl = new FrmCtrl(this, components);
            InitializeComponent();
        }
        /// <summary>
        /// �t�H�[�����N������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // �o���[���q���g�N���b�N�C�x���g�v���p�e�B
            FrmCtrl.EventHandler_BalloonTipClicked_Prop = this.NotifyIcon1_BalloonTipClicked;
            // �o���[���A�C�R���N���b�N�C�x���g�v���p�e�B
            FrmCtrl.EventHandler_BalloonIconClick_Prop = this.NotifyIcon1_Click;
            // �o���[���q���g�\���C�x���g�v���p�e�B
            FrmCtrl.EventHandler_BalloonTipShown_Prop = this.NotifyIcon1_BalloonTipShown;
            // �o���[���`�b�v�\���ݒ�
            FrmCtrl.BalloonSetting();
            // �o���[���R���g���[��
            FrmCtrl.Balloon();
            // �t�H�[�����\���ɂ���
            this.Hide();
            try
            {
                // �񓯊��Ŏ�M����
                FrmCtrl.NamedPipeObj.Execthread = true;
                FrmCtrl.NamedPipeObj.ReceiveMessage();
                // �o���[���A�C�R�������ŃG���[����
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
        #region �C�x���g����
        /// <summary>
        /// �o���[���q���g�N���b�N�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon1_BalloonTipClicked(object? sender, EventArgs e)
        {
#if DEBUG
            MessageBox.Show("NotifyIcon1_BalloonTipClicked:�G�ۃ��[���N��");
#else
            // �G�ۃ��[���N��
            FrmCtrl.ProcessStart(
                FrmCtrl.HideMailPath,
                FrmCtrl.HideMailParameter
                );
#endif
            // �t�H�[����ʂ���܂��B
            this.Dispose(true);
        }
        /// <summary>
        /// �o���[���`�b�v���\�����ꂽ���̃C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon1_BalloonTipShown(object? sender, EventArgs e)
        {
            // �t���O�Ŕ���
            if (true == FrmCtrl.BalloonTipShownAct_Prop)
            {
#if DEBUG
                MessageBox.Show("NotifyIcon1_BalloonTipShown:�G�ۃ��[���N��");
#else
                // �G�ۃ��[���N��
                FrmCtrl.ProcessStart(
                    FrmCtrl.HideMailPath,
                    FrmCtrl.HideMailParameter
                    );
#endif
                // �t�H�[����ʂ���܂��B
                this.Dispose(true);
            }
        }
        /// <summary>
        /// �o���[���A�C�R���N���b�N�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// �o���[���`�b�v���^�C���A�E�g�ŕ�����ɒʒm�̈�ɕ\�������A�C�R��
        /// </remarks>
        private void NotifyIcon1_Click(object? sender, EventArgs e)
        {
#if DEBUG
            MessageBox.Show("NotifyIcon1_Click:�G�ۃ��[���N��");
#else
            // �G�ۃ��[���N��
            FrmCtrl.ProcessStart(
                FrmCtrl.HideMailPath,
                FrmCtrl.HideMailParameter
                );
#endif
            // �t�H�[����ʂ���܂��B
            this.Dispose(true);
        }
    }
    #endregion
}