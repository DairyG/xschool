using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace XSchool.Helpers
{
    public class HttpHelper
    {
        public async Task Post(string url,IEnumerable<KeyValuePair<string,string>> parameters)
        {
            HttpClient client = new HttpClient();
            var content = new FormUrlEncodedContent(parameters);

            var response = await client.PostAsync(url, content);
        }
    }
}
