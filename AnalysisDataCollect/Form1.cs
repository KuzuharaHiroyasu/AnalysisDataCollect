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

namespace AnalysisDataCollect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void path_textbox_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                path_textbox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void pluseStartButton_MouseClick(object sender, MouseEventArgs e)
        {
            int fileCount;
            string path;
            string output_path = path_textbox.Text + "/_sum";
            Encoding enc = Encoding.GetEncoding("Shift_JIS");   // 文字コードを指定

            //まとめデータ出力用のsumフォルダが既にある場合は一旦中身のファイルごと削除)
            if (System.IO.Directory.Exists(output_path))
            {
                Directory.Delete(output_path, true);
            }

            //sumフォルダがない状態でサブフォルダの数を取得
            fileCount = Directory.GetDirectories(path_textbox.Text, "*", SearchOption.TopDirectoryOnly).Length;

            Directory.CreateDirectory(output_path);     //まとめデータ出力用のsumフォルダ作成

            for (int i = 0; i <= fileCount-1; i++)
            {
                path = path_textbox.Text + "/" + i;
                if (System.IO.Directory.Exists(path))
                {
                    string line = "";
                    string fileName = "";

                    /* Pluse */
                    /* 赤色 */
                    // 脈拍
                    fileName = path + "/snpk(1).txt"; 
                    if (System.IO.File.Exists(fileName))
                    {
                        using (System.IO.StreamReader file = new System.IO.StreamReader(path + "/snpk(1).txt", System.Text.Encoding.ASCII))
                        {
                            line = file.ReadLine();

                            // ファイルを開く
                            StreamWriter writer = new StreamWriter(output_path + "/snpk(1)_sum.txt", true, enc);

                            writer.Write(line + "\n");

                            writer.Close();
                        }
                    }
                    // AC平均値
                    fileName = path + "/ac_avg(1).txt";
                    if (System.IO.File.Exists(fileName))
                    {
                        using (System.IO.StreamReader file = new System.IO.StreamReader(path + "/ac_avg(1).txt", System.Text.Encoding.ASCII))
                        {
                            line = file.ReadLine();

                            // ファイルを開く
                            StreamWriter writer = new StreamWriter(output_path + "/ac_avg(1)_sum.txt", true, enc);

                            writer.Write(line + "\n");

                            writer.Close();
                        }
                    }

                    // DC平均値
                    fileName = path + "/dc_avg(1).txt";
                    if (System.IO.File.Exists(fileName))
                    {
                        using (System.IO.StreamReader file = new System.IO.StreamReader(path + "/dc_avg(1).txt", System.Text.Encoding.ASCII))
                        {
                            line = file.ReadLine();

                            // ファイルを開く
                            StreamWriter writer = new StreamWriter(output_path + "/dc_avg(1)_sum.txt", true, enc);

                            writer.Write(line + "\n");

                            writer.Close();
                        }
                    }

                    /* 赤外 */
                    // 脈拍
                    fileName = path + "/snpk(2).txt";
                    if (System.IO.File.Exists(fileName))
                    {
                        using (System.IO.StreamReader file = new System.IO.StreamReader(path + "/snpk(2).txt", System.Text.Encoding.ASCII))
                        {
                            line = file.ReadLine();

                            // ファイルを開く
                            StreamWriter writer = new StreamWriter(output_path + "/snpk(2)_sum.txt", true, enc);

                            writer.Write(line + "\n");

                            writer.Close();
                        }
                    }

                    // AC平均値
                    fileName = path + "/ac_avg(2).txt";
                    if (System.IO.File.Exists(fileName))
                    {
                        using (System.IO.StreamReader file = new System.IO.StreamReader(path + "/ac_avg(2).txt", System.Text.Encoding.ASCII))
                        {
                            line = file.ReadLine();

                            // ファイルを開く
                            StreamWriter writer = new StreamWriter(output_path + "/ac_avg(2)_sum.txt", true, enc);

                            writer.Write(line + "\n");

                            writer.Close();
                        }
                    }

                    // DC平均値
                    fileName = path + "/dc_avg(2).txt";
                    if (System.IO.File.Exists(fileName))
                    {
                        using (System.IO.StreamReader file = new System.IO.StreamReader(path + "/dc_avg(2).txt", System.Text.Encoding.ASCII))
                        {
                            line = file.ReadLine();

                            // ファイルを開く
                            StreamWriter writer = new StreamWriter(output_path + "/dc_avg(2)_sum.txt", true, enc);

                            writer.Write(line + "\n");

                            writer.Close();
                        }
                    }

                    // SpO2
                    fileName = path + "/spo2(0).txt";
                    if (System.IO.File.Exists(fileName))
                    {
                        using (System.IO.StreamReader file = new System.IO.StreamReader(path + "/spo2(0).txt", System.Text.Encoding.ASCII))
                        {
                            line = file.ReadLine();

                            // ファイルを開く
                            StreamWriter writer = new StreamWriter(output_path + "/spo2(0)_sum.txt", true, enc);

                            writer.Write(line + "\n");

                            writer.Close();
                        }
                    }

                    // AC平均値の比（赤色/赤外）
                    fileName = path + "/ac_avg_ratio(0).txt";
                    if (System.IO.File.Exists(fileName))
                    {
                        using (System.IO.StreamReader file = new System.IO.StreamReader(path + "/ac_avg_ratio(0).txt", System.Text.Encoding.ASCII))
                        {
                            line = file.ReadLine();

                            // ファイルを開く
                            StreamWriter writer = new StreamWriter(output_path + "/ac_avg_ratio(0)_sum.txt", true, enc);

                            writer.Write(line + "\n");

                            writer.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(path + "は存在しません");
                    return;
                }
            }
            MessageBox.Show("完了しました。\n" + output_path + "に結果を出力しました。");
        }

        private void path_textbox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void path_textbox_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] dragFilePathArr = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            path_textbox.Text = dragFilePathArr[0];
        }

        private void acceStartButton_MouseClick(object sender, MouseEventArgs e)
        {
            int fileCount;
            int cnt_nine = 0;
            string path;
            string output_path = path_textbox.Text + "/_sum";
            Encoding enc = Encoding.GetEncoding("Shift_JIS");   // 文字コードを指定

            //まとめデータ出力用のsumフォルダが既にある場合は一旦中身のファイルごと削除)
            if (System.IO.Directory.Exists(output_path))
            {
                Directory.Delete(output_path, true);
            }

            //sumフォルダがない状態でサブフォルダの数を取得
            fileCount = Directory.GetDirectories(path_textbox.Text, "*", SearchOption.TopDirectoryOnly).Length;

            Directory.CreateDirectory(output_path);     //まとめデータ出力用のsumフォルダ作成

            for (int i = 0; i <= fileCount - 1; i++)
            {
                path = path_textbox.Text + "/" + cnt_nine;
                if (System.IO.Directory.Exists(path))
                {
                    string line = "";
                    string fileName = "";

                    /* 加速度 */
                    // X軸
                    fileName = path + "/acce_x.txt";
                    if (System.IO.File.Exists(fileName))
                    {
                        using (System.IO.StreamReader file = new System.IO.StreamReader(path + "/acce_x.txt", System.Text.Encoding.ASCII))
                        {
                            line = file.ReadLine();

                            // ファイルを開く
                            StreamWriter writer = new StreamWriter(output_path + "/acce_x_sum.txt", true, enc);

                            writer.Write(line + "\n");

                            writer.Close();
                        }
                    }
                    // Y軸
                    fileName = path + "/acce_y.txt";
                    if (System.IO.File.Exists(fileName))
                    {
                        using (System.IO.StreamReader file = new System.IO.StreamReader(path + "/acce_y.txt", System.Text.Encoding.ASCII))
                        {
                            line = file.ReadLine();

                            // ファイルを開く
                            StreamWriter writer = new StreamWriter(output_path + "/acce_y_sum.txt", true, enc);

                            writer.Write(line + "\n");

                            writer.Close();
                        }
                    }
                    // Z軸
                    fileName = path + "/acce_z.txt";
                    if (System.IO.File.Exists(fileName))
                    {
                        using (System.IO.StreamReader file = new System.IO.StreamReader(path + "/acce_z.txt", System.Text.Encoding.ASCII))
                        {
                            line = file.ReadLine();

                            // ファイルを開く
                            StreamWriter writer = new StreamWriter(output_path + "/acce_z_sum.txt", true, enc);

                            writer.Write(line + "\n");

                            writer.Close();
                        }
                    }
                    cnt_nine += 9;
                }
                else
                {
                    MessageBox.Show(path + "は存在しません");
                    return;
                }
            }
            MessageBox.Show("完了しました。\n" + output_path + "に結果を出力しました。");
        }
    }
}
