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
                ["iin"] = "123456789004",
                ["password"] = "123456",
                ["role"] = "parent"
            };

            return await RequestToken(client, data);
        }

        public static async Task<string> GetAdminToken(HttpClient client)
        {
            var data = new JObject
            {
                ["iin"] = "123456789001",
                ["password"] = "123456",
                ["role"] = "admin"
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
                    $"Failed to authenticate. Server returned error with code: {(int) response.StatusCode} {response.StatusCode.ToString()}");

            var resultContent = await response.Content.ReadAsStringAsync();

            var result = JObject.Parse(resultContent)?["result"] as JObject;

            if (!result.ContainsKey("token")) throw new Exception("Token was not returned");

            var token = result["token"].ToString();

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new Exception("Failed to authenticate. Token is empty");
            }

            return token;
        }
    }
}