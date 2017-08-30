using com.google.zxing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspSmartDev.SDK
{
    /// <summary>
    /// 二维码生成与识别
    /// </summary>
    public class QrcodeHelper
    {
        public static Bitmap Encode(string content, int width = 350, int height = 350)
        {
            com.google.zxing.common.ByteMatrix byteMatrix = new MultiFormatWriter().encode(content, BarcodeFormat.QR_CODE, width, height);
            int matrixWidth = byteMatrix.Width;
            int matrixHeight = byteMatrix.Height;
            Bitmap bmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bmap.SetPixel(x, y, byteMatrix.get_Renamed(x, y) != -1 ? ColorTranslator.FromHtml("0xFF000000") : ColorTranslator.FromHtml("0xFFFFFFFF"));
                }
            }
            return bmap;
        }

        public static string Decode(string path)
        {
            Image img = Image.FromFile(path);
            Bitmap bmap = new Bitmap(img);
            LuminanceSource source = new RGBLuminanceSource(bmap, bmap.Width, bmap.Height);
            com.google.zxing.BinaryBitmap bitmap = new com.google.zxing.BinaryBitmap(new com.google.zxing.common.HybridBinarizer(source));
            return new MultiFormatReader().decode(bitmap).Text;

        }
    }
}
