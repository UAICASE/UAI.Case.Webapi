using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UAI.Case.Webapi.Config
{
    public class CustomContractResolver : DefaultContractResolver
    {


       
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {

            var prop = base.CreateProperty(member, memberSerialization);

            if (!prop.Writable && prop.PropertyName.Equals("Id"))
            {
                var property = member as PropertyInfo;
                if (property != null)
                {
                    var hasPrivateSetter = property.GetSetMethod(true) != null;
                    prop.Writable = hasPrivateSetter;
                }
            }

            return prop;
        }
    }
}
