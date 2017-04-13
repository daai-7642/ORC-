
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void CreateCode()
        {
            string helloWorld = "123";
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(helloWorld, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            string fileLocalSavePath = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new char[] { '\\' }) + "../../../Img/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            //Renderer renderer = new Renderer(5, Brushes.Black, Brushes.White);
            using (FileStream stream = new FileStream(fileLocalSavePath, FileMode.Create))
            {
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateCode();
        }
    }
}
