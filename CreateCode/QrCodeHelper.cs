using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCode
{
    public static class QrCodeHelper
    {
        /// <summary>    
        /// 调用此函数后使此两种图片合并，类似相册，有个    
        /// 背景图，中间贴自己的目标图片    
        /// </summary>    
        /// <param name="imgBack">粘贴的源图片</param>    
        /// <param name="destImg">粘贴的目标图片</param>    
        public static Image CombinImage(Image imgBack, string destImg)
        {
            Image img = Image.FromFile(destImg);        //照片图片  
            if (img.Height != 65 || img.Width != 65)
            {
                img = KiResizeImage(img, 65, 65, 0);
            }
            Graphics g = Graphics.FromImage(img);
            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);
          
            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);     

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框    

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);    

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }
        public static Bitmap AddContent(Image imgBack, string content)
        {
            int xwidth = 1000;//左右
            int yheigth = 700;//500正好
            int newwidth = imgBack.Width + xwidth;
            int newheigth = imgBack.Height + yheigth;
            Bitmap img1 = new Bitmap(newwidth,newheigth );

            Graphics g = Graphics.FromImage(img1);
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, newwidth, newheigth));
            g.DrawImage(imgBack, xwidth/2, yheigth/3, imgBack.Width, imgBack.Height);
            g.DrawString(content, new Font("Arial", 360), new SolidBrush(Color.Black), xwidth / 2, imgBack.Height+ yheigth / 3);
            string fileLocalSavePath = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new char[] { '\\' }) + "../../../Img/";
            string filepath = fileLocalSavePath + "生成的图片111" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";

             
            g.FillRectangle(System.Drawing.Brushes.Black,500,500, 1000, 1000);//相片四周刷一层黑色边框    
             img1.Save(filepath);
            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);    

            GC.Collect();
            return img1;
        }

        /// <summary>    
        /// Resize图片    
        /// </summary>    
        /// <param name="bmp">原始Bitmap</param>    
        /// <param name="newW">新的宽度</param>    
        /// <param name="newH">新的高度</param>    
        /// <param name="Mode">保留着，暂时未用</param>    
        /// <returns>处理以后的图片</returns>    
        public static Image KiResizeImage(Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量    
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }


    }
}
