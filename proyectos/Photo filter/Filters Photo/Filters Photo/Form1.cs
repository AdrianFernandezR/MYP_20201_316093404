using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;
using System.Drawing.Imaging;
namespace Filters_Photo
{
    /// <summary>
    ///+++ Code of a Image filters application
    /// </summary>
    public partial class Form1 : Form
    {   
        /// <item>
        /// Builder of the app's base
        /// </item>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loader of the app's base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        /// <summary>
        /// Picture box click function, no active but necessary for the load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Item wich save the output file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "jpg (*.jpg)|*.jpg|bmp (*.bmp)|*.bmp|png (*.png)|*.png";

            if(sfd.ShowDialog()==DialogResult.OK && sfd.FileName.Length > 0)
            {

                pictureBox2.Image.Save(sfd.FileName);
            }


        }

        /// <summary>
        /// function that load the file what we want to modify
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "jpg (*.jpg)|*.jpg|bmp (*.bmp)|*.bmp|png (*.png)|*.png";
            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName.Length > 0)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                

            }

        }

        /// <summary>
        /// Function that pixelate an image
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="squareSize"></param>
        public static void ApplyNormalPixelate(ref Bitmap bmp, Size squareSize)
        {
            Bitmap TempBmp = (Bitmap)bmp.Clone();

            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData TempBmpData = TempBmp.LockBits(new Rectangle(0, 0, TempBmp.Width, TempBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* ptr = (byte*)bmpData.Scan0.ToPointer();
                byte* TempPtr = (byte*)TempBmpData.Scan0.ToPointer();

                int stopAddress = (int)ptr + bmpData.Stride * bmpData.Height;

                int Val = 0;
                int i = 0, X = 0, Y = 0;
                int BmpStride = bmpData.Stride;
                int BmpWidth = bmp.Width;
                int BmpHeight = bmp.Height;
                int SqrWidth = squareSize.Width;
                int SqrHeight = squareSize.Height;
                int XVal = 0, YVal = 0;

                while ((int)ptr != stopAddress)
                {
                    X = i % BmpWidth;
                    Y = i / BmpWidth;

                    XVal = X + (SqrWidth - X % SqrWidth);
                    YVal = Y + (SqrHeight - Y % SqrHeight);

                    if (XVal < 0 && XVal >= BmpWidth)
                        XVal = 0;

                    if (YVal < 0 && YVal >= BmpHeight)
                        YVal = 0;

                    if (XVal > 0 && XVal < BmpWidth && YVal > 0 && YVal < BmpHeight)
                    {
                        Val = (YVal * BmpStride) + (XVal * 3);

                        ptr[0] = TempPtr[Val];
                        ptr[1] = TempPtr[Val + 1];
                        ptr[2] = TempPtr[Val + 2];
                    }

                    ptr += 3;
                    i++;
                }
            }

            bmp.UnlockBits(bmpData);
            TempBmp.UnlockBits(TempBmpData);
        }


        /// <summary>
        /// Box wich contains all the filters option, this one activate the filter too
        /// </summary>
        /// <exception cref="System.Exception">Thrown when the image isn't load
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Filters.SelectedItem == "Red")
            {
                
                try
                {
                    Image toEdit = (Image)pictureBox1.Image.Clone();
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox2.Image = toEdit;

                    Bitmap bmp = new Bitmap(toEdit);

                    int width = bmp.Width;
                    int heigth = bmp.Height;

                    Bitmap rbmp = new Bitmap(bmp);

                    for (int y = 0; y < heigth; y++)
                    {

                        for (int x = 0; x < width; x++)
                        {

                            Color p = bmp.GetPixel(x, y);

                            int a = p.A;
                            int r = p.R;
                            int g = p.G;
                            int b = p.B;

                            rbmp.SetPixel(x, y, Color.FromArgb(a, r, 0, 0));
                        }
                    }

                    pictureBox2.Image = rbmp;
                
                } 
                catch (System.Exception excep) 
                {
                   

                    MessageBox.Show("Select a valid file", "Image Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                   

                }
            }

            if (Filters.SelectedItem == "Blue")
            {

                try
                {
                    Image toEdit = (Image)pictureBox1.Image.Clone();
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox2.Image = toEdit;

                    Bitmap bmp = new Bitmap(toEdit);

                    int width = bmp.Width;
                    int heigth = bmp.Height;

                    Bitmap bbmp = new Bitmap(bmp);

                    for (int y = 0; y < heigth; y++)
                    {
                        for (int x = 0; x < width; x++)

                        {

                            Color p = bmp.GetPixel(x, y);

                            int a = p.A;
                            int r = p.R;
                            int g = p.G;
                            int b = p.B;

                            bbmp.SetPixel(x, y, Color.FromArgb(a, 0, 0, b));
                        }
                    }

                    pictureBox2.Image = bbmp;
                }
                catch (System.Exception excep)
                {


                    MessageBox.Show("Select a valid file", "Image Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
            }

            if (Filters.SelectedItem == "Green")
            {

                try
                {
                    Image toEdit = (Image)pictureBox1.Image.Clone();
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox2.Image = toEdit;

                    Bitmap bmp = new Bitmap(toEdit);

                    int width = bmp.Width;
                    int heigth = bmp.Height;

                    Bitmap gbmp = new Bitmap(bmp);

                    for (int y = 0; y < heigth; y++)
                    {

                        for (int x = 0; x < width; x++)
                        {

                            Color p = bmp.GetPixel(x, y);

                            int a = p.A;
                            int r = p.R;
                            int g = p.G;
                            int b = p.B;

                            gbmp.SetPixel(x, y, Color.FromArgb(a, 0, g, 0));
                        }
                    }

                    pictureBox2.Image = gbmp;

                }
                catch (System.Exception excep)
                {


                    MessageBox.Show("Select a valid file", "Image Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
            }

            if (Filters.SelectedItem == "Gray")
            {

                try
                {

                    Image toEdit = (Image)pictureBox1.Image.Clone();
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox2.Image = toEdit;

                    Bitmap bmp = new Bitmap(toEdit);

                    int width = bmp.Width;
                    int heigth = bmp.Height;

                    for (int y = 0; y < heigth; y++)
                    {

                        for (int x = 0; x < width; x++)
                        {

                            Color p = bmp.GetPixel(x, y);

                            int a = p.A;
                            int r = p.R;
                            int g = p.G;
                            int b = p.B;

                            int avg = ((r + g + b) / 3);

                            bmp.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                        }
                    }

                    pictureBox2.Image = bmp;
                }
                catch (System.Exception excep)
                {


                    MessageBox.Show("Select a valid file", "Image Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
            }

            if (Filters.SelectedItem != "Red" && Filters.SelectedItem != "Blue" && Filters.SelectedItem != "Gray" && Filters.SelectedItem != "Green")
            {

                MessageBox.Show("Select a valid option", "Filter Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

              
        }

        /// <summary>
        /// Button that allows apply the Pixel filter on an image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {


            Image toEdit = (Image)pictureBox1.Image.Clone();
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            int count = Convert.ToInt32(PixelNum.Value);

            Bitmap bmp = new Bitmap(toEdit);
            ApplyNormalPixelate(ref bmp, new Size(count, count));

            pictureBox2.Image = bmp;

        }

        /// <summary>
        /// Menu where you can select the quantity of pixels in te Pixel filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }

}

