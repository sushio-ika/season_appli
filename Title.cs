using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _0H04037_正田陸_uiux01
{
    public partial class Title : Form
    {
        public static int language = 0; // 言語設定を保存するグローバル変数
        public static int screen_mode = 0; // 画面モード設定を保存するグローバル変数

        private bool suppressRadioEvent = false;

        public Title()
        {
            InitializeComponent();
            this.Size = new Size(1920, 1080);
        }

        private void Title_Load(object sender, EventArgs e)
        {
            suppressRadioEvent = true;
            if (language == 0)
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;

                button1.Text = "ユーザ新規作成";
                button2.Text = "終了";
                button3.Text = "ログイン";
                radioButton3.Text = "ダークモード";
                radioButton4.Text = "ライトモード";
            }//日本語設定
            else
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;

                button1.Text = "Create Account";
                button2.Text = "Exit";
                button3.Text = "Login";
                radioButton3.Text = "Dark Mode";
                radioButton4.Text = "Light Mode";
            }//英語設定

            if (screen_mode == 0)
            {
                radioButton4.Checked = true;
                radioButton3.Checked = false;

                this.BackColor = ColorTranslator.FromHtml("#F5F5F5");
                this.ForeColor = ColorTranslator.FromHtml("#212121");
                button1.BackColor = ColorTranslator.FromHtml("#0D47A1");
                button2.BackColor = ColorTranslator.FromHtml("#0D47A1");
                button3.BackColor = ColorTranslator.FromHtml("#0D47A1");
                radioButton1.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                radioButton2.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                radioButton3.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                radioButton4.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                button1.ForeColor = Color.White;
                button2.ForeColor = Color.White;
                button3.ForeColor = Color.White;
                radioButton1.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                radioButton2.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                radioButton3.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                radioButton4.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                label1.ForeColor = ColorTranslator.FromHtml("#0D47A1");
            }// ライトモード
            else
            {
                radioButton3.Checked = true;
                radioButton4.Checked = false;

                this.BackColor = ColorTranslator.FromHtml("#121233");
                this.ForeColor = ColorTranslator.FromHtml("#E0E0E0");
                button1.BackColor = ColorTranslator.FromHtml("#3F51B5");
                button2.BackColor = ColorTranslator.FromHtml("#3F51B5");
                button3.BackColor = ColorTranslator.FromHtml("#3F51B5");
                radioButton1.BackColor = ColorTranslator.FromHtml("#424242");
                radioButton2.BackColor = ColorTranslator.FromHtml("#424242");
                radioButton3.BackColor = ColorTranslator.FromHtml("#424242");
                radioButton4.BackColor = ColorTranslator.FromHtml("#424242");
                button1.ForeColor = Color.White;
                button2.ForeColor = Color.White;
                button3.ForeColor = Color.White;
                radioButton1.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                radioButton2.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                radioButton3.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                radioButton4.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                label1.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
            }// ダークモード
            suppressRadioEvent = false;
        }
        //Title.csがロードされたとき

        private void button1_Click(object sender, EventArgs e)
        {
            account sc2 = new account();
            sc2.ShowDialog();
            this.Close();
        }
        //アカウント作成画面遷移ボタン

        private void button2_Click(object sender, EventArgs e)
        {
            // 終了確認ダイアログを表示  
            DialogResult result;
            if (Title.language == 0)
            {
                result = MessageBox.Show("アプリを終了してもよろしいですか？", "", MessageBoxButtons.YesNo);
            }
            else
            {
                result = MessageBox.Show("Are you sure you want to exit the application?", "", MessageBoxButtons.YesNo);
            }

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
        //終了ボタン

        private void button3_Click_1(object sender, EventArgs e)
        {
            login sc1 = new login();
            sc1.ShowDialog();
            this.Close();
        }
        //ログイン画面遷移ボタン

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (suppressRadioEvent) return;

            if (radioButton2.Checked)
            {
                DialogResult result;
                result = MessageBox.Show("日本語設定に変更しますか？\r\nDo you want to switch to Japanese?", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    language = 0; // 日本語に変更
                }
                else
                {
                    suppressRadioEvent = true;
                    radioButton1.Checked = true; // 英語に戻す
                    suppressRadioEvent = false;
                    return;
                }
                Title_Load(sender, e); // 言語変更後に再読み込み
            }
        }
        //日本語ラジオボタン

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (suppressRadioEvent) return;

            if (radioButton1.Checked)
            {
                DialogResult result;
                result = MessageBox.Show("英語設定に変更しますか？\r\nDo you want to switch to English?", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    language = 1; // 英語に変更
                }
                else
                {
                    suppressRadioEvent = true;
                    radioButton2.Checked = true; // 日本語に戻す
                    suppressRadioEvent = false;
                    return;
                }
                Title_Load(sender, e); // 言語変更後に再読み込み
            }
        }
        //英語ラジオボタン

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (suppressRadioEvent) return;

            if (radioButton4.Checked)
            {
                DialogResult result;
                if(language == 0)
                {
                    result = MessageBox.Show("ライトモードに変更しますか？", "", MessageBoxButtons.YesNo);
                }
                else
                {
                    result = MessageBox.Show("Do you want to switch to Light Mode?", "", MessageBoxButtons.YesNo);
                }

                if (result == DialogResult.Yes)
                {
                    screen_mode = 0; // 日本語に変更
                }
                else
                {
                    suppressRadioEvent = true;
                    radioButton3.Checked = true; // ダークモードに戻す
                    suppressRadioEvent = false;
                    return;
                }
                Title_Load(sender, e); // 画面モード変更後に再読み込み
            }
        }
        //ライトモードラジオボタン

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (suppressRadioEvent) return;

            if (radioButton3.Checked)
            {
                DialogResult result;
                if (language == 0)
                {
                    result = MessageBox.Show("ダークモードに変更しますか？", "", MessageBoxButtons.YesNo);
                }
                else
                {
                    result = MessageBox.Show("Do you want to switch to Dark Mode?", "", MessageBoxButtons.YesNo);
                }

                if (result == DialogResult.Yes)
                {
                    screen_mode = 1; // 日本語に変更
                }
                else
                {
                    suppressRadioEvent = true;
                    radioButton4.Checked = true; // ライトモードに戻す
                    suppressRadioEvent = false;
                    return;
                }
                Title_Load(sender, e); // 画面モード変更後に再読み込み
            }
        }
        //ダークモードラジオボタン
    }
}
