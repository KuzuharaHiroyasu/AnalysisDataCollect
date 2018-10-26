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

        /* テキストボックスクリックでフォルダ選択ダイアログ表示 */
        private void path_textbox_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                path_textbox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        /* テキストボックスにドラッグ&ドロップでファイルパスを選択 */
        private void path_textbox_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] dragFilePathArr = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            path_textbox.Text = dragFilePathArr[0];
        }

        private void path_textbox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        /* pluseデータまとめ開始ボタン押下 */
        private async void pluseStartButton_MouseClickAsync(object sender, MouseEventArgs e)
        {
            int fileCount;
            string output_path = path_textbox.Text + "/_sum";

            //まとめデータ出力用のsumフォルダが既にある場合は一旦中身のファイルごと削除)
            if (System.IO.Directory.Exists(output_path))
            {
                Directory.Delete(output_path, true);
            }

            //sumフォルダがない状態でサブフォルダの数を取得
            fileCount = Directory.GetDirectories(path_textbox.Text, "*", SearchOption.TopDirectoryOnly).Length;

            Directory.CreateDirectory(output_path);     //まとめデータ出力用のsumフォルダ作成


            // 時間のかかる処理を別スレッドで開始
            Boolean result = await Task.Run(() => pluseCollect(output_path, fileCount));

            if (result)
            {
                MessageBox.Show("完了しました。\n" + output_path + "に結果を出力しました。");
            }
        }

        /* apneaデータまとめ開始ボタン押下 */
        private async void apneaStartButton_MouseClickAsync(object sender, MouseEventArgs e)
        {
            int fileCount;
            string output_path = path_textbox.Text + "/_sum";

            //まとめデータ出力用のsumフォルダが既にある場合は一旦中身のファイルごと削除)
            if (System.IO.Directory.Exists(output_path))
            {
                Directory.Delete(output_path, true);
            }

            //sumフォルダがない状態でサブフォルダの数を取得
            fileCount = Directory.GetDirectories(path_textbox.Text, "*", SearchOption.TopDirectoryOnly).Length;

            Directory.CreateDirectory(output_path);     //まとめデータ出力用のsumフォルダ作成

            // 時間のかかる処理を別スレッドで開始
            Boolean result = await Task.Run(() => apneaCollect(output_path, fileCount));

            if (result)
            {
                MessageBox.Show("完了しました。\n" + output_path + "に結果を出力しました。");
            }
        }

        /* acceデータまとめ開始ボタン押下 */
        private async void acceStartButton_MouseClickAsync(object sender, MouseEventArgs e)
        {
            int fileCount;
            string output_path = path_textbox.Text + "/_sum";

            //まとめデータ出力用のsumフォルダが既にある場合は一旦中身のファイルごと削除)
            if (System.IO.Directory.Exists(output_path))
            {
                Directory.Delete(output_path, true);
            }

            //sumフォルダがない状態でサブフォルダの数を取得
            fileCount = Directory.GetDirectories(path_textbox.Text, "*", SearchOption.TopDirectoryOnly).Length;

            Directory.CreateDirectory(output_path);     //まとめデータ出力用のsumフォルダ作成

            // 時間のかかる処理を別スレッドで開始
            Boolean result = await Task.Run(() => acceCollect(output_path, fileCount));

            if (result)
            {
                MessageBox.Show("完了しました。\n" + output_path + "に結果を出力しました。");
            }
        }

        /* pluseデータまとめ処理 */
        private Boolean pluseCollect(string output_path, int fileCount)
        {
            string path = "";
            string fileName = "";
            string outputFilePath = "";

            for (int i = 0; i <= fileCount - 1; i++)
            {
                // カウント更新
                labelCntUp(i, fileCount);

                path = path_textbox.Text + "/" + i;
                if (System.IO.Directory.Exists(path))
                {
                    /* Pluse */
                    /* 赤色 */
                    // 脈拍
                    fileName = path + "/snpk(1).txt";
                    outputFilePath = output_path + "/snpk(1)_sum.txt";
                    dataCollect(fileName, outputFilePath);

                    // AC平均値
                    fileName = path + "/ac_avg(1).txt";
                    outputFilePath = output_path + "/ac_avg(1)_sum.txt";
                    dataCollect(fileName, outputFilePath);

                    // DC平均値
                    fileName = path + "/dc_avg(1).txt";
                    outputFilePath = output_path + "/dc_avg(1)_sum.txt";
                    dataCollect(fileName, outputFilePath);

                    /* 赤外 */
                    // 脈拍
                    fileName = path + "/snpk(2).txt";
                    outputFilePath = output_path + "/snpk(2)_sum.txt";
                    dataCollect(fileName, outputFilePath);

                    // AC平均値
                    fileName = path + "/ac_avg(2).txt";
                    outputFilePath = output_path + "/ac_avg(2)_sum.txt";
                    dataCollect(fileName, outputFilePath);

                    // DC平均値
                    fileName = path + "/dc_avg(2).txt";
                    outputFilePath = output_path + "/dc_avg(2)_sum.txt";
                    dataCollect(fileName, outputFilePath);

                    // SpO2
                    fileName = path + "/spo2(0).txt";
                    outputFilePath = output_path + "/spo2(0)_sum.txt";
                    dataCollect(fileName, outputFilePath);

                    // AC平均値の比（赤色/赤外）
                    fileName = path + "/ac_avg_ratio(0).txt";
                    outputFilePath = output_path + "/ac_avg_ratio(0)_sum.txt";
                    dataCollect(fileName, outputFilePath);
                }
                else
                {
                    MessageBox.Show(path + "は存在しません");
                    return false;
                }
            }
            return true;
        }

        /* apneaデータまとめ処理 */
        private Boolean apneaCollect(string output_path, int fileCount)
        {
            string path = "";
            string fileName = "";
            string outputFilePath = "";

            for (int i = 0; i <= fileCount - 1; i++)
            {
                // カウント更新
                labelCntUp(i, fileCount);

                path = path_textbox.Text + "/" + i;
                if (System.IO.Directory.Exists(path))
                {
                    /* 呼吸 */
                    // 無呼吸
                    fileName = path + "/apnea.txt";
                    outputFilePath = output_path + "/apnea_sum.txt";
                    dataCollect(fileName, outputFilePath);

                    // いびき判定
                    fileName = path + "/snore_.txt";
                    outputFilePath = output_path + "/snore__sum.txt";
                    dataCollect(fileName, outputFilePath);

                    // いびき生データ
                    fileName = path + "/rawsnore.txt";
                    outputFilePath = output_path + "/rawsnore_sum.txt";
                    dataCollect(fileName, outputFilePath);

                    // 閾値を超えた回数の移動累計
                    fileName = path + "/snore_Thre.txt";
                    outputFilePath = output_path + "/snore_Thre.txt";
                    dataCollect(fileName, outputFilePath);

                    // 呼吸生データ
                    fileName = path + "/raw.txt";
                    outputFilePath = output_path + "/raw.txt";
                    dataCollect(fileName, outputFilePath);
                }
                else
                {
                    MessageBox.Show(path + "は存在しません");
                    return false;
                }
            }
            return true;
        }

        /* acceデータまとめ処理 */
        private Boolean acceCollect(string output_path, int fileCount)
        {
            string path = "";
            string fileName = "";
            string outputFilePath = "";

            for (int i = 0; i <= fileCount - 1; i++)
            {
                // カウント更新
                labelCntUp(i, fileCount);

                path = path_textbox.Text + "/" + i;
                if (System.IO.Directory.Exists(path))
                {
                    /* 加速度 */
                    // X軸
                    fileName = path + "/acce_x.txt";
                    outputFilePath = output_path + "/acce_x.txt";
                    dataCollect(fileName, outputFilePath);

                    // Y軸
                    fileName = path + "/acce_y.txt";
                    outputFilePath = output_path + "/acce_y.txt";
                    dataCollect(fileName, outputFilePath);

                    // Z軸
                    fileName = path + "/acce_z.txt";
                    outputFilePath = output_path + "/acce_z.txt";
                    dataCollect(fileName, outputFilePath);
                }
                else
                {
                    MessageBox.Show(path + "は存在しません");
                    return false;
                }
            }
            return true;
        }

        /* データまとめ処理 */
        private void dataCollect(string fileName, string outputFilePath)
        {
            string line = "";
            Encoding enc = Encoding.GetEncoding("Shift_JIS");   // 文字コードを指定

            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(fileName, System.Text.Encoding.ASCII))
                {
                    line = file.ReadToEnd();
                    file.Close();
                    // ファイルを開く
                    StreamWriter writer = new StreamWriter(outputFilePath, true, enc);
                    writer.Write(line);
                    writer.Close();
                }
            }
        }

        /* ラベル更新 */
        private void labelCntUp(int cnt, int fileCount)
        {
            Counter_label.Text = (cnt + 1) + " / " + fileCount;
            Update();
        }
    }
}
