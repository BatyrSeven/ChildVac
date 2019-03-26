using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ChildVac.Test.Helpers
{
    public static class AuthenticationHelper
    {
        private static string Resource => "/api/Account";

        public static async Task<string> GetUserToken(HttpClient client)
        {
            var data = new JObject
            {
                ["login"] = "qwerty",
                ["password"] = "55555"
            };

            return await RequestToken(client, data);
        }

        public static async Task<string> GetAdminToken(HttpClient client)
        {
            var data = new JObject
            {
                ["login"] = "admin@gmail.com",
                ["password"] = "12345"
            };

            return await RequestToken(client, data);
        }

        private static async Task<string> RequestToken(HttpClient client, JObject data)
        {
            var content = new StringContent(data.ToString(),
                Encoding.UTF8,
                "application/json"); //CONTENT-TYPE header

            var response = await client.PostAsync(Resource, content);

            if (!response.IsSuccessStatusCode)
                throw new Exception(
                    $"Server returned error with code: {(int) response.StatusCode} {response.StatusCode.ToString()}");

            var resultContent = await response.Content.ReadAsStringAsync();

            var result = JObject.Parse(resultContent);

            if (!result.ContainsKey("token")) throw new Exception("Token was not returned");

            return result["token"].ToString();
        }
    }
}