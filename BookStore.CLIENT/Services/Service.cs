using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.CLIENT.Services
{
    public abstract class Service<T>
    {
        private readonly string baseURI;
        public Service(string uri) => baseURI = uri;

        public async Task<ObservableCollection<T>> GetAllAsync()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var url = baseURI;

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);

                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = $"Bearer {App.admintoken}";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = await streamReader.ReadToEndAsync();
                    result = result.Replace("0001-01-01T00:00:00", "");
                    Debug.WriteLine(result);
                    ObservableCollection<T> resp = JsonConvert.DeserializeObject<ObservableCollection<T>>(result);
                    return resp;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new ObservableCollection<T>();
            }
        }

        public async Task<T> PostAsync(T t)
        {
            try
            {
                var url = baseURI;

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = $"Bearer {App.admintoken}";
                httpRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    Debug.WriteLine(JsonConvert.SerializeObject(t, new StringEnumConverter()));
                    streamWriter.Write(JsonConvert.SerializeObject(t, new StringEnumConverter()));
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = await streamReader.ReadToEndAsync();
                    result = result.Replace("0001-01-01T00:00:00", "");
                    T resp = JsonConvert.DeserializeObject<T>(result);
                    return resp;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return t;
            }
        }

        public async Task<T> PutAsync(T t, int id)
        {
            try
            {
                var url = baseURI + $"/{id}";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";

                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = $"Bearer {App.admintoken}";
                httpRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(t, new StringEnumConverter()));
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = await streamReader.ReadToEndAsync();
                    result = result.Replace("0001-01-01T00:00:00", "");
                    T resp = JsonConvert.DeserializeObject<T>(result);
                    return resp;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return t;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var url = baseURI + $"/{id}";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "DELETE";

                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = $"Bearer {App.admintoken}";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = await streamReader.ReadToEndAsync();
                    Debug.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
