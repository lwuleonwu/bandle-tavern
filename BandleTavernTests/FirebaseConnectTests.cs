using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandleTavern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

            var result = await FirebaseConnect.Publish("tets", "Foxwell", partySize, partyMembers, partyRanks);
            Console.WriteLine(result);
        }

        [TestMethod()]
        public async Task FirebaseRemoveParty() {
            var result = await FirebaseConnect.RemoveParty("tets", "Foxwell");
            Console.WriteLine(result);
        }
    }
}