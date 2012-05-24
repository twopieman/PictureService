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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PictureService : IPictureService
    {
        public PictureService()
        {
            //First create the Container for this service
            CloudStorageAccount account = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            CloudBlobClient client = account.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference("testcontainer");
            container.CreateIfNotExist();
            container.SetPermissions(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });

            //now create the Blob   
            CloudBlob blob = container.GetBlobReference("TestBlob");

            using (var fileStream = System.IO.File.OpenRead("C:\\Temp\\test.txt")) 
            {
                blob.UploadFromStream(fileStream);
            }

        }



        public Picture GetPicture(string name)
        {
            throw new NotImplementedException();
        }

        public void UploadPicture(Picture picture)
        {
            throw new NotImplementedException();
        }
    }
}
