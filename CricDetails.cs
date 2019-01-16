using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XML_Data_Parsing
{
    [XmlRoot("Player"),Serializable()]
    public class Player
    {
        [XmlElement("Name")]
        public String name { get; set; } = String.Empty;
        [XmlElement("Dob")]
        public DateTime dob { get; set; }
        [XmlElement("gender")]
        public Char gender { get; set; }
        [XmlElement("Type")]
        public String type { get; set; }
        [XmlElement("Country")]
        public String country { get; set; }
        [XmlElement("MatchesPlayed")]
        public int? matchesPlayed { get; set; }
        [XmlElement("TotalRuns")]
        public int? totalRuns { get; set; }
        [XmlElement("BallsStanded")]
        public int? ballsStanded { get; set; }
        [XmlElement("RunsScored")]
        public int? runsScored { get; set; }
        [XmlElement("BallsBowled")]
        public int? ballsBowled { get; set; }
        [XmlElement("WicketsTaken")]
        public int? wicketsTaken { get; set; }
        [XmlElement("Year")]
        public int year { get; set; }
        public static void save(List<Player> plyrs,String filename)
        {
            XmlSerializer serialiser = new XmlSerializer(typeof(List<Player>));

            // Create the TextWriter for the serialiser to use
            TextWriter filestream = new StreamWriter(filename);

            //write to the file
            serialiser.Serialize(filestream, plyrs);

            // Close the file
            filestream.Close();
        }
        public static List<Player> load(String filename)
        {
            List<Player> plyrs;
            using (var reader = new StreamReader(filename))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<Player>),
                    new XmlRootAttribute("ArrayOfPlayer"));
                plyrs = (List<Player>)deserializer.Deserialize(reader);
            }
            return plyrs;
        }
        static void CreateXMLFile(List<Player> players)
        {

            string file = @"Players.xml";
            XDocument doc;

            if (!File.Exists(file))
            {
                doc = new XDocument();
                doc.Add(new XElement("Players"));
            }
            else
            {
                doc = XDocument.Load(file);
            }
            foreach (Player p in players)
            {
                doc.Root.Add(
                      new XElement("Player",
                                   new XElement("Name", p.name),
                                   new XElement("Dob", p.dob.ToString("dd/MM/yyyy")),
                                   new XElement("gender", p.gender),
                                   new XElement("Country", p.country),
                                   new XElement("Type", p.type),
                                   new XElement("MatchesPlayed", p.matchesPlayed),
                                   new XElement("TotalRuns", p.totalRuns),
                                   new XElement("BallsBowled", p.ballsBowled),
                                   new XElement("WicketsTaken", p.wicketsTaken),
                                   new XElement("BallsStanded", p.ballsStanded),
                                   new XElement("RunsScored", p.runsScored),
                                   new XElement("Year", p.year)
                            )
                      );
            }
            doc.Save(new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read));
        }
        public static void Main(String[] args)
        {
            /*List<Player> players = new List<Player>()
            {
                new Player(){name="Raj",dob=new DateTime(1997,05,05),gender='M',type="Batsman",country="India",matchesPlayed=10,totalRuns=100,ballsStanded=24,runsScored=40,year=2014},
                new Player(){name="Sriram pandey",dob=new DateTime(1994,02,28),gender='M',type="Bowler",country="India",matchesPlayed=14,ballsBowled=18,wicketsTaken=2,year=2013},
                new Player(){name="Mohammed Moshin",dob=new DateTime(1994,03,16),gender='M',type="Batsman",country="India",matchesPlayed=20,totalRuns=250,ballsStanded=50,runsScored=150,year=2015},
                new Player(){name="Dinesh",dob=new DateTime(1995,05,18),gender='M',type="Bowler",country="India",matchesPlayed=20,ballsBowled=42,wicketsTaken=5,year=2017}
            };
            save(players, @"Z:\Players.xml");*/
           
            //Getting from xml document
            List<Player> plyrs=load(@"Z:\Players.xml");
            Console.WriteLine("\nBefore Updation \n" +
                "Name : {0}",plyrs[0].name);
            //Updation
            plyrs[0].name = "Rajkumar";
            plyrs[1].name ="Sriram";
            Console.WriteLine("\nAfter Updation \n" +
              "Name : {0}", plyrs[0].name);
            //Saving the updation
            save(plyrs, @"Z:\Players.xml");

            Console.WriteLine("\nPlayers with more than 3 wickets");
            foreach(Player player in plyrs.Where(p=>p.wicketsTaken>3).ToList())
            {
                Console.WriteLine("Player Name : {0}  Wickets taken : {1} ", player.name, player.wicketsTaken);
            }
            Console.ReadLine();
        }
    }

}
