using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iTrash
{
    public class ContainerAgent
    {
        List<Container> containerList = new List<Container>();

        string hostIp = "192.168.1.40";

        bool? hasConnection;

        public bool? HasConnection
        {
            get { return hasConnection;  }
        }

        public List<Container> ContainerList
        {
            get { return containerList; }
        }

        public string HostIp
        {
            get { return hostIp;  }
        }

        public string responseData { get; set; }

        List<Container> MakeRequest(string type)
        {
            List<Container> NewContainers = new List<Container>();
            
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("http://" + Application.Current.Properties["HostIP"] + ":5000/api/v1/containers" + type);

            Request.Timeout = 500;
            Request.Method = "GET";

            Request.KeepAlive = true;
            try
            {
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();

                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader responseReader = new StreamReader(Response.GetResponseStream()))
                    {
                        String ResponseData = responseReader.ReadToEnd();

                        NewContainers = JsonConvert.DeserializeObject<List<Container>>(ResponseData);

                        hasConnection = true;
                        Application.Current.Properties["HasConnection"] = hasConnection;

                        return NewContainers;
                        

                    }
                }
                else
                {
                    hasConnection = false;
                    Application.Current.Properties["HasConnection"] = hasConnection;
                    return null;
                }
            }
            catch
            {
                hasConnection = false;
                Application.Current.Properties["HasConnection"] = hasConnection;
                return null;
            }
        }

        public async Task<List<Container>> InitiateDataUpdater()
        {
            while (true)
            {
                containerList = MakeRequest("/all");
                await Task.Delay(1000);
                Application.Current.Properties["ContainerList"] = containerList;
            }

        }

        public List<Container> RefreshNow()
        {
            containerList = MakeRequest("/all");
            return containerList;
        }

        public List<Container> SearchContainer(string type)
        {
            List<Container> specificList = new List<Container>();
            specificList = MakeRequest(type);

            return specificList;
        }
    }
}
