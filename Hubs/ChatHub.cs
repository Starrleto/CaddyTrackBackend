using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaddyTrack.Models.Hub;
using CaddyTrack.Services.Context;
using Microsoft.AspNetCore.SignalR;

namespace CaddyTrack.Hubs
{
    public class ChatHub : Hub
    {

        private readonly SharedDB _shared;

        public ChatHub(SharedDB db){
            _shared = db;
        }

        // In Front end, just create different buttons to join chatrooms
        // Pass the username of logged in user and name of chat joining with method on the button
        public async Task JoinChat(UserConnection con){
            await Clients.All.SendAsync(method:"ReceiveMessage", arg1:"admin", arg2:$"{con.Username} has entered the chat.");
        }

        public async Task JoinSpecificChat(UserConnection con){
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName:con.Chatroom);

            _shared.connections[Context.ConnectionId] = con;

            await Clients.Group(con.Chatroom).SendAsync(method:"ReceiveMessage", arg1:"admin", arg2:$"{con.Username} has entered the chat.");
        }

        public async Task SendMessage(string msg){
            if(_shared.connections.TryGetValue(Context.ConnectionId, out UserConnection conn)){
                await Clients.Group(conn.Chatroom).SendAsync(method:"ReceiveSpecificMessage", arg1:conn.Username, arg2:msg);
            }
        }

    }
}
