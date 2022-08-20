using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SuperPost.Entity.EnumClass;

namespace SuperPost.Entity
{
   public class HttpObject
    {
        public int ID;
        public string Name;
        public HttpMethod Method;
        public List<Dictionary<string, string>> Params;
        public List<Dictionary<string, string>> Headers;
        public List<Dictionary<string, object>> Body;

        public HttpObject()
        {
            Params = new List<Dictionary<string, string>>();
            Headers = new List<Dictionary<string, string>>();
            Body = new List<Dictionary<string, object>>();
        }

        public HttpObject(string name, HttpMethod method):this()
        {
            this.Name = name;
            this.Method = method;
        }



    }
}
