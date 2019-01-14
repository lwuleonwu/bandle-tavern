﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Text.RegularExpressions;

namespace BandleTavern {
    public static class FirebaseConnect {
        private static string firebaseApiKey = "AIzaSyALvkXp-aE5BYO95sxY62XEl_DdvnsmXSg";
        private static string summonerId;
        private static string idToken;
        private static string refreshToken;
        private static HttpClient httpClient = new HttpClient();

        /*
         * create account in database
         * returns firebase id token and refresh token if successful with 200 OK HTTP status code
         */
        public static async Task<string> SignUp(string summonerId) {
            FirebaseConnect.summonerId = summonerId;
            Console.WriteLine($"Signing up with summoner id: {summonerId}");

            string signupUrl = $"https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key={FirebaseConnect.firebaseApiKey}";
            string dataString = $"{{\"email\":\"{FirebaseConnect.summonerId}@bandle.tavern\",\"password\":\"haveadrink\",\"returnSecureToken\":true}}";

            using (var request = new HttpRequestMessage(new HttpMethod("POST"), signupUrl)) {
                request.Content = new StringContent(dataString, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);
                Console.WriteLine(response);

                var responseContent = response.Content;
                var result = "";

                // get response from firebase
                using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync())) {
                    result += await reader.ReadToEndAsync();

                    // parse idToken and refreshToken if request was successful
                    if (result.Contains("idToken")) {
                        string modifiedResult = Regex.Replace(result, @"\{*\}*\s+", "");
                        modifiedResult = modifiedResult.Replace("\"", string.Empty);
                        string[] tokens = modifiedResult.Split(':', ',');

                        FirebaseConnect.idToken = tokens[3];
                        FirebaseConnect.refreshToken = tokens[7];

                        /*
                        for (int i = 0; i < tokens.Length; i++) {
                            Console.WriteLine($"{i}: {tokens[i]}");
                        }
                        */
                    }
                }

                return result;
            }
        }

        /*
         * log into existing account in database
         * returns firebase id token and refresh token if successful with 200 OK HTTP status code
         */
        public static async Task<string> SignIn(string summonerId) {
            FirebaseConnect.summonerId = summonerId;
            Console.WriteLine($"Logging in with summonerId: {summonerId}");

            string signinUrl = $"https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key={FirebaseConnect.firebaseApiKey}";
            string dataString = $"{{\"email\":\"{FirebaseConnect.summonerId}@bandle.tavern\",\"password\":\"haveadrink\",\"returnSecureToken\":true}}";

            using (var request = new HttpRequestMessage(new HttpMethod("POST"), signinUrl)) {
                request.Content = new StringContent(dataString, Encoding.UTF8, "application/json");

                // send request to firebase
                var response = await httpClient.SendAsync(request);
                Console.WriteLine(response);

                var responseContent = response.Content;
                var result = "";

                // get response from firebase
                using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync())) {
                    result += await reader.ReadToEndAsync();

                    // parse idToken and refreshToken if request was successful
                    if (result.Contains("idToken")) {
                        string modifiedResult = Regex.Replace(result, @"\{*\}*\s+", "");
                        modifiedResult = modifiedResult.Replace("\"", string.Empty);
                        string[] tokens = modifiedResult.Split(':', ',');

                        FirebaseConnect.idToken = tokens[9];
                        FirebaseConnect.refreshToken = tokens[13];

                        /*
                        for (int i = 0; i < tokens.Length; i++) {
                            Console.WriteLine($"{i}: {tokens[i]}");
                        }
                        */
                    }
                }

                return result;
            }
        }

        /* 
         * refresh idToken by making refresh request with refresh token
         * need to use this for when session expires after one hour
         * returns new firebase id token and refresh token if successful with 200 OK HTTP status code
         */
        public static async Task<string> RefreshLoginSession() {
            Console.WriteLine($"Refreshing login session for summoner: {FirebaseConnect.summonerId}");

            string refreshUrl = $"https://securetoken.googleapis.com/v1/token?key={FirebaseConnect.firebaseApiKey}";
            string dataString = $"grant_type=refresh_token&refresh_token={FirebaseConnect.refreshToken}";

            using (var request = new HttpRequestMessage(new HttpMethod("POST"), refreshUrl)) {
                request.Content = new StringContent(dataString, Encoding.UTF8, "application/x-www-form-urlencoded");

                // send request to firebase
                var response = await httpClient.SendAsync(request);
                Console.WriteLine(response);

                var responseContent = response.Content;
                var result = "";

                // get response from firebase
                using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync())) {
                    result += await reader.ReadToEndAsync();

                    // parse idToken and refreshToken if request was successful
                    if (result.Contains("id_token")) {
                        string modifiedResult = Regex.Replace(result, @"\{*\}*\s+", "");
                        modifiedResult = modifiedResult.Replace("\"", string.Empty);
                        string[] tokens = modifiedResult.Split(':', ',');

                        FirebaseConnect.idToken = tokens[9];
                        FirebaseConnect.refreshToken = tokens[7];

                        /*
                        for (int i = 0; i < tokens.Length; i++) {
                            Console.WriteLine($"{i}: {tokens[i]}");
                        }
                        */
                    }
                }

                return result;
            }
        }
        
        /*
         * write data to database
         * returns the data published if successful with 200 OK HTTP status code
         */
        public static async Task<string> Publish(string missionTitle, string partyLeader, int partySize, string[] partyMembers,
            string[] partyRanks) {
            string partyData = "";

            // format party member data if party is of two or more players
            if (partySize > 1) {
                for (int i = 1; i < partySize; i++) {
                    string partyMember = partyMembers[i];
                    string partyMemberRank = partyRanks[i];
                    partyData += $",\"{partyMember}\":\"{partyMemberRank}\"";
                }
            }

            string dataString = $"{{\"{partyLeader}\":{{\"partySize\":\"{partySize}\",\"{partyLeader}\":\"{partyRanks[0]}\"{partyData}}}}}";
            string writeUrl = $"https://bandle-tavern.firebaseio.com/{missionTitle}.json?auth={FirebaseConnect.idToken}";

            using (var request = new HttpRequestMessage(new HttpMethod("PUT"), writeUrl)) {
                request.Content = new StringContent(dataString, Encoding.UTF8, "application/x-www-form-urlencoded");

                // send request to firebase
                var response = await httpClient.SendAsync(request);
                Console.WriteLine(response);

                var responseContent = response.Content;
                var result = "";

                // get response from firebase
                using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync())) {
                    result += await reader.ReadToEndAsync();
                }

                return result;
            }
        }

        /*
         * remove party from database
         * returns a JSON response containing null if successful with 200 OK HTTP status code
         */ 
        public static async Task<string> RemoveParty(string missionTitle, string partyLeader) {
            string deleteUrl = $"https://bandle-tavern.firebaseio.com/{missionTitle}/{partyLeader}.json?auth={FirebaseConnect.idToken}";

            using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), deleteUrl)) {
                // send request to firebase
                var response = await httpClient.SendAsync(request);
                Console.WriteLine(response);

                var responseContent = response.Content;
                var result = "";

                // get response from firebase
                using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync())) {
                    result += await reader.ReadToEndAsync();
                }

                return result;
            }
        }
    }
}
