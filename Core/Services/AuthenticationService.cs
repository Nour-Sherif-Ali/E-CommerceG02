using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared.DTOS.IdentityDto;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration configuration,IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            return User is not null;
        }

        public async Task<UserDto> GetCurrentUser(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email) ?? throw new UserNotFoundException(Email);
            return new UserDto()
            {
                Email = User.Email,
                DisplayName = User.DisplayName,
                Token = await CreateTokenAsync(User)
            };
        }

        public async Task<AddressDto> GetCurrentUserAddress(string Email)
        {
            var user  = await _userManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == Email) ?? throw new UserNotFoundException(Email);

           if(user.Address is not null)
            {
                return _mapper.Map<Address, AddressDto>(user.Address);
            }
            else
            {
                throw new AddressNotFoundException(user.UserName);
            }
        }
        public async Task<AddressDto> UpdateCurrentUserAddress(AddressDto addressDto, string Email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == Email) ?? throw new UserNotFoundException(Email);

            if(user.Address is not null) //update
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
                user.Address.Street = addressDto.Street;
            }
            else
            {
                user.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }

            await _userManager.UpdateAsync(user);
            return _mapper.Map<AddressDto>(user.Address);
        }
        //-------------------------------------------------------------------
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            // Check if Email Exits
            var User = await _userManager.FindByEmailAsync(loginDto.Email); 
            if(User is null)
            {
                throw new UserNotFoundException(loginDto.Email);
            }
            //Check Password is Valid 
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswordValid)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token =await CreateTokenAsync(User)
                };
            }
            else
            {
                throw new UnauthorizedException();
            }
        }

        public async Task<UserDto> Registerasync(RegisterDto registerDto)
        {
            //mapping Register Dto => Application User
            var User = new ApplicationUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };

            // Create User 
            var Result = await _userManager.CreateAsync(User, registerDto.Password);
            if(Result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token =await CreateTokenAsync(User)
                };
            }
            else
            {
                var Errors = Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new(ClaimTypes.Email,user.Email!),
                new(ClaimTypes.Name,user.UserName!),
                new(ClaimTypes.NameIdentifier,user.Id)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach(var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var SecretKey = configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer : configuration["JWTOptions:Issuer"] ,
                audience: configuration["JWTOptions:Audience"],
                claims : Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Creds



                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
