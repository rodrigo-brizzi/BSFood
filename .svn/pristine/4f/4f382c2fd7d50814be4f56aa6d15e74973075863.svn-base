using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BSFoodPDV.Apoio
{
    public static class Util
    {

        #region Propriedades e métodos para uso geral
       
        public static void FecharSistema()
        {
            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        public static MessageBoxImage GetMessageImage(int intCodigo)
        {
            string strNomeEnumIcone = Enum.GetName(typeof(MessageBoxImage), intCodigo);
            return (MessageBoxImage)Enum.Parse(typeof(MessageBoxImage), strNomeEnumIcone, true);
        }


        #endregion Geral



        #region Imagens

        public static byte[] ConvertBitmapSourceToByteArray(BitmapEncoder encoder, ImageSource imageSource)
        {
            byte[] bytes = null;
            var bitmapSource = imageSource as BitmapSource;

            if (bitmapSource != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }
            }
            return bytes;
        }

        public static byte[] ConvertBitmapSourceToByteArray(BitmapSource image)
        {
            byte[] data;
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public static byte[] ConvertBitmapSourceToByteArray(ImageSource imageSource)
        {
            var image = imageSource as BitmapSource;
            byte[] data;
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public static byte[] ConvertBitmapSourceToByteArray(Uri uri)
        {
            var image = new BitmapImage(uri);
            byte[] data;
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public static byte[] ConvertBitmapSourceToByteArray(string filepath)
        {
            var image = new BitmapImage(new Uri(filepath));
            byte[] data;
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public static BitmapImage ConvertByteArrayToBitmapImage(Byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }

        public static byte[] ResizeMax50Kbytes(byte[] byteImageIn)
        {
            byte[] currentByteImageArray = byteImageIn;
            double scale = 1f;

            MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
            Image fullsizeImage = Image.FromStream(inputMemoryStream);

            while (currentByteImageArray.Length > 50000)
            {
                Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new System.Drawing.Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));
                MemoryStream resultStream = new MemoryStream();

                fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                currentByteImageArray = resultStream.ToArray();
                resultStream.Dispose();
                resultStream.Close();

                scale -= 0.05f;
            }

            return currentByteImageArray;
        }

        public static byte[] ResizeThumb(string strCaminhoArquivo)
        {
            string strTipoImagem = Path.GetExtension(strCaminhoArquivo);
            Image imgOriginal = Image.FromFile(strCaminhoArquivo);
            Image imgNova = imgOriginal.GetThumbnailImage(100, 100, null, IntPtr.Zero);
            byte[] data;
            using (MemoryStream ms = new MemoryStream())
            {
                if (strTipoImagem.ToLower() == ".jpg" || strTipoImagem.ToLower() == ".jpeg")
                    imgNova.Save(ms, ImageFormat.Jpeg);
                if (strTipoImagem.ToLower() == ".png")
                    imgNova.Save(ms, ImageFormat.Png);
                data = ms.ToArray();
            }
            return data;
        }

        public static byte[] ResizeScala(string strCaminhoArquivo, int intScala)
        {
            string strTipoImagem = Path.GetExtension(strCaminhoArquivo);
            Image imgOriginal = Image.FromFile(strCaminhoArquivo);

            int originalWidth = imgOriginal.Width;
            int originalHeight = imgOriginal.Height;
            float percentWidth = (float)intScala / (float)originalWidth;
            float percentHeight = (float)intScala / (float)originalHeight;
            float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
            int newWidth = (int)(originalWidth * percent);
            int newHeight = (int)(originalHeight * percent);

            Image imgNova = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(imgNova))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgOriginal, 0, 0, newWidth, newHeight);
            }

            byte[] data;
            using (MemoryStream ms = new MemoryStream())
            {
                if (strTipoImagem.ToLower() == ".jpg" || strTipoImagem.ToLower() == ".jpeg")
                    imgNova.Save(ms, ImageFormat.Jpeg);
                if (strTipoImagem.ToLower() == ".png")
                    imgNova.Save(ms, ImageFormat.Png);
                data = ms.ToArray();
            }
            return data;
        }

        #endregion Imagens

    }
}
