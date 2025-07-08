using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0H04037_正田陸_uiux01
{
    public partial class account : Form
    {
        public static string hashpassword(string text)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
        //ハッシュ関数


        public account()
        {
            InitializeComponent();
            this.Size = new Size(1920, 1080);
            this.Shown += account_Shown; // ここでイベント登録
        }

        public static bool dontusechar(string input)
        {
            // 禁止文字リスト
            char[] invalidChars = { ':', '\r', '\n' };
            return !input.Any(c => invalidChars.Contains(c));
        }
        public static bool dontJapanese(string input)
        {
            // ひらがな・カタカナ・漢字・全角記号などを含む場合 true
            return Regex.IsMatch(input, @"[\p{IsHiragana}\p{IsKatakana}\p{IsCJKUnifiedIdeographs}]");
        }
        public static bool zenkaku(string input)
        {
            // 全角文字（Unicode: U+FF01～U+FF60, U+FFE0～U+FFEE など）を含むか判定
            return input.Any(c => (c >= 0xFF01 && c <= 0xFF60) || (c >= 0xFFE0 && c <= 0xFFEE));
        }

        private void account_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
        //textBox1にフォーカスを当てるためのイベント

        private void account_Load(object sender, EventArgs e)
        {
            if (Title.language == 0)
            {
                button1.Text = "アカウント作成";
                button2.Text = "タイトルに戻る";
                label1.Text = "ユーザ名";
                label2.Text = "パスワード";
                label3.Text = "アカウント作成画面";
            }//日本語設定
            else
            {
                button1.Text = "Create Account";
                button2.Text = "Back to Title";
                label1.Text = "User Name";
                label2.Text = "Password";
                label3.Text = "Create Account";
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
        }
        //account.csがロードされたとき

        private void button1_Click(object sender, EventArgs e)
        {
            login sc3 = new login();

            string new_name = textBox1.Text;    //入力されたユーザ名
            string new_password = textBox2.Text;//入力されたパスワード
            string user_data;
            int lineCount = 0;
            int pass_len = new_password.Length; //パスワードの長さ

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
            } //空白になっていないかチェック
            else if (!dontusechar(new_name) || !dontusechar(new_password) || dontJapanese(new_name) || dontJapanese(new_password))
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
            } //禁止文字または日本語チェック
            else if (pass_len < 6 || pass_len > 16) // パスワードの長さチェック
            {
                if (Title.language == 0)
                {
                    label4.Text = "パスワードは8文字以上16文字以下で\n入力してください";
                }
                else
                {
                    label4.Text = "Password must be between\n8 and 16 characters";
                }
                return;
            } //パスワード文字数チェック
            else if (zenkaku(new_name) || zenkaku(new_password)) // 全角文字チェック
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
            } //全角文字チェック

            //user_data.txtを読み取りで開く
            using (StreamReader rw = new StreamReader("user_data.txt", Encoding.GetEncoding("UTF-8")))
                {
                    //ファイルの行数をカウント
                    while ((user_data = rw.ReadLine()) != null)
                    {
                        lineCount++;
                    }
                }


            //ファイルにデータがある場合
            if (lineCount > 0)
            {
                string output_name, output_password;
                int sign;

                //user_data.txtを再度読み取りで開く
                using (StreamReader rw = new StreamReader("user_data.txt", Encoding.GetEncoding("UTF-8")))
                {
                    //ファイルの終端まで読み取り
                    while ((user_data = rw.ReadLine()) != null)
                    {
                        //ユーザ名とパスワードを分割
                        sign = user_data.IndexOf(':');
                        output_name = user_data.Substring(0, sign);     //ユーザ名
                        output_password = user_data.Substring(sign + 1);//パスワード

                        if (new_name == output_name)//入力ユーザ名がファイルのユーザ名と一致する場合
                        {
                            if (Title.language == 0)
                            {
                                MessageBox.Show("このユーザ名は既に使用されています\r\nログインしてください");
                            }
                            else
                            {
                                MessageBox.Show("This user name is already in use\r\nPlease log in");
                            }
                            //ログイン画面に遷移
                            sc3.ShowDialog();
                            this.Close();
                            return;
                        }
                    }
                }
            }

            new_password = hashpassword(new_password); //パスワードをハッシュ化

            //user_data.txtを追記モードで開く
            using (StreamWriter sw = new StreamWriter("user_data.txt", true, Encoding.GetEncoding("UTF-8")))
            {
                sw.WriteLine(new_name + ':' + new_password);//:で区切ってユーザ名とパスワードを書き込み
            }

            //自動でログインぺージに遷移
            if (Title.language == 0)
            {
                MessageBox.Show("アカウントが登録されました\r\nログインしてください");
            }
            else
            {
                MessageBox.Show("Account has been registered\r\nPlease log in");
            }
            sc3.ShowDialog();
            this.Close();
        }
        //アカウント作成ボタン

        private void button2_Click(object sender, EventArgs e)
        {
            //タイトルページに遷移
            Title sc2 = new Title();
            sc2.ShowDialog();
            this.Close();
        }
        //タイトルに戻るボタン
    }
}
