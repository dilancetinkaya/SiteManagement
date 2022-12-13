namespace PaymentManagement.API.Configuration
{
    public class MongoDbConfiguration
    {
        public string DbName { get; set; }
        public string ConnectionString { get; set; }
        public string InvoicePaymentCollection { get; set; }
        public string CreditCardInfoCollection { get; set; }
    }
}
