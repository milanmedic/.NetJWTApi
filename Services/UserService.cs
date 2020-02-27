using System.Collections.Generic;
using System;
using JwtApi.Domain.Models;
using System.Threading.Tasks;
using JwtApi.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtApi.Domain.Repositories;
using System.Text;
using Microsoft.Extensions.Options;

namespace JwtApi.Services {
    public class UserService : IUserService {
        private readonly IUserRepository _userRepository;
        private readonly ISecurity _securityService;
        private readonly AppSettings _appSettings;
        public UserService(IUserRepository userRepository, ISecurity securityService, IOptions<AppSettings> appSettings) {
            _userRepository = userRepository;
            _securityService = securityService;
            _appSettings = appSettings.Value;
        }
        public async Task<User> Authenticate(string email, string password){

            var user = await _userRepository.FindUser(email);
            if(user == null){
                return null;
            }

            if(!_securityService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)){
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            return user;
        }
        public async Task<User> RegisterUserAsync(User user, string password){
            if(string.IsNullOrWhiteSpace(password)){
                throw new AppException("Password is required");
            }
            var existingUser = await _userRepository.FindUser(user.Email);
            if(existingUser != null){
                throw new AppException("User already exists!");
            }

            byte[] passwordHash, passwordSalt;
            _securityService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userRepository.CreateUser(user);
            return user;
        }
        public async Task<User> GetUserAsync(int id){
            return await _userRepository.GetById(id);
        }
        public async Task<User> GetUserByEmailAsync(string email){
            return await _userRepository.GetByEmail(email);
        }

    }
}