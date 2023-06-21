//**********************************************
//                             Name: 黃匯勛                                     //
//                               Class: 資管三                                   //
//                              SID: S08490027                                 //
//                    Functions: 計時器及圖片播放                  //
//                                   Limitations:                                   //
//                               Assignment: No.3                             //
//                                     Date: 5/13                                   //
//**********************************************
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

namespace WindowsForm_HW3_Greenman
{
    public partial class Form1 : Form
    {
        int count ,photocount;//用來判別目前的秒數與圖片張別的變數。
        byte sec_counter = 30; //設定顯示秒數的最大值。
        byte photo_counter = 0;//設定圖片張數的最小值。
        byte clickevent = 0;//用來判定開始、暫停的變數。
        int[] tickVlaue = { 1000, 900, 750 , 50, 25, 12};//每次tick秒數(毫秒/次) 前3項控制至秒數，後3項控制圖片
        String iconFilePath = string.Empty;//設定winForm的icon路徑為空字串。
        String greenmanFilePath = string.Empty;//設定小綠人圖片的路徑為空字串。
        ImageList imageListBtn = new ImageList();//額外宣告一個imageList給開始、暫停以及結束的圖片用。
                                                                              //imageList1已經有拉出來，所以沒有特別再宣告。

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DirectoryInfo greenman = new DirectoryInfo(Application.StartupPath);//用來抓取小綠人圖片之路徑。
            greenmanFilePath = greenman.Parent.Parent.FullName + "\\image\\GreenWalking\\";
            imageList1.ImageSize = new Size(256, 256);//設定為圖片最大值，否則會太小。
           
            //===============================================//
            //   用來載入18張小綠人圖片的迴圈(圖片名稱01~18.bmp)。//
            //===============================================//
            for (int i = 1; i < 19; i++)//
            {
                if (i < 10)
                    imageList1.Images.Add(Image.FromFile(greenmanFilePath + "0" + i.ToString() + ".bmp"));
                else
                    imageList1.Images.Add(Image.FromFile(greenmanFilePath + i.ToString() + ".bmp"));
            }

            //=======================================//
            //       設定開始按鈕字串和結束按鈕字串為空       //
            //=======================================//
            buttonstart.Text = string.Empty;
            buttonEnd.Text = string.Empty;
            imageListBtn.ImageSize = new Size(42, 42);
            

            //===============================================//
            //  抓取開始、暫停以及結束圖片進上方宣告的imageListBtn//
            //===============================================//
            imageListBtn.Images.Add(Image.FromFile("..\\..\\image\\PlayPauseStop\\Play.png"));
            imageListBtn.Images.Add(Image.FromFile("..\\..\\image\\PlayPauseStop\\Pause.png"));
            imageListBtn.Images.Add(Image.FromFile("..\\..\\image\\PlayPauseStop\\Stop.png"));
            

            //=======================================//
            //    設定開始以及結束按鈕的字體位置和圖片      //
            //=======================================//
            buttonstart.BackgroundImageLayout = ImageLayout.Center;
            buttonstart.BackgroundImage = imageListBtn.Images[0];
            buttonEnd.BackgroundImageLayout = ImageLayout.Center;
            buttonEnd.BackgroundImage = imageListBtn.Images[2];


            //=======================================//
            //           設定winForm的Icon圖片和視窗文字           //
            //=======================================//
            this.Icon = new Icon("..\\..\\image\\TrafficLights\\1481525071_Citycons_trafficlight.ico");
            this.Text = "小綠人播放練習 - 變換速度";


            //==============================================//
            //    下方均為設定文字敘述、字體位置等一些基本設定    //
            //==============================================//
            labelSecond.Text = "倒數秒數 :";
            label3.Text = "";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.BackColor = Color.AliceBlue;
            label3.ForeColor = Color.Black;
            label3.Font = new Font("思源黑體", 40);
            
            
            labelFilename.Text = "檔案名稱 :";
            label2.Text = "";
            label2.Font = new Font("思源黑體", 16, FontStyle.Bold);
            label2.ForeColor = ColorTranslator.FromHtml("#3C3C3C") ; 
            label2.TextAlign = ContentAlignment.MiddleCenter;
            
            
            labelChangetime.Text = "變換時間(毫秒/次) :";
            label6.Text = "";
            label6.Font = new Font("思源黑體", 22, FontStyle.Bold);
            label6.ForeColor = ColorTranslator.FromHtml("#3C3C3C");
            label6.TextAlign = ContentAlignment.MiddleCenter;


