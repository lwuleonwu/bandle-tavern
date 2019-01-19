using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandleTavern {
    public class PartyMember {
        [JsonProperty("memberName")]
        public string memberName { get; set; }
        [JsonProperty("memberRank")]
        public string memberRank { get; set; }
    }

    public class PartyInfo {
        [JsonProperty("partyLeader")]
        public string partyLeader { get; set; }
        [JsonProperty("members")]
        public List<PartyMember> members { get; set; }
    }

    public class Party {
        [JsonProperty("partyInfo")]
        public PartyInfo partyInfo { get; set; }
    }

    public class FirebaseData {
        [JsonProperty("partySize")]
        public int partySize { get; set; }
        [JsonProperty("party")]
        public Party party { get; set; }
    }

    public class FirebaseKey {
        [JsonProperty("dataKey")]
        public string dataKey { get; set; }
        [JsonProperty("firebaseData")]
        public FirebaseData firebaseData { get; set; }
    }
}
