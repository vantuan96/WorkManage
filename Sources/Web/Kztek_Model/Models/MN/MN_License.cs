using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace Kztek_Model.Models.MN
{
    public class MN_License
    {
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ProjectName { get; set; }

        public bool IsExpire { get; set; }

        public string ExpireDate { get; set; }
    }
    public class MN_LicenseCustom
    {
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ProjectName { get; set; }

        public bool IsExpire { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}
