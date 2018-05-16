using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AmmoCRMTest.BL
{
    public static class Helpers
    {
        public static void Init(string host)
        {
            Paths = new Dictionary<PathsTypes, string>
        {
            { PathsTypes.Authorise, host+"/private/api/auth.php?type=json"},
            { PathsTypes.Account, host+"/api/v2/account?with=pipelines,groups,note_types,task_types,users,custom_fields" },
            { PathsTypes.Leads, host+"/api/v2/leads"},
            { PathsTypes.Contact,host+"/api/v2/contacts" },
            { PathsTypes.Pipeline, host+"/api/v2/pipelines" },
            { PathsTypes.PipelineSet, host+"/private/api/v2/json/pipelines/set" }
        };
        }

        public static string host;
        public static Dictionary<PathsTypes, string> Paths;



        public enum PathsTypes
        {
            Authorise = 0,
            Account = 1,
            Leads = 2,
            Contact = 3,
            Pipeline,
            PipelineSet
        }

        public static bool TryAddCookie(this WebRequest webRequest, CookieCollection cookie)
        {
            HttpWebRequest httpRequest = webRequest as HttpWebRequest;
            if (httpRequest == null)
            {
                return false;
            }

            if (httpRequest.CookieContainer == null)
            {
                httpRequest.CookieContainer = new CookieContainer();
            }

            httpRequest.CookieContainer.Add(cookie);
            return true;
        }

    }
}
