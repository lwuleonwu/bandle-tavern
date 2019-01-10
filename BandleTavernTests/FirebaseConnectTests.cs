using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandleTavern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandleTavern.Tests {
    [TestClass()]
    public class FirebaseConnectTests {
        [TestMethod()]
        public void InitTest() {
            Console.WriteLine("beginning tets");
        }

        [TestMethod()]
        public async Task FirebaseInit() {
            var result = await FirebaseConnect.Init("tets");
            Console.Write(result);
        }
    }
}