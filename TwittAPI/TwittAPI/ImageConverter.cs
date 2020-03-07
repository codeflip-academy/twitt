using System;
using SixLabors.ImageSharp;
using System.IO;
using SixLabors.ImageSharp.Formats;
using TwittAPI.Models;
using System.Configuration;

namespace TwittAPI
{
    public class ImageConverter
    {
        public Stream InputStream { get; }
        public Stream OutputStream { get; }
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

        public void StoreImageInProfile(Profile profile)
        {
            var id = profile.Id;
            
            System.Data.SqlClient.SqlConnection connection = null;
            connection = new System.Data.SqlClient.SqlConnection(
                ConfigurationManager.ConnectionStrings["Server=.;Database=Twitt;Integrated Security=True"].ConnectionString);
            connection.Open();
            
            using (var command = connection.CreateCommand())
            {
                IImageFormat format;
                using (var image = Image.Load(InputStream, out format))
                {
                    image.Save(OutputStream, format);
                    
                    var pic = ConvertImageToByteArray(image, format);
                    
                    command.CommandText = @"INSERT INTO Profile (ID, Picture) VAlUES (@id, @pic)";
                    
                    command.Parameters.AddWithValue(@"pic", pic);
                    
                    command.Parameters.AddWithValue(@"id", id);
                    
                }
                
            }
            
            connection.Close();
            
        }

    }


}