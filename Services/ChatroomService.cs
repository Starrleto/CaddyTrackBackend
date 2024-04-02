using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaddyTrack.Models;
using CaddyTrack.Models.DTO;
using CaddyTrack.Services.Context;

namespace CaddyTrack.Services
{
    public class ChatroomService
    {
        public DataContext _context;

        public ChatroomService(DataContext c){
            _context = c;
        }

        public bool AddMessage(MessageDTO message, string chatroom){

            MessageModel newMessage = new MessageModel(message.Message, message.PublisherName, message.UserID);

            if(DoesChatroomExist(chatroom)){
                ChatroomModel chat = GetChatroomByName(chatroom);
                chat.Messages.Add(newMessage);
                _context.Update<ChatroomModel>(chat);
            }
            return _context.SaveChanges() != 0;

        }

        public ChatroomModel GetChatroomByName(string name){
            return _context.Chatrooms.FirstOrDefault(room => room.ChatroomName == name);
        }

        public List<MessageModel> GetChatroomMessagesFrom(string name){
            if(DoesChatroomExist(name)){
                return GetChatroomByName(name).Messages;
            }
            else
                return null;
        }

        public bool AddChatroom(string name){
            if(!DoesChatroomExist(name)){
                ChatroomModel newChat = new ChatroomModel(name);
                _context.Add<ChatroomModel>(newChat);
            }
            return _context.SaveChanges() != 0;
        }

        public bool DeleteChatroom(string name){

            if(DoesChatroomExist(name)){
                ChatroomModel c = GetChatroomByName(name);
                _context.Remove<ChatroomModel>(c);
                return _context.SaveChanges() != 0;
            }
            return false;
        }        

        public bool DoesChatroomExist(string name){
            return GetChatroomByName(name) != null;
        }
    }
}