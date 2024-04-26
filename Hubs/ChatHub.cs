using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaddyTrack.Models.Hub;
using Microsoft.AspNetCore.SignalR;

namespace CaddyTrack.Hubs
{
    public class ChatHub : Hub
    {
        // In Front end, just create different buttons to join chatrooms
        // Pass the username of logged in user and name of chat joining with method on the button
        public async Task JoinChat(UserConnection con){
            await Clients.All.SendAsync(method:"RecieveMessage", arg1:"admin", arg2:$"{con.Username} has entered the chat.");
        }

        public async Task JoinSpecificChat(UserConnection con){
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName:con.Chatroom);
            await Clients.Group(con.Chatroom).SendAsync(method:"RecieveMessage", arg1:"admin", arg2:$"{con.Username} has entered the chat.");
        }
        }
    }
