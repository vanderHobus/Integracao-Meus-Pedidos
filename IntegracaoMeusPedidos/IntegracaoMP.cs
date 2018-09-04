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

            Stream stream = req.GetRequestStream();
            string json = new JavaScriptSerializer().Serialize(anonymousEntity);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            req.GetResponse();
            req.Abort();
            return null;
        }

        public T Insert<T>(T anonymousEntity) where T : IntegracaoMPType<T>, new()
        {
            T t = new T();
            HttpWebRequest req = CreateHttpWebRequest(t.Path(), "POST");

            Stream stream = req.GetRequestStream();
            string json = new JavaScriptSerializer().Serialize(anonymousEntity);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            req.GetResponse();
            req.Abort();
            return null;
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
            List<dynamic> list1 = serializer.Deserialize<List<dynamic>>(readStream.ReadToEnd());

            foreach (var obj in list1)
            {
                T newT = t.ConvertJson(obj);
                list.Add(newT);
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
    }
}