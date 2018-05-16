using AmmoCRMTest.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AmmoCRMTest.BL
{
    public static class Service
    {
        private static readonly HttpClient client = new HttpClient();
        private static JavaScriptSerializer serialiser = new JavaScriptSerializer();

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="login"></param>
        /// <param name="apiToken"></param>
        /// <returns></returns>
        public static AuthResult Auth(string login, string apiToken,string host)
        {
            Helpers.host = host;
            Helpers.Init(host);
            var _nvc = new NameValueCollection()
            {
                { "USER_LOGIN",login },
                { "USER_HASH",apiToken },
            };
           
            AuthResult _ar = null;

            using (var webClient = new CookieWebClient())
            {
                
                var response = webClient.UploadValues(Helpers.Paths[Helpers.PathsTypes.Authorise], _nvc);
                //var a = System.Web.HttpContext.Current.Session["cookie"];

                string _stringResponse = Encoding.UTF8.GetString(response);
                _ar = serialiser.Deserialize<AuthResult>(_stringResponse);
                _ar.Cookie = webClient.CookieContainer.GetCookies(new Uri(Helpers.Paths[Helpers.PathsTypes.Authorise]));
                return _ar;
            }
        }

        public static Account Account(CookieCollection cookie)
        {
            var _outputJson = _getJsonByHttpGetRequset(Helpers.Paths[Helpers.PathsTypes.Account], cookie, null);
            var _result = serialiser.Deserialize<Account>(_outputJson);
            return _result;
        }
        #region Contacts
        public static int AddContact(ContactForAdd contact, CookieCollection cookie)
        {
            try
            {
                var _inputJson = JsonConvert.SerializeObject(contact);
                var _outputJson = _getJsonByHttpPostRequest(Helpers.Paths[Helpers.PathsTypes.Contact], _inputJson, cookie);

                JObject _o = JObject.Parse(_outputJson);
                var _contectJsonString = (string)_o["_embedded"]["items"][0]["id"];
                return int.Parse(_contectJsonString);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static int UpdateContact(ContactForUpdate contact, CookieCollection cookie)
        {
            try
            {
                var _inputJson = JsonConvert.SerializeObject(contact);
                var _outputJson = _getJsonByHttpPostRequest(Helpers.Paths[Helpers.PathsTypes.Contact], _inputJson, cookie);
                JObject _o = JObject.Parse(_outputJson);
                var _contectJsonString = (string)_o["_embedded"]["items"][0]["id"];
                return int.Parse(_contectJsonString);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static List<ContactViewModel> GetContact(string contactId, CookieCollection cookie)
        {
            try
            {
                var _outputJson = _getJsonByHttpGetRequset(Helpers.Paths[Helpers.PathsTypes.Contact], cookie, new string[] { string.IsNullOrEmpty(contactId) ? "" : "id=" + contactId });
                JObject _o = JObject.Parse(_outputJson);
                var _contectJsonString = (string)_o["_embedded"]["items"].ToString();

                return (List<ContactViewModel>)JsonConvert.DeserializeObject<List<ContactViewModel>>(_contectJsonString);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Piplines
        public static Dictionary<int, PiplneViewModelPipline> CreatePipline(PiplineForAdd piplene, CookieCollection cookie)
        {

            try
            {
                var _wrap = new
                {
                    request = new
                    {
                        pipelines = piplene
                    }
                };
                var _inputJson = JsonConvert.SerializeObject(_wrap);

                var _outputJson = _getJsonByHttpPostRequest(Helpers.Paths[Helpers.PathsTypes.PipelineSet], _inputJson, cookie);
                JObject _o = JObject.Parse(_outputJson);
                var _json = _o["response"]["pipelines"]["add"]["pipelines"].ToString();
                var _obj = JsonConvert.DeserializeObject<Dictionary<int, PiplneViewModelPipline>>(_json);
                return _obj;
                // return int.Parse();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static int UpdatePipline(PiplineForUpdate piplene, CookieCollection cookie)
        {
            try
            {
                var _inputJson = serialiser.Serialize(piplene);
                var _outputJson = _getJsonByHttpPostRequest(Helpers.Paths[Helpers.PathsTypes.PipelineSet], _inputJson, cookie);

                JObject _o = JObject.Parse(_outputJson);
                return int.Parse(_o["_embedded"]["items"][0]["id"].ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static Dictionary<int, PiplneViewModelPipline> GetPiplines(CookieCollection cookie)
        {
            try
            {
                var _outputJson = _getJsonByHttpGetRequset(Helpers.Paths[Helpers.PathsTypes.Pipeline], cookie, new string[] { });
                //return _outputJson;
                JObject _o = JObject.Parse(_outputJson);
                var _json = _o["_embedded"]["items"].ToString();
                    return JsonConvert.DeserializeObject<Dictionary<int, PiplneViewModelPipline>>(_json);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Leads
        public static int CreateLead(LeadsForAdd lead, CookieCollection cookie)
        {
            var _inputJson = JsonConvert.SerializeObject(lead);
            var _outputJson = _getJsonByHttpPostRequest(Helpers.Paths[Helpers.PathsTypes.Leads], _inputJson, cookie);

            JObject _o = JObject.Parse(_outputJson);
            var _contectJsonString = (string)_o["_embedded"]["items"][0]["id"];
            return int.Parse(_contectJsonString);
        }

        public static int UpdateLead(LeadsForUpdate lead, CookieCollection cookie)
        {
            var _inputJson = JsonConvert.SerializeObject(lead);
            var _outputJson = _getJsonByHttpPostRequest(Helpers.Paths[Helpers.PathsTypes.Leads], _inputJson, cookie);

            JObject _o = JObject.Parse(_outputJson);
            var _contectJsonString = (string)_o["_embedded"]["items"][0]["id"];
            return int.Parse(_contectJsonString);
        }

        public static List<LeadsViewModel> GetLeads(LeadsRequest request, CookieCollection cookie)
        {
            try
            {
                var _outputJson = _getJsonByHttpGetRequset(Helpers.Paths[Helpers.PathsTypes.Leads], cookie, new string[] {});
                JObject _o = JObject.Parse(_outputJson);
                var _contectJsonString = (string)_o["_embedded"]["items"].ToString();
                var _pff = _contectJsonString.Replace("\"custom_fields\": {},", "\"custom_fields\": [],");
                var _obj = JsonConvert.DeserializeObject<List<LeadsViewModel>>(_pff);
                return _obj;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
        #endregion

        private static string _getJsonByHttpGetRequset(string urlPath, CookieCollection cookie, string[] paramm)
        {
            var request = (HttpWebRequest)WebRequest.Create(urlPath + (paramm != null && paramm.Count() > 0 ? "?" + string.Join("&", paramm) : ""));

            request.Method = "GET";
            request.ContentType = "application/json";
            request.TryAddCookie(cookie);
            //request.CookieContainer.Add(cookie);

            string _stringResponse = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    _stringResponse = reader.ReadToEnd();
                }
                response.Close();
            }
            return _stringResponse;
        }

        private static string _getJsonByHttpPostRequest(string urlPath, NameValueCollection values, CookieCollection cookie = null)
        {

            //using (var webReq = new WebRequest())
            //{
            //    var response = webClient.UploadValues(urlPath, values);

            //    string _stringResponse = Encoding.UTF8.GetString(response);
            //    return _stringResponse;
            //}

            using (var webClient = new CookieWebClient())
            {
                var response = webClient.UploadValues(urlPath, values);
                //var a = System.Web.HttpContext.Current.Session["cookie"];

                string _stringResponse = Encoding.UTF8.GetString(response);
                return _stringResponse;
            }


        }

        private static string _getJsonByHttpPostRequest(string urlPath, string inputJson, CookieCollection cookie)
        {
            var body = Encoding.UTF8.GetBytes(inputJson);
            var request = (HttpWebRequest)WebRequest.Create(urlPath);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = body.Length;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookie);

            using (System.IO.Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }

            string _stringResponse = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    _stringResponse = reader.ReadToEnd();
                }
                response.Close();
            }
            return _stringResponse;
        }
    }
}
