using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.DTO;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly WebApiDbContext _context;
        readonly IMapper mapper = new Mapper(new MapperConfiguration(x => x.AddProfile<MappingProfile>()));

        public UserService(IOptions<AppSettings> appSettings, WebApiDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        private byte[] CreatePasswordHash(string password)
        {
            var hmac = new HMACSHA512(Encoding.ASCII.GetBytes(_appSettings.Salt));
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return passwordHash;
        }

        public AuthenticateToken Authenticate(AuthenticateModel model)
        {
            var authToken = new AuthenticateToken();
            var passwordHash = CreatePasswordHash(model.Password);
            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == passwordHash);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                // token expires in a day
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            authToken.Token = tokenHandler.WriteToken(token);
            authToken.ValidFrom = token.ValidFrom;
            authToken.ValidTo = token.ValidTo;

            return authToken;
        }

        public IEnumerable<UserDto> GetAll()
        {
            // return null if no user registered
            if (_context.Users.FirstOrDefault() == null)
                return null;

            return mapper.Map<IEnumerable<UserDto>>(_context.Users);
        }

        public UserDto GetUser(long id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            // return null if user not found
            if (user == null)
                return null;

            return mapper.Map<User, UserDto>(user);
        }

        public UserDto CreateUser(CreateUserDto user)
        {
            // check registered email
            var userInDb = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            if (userInDb != null)
                return null;

            // map encrypted password
            var mapUser = mapper.Map<User>(user);
            mapUser.Password = CreatePasswordHash(user.Password);

            _context.Users.Add(mapUser);
            _context.SaveChanges();

            return mapper.Map<UserDto>(mapUser);
        }

        public UserDto UpdateUser(long id, EditUserDto editUserDto)
        {
            var userInDb = _context.Users.FirstOrDefault(x => x.Id == id);

            // return null if user not found
            if (userInDb == null)
                return null;

            mapper.Map(editUserDto, userInDb);
            _context.SaveChanges();

            return mapper.Map<UserDto>(userInDb);
        }

        public bool DeleteUser(long id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            // return null if user not found
            if (user == null)
                return false;

            _context.Users.Remove(user);
            _context.SaveChanges();

            return true;
        }
    }

    public interface IUserService
    {
        AuthenticateToken Authenticate(AuthenticateModel model);
        IEnumerable<UserDto> GetAll();
        UserDto GetUser(long id);
        UserDto CreateUser(CreateUserDto user);
        bool DeleteUser(long id);
        UserDto UpdateUser(long id, EditUserDto editUserDto);
    }
}