using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using LcuApiTavern;

namespace BandleTavern
{
    public class Options
    {
        [XmlIgnore]
        public static Options Active { get; set; }


        [XmlElement("ClientDirectory")]
        public string ClientDirectory {
            get => LeagueClient.ClientDirectory;
            set => LeagueClient.ClientDirectory = value;
        }


        [XmlIgnore]
        private static string OptionsFileDirectory = string.Format("{0}{1}{2}",
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            Path.DirectorySeparatorChar,
            "BandleTavern"
            );
        [XmlIgnore]
        private static string OptionsFileName = "Options.xml";

        public static void LoadOptions()
        {
            XmlSerializer xs;

            Directory.CreateDirectory(OptionsFileDirectory);
            
            xs = new XmlSerializer(typeof(Options));

            string FilePath = string.Format("{0}{1}{2}", OptionsFileDirectory, Path.DirectorySeparatorChar, OptionsFileName);
            if (File.Exists(FilePath))
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    Active = (Options)xs.Deserialize(sr);
                }
            }
            else
            {
                LoadDefaultOptions();
            }
        }

        public static Options LoadDefaultOptions()
        {
            Options o = new Options();
            o.ClientDirectory = "";
            Active = o;
            return o;
        }

        public static void SaveOptions()
        {
            XmlSerializer xs;

            Directory.CreateDirectory(OptionsFileDirectory);
            string FilePath = string.Format("{0}{1}{2}", OptionsFileDirectory, Path.DirectorySeparatorChar, OptionsFileName);
            xs = new XmlSerializer(typeof(Options));

            using (TextWriter tw = new StreamWriter(FilePath))
            {
                xs.Serialize(tw, Active);
            }
        }
    }
}
