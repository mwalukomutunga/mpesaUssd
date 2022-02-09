using BimaPimaUssd.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace BimaPimaUssd.Models
{
    public class Reg_User
    {
        public bool IsLogged { get; set; }
        public string Token { get; set; }

    }

    public class Activation
    {
        public Activation(string exten)
        {
            DateActivated = DateTime.Now;
            Extension = exten;
            PaymentAmount =0;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public string Extension { get; set; }
        public decimal PaymentAmount { get; set; }

        public string PhoneNumber { get; set; }
        public string VoucherCode { get; set; }
        public string ValueChain { get; set; }
        public string SerialNumber { get; set; }
        public string Denomination { get; set; }
        [JsonPropertyName("Project name")]
        public string Projectname { get; set; }
        public string ProductName { get; set; }
        public DateTime DateActivated { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }

    public class CardsSerial
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        [JsonPropertyName("Value Chain")]
        public string ValueChain { get; set; }
        [JsonPropertyName("Voucher Code")]
        public string VoucherCode { get; set; }
        [JsonPropertyName("Serial Number")]
        public string SerialNumber { get; set; }
        [JsonPropertyName("Denomination")]
        public string Denomination { get; set; }
        [JsonPropertyName("Project name")]
        public string Projectname { get; set; }
        [JsonPropertyName("Product Name")]
        public string ProductName { get; set; }
     
    }

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public string Msisdn { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PlantingMonth { get; set; }
        public string PlantingWeek { get; set; }
        //public DateTime ComputedPlantingDate 
        //{
        //    get
        //    {
        //        return new DateTime(DateOfQuery.Year, Convert.ToInt32(PlantingMonth),AppConstant.GetLastDayOfWeek(Convert.ToInt32(PlantingMonth), Convert.ToInt32(PlantingWeek)));
        //    }
        //}

        public string NearestPrimarySchool { get; set; }
        public DateTime DateOfQuery { get; set; }



    }

}
