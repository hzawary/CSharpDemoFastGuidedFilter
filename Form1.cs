// Implemented test filters by HZ, March-2016
// hashemzawary@gmail.com
// https://www.linkedin.com/in/hashem-zavvari-53b01457
//

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AForge.Imaging.Filters;

namespace WinFormDemoFastGuidedFilter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        readonly Stopwatch _elapsedTime = new Stopwatch();

        private void button2_Click(object sender, EventArgs e)
        {
            _elapsedTime.Restart();

            using (var bmp = AForge.Imaging.Image.Convert8bppTo16bpp((Bitmap)pictureBox1.Image))
            {
                var fastGuidedFilter = new FastGuidedFilter
                {
                    KernelSize = 8,
                    Epsilon = 0.02f,
                    SubSamplingRatio = 0.25f,
                    OverlayImage = (Bitmap)bmp.Clone()
                };

                fastGuidedFilter.ApplyInPlace(bmp);
                fastGuidedFilter.OverlayImage.Dispose();

                pictureBox2.Image?.Dispose();

                pictureBox2.Image = AForge.Imaging.Image.Convert16bppTo8bpp(bmp);
            }

            _elapsedTime.Stop();
            Text = $"{_elapsedTime.Elapsed.Milliseconds} (ms)";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);

            _elapsedTime.Restart();

            using (var bmp = AForge.Imaging.Image.Convert8bppTo16bpp((Bitmap)pictureBox2.Image))
            {
                var fastGuidedFilter = new FastGuidedFilter
                {
                    KernelSize = 16,
                    Epsilon = 0.16f,
                    SubSamplingRatio = 0.25f,
                    OverlayImage = (Bitmap)bmp.Clone()
                };

                var guidedImage = fastGuidedFilter.Apply(bmp);

                var subtracted = new Subtract(guidedImage).Apply(fastGuidedFilter.OverlayImage);
                guidedImage.Dispose();

                //var mul = 100;
                //var Multiply = FastGuidedFilter.GetFilledImage(
                //    subtracted.Width, subtracted.Height,
                //    subtracted.PixelFormat, Color.FromArgb(mul, mul, mul));

                //new Multiply(Multiply).ApplyInPlace(subtracted);
                //Multiply.Dispose();

                new Add(subtracted).ApplyInPlace(fastGuidedFilter.OverlayImage);
                subtracted.Dispose();

                pictureBox2.Image?.Dispose();

                pictureBox2.Image = AForge.Imaging.Image.Convert16bppTo8bpp(fastGuidedFilter.OverlayImage);

                fastGuidedFilter.OverlayImage.Dispose();
            }

            _elapsedTime.Stop();
            Text = $"{_elapsedTime.Elapsed.Milliseconds} (ms)";
        }
    }
}
