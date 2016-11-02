using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Interfaces;

namespace UAI.Case.EFProvider
{
    public class Creds : ICreds
    {
        public string host { get; set; }
        public string cs { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string uri { get; set; }
        public string port { get; set; }
        public string database { get; set; }
       
        public void GetDataFromUri(string uri)
        {
            this.uri= uri;
            username = (uri.Split('/')[2]).Split(':')[0];
            password = (uri.Split(':')[2]).Split('@')[0];
            host= (uri.Split('@')[1]).Split(':')[0];
            port = (uri.Split(':')[3]).Split('/')[0];
            database = (uri.Split('/')[3]);
            cs = string.Format("User ID={0};Password={1};Host={2};Port={3};Database={4};Pooling=true;",username,password,host,port,database);
            
        }
    }
}
