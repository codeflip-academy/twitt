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
        // This method converts an image to an array of bytes.
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
            catch(Exception)
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
        //this method sends a image from the profile table.
        public void SendImageToProfile(HttpContext context)
        {
             System.Data.SqlClient.SqlDataReader reader = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand selectCommand = null;

            try
            {
                 connection = new System.Data.SqlClient.SqlConnection
		        (System.Configuration.ConfigurationManager.ConnectionStrings["Server=.;Database=Twitt;Integrated Security=True"].ConnectionString);
		        
                selectImage = new System.Data.SqlClient.SqlCommand("SELECT Picture from Profile where ID =" + 
		            context.Request.QueryString["ID"], conn);

                connection.Open();

                reader = selectCommand.ExecuteReader();

                while(reader.Read())
                {
                    context.Response.ContentType = "image/jpg";
                    context.Response.BinaryReader((byte[])reader["Picture"]);
                }

                if(reader != null)
                {
                    reader.Close();
                }

            }
            finally
            {
                if(connection != null)
                {
                    connection.Close();
                }
            }
        }
        // This method sends an image from the post
        public void SendImageToPost(HttpContext context)
        {
             System.Data.SqlClient.SqlDataReader reader = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand selectCommand = null;

            try
            {
                 connection = new System.Data.SqlClient.SqlConnection
		        (System.Configuration.ConfigurationManager.ConnectionStrings["Server=.;Database=Twitt;Integrated Security=True"].ConnectionString);
		        
                selectImage = new System.Data.SqlClient.SqlCommand("SELECT Picture from Post where ID =" + 
		            context.Request.QueryString["ID"], conn);

                connection.Open();

                reader = selectCommand.ExecuteReader();

                while(reader.Read())
                {
                    context.Response.ContentType = "image/jpg";
                    context.Response.BinaryReader((byte[])reader["Picture"]);
                }

                if(reader != null)
                {
                    reader.Close();
                }

            }
            finally
            {
                if(connection != null)
                {
                    connection.Close();
                }
            }
        }


    }
    

}