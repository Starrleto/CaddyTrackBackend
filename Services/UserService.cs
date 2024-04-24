using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CaddyTrack.Models;
using CaddyTrack.Models.DTO;
using CaddyTrack.Services.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CaddyTrack.Services
{
    public class UserService : ControllerBase
    {
        public DataContext _context;
        public UserService(DataContext context){
            _context = context;
        }

        public bool AddUser(CreateAccountDTO addUser){

            if(!DoesUserExist(addUser.Username)){

                PasswordDTO password = HashPassword(addUser.Password);

                UserModel newUser = new UserModel(addUser.Username, password.Salt, password.Hash, addUser.ProfilePicture);
                _context.Add<UserModel>(newUser);
                return _context.SaveChanges() != 0;
            }

            return false;
        }

        public bool DoesUserExist(string name){
            return _context.UserInfo.Where(user => user.Username == name).Any();
        }

        public UserModel GetUserByUsername(string name){
            return _context.UserInfo.SingleOrDefault(user => user.Username == name);
        }

        public PasswordDTO HashPassword(string pass){

            byte[] saltByte = new byte[64];

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

            crypto.GetNonZeroBytes(saltByte);

            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(pass, saltByte, 10000);

            string salt = Convert.ToBase64String(saltByte);
            string hash = Convert.ToBase64String(rfc.GetBytes(256));

            return new PasswordDTO(salt, hash);
        }

        public bool VerifyUserPassword(string? pass, string? storedSalt, string? storedHash){
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            Rfc2898DeriveBytes b = new Rfc2898DeriveBytes(pass, saltBytes, 10000);
            string newHash = Convert.ToBase64String(b.GetBytes(256));
            return newHash == storedHash;
        }

        public bool DeleteUser(string user){
            UserModel u = GetUserByUsername(user);

            if(u != null){
                _context.Remove<UserModel>(u);
                return _context.SaveChanges() != 0;
            }
            return false;
        }

        public bool UpdateUser(string username, UpdateUserDTO update){

            UserModel user = GetUserByUsername(username);

            if(user != null){
                user.Username = update.Username;
                user.ProfilePicture = update.ProfilePicture;

                PasswordDTO newPassword = HashPassword(update.Password);

                user.Salt = newPassword.Salt;
                user.Hash = newPassword.Hash;

                _context.Update<UserModel>(user);
            }

            return _context.SaveChanges() != 0;
        }

        public IActionResult Login(LoginDTO user){

            IActionResult result = Unauthorized();

            if(DoesUserExist(user.Username)){
                UserModel person = GetUserByUsername(user.Username);

                if(VerifyUserPassword(user.Password, person.Salt, person.Hash)){
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey123"));
                    
                    var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokeOptions = new JwtSecurityToken(
                        
                        issuer:"http://localhost:5195",
                        audience:"http://localhost:5195",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signInCredentials

                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    result = Ok(new { Token = tokenString});
                }
            }

            return result;
        }

        public UserInfoDTO GetUserInfoByName(string name){
            if(DoesUserExist(name)){
                UserModel user = GetUserByUsername(name);
                UserInfoDTO info = new UserInfoDTO(user.ID, user.Username, user.ProfilePicture, user.Trackers);
                return info;
            }
            return null;
        }

        public bool ForgotPassword(string name, string newPassword){

            if(DoesUserExist(name)){
                UserModel user = GetUserByUsername(name);

                PasswordDTO newPass = HashPassword(newPassword);

                user.Salt = newPass.Salt;
                user.Hash = newPass.Hash;

                _context.Update<UserModel>(user);
            }

            return _context.SaveChanges() != 0;
        }
    }
}
