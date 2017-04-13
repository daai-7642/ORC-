
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
using ThoughtWorks.QRCode.Codec;

namespace CreateCode
{
    public partial class Form1 : Form
    {

        //程序路径  
        readonly string currentPath = Application.StartupPath + @"\BarCode_Images";
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
        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        /// <summary>  
        /// 生成二维码图片  
        /// </summary>  
        /// <param name="codeNumber">要生成二维码的字符串</param>       
        /// <param name="size">大小尺寸</param>  
        /// <returns>二维码图片</returns>  
        public Bitmap Create_ImgCode(string codeNumber, int size)
        {
            //创建二维码生成类  
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式  
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度  
            qrCodeEncoder.QRCodeScale = size;
            //设置编码版本  
            qrCodeEncoder.QRCodeVersion = 0;
            //设置编码错误纠正  
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //生成二维码图片  
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeNumber);
            return image;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image img= Create_ImgCode("123", 100);
            //文件名称  
            string guid = Guid.NewGuid().ToString().Replace("-", "") + ".png";
            string fileLocalSavePath = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new char[] { '\\' }) + "../../../Img/" +guid;

            img.Save(fileLocalSavePath, System.Drawing.Imaging.ImageFormat.Png);
             
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string fileLocalSavePath = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new char[] { '\\' }) + "../../../Img/";
            string filepath=fileLocalSavePath + "生成的图片" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg"; 

            //QrCodeHelper.CombinImage(Create_ImgCode("123456",200), fileLocalSavePath+"/dest.jpg").Save(filepath);
            QrCodeHelper.AddContent(Create_ImgCode("123456", 200), "123456456123");

            
        }
    }
}
