using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Common;

namespace UAI.Case.Webapi.hubs
{
    public abstract class BaseHub : Hub
    {

        private readonly static UaiCaseElementHolder _holders = new UaiCaseElementHolder();
        private readonly static ConnectionMapping<string> _connections =
         new ConnectionMapping<string>();


        protected IList GetAllHolds(Guid roomId)
        {
            return _holders.GetAllHolds(roomId);
        }
        protected void Hold(HoldDTO dto)
        {
            _holders.Hold(dto);
        }

        protected void Release(HoldDTO dto)
        {
            _holders.Release(dto);
        }

      

        public static ConnectionMapping<string> GetConnectionMapping()
        {
            return _connections;
        }


       
        protected   void RemoveFromAllrooms()
        {
         

            foreach (var item in _connections.Groups)
            {

                RemoveFromRoom(item.Key);

                
            }
        }
        

        protected void RemoveFromRoom(string room)
        {
            string name = Context.QueryString["userId"];
            _connections.RemoveFromGroup(room, name);

            dynamic c = new ExpandoObject();
            c.room = room;
            c.user = name;
            Clients.Group(room, Context.ConnectionId).LeaveRoom(c);

        }
        protected void AddToRoom(string room)
        {
            string name = Context.QueryString["userId"];

            _connections.AddGroup(room, name);


            dynamic c = new ExpandoObject();
            c.room = room;
            c.user = name;
            Clients.Group(room, Context.ConnectionId).JoinRoom(c);
        }



        public IList GetAllInRoom(string roomId)
        {
            return _connections.GetAllConnectedInGroup(roomId);
        }
        public void GetAllConnectedInRoom(string roomId)
        {

            dynamic c = new ExpandoObject();
            c.room = roomId;
            c.users = _connections.GetAllConnectedInGroup(roomId);


            Clients.Client(Context.ConnectionId).AllConnectedInRoom(c);
        



        }

        public override Task OnConnected()
        {
                if (Context.QueryString.ContainsKey("userId"))

            {
                string name = Context.QueryString["userId"];

                

                _connections.Add(name, Context.ConnectionId);
                
                Clients.Client(Context.ConnectionId).AllConnectedUsers(_connections.GetAllConnectedUsers());
                Clients.All.UserConnected(name);


            }

                
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {

                    if (Context.QueryString.ContainsKey("userId"))

            {
                string name = Context.QueryString["userId"];
                Clients.All.UserDisconnected(name);




                //salgo de todos los rooms y libero todo en el contenedor
                _holders.ReleaseAll(Guid.Parse(name));
                RemoveFromAllrooms();

                  _connections.Remove(name, Context.ConnectionId);
            }





            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {

            if (Context.QueryString.ContainsKey("userId"))

            {
                string name = Context.QueryString["userId"];

                if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
                {
                    Clients.Client(Context.ConnectionId).AllConnectedUsers(_connections.GetAllConnectedUsers());
                    Clients.All.ConnectedUser(name);
                    _connections.Add(name, Context.ConnectionId);
                }
            }




            return base.OnReconnected();
        }

    }
}
