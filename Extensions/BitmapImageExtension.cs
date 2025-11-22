using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TravelPlanning.Extensions
{
    public static class BitmapImageExtension
    {
        public static string ToBase64(this BitmapSource bitmapImage)
        {
            byte[] data;
            var encoder = new PngBitmapEncoder();   // 或 JpegBitmapEncoder 等
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return Convert.ToBase64String(data);
        }

        public static BitmapSource Resize(this BitmapSource source, int width, int height)
        {
            var group = new DrawingGroup();
            group.Children.Add(new ImageDrawing(source, new Rect(0, 0, width, height)));

            var drawingVisual = new DrawingVisual();
            using (var context = drawingVisual.RenderOpen())
            {
                context.DrawDrawing(group);
            }
            var resizedImage = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            resizedImage.Render(drawingVisual);
            return resizedImage;
        }

        public static void SaveJpeg(this BitmapSource bitmap, string path, int quality = 90)
        {
            var encoder = new JpegBitmapEncoder
            {
                QualityLevel = quality
            };
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                encoder.Save(stream);
            }
        }
    }
}
