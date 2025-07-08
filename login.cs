using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0H04037_正田陸_uiux01
{
    public partial class login : Form
    {
        public static int loginnum;//ログイン成功した行番号を保存する変数

        public login()
        {
            InitializeComponent();
            this.Size = new Size(1920, 1080);
        }

        private void login_Load(object sender, EventArgs e)
        {
            if (Title.language == 0)
            {
                button1.Text = "ログイン";
                button2.Text = "タイトルに戻る";
                label1.Text = "ユーザ名";
                label2.Text = "パスワード";
                label3.Text = "ログイン画面";
            }//日本語設定
            else
            {
                button1.Text = "Login";
                button2.Text = "Back to Title";
                label1.Text = "User Name";
                label2.Text = "Password";
                label3.Text = "Login Screen";
            }//英語設定

            if (Title.screen_mode == 0)
            {
                this.BackColor = ColorTranslator.FromHtml("#F5F5F5");
                this.ForeColor = ColorTranslator.FromHtml("#212121");
                button1.BackColor = ColorTranslator.FromHtml("#0D47A1");
                button2.BackColor = ColorTranslator.FromHtml("#0D47A1");
                button1.ForeColor = Color.White;
                button2.ForeColor = Color.White;
                label1.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                label2.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                label3.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                label4.ForeColor = ColorTranslator.FromHtml("#0D47A1");
            }//ライトモード
            else
            {
                this.BackColor = ColorTranslator.FromHtml("#121233");
                this.ForeColor = ColorTranslator.FromHtml("#E0E0E0");
                button1.BackColor = ColorTranslator.FromHtml("#3F51B5");
                button2.BackColor = ColorTranslator.FromHtml("#3F51B5");
                button1.ForeColor = Color.White;
                button2.ForeColor = Color.White;
                label1.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                label2.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                label3.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                label4.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
            }//ダークモード
            textBox1.Focus();
        }
        //login.csがロードされたとき


        private void button1_Click(object sender, EventArgs e)
        {
            string login_name = textBox1.Text;    //入力されたユーザ名
            string login_password = textBox2.Text;//入力されたパスワード
            string user_data;
            int linecount = 0;
            
            loginnum = 0;
            login_password = account.hashpassword(login_password);// パスワードをハッシュ化

            //ユーザ名かパスワードが空欄の場合
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                if (Title.language == 0)
                {
                    label4.Text = "ユーザ名とパスワードを入力してください";
                }
                else
                {
                    label4.Text = "Please enter a user name and password";
                }
                return;
            }
            else if (!account.dontusechar(login_name) || !account.dontusechar(login_password) || account.dontJapanese(login_name) || account.dontJapanese(login_password))
            {
                if (Title.language == 0)
                {
                    label4.Text = "ユーザ名・パスワードに使用できない\n文字が含まれています";
                }
                else
                {
                    label4.Text = "User name or password\ncontains invalid characters";
                }
                return;
            }
            else if(account.zenkaku(login_name) || account.zenkaku(login_password))
            {
                if (Title.language == 0)
                {
                    label4.Text = "半角文字で入力してください";
                }
                else
                {
                    label4.Text = "Please use half-width characters";
                }
                return;
            }

            //user_data.txtを読み取りで開く
            using (StreamReader rw = new StreamReader("user_data.txt", Encoding.GetEncoding("UTF-8")))
                {
                    //ファイルの行数をカウント
                    while ((user_data = rw.ReadLine()) != null)
                    {
                        linecount++;
                    }
                }

            if (linecount > 0) //ファイルにデータがある場合
            {
                string input_name, input_password;
                int sign;

                //user_data.txtを再度読み取りで開く
                using (StreamReader rw = new StreamReader("user_data.txt", Encoding.GetEncoding("UTF-8")))
                {
                    //ファイルの終端まで読み込む
                    while ((user_data = rw.ReadLine()) != null)
                    {
                        // ユーザ名とパスワードを分割
                        sign = user_data.IndexOf(':');
                        input_name = user_data.Substring(0, sign);     // ユーザ名
                        input_password = user_data.Substring(sign + 1);// パスワード

                        if (login_name == input_name)//入力ユーザ名とファイルのユーザ名が一致した場合
                        {
                            if (login_password == input_password)//入力パスワードとファイルのパスワードが一致した場合
                            {
                                //アプリページへ遷移
                                if (Title.language == 0)
                                {
                                    MessageBox.Show("ログインに成功しました");
                                }
                                else
                                {
                                    MessageBox.Show("Login successful");
                                }
                                season sc3 = new season();
                                sc3.ShowDialog();
                                this.Close();
                                return;
                            }
                        }
                        loginnum++;
                    }
                    // ユーザ名またはパスワードが一致しなかった場合
                    if (Title.language == 0)
                    {
                        label4.Text = "ユーザ名またはパスワードが間違っています";
                    }
                    else
                    {
                        label4.Text = "User name or password is incorrect";
                    }
                }
            }
            else// ファイルにデータがない場合、アカウント作成ページへ遷移
            {
                if (Title.language == 0)
                {
                    MessageBox.Show("アカウントがまだありません\r\nアカウント作成をしてください");
                }
                else
                {
                    MessageBox.Show("There is no account yet\r\nPlease create an account");
                }
                account sc1 = new account();
                sc1.ShowDialog();
                this.Close();
            }
        }
        //ログインボタン


        private void button2_Click(object sender, EventArgs e)
        {
            //タイトルページへ遷移
            Title sc1 = new Title();
            sc1.ShowDialog();
            this.Close();
        }
        //タイトルに戻るボタン
    }
}
