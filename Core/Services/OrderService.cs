using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Microsoft.Identity.Client;
using Services.Abstractions;
using Services.Specifications;
using Shared.DTOS.OrderDtos;

namespace Services
{
    public class OrderService(IBasketRepository _basketRepository, IMapper _mapper ,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string Email)
        {
            //Map Address To OrderAddress
            var OrderAddress = _mapper.Map<ShippingAddressDto, ShippingAddress>(orderDto.Address);
            //Get Basket 
            var Basket = await _basketRepository.GetBasketAsync(orderDto.BasketId) ?? throw new BasketNotFoundException(orderDto.BasketId);
            //Create OrderItem List 
            List<OrderItem> OrderItems = [];
            var ProductRepo = _unitOfWork.GetReposityory<Product, int>();
            foreach(var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);

                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrdered()
                    {
                        ProductId = Product.Id,
                        ProductName = Product.Name,
                        PictureUrl = Product.PictureUrl
                    },

                    Price = Product.Price,
                    Quantity = item.Quantity
                };

                OrderItems.Add(orderItem);
            }
            //Get DeliveryMethod 
            var DeliveryMethod =await _unitOfWork.GetReposityory<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);
            //Caluclate Subtotal    
            var SubTotal = OrderItems.Sum(I => I.Quantity * I.Price);

            //Create Order
            var Order = new Order(Email, OrderAddress, DeliveryMethod, OrderItems, SubTotal);

            //Add Order 
            await _unitOfWork.GetReposityory<Order, Guid>().AddAsync(Order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order, OrderToReturnDto>(Order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods()
        {
            var DeliveryMethods = await _unitOfWork.GetReposityory<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(DeliveryMethods);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email)
        {
            var Spec = new OrderSpecifications(Email);
           var Orders = await  _unitOfWork.GetReposityory<Order,Guid>().GetAllAsync(Spec);
           return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(Orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id)
        {
            var spec = new OrderSpecifications(Id);
            var Order = await _unitOfWork.GetReposityory<Order,Guid>().GetByIdAsync(spec);
            return _mapper.Map<OrderToReturnDto>(Order);
        }
    }
}
