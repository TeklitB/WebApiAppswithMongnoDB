using MongoDB.Bson.Serialization.Attributes;

namespace AccountMgtApi.Models
{
    public class Transfer
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public int Id { get; set; }
        [BsonElement("transfer_id")]
        public string TransferId { get; set; }
        [BsonElement("to_account")]
        public string ToAccount { get; set; }
        [BsonElement("from_account")]
        public string FromAccount { get; set; }
        [BsonElement("amount")]
        public int Amount { get; set; }
        [BsonElement("last_updated")]
        public DateTimeOffset LastUpdated { get; set; }
    }
}
