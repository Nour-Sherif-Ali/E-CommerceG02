using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Services.Abstractions;
using Shared.DTOS.BasketDtos;

namespace Services
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        private readonly IBasketRepository basketRepository = _basketRepository;
        private readonly IMapper mapper = _mapper;

        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket>(basket);
            var CreatedorUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (CreatedorUpdatedBasket is not null)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                throw new Exception("Can Not Update Or Create Basket Now , Try Again Later. ");
            }
        }

      

        public async Task<BasketDto?> GetBasketAsync(string Key)
        {
            var Basket = await _basketRepository.GetBasketAsync(Key);
            if(Basket is not null)
            {
                return _mapper.Map<CustomerBasket, BasketDto>(Basket);
            }
            else
            {
                throw new BasketNotFoundException(Key);
            }
        }
        public async Task<bool> DeleteBasketAsync(string Key) => await _basketRepository.DeleteBasketAsync(Key);


    }
}
