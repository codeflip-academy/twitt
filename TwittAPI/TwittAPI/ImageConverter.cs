using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TwittAPI
{
    public class ImageConverter
    {
            //This method converts an image to an array of bytes.
        public byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert, System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] ret;

            try
            {
                using(MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    ret = ms.ToArray();
                }
            }
            catch(Exeption)
            {
                throw;
            }
            return ret;
        }

        //This method stores an image in the profile
        public void StoreImageInProfile(object sender, EventArgs e)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(flImage.PostedFile.InputStream);
        
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                try
                {
                    connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings[ "Server=.;Database=Twitt;Integrated Security=True"].ConnectionString);
                    conection.Open();

                    System.Data.SqlClient.SqlCommand insertCommand = ("INSERT INTO Profile (Picture) VALUES (1, @pic)", conection);
                    insertCommand.Parameters.Add(@"pic", SqlDBType.Image, 0).Value = ConvertImageToByteArray(image, System.Drawing.Imaging.ImageFormat,Jpeg);


                    int result = insertCommand.ExecuteNonQuery();
                    if(result == 1)
                    {
                         lblRes.Text = "msg record Created Successfully";
                    }
                }
                catch(Exeption ex)
                {
                     lblRes.Text = "Error: " + ex.Message;
                }

            }

            finally
            {
                if(connection != null)
                {
                    Connection.Close();
                }
            }

        }

        //This method stores an image in a post
        public void StoreImageInPost(object sender, EventArgs e)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(flImage.PostedFile.InputStream);
        
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                try
                {
                    connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings[ "Server=.;Database=Twitt;Integrated Security=True"].ConnectionString);
                    conection.Open();

                    System.Data.SqlClient.SqlCommand insertCommand = ("INSERT INTO Post (Picture) VALUES (1, @pic)", conection);
                    insertCommand.Parameters.Add(@"pic", SqlDBType.Image, 0).Value = ConvertImageToByteArray(image, System.Drawing.Imaging.ImageFormat,Jpeg);


                    int result = insertCommand.ExecuteNonQuery();
                    if(result == 1)
                    {
                         lblRes.Text = "msg record Created Successfully";
                    }
                }
                catch(Exeption ex)
                {
                     lblRes.Text = "Error: " + ex.Message;
                }

            }

            finally
            {
                if(connection != null)
                {
                    Connection.Close();
                }
            }
        }
    }
    

}