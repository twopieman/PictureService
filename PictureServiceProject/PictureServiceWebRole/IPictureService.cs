using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PictureServiceWebRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPictureService
    {

        [OperationContract]
        Picture GetPicture(string name);

        [OperationContract]
        void UploadPicture(Picture picture);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Picture
    {
        [DataMember]
        public byte[] PictureStream { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<string> Tags { get; set; }
    }
}
