using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TransformerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SimpleTest()
        {
            string[] transformers = new string[]
            {
                "Soundwave, D, 8,9,2,6,7,5,6,10",
                "Bluestreak, A, 6,6,7,9,5,2,9,7",
                "Hubcap: A, 4,4,4,4,4,4,4,4"
            };

            StringWriter results = new StringWriter();
            if( Transformers.Game.Run(transformers, results) == false )
            {
                Console.WriteLine("Bad arguements");
            }

            string text = results.ToString();
            Console.WriteLine(text);
        }

        [TestMethod]
        public void SimpleTestReversedEntries()
        {
            string[] transformers = new string[]
            {
                "Soundwave, D, 8,9,2,6,7,5,6,10",
                "Hubcap: A, 4,4,4,4,4,4,4,4",
                "Bluestreak, A, 6,6,7,9,5,2,9,7"
            };

            StringWriter results = new StringWriter();
            if( Transformers.Game.Run(transformers, results) == false )
            {
                Console.WriteLine("Bad arguements");
            }

            string text = results.ToString();
            Console.WriteLine(text);
        }

        [TestMethod]
        public void ByRank()
        {
            string[] transformers = new string[]
            {
                "Bonecrusher, D, 9,3,2,9,4,8,6,6",
                "Hubcap: A, 4,4,4,4,4,4,4,4",
                "Afterburner: A, 7,6,6,6,5,9,7,7",
            };

            StringWriter results = new StringWriter();
            if( Transformers.Game.Run(transformers, results) )
            {
                Console.WriteLine("Bad arguements");
            }

            string text = results.ToString();
            Console.WriteLine(text);
        }

        [TestMethod]
        public void NoAutobots()
        {
            string[] transformers = new string[]
            {
                "Bonecrusher, D, 9,3,2,9,4,8,6,6"
            };

            StringWriter results = new StringWriter();
            if( Transformers.Game.Run(transformers, results) == false )
            {
                Console.WriteLine("Bad arguements");
            }

            string text = results.ToString();
            Console.WriteLine(text);
        }

        [TestMethod]
        public void NoDecepticons()
        {
            string[] transformers = new string[]
            {
                "Hubcap: A, 4,4,4,4,4,4,4,4",
                "Afterburner: A, 7,6,6,6,5,9,7,7",
            };

            StringWriter results = new StringWriter();
            if( Transformers.Game.Run(transformers, results) == false )
            {
                Console.WriteLine("Bad arguements");
            }

            string text = results.ToString();
            Console.WriteLine(text);
        }

        [TestMethod]
        public void BadData()
        {
            string[] transformers = new string[]
            {
                "Hubcap: A, 4,4,C,4,4,4,4,4",
                "Afterburner: A, 7,6,6,6,5,9,7,7",
            };

            StringWriter results = new StringWriter();
            if( Transformers.Game.Run(transformers, results) == false )
            {
                Console.WriteLine("Bad arguements");
            }

            string text = results.ToString();
            Console.WriteLine(text);
        }

        [TestMethod]
        public void MultipleEntries()
        {
            string[] transformers = new string[]
            {
            "Blitzwing: D, 8,5,9,8,6,7,7,7",
            "Blastoff: D, 3,8,10,3,5,5,9,10",
            "CouterPunch, D, 6,9,4,6,7,10,6,9",
            "Fangry: D, 6,8,6,8,6,8,6,8",
            "Hubcap: A, 4,4,4,4,4,4,4,4",
            "Afterburner: A, 7,6,6,6,5,9,7,7",
            "Blaster: A, 8,8,2,8,7,9,7,9",
            "Crosshairs: A, 6,8,4,8,6,8,7,9"
            };

            StringWriter results = new StringWriter();
            if( Transformers.Game.Run(transformers, results) == false)
            {
                Console.WriteLine("Bad arguements");
            }

            string text = results.ToString();
            Console.WriteLine(text);
        }

        [TestMethod]
        public void Tie()
        {
            string[] transformers = new string[]
            {
            "Blitzwing: D, 8,5,9,8,6,7,7,7",
            "Blastoff: D, 3,8,10,3,5,5,9,10",
            "CouterPunch, D, 6,9,4,6,7,10,6,9",
            "Fangry: D, 6,8,6,8,6,8,6,8",
            "Fortress Maximus: A, 10,10,10,9,10,10,10,9",
            "Afterburner: A, 7,6,6,6,5,9,7,7",
            "Blaster: A, 8,8,2,8,7,9,7,9",
            "Crosshairs: A, 6,8,4,8,6,8,7,9"
            };

            StringWriter results = new StringWriter();
            if( Transformers.Game.Run(transformers, results) == false )
            {
                Console.WriteLine("Bad arguements");
            }

            string text = results.ToString();
            Console.WriteLine(text);
        }


        [TestMethod]
        public void Optumus()
        {
            string[] transformers = new string[]
            {
            "Blitzwing: D, 8,5,9,8,6,7,7,7",
            "Blastoff: D, 3,8,10,3,5,5,9,10",
            "Optimus Prime: A, 10,10,8,10,10,10,8,10",
            "Afterburner: A, 7,6,6,6,5,9,7,7"
            };

            StringWriter results = new StringWriter();
            if (Transformers.Game.Run(transformers, results) == false)
            {
                Console.WriteLine("Bad arguements");
            }

            string text = results.ToString();
            Console.WriteLine(text);
        }

        [TestMethod]
        public void Predaking()
        {
            string[] transformers = new string[]
            {
            "Predaking: D, 10,5,11,8,7,9,9,8",
            "Blastoff: D, 3,8,10,3,5,5,9,10",
            "Rodimus Prime: A, 10,10,9,10,10,10,9,10",
            "Afterburner: A, 7,6,6,6,5,9,7,7"
            };

            StringWriter results = new StringWriter();
            if (Transformers.Game.Run(transformers, results) == false)
            {
                Console.WriteLine("Bad arguements");
            }

            string text = results.ToString();
            Console.WriteLine(text);
        }

        [TestMethod]
        public void FocedEnding()
        {
            string[] transformers = new string[]
            {
            "Blastoff: D, 3,8,10,3,5,5,9,10",
            "Predaking: D, 10,5,11,8,7,9,9,8",
            "Afterburner: A, 7,6,6,6,5,9,7,7",
            "Optimus Prime: A, 10,10,8,10,10,10,8,10"
            };

            StringWriter results = new StringWriter();
            if (Transformers.Game.Run(transformers, results) == false)
            {
                Console.WriteLine("Bad arguements");
            }

            string text = results.ToString();
            Console.WriteLine(text);
        }
    }
}

