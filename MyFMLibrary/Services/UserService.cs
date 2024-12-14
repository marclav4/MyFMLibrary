using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyFMLibrary.Areas.Identity.Data;
using MyFMLibrary.DTOs;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyFMLibrary.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration) 
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task Register(RegisterRequestDTO request)
        {
            User user = new User();
            user.UserName = request.UserName;
            user.Email = request.Email;
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new Exception($"Error: {result.Errors.FirstOrDefault().Description}");
        }

        public async Task<LoginResultDTO> Login(LoginRequestDTO request)
        {
            User user = await _userManager.FindByNameAsync(request.UserName);

            if (await _userManager.CheckPasswordAsync(user, request.Password))
            {
                LoginResultDTO loginResult = new LoginResultDTO();
                loginResult.UserName = user.UserName;
                loginResult.Token = await GenerateToken(user);
                loginResult.TokenMinutes = Convert.ToInt32(_configuration.GetSection("Jwt:ExpireInMinutes").Value);
                return loginResult;
            }

            throw new Exception("Invalid credentials.");
        }

        internal async Task<string> GenerateToken(User user)
        {
            DateTime now = DateTime.Now;
            byte[] secret = Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt:Key").Value);
            double minutes = Convert.ToDouble(_configuration.GetSection("Jwt:ExpireInMinutes").Value);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            /* Omitted in this case
             * IList<string> roles = await _userManager.GetRolesAsync(user);

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }*/

            JwtSecurityToken jwtToken = new JwtSecurityToken(null, null, claims, expires: now.AddMinutes(minutes), 
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), 
                SecurityAlgorithms.HmacSha256Signature));

            string accessTokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return accessTokenString;
        }
    }
}
