using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UAI.Case.Application;

namespace UAI.Case.Webapi.hubs
{

    public class IsTypingDTO
    {
        public String RoomName {get;set;}
    public  String Usuario { get; set; }
}

    [HubName("MessageHub")]
    public class MessageHub : BaseHub
    {
        
        public void JoinRoom(string roomName)
        {
          
            AddToRoom(roomName);
            Groups.Add(Context.ConnectionId, roomName);
            GetAllConnectedInRoom(roomName);
            //return GetAllInRoom(roomName);


        }

       


        public void ReleaseElement(HoldDTO dto)
        {

            if (dto != null)
            {
                Release(dto);
                Clients.Group(dto.RoomId.ToString()).ReleaseElement(dto);

            }
        }
         public IList GetAllHoldsInRoom(Guid roomId)
        {
            return GetAllHolds(roomId);
                  //  Clients.Group(roomId.ToString()).AllHolds(GetAllHolds(roomId));

        }
        public void HoldElement(HoldDTO dto)
        {

            if (dto != null)
            {
                Hold(dto);
                Clients.Group(dto.RoomId.ToString()).HoldElement(dto);
                //Clients.OthersInGroup() probar
            }

        }

        public void IsTyping(string dto)
        {

            if (dto != null)
            {

                
                string[] d = dto.Split('%');

                if (d.Length == 2)
                {
                    string roomName = d[0];
                    string user = d[1];
                    Clients.Group(roomName).SayWhoIsTyping(dto);
                }
            }

        }

        public Task LeaveRoom(string roomName)
        {
            RemoveFromRoom(roomName);
            return Groups.Remove(Context.ConnectionId, roomName);
            
        }


    }
}
