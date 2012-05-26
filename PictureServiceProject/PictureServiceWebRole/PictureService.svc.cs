using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace PictureServiceWebRole
{
    
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PictureService : IPictureService
    {
        private CloudStorageAccount storageAccount;
        private CloudBlobClient blobClient;
        private CloudBlobContainer blobContainer;
        private const string ContainerName = "testcontainer";


        public PictureService()
        {
            //Create connection to blob storage
            ConnectToBlobStorage();
        }


        /// <summary>
        /// Connects to the storage account and creates the default container 
        /// </summary>
        private void ConnectToBlobStorage()
        {
            storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            blobClient = storageAccount.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference("testcontainer");
            blobContainer.CreateIfNotExist();
            blobContainer.SetPermissions(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });
        }


        private void TempFunction()
        {
            // This function currently just uploads the blob however 
            // I need to refactor this into a proper function.
            CloudBlob blob = blobContainer.GetBlobReference("TestBlob");

            using (var fileStream = System.IO.File.OpenRead("C:\\Temp\\test.txt"))
            {
                blob.UploadFromStream(fileStream);
            }
        }


        public Picture GetPicture(string name)
        {
            CloudBlob blob = blobContainer.GetBlobReference(name);
            Picture retVal = new Picture();
            retVal.Name = name;
            retVal.PictureStream = blob.DownloadByteArray();
            return retVal;
        }

        public void UploadPicture(Picture picture)
        {
            CloudBlob blob = blobContainer.GetBlobReference(picture.Name);
            blob.UploadByteArray(picture.PictureStream);
        }
    }
}
