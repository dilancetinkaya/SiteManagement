using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaymentManagement.API.Configuration;
using PaymentManagement.API.Models.Dtos;
using PaymentManagement.API.Models.Entities;
using PaymentManagement.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentManagement.API.Services
{
    public class CreditCardInfoService : ICreditCardInfoService
    {
        private IMongoCollection<CreditCardInfo> _creditCardInfoCollection; // Db'set e benziyor
        private MongoDbConfiguration _config;


        public CreditCardInfoService(IOptions<MongoDbConfiguration> config)
        {
            _config = config.Value;
            MongoClient client = new MongoClient(_config.ConnectionString);
            IMongoDatabase db = client.GetDatabase(_config.DbName);
            _creditCardInfoCollection = db.GetCollection<CreditCardInfo>(_config.CreditCardInfoCollection);

        }
        public async Task Add(CreditCardInfo creditCardInfo)
        {
            await _creditCardInfoCollection.InsertOneAsync(creditCardInfo);
        }

        public async Task Delete(string id)
        {
            await _creditCardInfoCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<ICollection<CreditCardInfo>> GetAllAsync()
        {
            return await _creditCardInfoCollection.Find(x => true).ToListAsync();
        }

        public async Task<CreditCardInfo> GetByFilter(CreateInvoicePaymentDto filter)
        {
            return await _creditCardInfoCollection.Find(x => x.CardNumber == filter.CardNumber &&
                                                          x.Owner == filter.Owner &&
                                                          x.Cvv == filter.Cvv &&
                                                          x.ValidMonth == filter.ValidMonth &&
                                                          x.ValidYear == filter.ValidYear).FirstOrDefaultAsync();
        }

        public async Task<CreditCardInfo> GetById(string id)
        {
            return await _creditCardInfoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(string id, CreditCardInfo creditCardInfo)
        {
            await _creditCardInfoCollection.ReplaceOneAsync(x => x.Id == id, creditCardInfo);
        }
    }
}
