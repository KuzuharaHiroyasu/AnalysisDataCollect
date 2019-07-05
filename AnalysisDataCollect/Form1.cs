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
        string path = "";
        string output_path = "";
        Encoding enc = Encoding.GetEncoding("Shift_JIS");   // 文字コードを指定
        bool stopFlag = false;

        public Form1()
        {
            InitializeComponent();
        }

        /************************************************************************/
        /* 関数名   : Form1_Shown									            */
        /* 機能     : フォーム表示時のイベント                        		    */
        /* 引数     : object   : sender                                         */
        /*          : EventArgs: e                                              */
        /* 戻り値   : なし														*/
        /************************************************************************/
        private void Form1_Shown(object sender, EventArgs e)
        {
            stopButton.Enabled = false;
        }

        /************************************************************************/
        /* 関数名   : path_textbox_MouseClick									*/
        /* 機能     : テキストボックスクリックでフォルダ選択ダイアログ表示		*/
        /* 引数     : object        : sender                                    */
        /*          : MouseEventArgs: e                                         */
        /* 戻り値   : なし														*/
        /************************************************************************/
        private void path_textbox_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                path_textbox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        /************************************************************************/
        /* 関数名   : path_textbox_DragDrop									    */
        /* 機能     : テキストボックスにドラッグ&ドロップでファイルパスを選択	*/
        /* 引数     : object       : sender                                     */
        /*          : DragEventArgs: e                                          */
        /* 戻り値   : なし														*/
        /************************************************************************/
        private void path_textbox_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] dragFilePathArr = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            path_textbox.Text = dragFilePathArr[0];
        }

        /************************************************************************/
        /* 関数名   : path_textbox_DragEnter									*/
        /* 機能     : テキストボックスにドラッグ&ドロップでファイルパスを選択	*/
        /* 引数     : object       : sender                                     */
        /*          : DragEventArgs: e                                          */
        /* 戻り値   : なし														*/
        /************************************************************************/
        private void path_textbox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        /************************************************************************/
        /* 関数名   : photorefStartButton_MouseClickAsync						*/
        /* 機能     : photorefデータまとめ開始ボタン押下                   		*/
        /* 引数     : object        : sender                                    */
        /*          : MouseEventArgs: e                                         */
        /* 戻り値   : なし														*/
        /************************************************************************/
        private async void photorefStartButton_MouseClickAsync(object sender, MouseEventArgs e)
        {
            int fileCount;

            // ボタン無効化
            buttonStateChange(false);

            /* データ出力用フォルダ作成 */
            createOutputFolder();

            //sumフォルダを抜いたサブフォルダの数を取得
            fileCount = (Directory.GetDirectories(path_textbox.Text, "*", SearchOption.TopDirectoryOnly).Length) - 1;

            // プログレスバー設定
            progressBar.Minimum = 0;
            progressBar.Maximum = fileCount;

            // まとめ処理を別スレッドで開始
            bool result = await Task.Run(() => photorefCollect(output_path, fileCount));

            if (stopFlag)
            {
                stopFlag = false;
            }
            else
            {
                msgShow(result);
            }

            // ボタン有効化
            buttonStateChange(true);
        }

        /************************************************************************/
        /* 関数名   : apneaStartButton_MouseClickAsync							*/
        /* 機能     : apneaデータまとめ開始ボタン押下		                    */
        /* 引数     : object        : sender                                    */
        /*          : MouseEventArgs: e                                         */
        /* 戻り値   : なし														*/
        /************************************************************************/
        private async void apneaStartButton_MouseClickAsync(object sender, MouseEventArgs e)
        {
            int fileCount;

            // ボタン無効化
            buttonStateChange(false);

            /* データ出力用フォルダ作成 */
            createOutputFolder();

            //sumフォルダを抜いたサブフォルダの数を取得
            fileCount = (Directory.GetDirectories(path_textbox.Text, "*", SearchOption.TopDirectoryOnly).Length) - 1;

            // プログレスバー設定
            progressBar.Minimum = 0;
            progressBar.Maximum = fileCount;

            // まとめ処理を別スレッドで開始
            bool result = await Task.Run(() => apneaCollect(output_path, fileCount));

            if (stopFlag)
            {
                stopFlag = false;
            }
            else
            {
                msgShow(result);
            }

            // ボタン有効化
            buttonStateChange(true);
        }

        /************************************************************************/
        /* 関数名   : acceStartButton_MouseClickAsync							*/
        /* 機能     : acceデータまとめ開始ボタン押下		                    */
        /* 引数     : object        : sender                                    */
        /*          : MouseEventArgs: e                                         */
        /* 戻り値   : なし														*/
        /************************************************************************/
        private async void acceStartButton_MouseClickAsync(object sender, MouseEventArgs e)
        {
            int fileCount;

            // ボタン無効化
            buttonStateChange(false);
            
            /* データ出力用フォルダ作成 */
            createOutputFolder();

            //sumフォルダを抜いたサブフォルダの数を取得
            fileCount = (Directory.GetDirectories(path_textbox.Text, "*", SearchOption.TopDirectoryOnly).Length) - 1;

            // プログレスバー設定
            progressBar.Minimum = 0;
            progressBar.Maximum = fileCount;

            // まとめ処理を別スレッドで開始
            bool result = await Task.Run(() => acceCollect(output_path, fileCount));

            if (stopFlag)
            {
                stopFlag = false;
            }
            else
            {
                msgShow(result);
            }

            // ボタン有効化
            buttonStateChange(true);
        }

        /************************************************************************/
        /* 関数名   : stopButton_Click		                					*/
        /* 機能     : 停止ボタン押下		                                    */
        /* 引数     : object   : sender                                         */
        /*          : EventArgs: e                                              */
        /* 戻り値   : なし														*/
        /************************************************************************/
        private void stopButton_Click(object sender, EventArgs e)
        {
            string message = "作業を停止しますか？";

            string caption = "停止";

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result = MessageBox.Show(this, message, caption, buttons, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                stopFlag = true;
            }
        }

        /************************************************************************/
        /* 関数名   : createOutputFolder						    			*/
        /* 機能     : データ出力用フォルダ作成		                            */
        /* 引数     : なし                                                      */
        /* 戻り値   : なし														*/
        /************************************************************************/
        private void createOutputFolder()
        {
            output_path = path_textbox.Text + "/_sum";

            //まとめデータ出力用のsumフォルダが既にある場合は一旦中身のファイルごと削除)
            while (System.IO.Directory.Exists(output_path))
            {
                Directory.Delete(output_path, true);
            }
            Directory.CreateDirectory(output_path);     //まとめデータ出力用のsumフォルダ作成
        }

        /************************************************************************/
        /* 関数名   : pluseCollect									            */
        /* 機能     : pluseデータまとめ処理		                                */
        /* 引数     : string: output_path                                       */
        /*          : int   : fileCount                                         */
        /* 戻り値   : true : 成功                                               */
        /*          : false: 失敗                                               */
        /************************************************************************/
        private bool pluseCollect(string output_path, int fileCount)
        {
            string fileName = "";
            string outputFilePat_snpk_1 = output_path + "/snpk(1)_sum.txt";
            string outputFilePath_ac_avg_1 = output_path + "/ac_avg(1)_sum.txt";
            string outputFilePath_dc_avg_1 = output_path + "/dc_avg(1)_sum.txt";
            string outputFilePath_snpk_2 = output_path + "/snpk(2)_sum.txt";
            string outputFilePath_ac_avg_2 = output_path + "/ac_avg(2)_sum.txt";
            string outputFilePath_dc_avg_2 = output_path + "/dc_avg(2)_sum.txt";
            string outputFilePath_spo2 = output_path + "/spo2(0)_sum.txt";
            string outputFilePath_ac_avg_ratio = output_path + "/ac_avg_ratio(0)_sum.txt";

            // 各まとめ用ファイルを開く
            StreamWriter writer_snpk_1 = new StreamWriter(outputFilePat_snpk_1, true, enc);
            StreamWriter writer_ac_avg_1 = new StreamWriter(outputFilePath_ac_avg_1, true, enc);
            StreamWriter writer_dc_avg_1 = new StreamWriter(outputFilePath_dc_avg_1, true, enc);
            StreamWriter writer_snpk_2 = new StreamWriter(outputFilePath_snpk_2, true, enc);
            StreamWriter writer_ac_avg_2 = new StreamWriter(outputFilePath_ac_avg_2, true, enc);
            StreamWriter writer_dc_avg_2 = new StreamWriter(outputFilePath_dc_avg_2, true, enc);
            StreamWriter writer_spo2 = new StreamWriter(outputFilePath_spo2, true, enc);
            StreamWriter writer_ac_avg_ratio = new StreamWriter(outputFilePath_ac_avg_ratio, true, enc);

            for (int i = 0; i <= fileCount - 1; i++)
            {
                if(stopFlag)
                {// 停止要求がきたら抜ける
                    break;
                }

                // カウント更新
                labelCntUp(i, fileCount);

                path = path_textbox.Text + "/" + i;
                if (System.IO.Directory.Exists(path))
                {
                    /* Pluse */
                    /* 赤色 */
                    // 脈拍
                    fileName = path + "/snpk(1).txt";
                    dataCollect(fileName, writer_snpk_1);

                    // AC平均値
                    fileName = path + "/ac_avg(1).txt";
                    dataCollect(fileName, writer_ac_avg_1);

                    // DC平均値
                    fileName = path + "/dc_avg(1).txt";
                    dataCollect(fileName, writer_dc_avg_1);

                    /* 赤外 */
                    // 脈拍
                    fileName = path + "/snpk(2).txt";
                    dataCollect(fileName, writer_snpk_2);

                    // AC平均値
                    fileName = path + "/ac_avg(2).txt";
                    dataCollect(fileName, writer_ac_avg_2);

                    // DC平均値
                    fileName = path + "/dc_avg(2).txt";
                    dataCollect(fileName, writer_dc_avg_2);

                    // SpO2
                    fileName = path + "/spo2(0).txt";
                    dataCollect(fileName, writer_spo2);

                    // AC平均値の比（赤色/赤外）
                    fileName = path + "/ac_avg_ratio(0).txt";
                    dataCollect(fileName, writer_ac_avg_ratio);
                }
                else
                {
                    fileCount++;
                }
            }
            writer_snpk_1.Close();
            writer_ac_avg_1.Close();
            writer_dc_avg_1.Close();
            writer_snpk_2.Close();
            writer_ac_avg_2.Close();
            writer_dc_avg_2.Close();
            writer_spo2.Close();
            writer_ac_avg_ratio.Close();
            
            return true;
        }

        /************************************************************************/
        /* 関数名   : apneaCollect									            */
        /* 機能     : apneaデータまとめ処理		                                */
        /* 引数     : string: output_path                                       */
        /*          : int   : fileCount                                         */
        /* 戻り値   : true : 成功                                               */
        /*          : false: 失敗                                               */
        /************************************************************************/
        private bool apneaCollect(string output_path, int fileCount)
        {
            string fileName = "";
            string outputFilePath_apnea = output_path + "/apnea_sum.txt";
            string outputFilePath_snore = output_path + "/snore__sum.txt";
            string outputFilePath_rawsnore = output_path + "/rawsnore_sum.txt";
            string outputFilePath_snore_Thre = output_path + "/snore_Thre_sum.txt";
            string outputFilePath_raw = output_path + "/raw_sum.txt";
            string outputFilePath_RMS = output_path + "/RMS_sum.txt";
            string outputFilePath_raw_heartBeatRemov = output_path + "/raw_heartBeatRemov_sum.txt";
            string outputFilePath_edge_movave = output_path + "/edge_movave_sum.txt";
            string outputFilePath_movave = output_path + "/movave_sum.txt";


            // 各まとめ用ファイルを開く
            StreamWriter writer_apnea = new StreamWriter(outputFilePath_apnea, true, enc);
            StreamWriter writer_snore = new StreamWriter(outputFilePath_snore, true, enc);
            StreamWriter writer_rawsnore = new StreamWriter(outputFilePath_rawsnore, true, enc);
            StreamWriter writer_snore_Thre = new StreamWriter(outputFilePath_snore_Thre, true, enc);
            StreamWriter writer_raw = new StreamWriter(outputFilePath_raw, true, enc);
            StreamWriter writer_RMS = new StreamWriter(outputFilePath_RMS, true, enc);
            StreamWriter writer_raw_heartBeatRemov = new StreamWriter(outputFilePath_raw_heartBeatRemov, true, enc);
            StreamWriter writer_edge_movave = new StreamWriter(outputFilePath_edge_movave, true, enc);
            StreamWriter writer_movave = new StreamWriter(outputFilePath_movave, true, enc);

            for (int i = 0; i <= fileCount - 1; i++)
            {
                if (stopFlag)
                {// 停止要求がきたら抜ける
                    break;
                }

                // カウント更新
                labelCntUp(i, fileCount);

                path = path_textbox.Text + "/" + i;
                if (System.IO.Directory.Exists(path))
                {
                    /* 呼吸 */
                    // 無呼吸
                    fileName = path + "/apnea.txt";
                    dataCollect(fileName, writer_apnea);

                    // いびき判定
                    fileName = path + "/snore_.txt";
                    dataCollect(fileName, writer_snore);

                    // いびき生データ
                    fileName = path + "/rawsnore.txt";
                    dataCollect(fileName, writer_rawsnore);

                    // 閾値を超えた回数の移動累計
                    fileName = path + "/snore_Thre.txt";
                    dataCollect(fileName, writer_snore_Thre);

                    // 呼吸生データ
                    fileName = path + "/raw.txt";
                    dataCollect(fileName, writer_raw);

                    // rmsデータ
                    fileName = path + "/RMS.txt";
                    dataCollect(fileName, writer_RMS);

                    // raw_heartBeatRemovデータ
                    fileName = path + "/raw_heartBeatRemov.txt";
                    dataCollect(fileName, writer_raw_heartBeatRemov);

                    // edge_movaveデータ
                    fileName = path + "/edge_movave.txt";
                    dataCollect(fileName, writer_edge_movave);

                    // movaveデータ
                    fileName = path + "/movave.txt";
                    dataCollect(fileName, writer_movave);
                } else
                {
                    fileCount++;
                }
            }
            writer_apnea.Close();
            writer_snore.Close();
            writer_rawsnore.Close();
            writer_snore_Thre.Close();
            writer_raw.Close();
            writer_RMS.Close();
            writer_raw_heartBeatRemov.Close();
            writer_edge_movave.Close();
            writer_movave.Close();

            return true;
        }

        /************************************************************************/
        /* 関数名   : acceCollect									            */
        /* 機能     : acceデータまとめ処理		                                */
        /* 引数     : string: output_path                                       */
        /*          : int   : fileCount                                         */
        /* 戻り値   : true : 成功                                               */
        /*          : false: 失敗                                               */
        /************************************************************************/
        private bool acceCollect(string output_path, int fileCount)
        {
            // 時間保存用テキスト
            DateTime dataTime;
            string saveTimeFilePath = output_path + "/saveTime.txt";
            StreamWriter saveTime = new StreamWriter(saveTimeFilePath, true, enc); ;

            // データ保存用テキスト
            string fileName = "";
            string outputFilePath_X = output_path + "/acce_x_sum.txt";
            string outputFilePath_Y = output_path + "/acce_y_sum.txt";
            string outputFilePath_Z = output_path + "/acce_z_sum.txt";

            // X, Y, Zのまとめ用ファイルを開く
            StreamWriter writer_X = new StreamWriter(outputFilePath_X, true, enc);
            StreamWriter writer_Y = new StreamWriter(outputFilePath_Y, true, enc);
            StreamWriter writer_Z = new StreamWriter(outputFilePath_Z, true, enc);

            for (int i = 0; i <= fileCount - 1; i++)
            {
                if (stopFlag)
                {// 停止要求がきたら抜ける
                    break;
                }

                // カウント更新
                labelCntUp(i, fileCount);

                path = path_textbox.Text + "/" + i;
                if (System.IO.Directory.Exists(path))
                {
                    /* 加速度 */
                    // X軸
                    fileName = path + "/acce_x.txt";
                    dataCollect(fileName, writer_X);

                    // Y軸
                    fileName = path + "/acce_y.txt";
                    dataCollect(fileName, writer_Y);

                    // Z軸
                    fileName = path + "/acce_z.txt";
                    dataCollect(fileName, writer_Z);
                }
                else
                {
                    fileCount++;
                }

                if(i == 0)
                {// 最初のフォルダ作成時間保存
                    saveTime.Write("[開始]\n");
                    dataTime = System.IO.Directory.GetCreationTime(path);
                    saveTime.Write(dataTime.ToString() + "\n" + "\n");
                }
                else if (i == (fileCount - 1))
                {// 最後のフォルダ作成時間保存
                    saveTime.Write("[終了]\n");
                    dataTime = System.IO.Directory.GetCreationTime(path);
                    saveTime.Write(dataTime.ToString() + "\n");
                }

            }
            writer_X.Close();
            writer_Y.Close();
            writer_Z.Close();
            saveTime.Close();
            return true;
        }

        /************************************************************************/
        /* 関数名   : photorefCollect								            */
        /* 機能     : photorefデータまとめ処理	                                */
        /* 引数     : string: output_path                                       */
        /*          : int   : fileCount                                         */
        /* 戻り値   : true : 成功                                               */
        /*          : false: 失敗                                               */
        /************************************************************************/
        private bool photorefCollect(string output_path, int fileCount)
        {
            string fileName = "";
            string outputFilePath_photoref = output_path + "/photoref_sum.txt";

            // フォトセンサーのまとめ用ファイルを開く
            StreamWriter writer_photoref = new StreamWriter(outputFilePath_photoref, true, enc);

            for (int i = 0; i <= fileCount - 1; i++)
            {
                if (stopFlag)
                {// 停止要求がきたら抜ける
                    break;
                }

                // カウント更新
                labelCntUp(i, fileCount);

                path = path_textbox.Text + "/" + i;
                if (System.IO.Directory.Exists(path))
                {
                    /* フォトセンサー */
                    fileName = path + "/photoref.txt";
                    dataCollect(fileName, writer_photoref);
                }
                else
                {
                    fileCount++;
                }
            }
            writer_photoref.Close();

            return true;
        }

        /************************************************************************/
        /* 関数名   : dataCollect					            				*/
        /* 機能     : データまとめ処理		                                    */
        /* 引数     : string: fileName                                          */
        /*          : StreamWriter: writer                                      */
        /* 戻り値   : なし														*/
        /************************************************************************/
        private void dataCollect(string fileName, StreamWriter writer)
        {
            string line = "";

            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(fileName, System.Text.Encoding.ASCII))
                {
                    line = file.ReadToEnd();
                    file.Close();
                    // ファイルに書きこむ
                    writer.Write(line);
                }
            }
        }

        /************************************************************************/
        /* 関数名   : labelCntUp					            				*/
        /* 機能     : ラベル更新   		                                        */
        /* 引数     : int: cnt                                                  */
        /*          : int: fileCount                                            */
        /* 戻り値   : なし														*/
        /************************************************************************/
        private void labelCntUp(int cnt, int fileCount)
        {
            this.Invoke((MethodInvoker)delegate
            {
                progressBar.Value = cnt;
                progressBar.Maximum = fileCount;
                Counter_label.Text = (cnt + 1) + " / " + fileCount;
                Update();
            });
        }

        /************************************************************************/
        /* 関数名   : msgShow                  									*/
        /* 機能     : メッセージ表示		                                    */
        /* 引数     : bool: result                                              */
        /* 戻り値   : なし														*/
        /************************************************************************/
        private void msgShow(bool result)
        {
            // メッセージを最前面に表示
            using (Form f = new Form())
            {
                f.TopMost = true;
                if (result)
                {
                    MessageBox.Show(f, "完了しました。\n" + output_path + "に結果を出力しました。");
                }
                else
                {
                    MessageBox.Show(f, path + "は存在しません");
                }
                f.TopMost = false;
            }
        }

        /************************************************************************/
        /* 関数名   : buttonStateChange        									*/
        /* 機能     : ボタンの状態変更    	                                    */
        /* 引数     : bool: enable                                              */
        /* 戻り値   : なし														*/
        /************************************************************************/
        private void buttonStateChange(bool enable)
        {
            if(enable)
            {
                acceStartButton.Enabled = true;
                apneaStartButton.Enabled = true;
                photorefStartButton.Enabled = true;
                stopButton.Enabled = false;
            }
            else
            {
                acceStartButton.Enabled = false;
                apneaStartButton.Enabled = false;
                photorefStartButton.Enabled = false;
                stopButton.Enabled = true;
            }
        }
    }
}
