using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AccountMgtApi.Models
{
    public class Transactions
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("transfer_id")]
        public string TransferId { get; set; }
        [BsonElement("to_account")]
        public string ToAccount { get; set; }
        [BsonElement("from_account")]
        public string FromAccount { get; set; }
        [BsonElement("amount")]
        public decimal Amount { get; set; }
        [BsonElement("last_updated")]
        public DateTimeOffset LastUpdated { get; set; }
    }
}
