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
            List<FirebaseKey> keys = await FirebaseConnect.RetrieveData("First Win of the Day");

            foreach (var key in keys) {
                Console.WriteLine(key.dataKey);
                Console.WriteLine(key.firebaseData.partySize);
            }
        }

        [TestMethod()]
        public async Task FirebaseUpdateParty() {
            PartyMember member1 = new PartyMember { memberName = "Zaichata", memberRank = "Unranked" };
            PartyMember member2 = new PartyMember { memberName = "Foxwell", memberRank = "Gold 2" };
            List<PartyMember> updatedMembers = new List<PartyMember>(new PartyMember[] { member1, member2 });

            PartyInfo updatedPartyInfo = new PartyInfo { members = updatedMembers, partyLeader = updatedMembers[0].memberName };
            Party updatedParty = new Party() { partyInfo = updatedPartyInfo };

            FirebaseData updatedData = new FirebaseData { party = updatedParty, partySize = updatedMembers.Count };

            // pulled manually from database
            string dataKey = "-LWgz8jXd8WG1y-pkcvX";
            FirebaseKey updatedFirebaseKey = new FirebaseKey { dataKey = dataKey, firebaseData = updatedData };

            await FirebaseConnect.SignIn("scarra");
            var result = await FirebaseConnect.UpdateParty("First Win of the Day", updatedFirebaseKey);
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