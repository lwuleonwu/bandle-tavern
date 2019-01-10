using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace BandleTavern {
    public static class FirebaseConnect {
        private static string summonerId;
        private static string firebaseApiKey = "AIzaSyALvkXp-aE5BYO95sxY62XEl_DdvnsmXSg";
        private static HttpClient httpClient = new HttpClient();

        // initialize with necessary information
        public static async Task<HttpResponseMessage> Init(string summonerId) {
            FirebaseConnect.summonerId = summonerId;
            Console.WriteLine($"summoner id is {summonerId}");

            string signupUrl = $"https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key={FirebaseConnect.firebaseApiKey}";
            string dataString = $"{{\"email\":\"{FirebaseConnect.summonerId}@bandle.tavern\",\"password\":\"haveadrink\",\"returnSecureToken\":true}}";

            using (var request = new HttpRequestMessage(new HttpMethod("POST"), signupUrl)) {
                request.Content = new StringContent(dataString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                return response;
            }
        }
    }
}