            //===============================================//
            //      設置計時器1、2的初始tickValue和紀錄值的初始值      //
            //===============================================//
            timer1.Interval = tickVlaue[0];
            timer2.Interval = tickVlaue[3];
            timer1.Enabled = false;
            timer2.Enabled = false;
            count = sec_counter;
            photocount = photo_counter;

        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            //=======================================//
            //     按下結束按鈕時需把計時器給關閉再退出     //
            //=======================================//
            timer1.Enabled = false;
            timer2.Enabled = false;
            Application.Exit();
        }

        private void buttonstart_Click(object sender, EventArgs e)
        {
            //================================================//
            //       判斷按下時的clickevent的值，用來開始以及暫停          //
            //       首次按下clickevent會+1，之後取eventclick的餘數          //
            //       來判斷是否繼續會暫停。                                                  //
            //===============================================//

            timer1.Interval = tickVlaue[0];//按下時同時延遲1秒
            timer2.Interval = tickVlaue[0];
            if (clickevent == 0)
            {
                timer1.Enabled = true;
                timer2.Enabled = true;
                buttonstart.BackgroundImage = imageListBtn.Images[1];
                clickevent++;
            }
            else if (clickevent % 2 == 1)
            {
                timer1.Stop();
                timer2.Stop();
                buttonstart.BackgroundImage = imageListBtn.Images[0];
                clickevent++;
            }
            else if (clickevent % 2 == 0)
            {
                timer1.Start();
                timer2.Start();
                buttonstart.BackgroundImage = imageListBtn.Images[1];
                clickevent--;
            }
            else//clickevent的值不屬上面時會直接跳出視窗告訴錯誤。
            {
                MessageBox.Show("Button發生預期外的錯誤", "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //================================================//
            //       顯示倒數秒數的設定。                                                        //
            //       當count為30時計時器設定成初始tickValue。                   //
            //       而在設定的特定秒數(20、10秒)時修改tickValue，        //
            //       用來加快、減慢秒數圖片的顯示以及文字描述的        //
            //       顏色和背景顏色。                                                              //
            //===============================================//
            label3.Text = Convert.ToString(count);
            
            if (count == 30)
            {
                timer1.Interval = tickVlaue[0];
                timer2.Interval = tickVlaue[3];
                label6.Text = tickVlaue[3].ToString();
                label3.BackColor = ColorTranslator.FromHtml("#00EC00");
                label3.ForeColor = ColorTranslator.FromHtml("#272727");
                count--;
            }
            else if (count > 20)
            {
                timer1.Interval = tickVlaue[0];
                timer2.Interval = tickVlaue[3];
                count--;
            }
            else if (count > 10)
            {
                timer1.Interval = tickVlaue[1];
                timer2.Interval = tickVlaue[4];
                label6.Text = tickVlaue[4].ToString();
                label3.BackColor = ColorTranslator.FromHtml("#F9F900");
                label3.ForeColor = ColorTranslator.FromHtml("#F75000");
                count--;
            }
            else if (count > 1 && count <= 10)
            {
                timer1.Interval = tickVlaue[2];
                timer2.Interval = tickVlaue[5];
                label6.Text = tickVlaue[5].ToString();
                label3.BackColor = ColorTranslator.FromHtml("#FF0000");
                label3.ForeColor = ColorTranslator.FromHtml("#00FFFF");
                count--;
            }
            else if (count == 1)
            {
                count = sec_counter;
            }
            else//count值不屬上面時會直接跳出視窗告訴錯誤。
                MessageBox.Show("計時器數值內容錯誤!", "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //=======================================//
            //          按下叉叉時需把計時器給關閉再退出        //
            //=======================================//
            timer1.Enabled = false;
            timer2.Enabled = false;
            Application.Exit();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //==========================================================//
            //        設定播放的小綠人圖片和顯示圖片時所時用的是哪一張圖片          //
            //==========================================================//

            if (photocount < 10)
            {
                pictureBox1.Image = imageList1.Images[photocount];
                label2.Text = "0" + photocount + ".bmp";
                photocount++;
            }
            else if (photocount >= 10 && photocount < 18)
            {
                pictureBox1.Image = imageList1.Images[photocount];
                label2.Text =  photocount + ".bmp";
                photocount++;
            }
            else if (photocount == 18)
                photocount = photo_counter;
            else//photocount值不屬上面時會直接跳出視窗告訴錯誤。
                MessageBox.Show("計時器圖片出現錯誤!", "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
