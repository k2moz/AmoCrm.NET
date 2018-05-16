using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmmoCRMTest.Entities
{
    [DataContract()]
    public class Auth
    {
        [DataMember(Name = "USER_LOGIN")]
        public string Login { get; set; }

        [DataMember(Name = "USER_HASH")]
        public string ApiToken { get; set; }
    }
    [DataContract]
    public class AuthResult
    {
    
        public AuthResultResponse response { get; set; }
        [IgnoreDataMember]
        public CookieCollection Cookie { get; set; }
    }

    [DataContract]
    public class AuthResultResponse{
        public string error { get; set; }
        public string domain { get; set; }
        public bool auth { get; set; }
        public long server_time { get; set; }
        public string error_code { get; set; }
    }
}
