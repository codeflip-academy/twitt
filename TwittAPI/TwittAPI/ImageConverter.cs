using System;
using SixLabors.ImageSharp;
using System.IO;
using SixLabors.ImageSharp.Formats;

namespace TwittAPI
{
    public class ImageConverter
    {
        // This method converts an image to an array of bytes.
        public byte[] ConvertImageToByteArray(Image imageToConvert, IImageFormat imageFormat)
        {
            byte[] array;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, imageFormat);
                    array = ms.ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return array;
        }

        public Image ConvertByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                return Image.Load(ms);
            }
        }
        

    }


}