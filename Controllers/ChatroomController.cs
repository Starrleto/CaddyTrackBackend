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
    [Route("ChatroomController")]
    public class ChatroomController
    {
        private readonly ChatroomService _service;

        public ChatroomController(ChatroomService s){
            _service = s;
        }

        [HttpPost]
        [Route("SendMessage/{name}")]

        public bool SendMessage(MessageDTO message, string name){
            return _service.AddMessage(message, name);
        }

        [HttpPost]
        [Route("CreateChatroom/{name}")]

        public bool CreateChatroom(string name){
            return _service.AddChatroom(name);
        }

        [HttpGet]
        [Route("GetMessagesFromChatroom/{name}")]

        public List<MessageModel> GetMessages(string name){
            return _service.GetChatroomMessagesFrom(name);
        }

        [HttpDelete]
        [Route("DeleteChatroom/{name}")]

        public bool Delete(string name){
            return _service.DeleteChatroom(name);
        }
    }
}