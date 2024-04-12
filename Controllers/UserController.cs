using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaddyTrack.Models;
using CaddyTrack.Models.DTO;
using CaddyTrack.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaddyTrack.Controllers
{

    [ApiController]
    [Route("UserController")]
    public class UserController
    {
        private readonly UserService _data;

        public UserController(UserService d){
            _data = d;
        } 

        [HttpPost]
        [Route("AddUser")]
        public bool AddUser(CreateAccountDTO addUser){
            return _data.AddUser(addUser);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDTO login){
            return _data.Login(login);
        }

        [HttpPut]
        [Route("UpdateUser/{name}")]
        public bool UpdateUser(string name, UpdateUserDTO update){
            return _data.UpdateUser(name, update);
        }

        [HttpPut]
        [Route("UpdateUserPassword/{name}/{newPassword}")]
        public bool ForgotPassword(string name, string newPassword){
            return _data.ForgotPassword(name, newPassword);
        }

        [HttpGet]
        [Route("GetUserInfoByName/{name}")]
        public UserInfoDTO GetUserInfoByName(string name){
            return _data.GetUserInfoByName(name);
        }

        [HttpDelete]
        [Route("RemoveUser/{name}")]
        public bool DeleteUser(string name){
            return _data.DeleteUser(name);
        }
    }
}