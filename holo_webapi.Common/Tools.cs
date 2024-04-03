using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace holo_webapi.Common
{
    public class Tools
    {
        /// <summary>
        /// 生成验证码的字符串
        /// </summary>
        /// <param name="length">要生成字符串长度</param>
        /// <returns></returns>
        public static string CreateValidateString() // 静态方法
        {

            //准备一组供验证码展示的数据
            string chars = "abcdefghijklmnopqrstuvwxyz"; // 作为验证码的字符池。
            //创建一个 Random 对象 r，并使用当前时间的毫秒数作为种子，以确保每次生成的验证码都是不同的。
            Random r = new(DateTime.Now.Millisecond); 
            string validateString = ""; //用于存储生成的验证码。
            int length = 4; //表示验证码的长度
            for (int i = 0; i < length; i++)
            {
                validateString += chars[r.Next(chars.Length)];
                /*  通过调用 r.Next(chars.Length)，随机生成一个介于 0（包括）和 chars.Length（不包括）之间的整数，
                    作为索引来从 chars 字符串中获取一个字符，并将其添加到 validateString 中。*/
            }
            return validateString;  // 将生成的验证码字符串返回
        }


        //下面这个静态方法创建了一个画布，并在画布上绘制了验证码字符串，然后把绘制好的图片保存到缓冲区中，并将保存的图片字节数据作为结果返回。
        public static Byte[] CreateValidateCodeBuffer(string validateCode) //静态方法
        {
            //bmp -> 位图 
            //1. 创建画布，设置画布的长宽
            using Bitmap bitmap = new(200, 60);

            //2. 创建画笔，告诉画笔在哪个画布上画画
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);//用白色覆盖画布，并清除画布上所有的内容


            //回家作業
            //设置字体的参数(设置字体的名称，大小，粗细以及斜体)
            Font font = new("微软雅黑", 12, FontStyle.Bold | FontStyle.Italic);
            //通過graphics.MeasureString方法計算字符串的長度
            var size = graphics.MeasureString(validateCode, font);
            //通過長度生成新的畫布
            //1.98 Convert.ToInt32(1.98) = 1
            //向上取整：天花板函数；向下取整：地板函数
            using Bitmap bitmapText = new(Convert.ToInt32(Math.Ceiling(size.Width)), Convert.ToInt32(Math.Ceiling(size.Height)));
            //將文字寫入，生成圖片
            Graphics graphicsText = Graphics.FromImage(bitmapText);

            //將圖片縮放放到原畫布上

            //3. 配置画图参数
            //3.1 设置颜色刷范围参数
            RectangleF rf = new(0, 0, bitmap.Width, bitmap.Height);
            //3.2 设置刷子的颜色(设置为渐变)
            LinearGradientBrush brush = new(rf, Color.Red, Color.DarkBlue, 1.2f, true);

            //4. 将字符串绘制到场景中
            graphicsText.DrawString(validateCode, font, brush, 0, 0);

            graphics.DrawImage(bitmapText, 10, 10, 190, 50);
            //5. 将图片放到缓冲区中
            //5.1 创建一个用于保存图片的缓冲器
            MemoryStream memoryStream = new();
            //5.2 把图片保存到缓冲区
            bitmap.Save(memoryStream, ImageFormat.Jpeg);

            // //6. 这个时候图片已经在缓冲区了，bitmap对象自然就没有用了，卸磨杀驴之
            // bitmap.Dispose();
            return memoryStream.ToArray();
        }
    }
}
