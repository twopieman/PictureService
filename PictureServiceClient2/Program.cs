using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PictureServiceWebRole;
using System.IO;

namespace PictureServiceClient2
{
    class Program
    {
        static void Main(string[] args)
        {
            //string filePath = @"C:\Users\Public\Pictures\Sample Pictures\desert.jpg";
            string filePath = @"C:\wcftest\test.txt";


            Picture pictureToUpload = new Picture();
            //pictureToUpload.Name = "desert.jpg";
            pictureToUpload.Name = "test.txt";
            pictureToUpload.PictureStream =  File.ReadAllBytes(filePath);

            PictureServiceClient client = new PictureServiceClient();
            client.UploadPicture(pictureToUpload);
        }
    }
}