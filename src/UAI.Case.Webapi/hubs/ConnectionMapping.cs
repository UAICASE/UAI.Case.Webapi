using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Webapi.hubs
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();


        private readonly Dictionary<T, HashSet<string>> _groups =
                new Dictionary<T, HashSet<string>>();

        public Dictionary<T, HashSet<string>> Groups { get { return _groups; } }

        public void RemoveFromGroup(T groupKey, string userId)
        {

            lock (_groups)
            {
                HashSet<string> connections;
                if (!_groups.TryGetValue(groupKey, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(userId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(groupKey);
                    }
                }
            }
        }


        public void AddGroup(T groupKey, string userId)
        {
            lock (_groups)
            {
                HashSet<string> connections;
                if (!_groups.TryGetValue(groupKey, out connections))
                {
                    connections = new HashSet<string>();
                    connections.Add(userId);
                    _groups.Add(groupKey, connections);
                }
                if (!connections.Contains(userId))
                {
                    connections.Add(userId);
                }

               
            }
        }

        public IList GetAllConnectedInGroup(T groupKey)
        {
            HashSet<string> connections;
            IList conn = new List<string>();
            if (_groups.TryGetValue(groupKey, out connections))
            {

                lock (_groups)
                {

                    foreach (var item in connections)
                    {

                        conn.Add(item);



                    }

                }

                return conn;

            }
            return null;


            
            

        }


        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }


    

        public IList GetAllConnectedUsers()
        {
            IList conn = new List<string>();
            lock (_connections)
            {
                
                foreach (var item in _connections)
                {
                    
                        conn.Add(item.Key );
                    

                    
                }

            }

            return conn;
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}
