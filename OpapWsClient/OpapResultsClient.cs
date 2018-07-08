using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpapWsClient
{
    public class OpapResultsClient
    {
        private JsonClient jsonClient;

        public OpapResultsClient()
        {
            jsonClient = new JsonClient();
        }

        public OpapResults GetLastResults(string game)
        {
            string url = "http://applications.opap.gr/DrawsRestServices/" + game + "/last.json";
            return ParseOpapResults(url);
        }

        public OpapResults GetSpecificDrawResults(string game, string drawNumber)
        {
            string url = "http://applications.opap.gr/DrawsRestServices/" + game + "/" + drawNumber + ".json";
            return ParseOpapResults(url);
        }

        public OpapDateResults GetDrawByDateResults(string game, string drawDate)
        {
            string url = "http://applications.opap.gr/DrawsRestServices/" + game + "/drawDate/" + drawDate + ".json";
            string response = jsonClient.GET(url);
            if (string.IsNullOrEmpty(response))
                return null;

            OpapDateResults opapResults = null;
            opapResults = JsonConvert.DeserializeObject<OpapDateResults>(response);
            return opapResults;
        }

        private OpapResults ParseOpapResults(string url)
        {
            string response = jsonClient.GET(url);
            if (string.IsNullOrEmpty(response))
                return null;

            OpapResults opapResults = null;
            opapResults = JsonConvert.DeserializeObject<OpapResults>(response);
            return opapResults;
        }
    }
}
