using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using StackExchange.Redis;

namespace Persistance.Repositories
{
    public class BasketRepository(IConnectionMultiplexer conncetion) : IBasketRepository
    {
        private readonly IDatabase _database = conncetion.GetDatabase();

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var ISCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(1)); //the basket expires after 1 day 
            if(ISCreatedOrUpdated)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                return null;
            }
        }

       

        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
            var Basket = await _database.StringGetAsync(key);
            if(Basket.IsNullOrEmpty)
                return null;
            else
            {
                return JsonSerializer.Deserialize<CustomerBasket>(Basket);  
            }
        }

        public async Task<bool> DeleteBasketAsync(string Id) => await _database.KeyDeleteAsync(Id);

    }
}
