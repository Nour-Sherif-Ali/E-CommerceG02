using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;

namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository, UserManager<ApplicationUser> _userManager, IConfiguration configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));

        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository,mapper));
        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, configuration , mapper));

        public IProductService ProductService => _LazyProductService.Value ;

        public IBasketService BasketService => _LazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;
    }
}
