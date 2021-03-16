using System;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Imaging;
using QRCoder;

namespace QrcodeHelper
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // args = new string[2] ;
            //
            //  args[0] = "www.shersoft.vindians.in\n rs 1000 \n created by hasif,hasif,sanju,nabeel";
            //  args[1] = @"D:\sheracc\pic\123";
            if (args == null)
            {
                Console.WriteLine("Please enter proper input like qrcodenumber,and path where you want to put the qr");
            }
            else
            {
                try
                {
                    Console.WriteLine(args[0]);
                    String qrbarcode = args[0];
                    String path = @"D:\sheracc\pic\123";
                    try
                    {
                        path = args[1];
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    var generateQr = new GenerateQr();
                    // generateQr.saveQrIcon(qrbarcode, path);
                    // generateQr.saveQrImage(qrbarcode, path);
                    generateQr.saveQrIcon24bit(qrbarcode, path);
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Please enter proper input like qrcodenumber,and path where you want to put the qr");
                    Console.WriteLine(e);
                }
            }
        }
    }

    class GenerateQr
    {
        public Bitmap getQrBitmap(String qrnumber)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData =
                qrGenerator.CreateQrCode(qrnumber, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(5, Color.Black, Color.White, true);
            return qrCodeImage;
        }

        public Boolean saveQrImage(String qrnumber, String path = @"D:\sheracc\pic\123")
        {
            foreach (var s in qrnumber.Split(Convert.ToChar(",")))
            {
                getQrBitmap(s).Save(path + @"\" + s.Replace("\n", "") + ".png");
            }

            return true;
        }

        public void saveQrIcon(String qrnumber, String path = @"D:\sheracc\pic\123")
        {
            foreach (var s in qrnumber.Split(Convert.ToChar(",")))
            {
                Icon ico;
                using (Bitmap Cbitmap = getQrBitmap(s))
                {
                    Cbitmap.MakeTransparent(Color.White);
                    System.IntPtr icH = Cbitmap.GetHicon();
                    ico = Icon.FromHandle(icH);
                }

                var da = path + @"\" + s + ".ico";
                using (System.IO.FileStream f = new System.IO.FileStream(da, System.IO.FileMode.OpenOrCreate))
                {
                    ico.Save(f);
                }

                getQrBitmap(s).Save(path + @"\" + s.Replace("\n", "") + ".png");
            }
        }

        public Bitmap ConvertTo24bpp(Image img)
        {
            var bmp = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
            return bmp;
        }

        public void saveQrIcon24bit(String qrnumber, String path = @"D:\sheracc\pic\123")
        {
            foreach (var s in qrnumber.Split(Convert.ToChar(",")))
            {
                Icon ico;
                using (Bitmap Cbitmap = getQrBitmap(s))
                {
                    var convertTo24Bpp = ConvertTo24bpp(Cbitmap);
                    convertTo24Bpp.Save(path + @"\" + s.Replace("\n", "") + ".png");
                }
            }
        }
    }
}