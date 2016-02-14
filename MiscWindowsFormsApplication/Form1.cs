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

namespace MiscWindowsFormsApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Dock the PictureBox to the form and set its background to white.
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.BackColor = Color.White;
            // Connect the Paint event of the PictureBox to the event handler method.
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);

            // Add the PictureBox control to the Form. 
             this.Controls.Add(pictureBox1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;

            // Draw a string on the PictureBox.
            //g.DrawString("This is a diagonal line drawn on the control",new Font("Arial", 10), System.Drawing.Brushes.Blue, new Point(30, 30));
            // Draw a line in the PictureBox.
            //g.DrawLine(System.Drawing.Pens.Red, pictureBox1.Left, pictureBox1.Top,
            //    pictureBox1.Right, pictureBox1.Bottom);

            //DrawAJpegImage(e);

            //DrawBitMapImage(e);

            DrawBitMapImage1(e);
        }

        private void DrawAJpegImage(PaintEventArgs e)
        {
            // Create image.
            Image newImage = Image.FromFile(@"C:\Hariom\RADAPREMIERE, JASON TEST.Se1.Img1.jpg");

            // Create coordinates for upper-left corner. 

            // of image and for size of image. 
            int x = 100;
            int y = 100;
            int width = 450;
            int height = 150;

            // Draw image to screen.
            e.Graphics.DrawImage(newImage, x, y, width, height);
        }
        private void DrawBitMapImage(PaintEventArgs e)
        {
            try
            {
                
            // create a bitmap file
            var imageBytes = File.ReadAllBytes(@"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\WebAPISampleProject\DataFiles\radaLosslessFromServer");
            var image = Image.FromStream(new MemoryStream(imageBytes));
            //var image = Image.FromFile(@"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\WebAPISampleProject\DataFiles\radaLosslessFromServer");
            Bitmap bitmap = new Bitmap(image);
            e.Graphics.DrawImage(bitmap,0, 0);

            //var image1 = new Bitmap(@"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\WebAPISampleProject\DataFiles\radaLosslessFromServer", true);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void DrawBitMapImage1(PaintEventArgs e)
        {
            try
            {
                var image1 = new Bitmap(@"C:\Hariom\Personal\DotNetExperiments\TeamWorkManagement\WebAPISampleProject\DataFiles\radaLosslessFromServer", false);

                int x, y;

                // Loop through the images pixels to reset color. 
                for (x = 0; x < image1.Width; x++)
                {
                    for (y = 0; y < image1.Height; y++)
                    {
                        Color pixelColor = image1.GetPixel(x, y);
                        Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                        image1.SetPixel(x, y, newColor);
                    }
                }

                // Set the PictureBox to display the image.
                pictureBox1.Image = image1;

                // Display the pixel format in Label1.
                //Label1.Text = "Pixel format: " + image1.PixelFormat.ToString();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
