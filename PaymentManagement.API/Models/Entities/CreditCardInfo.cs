using MongoDB.Bson.Serialization.Attributes;

namespace PaymentManagement.API.Models.Entities
{
    public class CreditCardInfo
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Owner { get; set; }
        public string CardNumber { get; set; }
        public int ValidMonth { get; set; }
        public int ValidYear { get; set; }
        public int Cvv { get; set; }
        public double Balance { get; set; }
    }
}
