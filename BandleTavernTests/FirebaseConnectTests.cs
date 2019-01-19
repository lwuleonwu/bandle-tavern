using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandleTavern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BandleTavern.Tests {
    [TestClass()]
    public class FirebaseConnectTests {
        [TestMethod()]
        public void InitTest() {
            Console.WriteLine("Init");
        }

        [TestMethod()]
        public async Task FirebaseSignUp() {
            var result = await FirebaseConnect.SignUp("scarra");
            Console.WriteLine(result);
        }

        [TestMethod()]
        public async Task FirebaseSignIn() {
            var result = await FirebaseConnect.SignIn("scarra");
            Console.WriteLine(result);
        }

        [TestMethod()]
        public async Task FirebaseRefresh() {
            var result = await FirebaseConnect.RefreshLoginSession();
            Console.WriteLine(result);
        }

        [TestMethod()]
        public async Task FirebasePublish() {
            int partySize = 3;
            string[] partyMembers = {"Foxwell", "Zaichata", "lt"};
            string[] partyRanks = {"Gold 4", "Unkranked", "Unranked"};

            int partySize2 = 2;
            string[] partyMembers2 = {"Dragon Husbando", "Tied and Knotted"};
            string[] partyRanks2 = {"Unranked", "dont even use this one anymore lol2"};

            await FirebaseConnect.SignIn("scarra");
            var result = await FirebaseConnect.Publish("First Win of the Day", partyMembers[0], partySize, partyMembers, partyRanks);
            Console.WriteLine(result);
            var result2 = await FirebaseConnect.Publish("Tets", partyMembers2[0], partySize2, partyMembers2, partyRanks2);
            Console.WriteLine(result2);
        }

        [TestMethod()]
        public async Task FirebaseRetrieveData() {
            await FirebaseConnect.SignIn("scarra");
            var result = await FirebaseConnect.RetrieveData("First Win of the Day");
            // Console.WriteLine(result);

            JObject parsedJson = JObject.Parse(result);
            // need to retrieve FirebaseKey.dataKey from parsedJson
            // is first field in parsedJson
            FirebaseKey firebase = (FirebaseKey) parsedJson.ToObject(typeof (FirebaseKey));
            Console.WriteLine(firebase.firebaseData);
        }

        [TestMethod()]
        public async Task FirebaseRemoveParty() {
            await FirebaseConnect.SignIn("scarra");
            string dataKey = "-LW_8qm72MmKiCStO3oh"; // retrieved manually from database
            var result = await FirebaseConnect.RemoveParty("First Win of the Day", dataKey);
            Console.WriteLine(result);
        }
    }
}