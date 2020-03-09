using System;
using SixLabors.ImageSharp;
using System.IO;
using SixLabors.ImageSharp.Formats;
using TwittAPI.Models;
using System.Configuration;
using System.Data.SqlClient;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace TwittAPI
{
    public class ImageConverter : object
    {
        public Stream InputStream { get; }
        public Stream OutputStream { get; }
        
        public ImageConverter(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }
        public SqlConnection Connection { get; set; }
        
        // This method converts an image to an array of bytes.
        
        
        public Image DownloadImageFromUrl(string imageUrl)
        {
            Image image = null;
            
            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;
                
                System.Net.WebResponse webResponse = webRequest.GetResponse();
                
                Stream stream = webResponse.GetResponseStream();
                
                IImageFormat format;
                image = Image.Load(stream, out format);
                
                webResponse.Close();
            }
            catch (Exception ex)
            {
                return null;
            }
            
            return image;
        }
        public byte[] ConvertImageToByteArray(Image imageToConvert, IImageEncoder encoder)
        {
            byte[] array;
            
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, encoder);
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
        
        public void StoreImageInProfile(Image image, Profile profile)
        {
            var id = profile.Id;
            
                        
           
            Connection.Open();

            using (var command = Connection.CreateCommand())
            {

                using (var stream = new MemoryStream())
                {
                    var encoder = new JpegEncoder()
                    {
                        Quality = 40,
                    };
                    image.Save(stream, encoder);
                    
                    var pic = ConvertImageToByteArray(image, encoder);
                    
                    command.CommandText = @"Update Profile SET Picture = @pic WHERE ID = @id";
                    
                    command.Parameters.AddWithValue(@"pic", pic);
                    
                    command.Parameters.AddWithValue(@"id", id);
                    
                    command.ExecuteNonQuery();
                    
                }
                
                
                
                
            }

            Connection.Close();

        }
        
    }
    
    
}