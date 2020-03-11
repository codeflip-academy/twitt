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
    public class ImageHandler : object
    {
        private readonly string _connectionString;
        public ImageHandler(string connectionString)
        {
            _connectionString = connectionString;
        }

        public byte[] ConvertStringToByteArray(string image)
        {
            byte[] imageBytes = Convert.FromBase64String(image);

            return imageBytes;
        }

        public void StoreImageProfile(ProfileModels profile)
        {
            var id = profile.Id;
            var s = profile.Picture;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"Update Profile SET Picture = @pic WHERE ID = @id";

                    var pic = ConvertStringToByteArray(s);

                    command.Parameters.AddWithValue(@"pic", pic);
                    command.Parameters.AddWithValue(@"id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void StoreImagePost(TwittModels post)
        {
            var id = post.Id;
            var s = post.Picture;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"Update Post SET Picture = @pic WHERE ID = @id";

                    var pic = ConvertStringToByteArray(s);

                    command.Parameters.AddWithValue(@"pic", pic);
                    command.Parameters.AddWithValue(@"id", id);

                    command.ExecuteNonQuery();
                }
            }

        }

        public Image ConvertByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                var image = Image.Load(ms);

                return image;
            }
        }

        public void GetImageFromProfile(Profile profile)
        {
            var id = profile.Id;
            byte[] byteArray;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT Picture FROM Profile WHERE ID = @id";
                    command.Parameters.AddWithValue("@id", id);
                    byteArray = (byte[])command.ExecuteScalar();
                    ConvertByteArrayToImage(byteArray);
                }
            }
        }

        public void GetImageFromPost(Twitt post)
        {
            var id = post.Id;
            byte[] byteArray;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT Picture FROM Post WHERE ID = @id";
                    command.Parameters.AddWithValue("@id", id);
                    byteArray = (byte[])command.ExecuteScalar();
                    ConvertByteArrayToImage(byteArray);
                }
            }
        }
    }
}
