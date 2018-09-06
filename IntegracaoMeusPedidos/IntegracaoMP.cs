using IntegracaoMeusPedidos.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace IntegracaoMeusPedidos
{
    public class IntegracaoMP
    {
        public string CompanyToken { get; set; }
        public string ApplicationToken { get; set; }
        public bool Sandbox { get; set; }

        public IntegracaoMP(string companyToken, string applicationToken, bool sandbox = false)
        {
            this.ApplicationToken = applicationToken;
            this.CompanyToken = companyToken;
            this.Sandbox = sandbox;
        }

        private string getPath(string path)
        {
            return (Sandbox ? "https://sandbox.meuspedidos.com.br/api/v1/" : "https://integracao.meuspedidos.com.br/api/v1/") + path;
        }

        public T Update<T>(T anonymousEntity, int id) where T : IntegracaoMPType<T>, new()
        {
            T t = new T();
            string apiPath = t.Path() + "/" + id;
            HttpWebRequest req = CreateHttpWebRequest(apiPath, "PUT");

            SendRequest(anonymousEntity, req);
            return null;
        }

        public T Insert<T>(T anonymousEntity) where T : IntegracaoMPType<T>, new()
        {
            T t = new T();
            HttpWebRequest req = CreateHttpWebRequest(t.Path(), "POST");

            SendRequest(anonymousEntity, req);

            WebResponse response = req.GetResponse();
            int MeusPedidosID = 0;
            Int32.TryParse(response.Headers.Get("MeusPedidosID"), out MeusPedidosID);

            return null;
        }

        public T Get<T>(int id) where T : IntegracaoMPType<T>, new()
        {
            T t = new T();
            string apiPath = t.Path() + "/" + id;
            HttpWebRequest req = CreateHttpWebRequest(apiPath);

            WebResponse response = req.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var x = serializer.Deserialize<dynamic>(readStream.ReadToEnd());

            return t.ConvertJson(x);
        }

        public List<T> GetAll<T>(DateTime ultimaSincronizacao) where T : IntegracaoMPType<T>, new()
        {
            T t = new T();
            HttpWebRequest req = CreateHttpWebRequest(t.Path());

            WebResponse response = req.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            List<T> list = new List<T>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<dynamic> anonymousList = serializer.Deserialize<List<dynamic>>(readStream.ReadToEnd());

            foreach (var obj in anonymousList)
            {
                list.Add(t.ConvertJson(obj));
            }

            return list;
        }

        private HttpWebRequest CreateHttpWebRequest(string apiPath, string method = "GET")
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(this.getPath(apiPath));
            req.Method = method;
            req.ContentType = "application/json";
            req.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");
            req.Headers.Add("ApplicationToken", this.ApplicationToken);
            req.Headers.Add("CompanyToken", this.CompanyToken);
            return req;
        }

        private static void SendRequest<T>(T anonymousEntity, HttpWebRequest req)
        {
            try
            {
                Stream stream = req.GetRequestStream();
                string json = new JavaScriptSerializer().Serialize(anonymousEntity);
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
                req.GetResponse();
                req.Abort();
            }
            catch (WebException we)
            {
                var resp = new StreamReader(we.Response.GetResponseStream()).ReadToEnd();
                MensagemMP mensagem = new JavaScriptSerializer().Deserialize<MensagemMP>(resp);
                if (we.Response != null && ((HttpWebResponse)we.Response).StatusCode == HttpStatusCode.PreconditionFailed)
                {

                }
            }
        }
    }
}