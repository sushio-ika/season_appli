using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace _0H04037_正田陸_uiux01
{
    public partial class season : Form
    {
        private bool suppressErrorMessage = false;

        private int month;// 選択された月を保存する変数

        private string user_info;//ログインしたユーザ情報を保存する変数
        private int user_sign;   //ユーザ情報の区切り文字の位置を保存する変数
        private string check_info;//旬の食材の情報を保存する変数
        private int check_sign;   //旬の食材の情報の区切り文字の位置を保存する変数

        string line = "---------------------------------------------------------------------------------------------";
        private string veg_item;  //旬の野菜の情報を保存する変数
        private string fruit_item;//旬の果物の情報を保存する変数
        private string fish_item; //旬の魚の情報を保存する変数
        private string[] English_month =
        {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
        };


        private void textlengthcheck(string text)
        {
            var lines = textBox1.Lines;
            if (text.Length > 50)
            {
                lines = lines.Skip(lines.Length - 50).ToArray();
                textBox1.Lines = lines;
            }
        }
        // テキストボックスの文字数をチェックして、50行を超えた行を削除する関数

        private void removeline()
        {
            // 末尾から線を探して最初に見つかったものを削除
            for (int i = listBox1.Items.Count - 1; i >= 0; i--)
            {
                if (listBox1.Items[i].ToString() == line)
                {
                    listBox1.Items.RemoveAt(i);
                    break;
                }
            }
        }
        // リストボックスから線を削除する関数

        public season()
        {
            InitializeComponent();
            this.Size = new Size(1920, 1080);
        }

        private void season_Load(object sender, EventArgs e)
        {
            label4.Visible = false; // ラベルを非表示にする
            label5.Visible = false; // ラベルを非表示にする
            using (StreamReader sr = new StreamReader("screen_setting.txt", Encoding.GetEncoding("UTF-8")))
            {
                string setting_info;// 画面設定情報を読み取る変数
                string list_item;   // コンボボックスのアイテムを保存する変数
                while ((setting_info = sr.ReadLine()) != null) 
                {
                    //画面設定情報の先頭の数字がコンボボックスのインデックスと同じ場合は×を○に変換
                    string[] user_parts = setting_info.Split(':');
                    list_item = comboBox2.Items[int.Parse(user_parts[0])].ToString();
                    list_item = list_item.Replace("×", "〇");
                    comboBox2.Items[int.Parse(user_parts[0])] = list_item;
                }
            }

            if (Title.language == 0)
            {
                button2.Text = "ログアウト";
                button1.Text = "画面リセット";
                button3.Text = "画面設定保存";
                button4.Text = "画面設定削除";
                checkBox1.Text = "旬の野菜";
                checkBox2.Text = "旬の果物";
                checkBox3.Text = "旬の魚";
                checkBox6.Text = "イラスト";
                label3.Text = "まずは月を選択するか保存済み画面を選択してください";
                label6.Text = "背景色変更";
                label7.Text = "保存した画面を選択";
                label8.Text = "確認したい月を選択";
            }//日本語設定
            else
            {
                button2.Text = "Logout";
                button1.Text = "Reset Screen";
                button3.Text = "Save Screen Settings";
                button4.Text = "Delete Screen Settings";
                checkBox1.Text = "Vegetables";
                checkBox2.Text = "Fruits";
                checkBox3.Text = "Fishes";
                checkBox6.Text = "Illustration";
                label3.Text = "Please select a month or a saved screen setting first";
                label6.Text = "Change Background Color";
                label7.Text = "Select Saved Screen";
                label8.Text = "Select a month";
            }//英語設定

            if (Title.screen_mode == 0)
            {
                this.BackColor = ColorTranslator.FromHtml("#F5F5F5");
                this.ForeColor = ColorTranslator.FromHtml("#212121");
                button2.BackColor = ColorTranslator.FromHtml("#0D47A1");
                button1.BackColor = ColorTranslator.FromHtml("#0D47A1");
                button2.ForeColor = Color.White;
                button1.ForeColor = Color.White;
                label1.BackColor = Color.White;
                label2.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                label3.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                label6.BackColor = Color.White;
                label6.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                label7.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                label8.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                listBox1.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                listBox1.ForeColor = ColorTranslator.FromHtml("#212121");
                comboBox1.ForeColor = ColorTranslator.FromHtml("#212121");
                textBox1.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                textBox1.ForeColor = ColorTranslator.FromHtml("#212121");
                trackBar2.BackColor = Color.White;
                checkBox1.BackColor = Color.White;
                checkBox2.BackColor = Color.White;
                checkBox3.BackColor = Color.White;
                checkBox6.BackColor = Color.White;
                checkBox1.ForeColor = Color.Black;
                checkBox2.ForeColor = Color.Black;
                checkBox3.ForeColor = Color.Black;
                checkBox6.ForeColor = Color.Black;
            }//ライトモード
            else
            {
                this.BackColor = ColorTranslator.FromHtml("#121233");
                this.ForeColor = ColorTranslator.FromHtml("#E0E0E0");
                button2.BackColor = ColorTranslator.FromHtml("#3F51B5");
                button1.BackColor = ColorTranslator.FromHtml("#3F51B5");
                button2.ForeColor = Color.White;
                button1.ForeColor = Color.White;
                label1.BackColor = ColorTranslator.FromHtml("#212121");
                label2.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                label3.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                label6.BackColor = ColorTranslator.FromHtml("#212121");
                label6.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                label7.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                label8.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                listBox1.BackColor = ColorTranslator.FromHtml("#212121");
                listBox1.ForeColor = ColorTranslator.FromHtml("#E0E0E0");
                comboBox1.ForeColor = ColorTranslator.FromHtml("#212121");
                trackBar2.BackColor = ColorTranslator.FromHtml("#212121");
                textBox1.BackColor = ColorTranslator.FromHtml("#212121");
                textBox1.ForeColor = ColorTranslator.FromHtml("#E0E0E0");
                checkBox1.BackColor = ColorTranslator.FromHtml("#212121");
                checkBox2.BackColor = ColorTranslator.FromHtml("#212121");
                checkBox3.BackColor = ColorTranslator.FromHtml("#212121");
                checkBox6.BackColor = ColorTranslator.FromHtml("#212121");
                checkBox1.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                checkBox2.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                checkBox3.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                checkBox6.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
            }//ダークモード

            //user_data.txtを読み取りで開く  
            using (StreamReader rw = new StreamReader("user_data.txt", Encoding.GetEncoding("UTF-8")))
            {
                for (int i = 0; i < login.loginnum; i++)//ログインした行数までスキップ  
                {
                    rw.ReadLine();
                }
                user_info = rw.ReadLine();//ログインデータ  
                user_sign = user_info.IndexOf(':');
            }

            //ログインしたユーザ情報をテキストファイルに日本語で追加
            using (StreamWriter sw = new StreamWriter("log_data_jpn.txt", true, Encoding.GetEncoding("UTF-8")))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + user_info.Substring(0, user_sign) + "さんがログインしました。");
            }

            //ログインしたユーザ情報をテキストファイルに英語で追加
            using (StreamWriter sw = new StreamWriter("log_data_eng.txt", true, Encoding.GetEncoding("UTF-8")))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + user_info.Substring(0, user_sign) + " logged in");
            }

            //ログインしたユーザ情報をラベルに表示してテキストボックスにログイン状態を追加
            if (Title.language == 0)
            {
                label2.Text = user_info.Substring(0, user_sign) + "さんようこそ！";
                textBox1.Text = File.ReadAllText("log_data_jpn.txt", Encoding.UTF8);

            }
            else
            {
                label2.Text = "Hello, " + user_info.Substring(0, user_sign) + "!";
                textBox1.Text = File.ReadAllText("log_data_eng.txt", Encoding.UTF8);
            }
            textlengthcheck(textBox1.Text);
        }
        //season.csがロードされたとき

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result;

            // ログアウトの確認ダイアログを表示  
            if (Title.language == 0)
            {
                result = MessageBox.Show("本当にログアウトしますか？", "", MessageBoxButtons.YesNo);
            }
            else
            {
                result = MessageBox.Show("Do you really want to logout?", "", MessageBoxButtons.YesNo);
            }

            //user_data.txtを読み取りで開く  
            using (StreamReader rw = new StreamReader("user_data.txt", Encoding.GetEncoding("UTF-8")))
            {
                for (int i = 0; i < login.loginnum; i++)//ログインした行数までスキップ  
                {
                    rw.ReadLine();
                }
                user_info = rw.ReadLine();//ログインデータ  
                user_sign = user_info.IndexOf(':');
            }

            if (result == DialogResult.Yes)
            {
                // ログアウトしたユーザ情報をテキストファイルに日本語で追加
                using (StreamWriter sw = new StreamWriter("log_data_jpn.txt", true, Encoding.GetEncoding("UTF-8")))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + user_info.Substring(0, user_sign) + "さんがログアウトしました。");
                }

                // ログアウトしたユーザ情報をテキストファイルに英語で追加
                using (StreamWriter sw = new StreamWriter("log_data_eng.txt", true, Encoding.GetEncoding("UTF-8")))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + user_info.Substring(0, user_sign) + " logged out");
                }

                //タイトルページへ遷移  
                Title sc1 = new Title();
                sc1.ShowDialog();
                this.Close();
            }
        }
        //終了ボタン

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //月を変更した場合、チェックボックスの状態を維持したままにする

            listBox1.Items.Clear(); // リストボックスをクリア
            label3.Text = "";
            label4.Visible = false; // ラベルを非表示にする
            label5.Visible = false; // ラベルを非表示にする
            if (checkBox1.Checked)
            {
                checkBox1_CheckedChanged(checkBox1, EventArgs.Empty);
            }
            if (checkBox2.Checked)
            {
                checkBox2_CheckedChanged(checkBox2, EventArgs.Empty);
            }
            if (checkBox3.Checked)
            {
                checkBox3_CheckedChanged(checkBox3, EventArgs.Empty);
            }
            if (checkBox6.Checked)
            {
                checkBox6_CheckedChanged(checkBox6, EventArgs.Empty);
            }
        }
        //月選択コンボボックス


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // ComboBoxの選択された月を取得  
            month = comboBox1.SelectedIndex + 1;

            if (1 <= month && month <= 12)
            {
                using (StreamReader rw = new StreamReader("vegetables_data.txt", Encoding.GetEncoding("UTF-8")))
                {
                    for (int i = 1; i < month; i++)
                    {
                        rw.ReadLine();
                    }
                    check_info = rw.ReadLine();
                    check_sign = check_info.IndexOf(':');
                }

                if (checkBox1.Checked)
                {
                    //リストボックスにすでにアイテムが入っている場合線を追加
                    if (listBox1.Items.Count > 0)
                    {
                        listBox1.Items.Add(line);
                    }

                    //チェックボックスがオンのとき、花の名前を表示  
                    if (Title.language == 0)
                    {
                        veg_item = month + "月が旬の野菜\t:" + check_info.Substring(0, check_sign);
                    }
                    else
                    {
                        veg_item = "Vegetable in season in\t" + English_month[month - 1] + "\t:" + check_info.Substring(check_sign + 1);
                    }
                    listBox1.Items.Add(veg_item);
                }//チェックボックス選択している場合
                else
                {
                    removeline();
                    listBox1.Items.Remove(veg_item); // 保存したインデックスを使用して削除
                }                  //チェックボックス選択していない場合

            }  //コンボボックス選択済みの場合
            else
            {
                if (!suppressErrorMessage)
                {
                    suppressErrorMessage = true;
                    if (Title.language == 0)
                    {
                        MessageBox.Show("有効な月を選択してください");
                    }
                    else
                    {
                        MessageBox.Show("Please select a valid month");
                    }
                    checkBox1.Checked = false;
                    label4.Visible = true; // ラベルを表示する
                    label5.Visible = true; // ラベルを表示する
                    suppressErrorMessage = false;
                }
            }                            //コンボボックス選択していない場合
        }
        //旬の野菜チェックボックス


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // ComboBoxの選択された月を取得  
            month = comboBox1.SelectedIndex + 1;

            if (1 <= month && month <= 12)
            {
                using (StreamReader rw = new StreamReader("fruits_data.txt", Encoding.GetEncoding("UTF-8")))
                {
                    for (int i = 1; i < month; i++)
                    {
                        rw.ReadLine();
                    }
                    check_info = rw.ReadLine();
                    check_sign = check_info.IndexOf(':');
                }

                if (checkBox2.Checked)
                {
                    if (listBox1.Items.Count > 0)
                    {
                        listBox1.Items.Add(line);
                    }

                    //チェックボックスがオンのとき、花の名前を表示  
                    if (Title.language == 0)
                    {
                        fruit_item = month + "月が旬の果物\t:" + check_info.Substring(0, check_sign);
                    }
                    else
                    {
                        fruit_item = "Fruits in season in\t" + English_month[month - 1] + "\t:" + check_info.Substring(check_sign + 1);
                    }
                    listBox1.Items.Add(fruit_item);
                }
                else
                {
                    removeline();
                    listBox1.Items.Remove(fruit_item); // 保存したインデックスを使用して削除
                }

            }
            else
            {
                if (!suppressErrorMessage)
                {
                    suppressErrorMessage = true;
                    if (Title.language == 0)
                    {
                        MessageBox.Show("有効な月を選択してください");
                    }
                    else
                    {
                        MessageBox.Show("Please select a valid month");
                    }
                    checkBox2.Checked = false;
                    label4.Visible = true; // ラベルを表示する
                    label5.Visible = true; // ラベルを表示する
                    suppressErrorMessage = false;
                }
            }
        }
        //旬の果物チェックボックス


        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            // ComboBoxの選択された月を取得  
            month = comboBox1.SelectedIndex + 1;

            if (1 <= month && month <= 12)
            {
                using (StreamReader rw = new StreamReader("fish_data.txt", Encoding.GetEncoding("UTF-8")))
                {
                    for (int i = 1; i < month; i++)
                    {
                        rw.ReadLine();
                    }
                    check_info = rw.ReadLine();
                    check_sign = check_info.IndexOf(':');
                }

                if (checkBox3.Checked)
                {
                    if (listBox1.Items.Count > 0)
                    {
                        listBox1.Items.Add(line);
                    }

                    //チェックボックスがオンのとき、花の名前を表示  
                    if (Title.language == 0)
                    {
                        fish_item = month + "月が旬の魚\t:" + check_info.Substring(0, check_sign);
                    }
                    else
                    {
                        fish_item = "Fishes in season in\t" + English_month[month - 1] + "\t:" + check_info.Substring(check_sign + 1);
                    }
                    listBox1.Items.Add(fish_item);
                }
                else
                {
                    removeline();
                    listBox1.Items.Remove(fish_item); // 保存したインデックスを使用して削除
                }

            }
            else
            {
                if (!suppressErrorMessage)
                {
                    suppressErrorMessage = true;
                    if (Title.language == 0)
                    {
                        MessageBox.Show("有効な月を選択してください");
                    }
                    else
                    {
                        MessageBox.Show("Please select a valid month");
                    }
                    checkBox3.Checked = false;
                    label4.Visible = true; // ラベルを表示する
                    label5.Visible = true; // ラベルを表示する
                    suppressErrorMessage = false;
                }
            }
        }
        //旬の魚チェックボックス


        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                // ComboBoxの選択された月を取得  
                month = comboBox1.SelectedIndex + 1;

                switch (month)
                {
                    case 1:
                        pictureBox1.Image = Image.FromFile("1月.png");
                        break;
                    case 2:
                        pictureBox1.Image = Image.FromFile("2月.png");
                        break;
                    case 3:
                        pictureBox1.Image = Image.FromFile("3月.png");
                        break;
                    case 4:
                        pictureBox1.Image = Image.FromFile("4月_3.png");
                        break;
                    case 5:
                        pictureBox1.Image = Image.FromFile("5月.png");
                        break;
                    case 6:
                        pictureBox1.Image = Image.FromFile("6月.png");
                        break;
                    case 7:
                        pictureBox1.Image = Image.FromFile("7月.png");
                        break;
                    case 8:
                        pictureBox1.Image = Image.FromFile("8月.png");
                        break;
                    case 9:
                        pictureBox1.Image = Image.FromFile("9月_1.png");
                        break;
                    case 10:
                        pictureBox1.Image = Image.FromFile("10月_1.png");
                        break;
                    case 11:
                        pictureBox1.Image = Image.FromFile("11月.png");
                        break;
                    case 12:
                        pictureBox1.Image = Image.FromFile("12月.png");
                        break;
                    default:// 無効な月の場合
                        if (!suppressErrorMessage)
                        {
                            suppressErrorMessage = true;
                            if (Title.language == 0)
                            {
                                MessageBox.Show("有効な月を選択してください");
                            }
                            else
                            {
                                MessageBox.Show("Please select a valid month");
                            }
                            checkBox6.Checked = false;
                            label4.Visible = true; // ラベルを表示する
                            label5.Visible = true; // ラベルを表示する
                            suppressErrorMessage = false;
                        }
                        break;
                }
            } //チェックボックスがオンのとき
            else
            {
                pictureBox1.Image = null; // チェックが外れたときは画像を非表示にする
            }                   //チェックボックスがオフのとき
        }
        //月のイラストチェックボックス
        


        private void button1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox6.Checked = false;
            trackBar2.Value = 0; // トラックバーを初期化
            listBox1.Items.Clear(); // リストボックスをクリア
            pictureBox1.Image = null; // 画像を非表示にする

            if (Title.screen_mode == 0)
            {
                this.BackColor = ColorTranslator.FromHtml("#F5F5F5");
            }
            else
            {
                this.BackColor = ColorTranslator.FromHtml("#121233");
            }
        }
        //画面リセットボタン


        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            int bar_value = trackBar2.Value;
            int month = comboBox1.SelectedIndex + 1;

            if (Title.screen_mode == 0)
            {
                if (3 <= month && month <= 5)
                {
                    this.BackColor = Color.FromArgb(245, 245 - bar_value, 245);
                }       //春の場合
                else if (6 <= month && month <= 8)
                {
                    this.BackColor = Color.FromArgb(245 - bar_value * 2, 245, 245 - bar_value);
                }  //夏の場合
                else if (9 <= month && month <= 11)
                {
                    this.BackColor = Color.FromArgb(245, 245 - bar_value, 245 - bar_value * 2);
                } //秋の場合
                else
                {
                    this.BackColor = Color.FromArgb(245 - bar_value * 3, 245, 245);
                }                                //冬の場合
            } //ライトモードの場合
            else
            {
                this.BackColor = Color.FromArgb(18, 18 + bar_value, 51 + bar_value);
            }                        //ダークモードの場合
        }
        //背景色変更トラックバー

        
        private void button3_Click(object sender, EventArgs e)
        {
            textlengthcheck(textBox1.Text);// テキストボックスの文字数をチェック

            //現在の画面情報を保存
            int save_index;
            int now_month = comboBox1.SelectedIndex + 1;
            int now_red = this.BackColor.R;
            int now_green = this.BackColor.G;
            int now_blue = this.BackColor.B;
            int now_bar_value = trackBar2.Value;
            int now_check1 = checkBox1.Checked ? 1 : 0;
            int now_check2 = checkBox2.Checked ? 1 : 0;
            int now_check3 = checkBox3.Checked ? 1 : 0;
            int now_check6 = checkBox6.Checked ? 1 : 0;
            int now_lang = Title.language;
            int now_mode = Title.screen_mode;

            string[] parts = { };
            int linecount = File.ReadAllLines("screen_setting.txt", Encoding.UTF8).Length;// 画面設定の行数を取得

            if (linecount >= 10)
            {
                if (!suppressErrorMessage)
                {
                    suppressErrorMessage = true;
                    if (Title.language == 0)
                    {
                        MessageBox.Show("画面設定の保存数が上限を超えています。いずれかを削除してください"); ;
                    }
                    else
                    {
                        MessageBox.Show("The number of saved screen settings exceeds the limit. Please delete one.");
                    }
                    suppressErrorMessage = false;
                }
            }// 画面設定の保存数が上限を超えている場合

            string save_info;
            int flg;

            // 画面設定の保存数が上限を超えていない場合、空いているインデックスを探す
            for (save_index = 0; save_index < 10; save_index++)
            {
                flg = 0;
                using (StreamReader sr = new StreamReader("screen_setting.txt", Encoding.GetEncoding("UTF-8")))
                {
                    while ((save_info = sr.ReadLine()) != null)
                    {
                        parts = save_info.Split(':');
                        if (int.Parse(parts[0]) == save_index)//save_indexの値がすでに登録されていた場合
                        {
                            flg++;
                        }
                    }
                }

                if (flg == 0)//一つも登録されていなかった場合
                {
                    break;
                }
            }

            // 画面設定ファイルに保存
            using (StreamWriter sw = new StreamWriter("screen_setting.txt", true, Encoding.GetEncoding("UTF-8")))
            {
                sw.WriteLine(save_index + ":" + now_month + ":" + now_red + ":" + now_green + ":" + now_blue + ":" + now_bar_value + ":" + now_check1 + ":" + now_check2 + ":" + now_check3 + ":" + now_check6 + ":" + now_lang + ":" + now_mode);
            }

            //変更情報をテキストファイルに日本語で追加
            using (StreamWriter sw = new StreamWriter("log_data_jpn.txt", true, Encoding.GetEncoding("UTF-8")))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + "画面設定" + (save_index + 1) + "に保存しました。");
            }

            //変更情報をテキストファイルに英語で追加
            using (StreamWriter sw = new StreamWriter("log_data_eng.txt", true, Encoding.GetEncoding("UTF-8")))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + "Screen settings" + (save_index + 1) + " saved");
            }

            if (Title.language == 0)
            {
                MessageBox.Show("画面設定" + (save_index + 1) + "に保存しました");
                textBox1.Text += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + "画面設定" + (save_index + 1) + "に保存しました。\n";
            }
            else
            {
                MessageBox.Show("Screen settings" + (save_index + 1) + " saved");
                textBox1.Text += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + "Screen settings" + (save_index + 1) + " saved\n";
            }
            textBox1.SelectionStart = textBox1.Text.Length; // テキストボックスのカーソルを最後に移動
            textBox1.ScrollToCaret();                       // テキストボックスのスクロールを最後に移動

            // 追加したコンボボックスのアイテムの×を〇に変換
            string item = comboBox2.Items[save_index].ToString();
            item = item.Replace("×", "〇");
            comboBox2.Items[save_index] = item;
        }
        //画面設定保存ボタン


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textlengthcheck(textBox1.Text);// テキストボックスの文字数をチェック
            label5.Visible = false; // ラベルを非表示にする
      
            int select_index = comboBox2.SelectedIndex;
            string[] parts = { };

            using (StreamReader sw = new StreamReader("screen_setting.txt", Encoding.GetEncoding("UTF-8")))
            {
                // 選択された画面設定のインデックスを探す
                while ((user_info = sw.ReadLine()) != null)
                {
                    // コロンで分割
                    parts = user_info.Split(':');
                    if (select_index == int.Parse(parts[0]))
                    {
                        break;
                    }
                }
            }

            if (user_info == null || select_index != int.Parse(parts[0])) 
            {
                if (Title.language == 0)
                {
                    MessageBox.Show("画面設定が保存されていません");
                }
                else
                {
                    MessageBox.Show("Screen settings have not been saved");
                }
                return;
            } //選択された画面設定が保存されていない場合

            if (parts.Length != 12)
            {
                MessageBox.Show("設定ファイルのフォーマットが不正です");
                return;
            } //画面情報の値が不正な場合

            // 選択された画面設定を適用
            comboBox1.SelectedIndex = int.Parse(parts[1]) - 1;
            this.BackColor = Color.FromArgb(int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]));
            trackBar2.Value = int.Parse(parts[5]);
            checkBox1.Checked = int.Parse(parts[6]) == 1;
            checkBox2.Checked = int.Parse(parts[7]) == 1;
            checkBox3.Checked = int.Parse(parts[8]) == 1;
            checkBox6.Checked = int.Parse(parts[9]) == 1;
            Title.screen_mode = int.Parse(parts[11]);

            if (Title.screen_mode == 0)
            {
                this.ForeColor = ColorTranslator.FromHtml("#212121");
                button2.BackColor = ColorTranslator.FromHtml("#0D47A1");
                button1.BackColor = ColorTranslator.FromHtml("#0D47A1");
                button2.ForeColor = Color.White;
                button1.ForeColor = Color.White;
                label1.BackColor = Color.White;
                label2.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                label3.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                label6.BackColor = Color.White;
                label6.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                label7.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                label8.ForeColor = ColorTranslator.FromHtml("#0D47A1");
                listBox1.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                listBox1.ForeColor = ColorTranslator.FromHtml("#212121");
                comboBox1.ForeColor = ColorTranslator.FromHtml("#212121");
                trackBar2.BackColor = Color.White;
                textBox1.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                textBox1.ForeColor = ColorTranslator.FromHtml("#212121");
                checkBox1.BackColor = Color.White;
                checkBox2.BackColor = Color.White;
                checkBox3.BackColor = Color.White;
                checkBox6.BackColor = Color.White;
                checkBox1.ForeColor = Color.Black;
                checkBox2.ForeColor = Color.Black;
                checkBox3.ForeColor = Color.Black;
                checkBox6.ForeColor = Color.Black;
            } //コントロールをライトモードに適用
            else
            {
                this.ForeColor = ColorTranslator.FromHtml("#E0E0E0");
                button2.BackColor = ColorTranslator.FromHtml("#3F51B5");
                button1.BackColor = ColorTranslator.FromHtml("#3F51B5");
                button2.ForeColor = Color.White;
                button1.ForeColor = Color.White;
                label1.BackColor = ColorTranslator.FromHtml("#212121");
                label2.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                label3.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                label6.BackColor = ColorTranslator.FromHtml("#212121");
                label6.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                label7.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                label8.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                listBox1.BackColor = ColorTranslator.FromHtml("#212121");
                listBox1.ForeColor = ColorTranslator.FromHtml("#E0E0E0");
                comboBox1.ForeColor = ColorTranslator.FromHtml("#212121");
                trackBar2.BackColor = ColorTranslator.FromHtml("#212121");
                textBox1.BackColor = ColorTranslator.FromHtml("#212121");
                textBox1.ForeColor = ColorTranslator.FromHtml("#E0E0E0");
                checkBox1.BackColor = ColorTranslator.FromHtml("#212121");
                checkBox2.BackColor = ColorTranslator.FromHtml("#212121");
                checkBox3.BackColor = ColorTranslator.FromHtml("#212121");
                checkBox6.BackColor = ColorTranslator.FromHtml("#212121");
                checkBox1.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                checkBox2.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                checkBox3.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
                checkBox6.ForeColor = ColorTranslator.FromHtml("#BBDEFB");
            }                        //コントロールをダークモードに適用

            //変更した画面情報をテキストファイルに日本語で追加
            using (StreamWriter sw = new StreamWriter("log_data_jpn.txt", true, Encoding.GetEncoding("UTF-8")))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + "画面設定"+ (select_index+1) +"を適用しました。");
            }

            //変更した画面情報をテキストファイルに英語で追加
            using (StreamWriter sw = new StreamWriter("log_data_eng.txt", true, Encoding.GetEncoding("UTF-8")))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + "applied screen settings"+ (select_index+1));
            }

            if (Title.language == 0)
            {
                textBox1.Text += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + "画面設定" + (select_index + 1) + "を適用しました。\n";
            }
            else
            {
                textBox1.Text += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + "applied screen settings" + (select_index + 1) + "\n";
            }
            textBox1.SelectionStart = textBox1.Text.Length; // テキストボックスのカーソルを最後に移動
            textBox1.ScrollToCaret();                       // テキストボックスのスクロールを最後に移動
        }
        //画面設定格納コンボボックス


        private void button4_Click(object sender, EventArgs e)
        {
            textlengthcheck(textBox1.Text);// テキストボックスの文字数をチェック

            int delate_index = comboBox2.SelectedIndex;
            string delate_info = null;

            if (delate_index < 0)
            {
                if (Title.language == 0)
                {
                    MessageBox.Show("削除する設定を選択してください");
                }
                else
                {
                    MessageBox.Show("Please select a setting to delete");
                }
                return;
            }//削除する設定が選択されていない場合
            else
            {
                comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
                // イベントハンドラを一時的に解除

                string[] parts = { };
                string delate_id = null;

                using (StreamReader sw = new StreamReader("screen_setting.txt", Encoding.GetEncoding("UTF-8")))
                {
                    while ((delate_info = sw.ReadLine()) != null) 
                    {
                        parts = delate_info.Split(':');

                        //削除する設定のインデックスを探し、変数に保存
                        if (delate_index == int.Parse(parts[0]))
                        {
                            delate_id = parts[0];
                            break;
                        }
                    }
                }

                if (delate_info == null)
                {
                    if (Title.language == 0)
                    {
                        MessageBox.Show("画面設定が保存されていません");
                    }
                    else
                    {
                        MessageBox.Show("Screen settings have not been saved");
                    }
                    comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
                    return;
                }//選択された画面設定が保存されていない場合


                // 選択された設定を削除  
                string[] lines = File.ReadAllLines("screen_setting.txt", Encoding.UTF8);

                using (StreamWriter sw = new StreamWriter("screen_setting.txt", false, Encoding.UTF8))
                {
                    foreach (var line in lines)
                    {
                        var lineParts = line.Split(':');
                        if (lineParts.Length > 0 && lineParts[0] != delate_id)
                        {
                            sw.WriteLine(line);
                        }
                    }
                }

                // 削除されたコンボボックスのアイテムの○を×に変換
                string item = comboBox2.Items[int.Parse(parts[0])].ToString();
                item = item.Replace("〇", "×");
                comboBox2.Items[int.Parse(parts[0])] = item;

                // 画面設定の削除情報をテキストファイルに日本語で追加
                using (StreamWriter sw = new StreamWriter("log_data_jpn.txt", true, Encoding.GetEncoding("UTF-8")))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + "画面設定" + (delate_index + 1) + "を削除しました。");
                }

                // 画面設定の削除情報をテキストファイルに英語で追加
                using (StreamWriter sw = new StreamWriter("log_data_eng.txt", true, Encoding.GetEncoding("UTF-8")))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + "Screen settings" + (delate_index + 1) + " deleted");
                }

                if (Title.language == 0)
                {
                    MessageBox.Show("画面設定" + (delate_index + 1) + "を削除しました");
                    textBox1.Text+= DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + "画面設定" + (delate_index + 1) + "を削除しました。\n";
                }
                else
                {
                    MessageBox.Show("Screen settings " + (delate_index + 1) + " deleted");
                    textBox1.Text += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss  ") + "Screen settings " + (delate_index + 1) + " deleted\n";
                }
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();

                button1_Click(sender, e);//画面リセットボタンの処理を呼び出す
                comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            }
        }
        //画面設定削除ボタン




        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_MouseUp_1(object sender, MouseEventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
