using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Transformers
{

    /// <summary>
    /// Maintains the capabilities of the robots that make up autobots and decepticon
    /// </summary>
    internal class Robot : IEquatable<Robot>, IComparable<Robot>
    {
        string name;   // Name of the robot 
        bool supreme;  // Is this a special robot that has special fighting abilities

        internal Robot(string n)
        {
            name = n;
        }

        internal bool Supreme
        {
            get => supreme;
        }

        protected bool Special
        {
            set => supreme = value;
        }

        // All transformer have a name
        internal string Name
        {
            get => name;
        }

        // Status maintains the result of the last fight - true for win, false for lose
        internal bool Status { get; set; }

        // Transformer standard properties
        internal int Strength { get; set; }
        internal int Intelligence { get; set; }
        internal int Speed { get; set; }
        internal int Endurance { get; set; }
        internal int Rank { get; set; }
        internal int Courage { get; set; }
        internal int Firepower { get; set; }
        internal int Skill { get; set; }

        internal int Rating()
        {
            return Strength + Intelligence + Speed + Endurance + Firepower;
        }

        /// <summary>
        /// Determine who would win in a fight
        /// </summary>
        /// <param name="other"></param>
        /// <returns> 
        /// null - fight was a draw
        /// true - this robotw would win
        /// false - the other robot would win 
        /// </returns>
        internal bool? Test(Robot other)
        {
            int courageDif = Courage - other.Courage;

            // If the courage is different by 4 or more and the strength
            // is different by 3 between the two the fight is over
            if (courageDif > 3)
            {
                int strengthDif = Strength - other.Strength;
                if (strengthDif > 2)
                {
                    return true;
                }
            }
            else if (courageDif < -3)
            {
                int strengthDif = Strength - other.Strength;
                if (strengthDif < -2)
                {
                    return false;
                }
            }

            // If the skill is different by 3 or more the fight is over
            int skillDiff = Skill - other.Skill;
            if (Math.Abs(skillDiff) > 2)
            {
                if (skillDiff > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // Otherwise the robot who has the greatest rating
            if (Rating() > other.Rating())
            {
                return true;
            }
            else if (Rating() < other.Rating())
            {
                return false;
            }

            // Return null if the ratings are the same
            return null;
        }

        // Equals and CompareTo are used for Sorting
        public bool Equals(Robot other)
        {
            if (other == null)
            {
                return false;
            }
            return Rating() == other.Rating();
        }

        // Used for the 
        public int CompareTo(Robot other)
        {
            if (other == null)
            {
                return 1;
            }
            return Rating() - other.Rating();
        }
    }

    internal class Autobot : Robot
    {
        internal Autobot(string n) : base(n)
        {
            if (string.Compare(n, "Optimus Prime", true) == 0)
            {
                Special = true;
            }
        }
    }

    internal class Deception : Robot
    {
        internal Deception(string n) : base(n)
        {
            if (string.Compare(n, "Predaking", true) == 0)
            {
                Special = true;
            }
        }
    }

    /// <summary>
    /// Team makes up one type of robot - team can be sorted 
    /// </summary>
    internal class Team
    {
        List<Robot> members;

        internal String Name { get; set; }

        internal Robot member(int i)
        {
            return members[i];
        }

        internal int Count
        {
            get => members.Count;
        }

        internal Team()
        {
            members = new List<Robot>();
        }


        /// <summary>
        /// Output of the members that fought and won or did not fight
        /// </summary>
        /// <param name="results"></param>
        /// <param name="battles"></param>
        internal void WriteTeam(StringWriter results, int battles)
        {
            int i = 0;
            // If the member was in a battle use their
            // status to determine if they survived
            // The number of battles is the size of the smallest
            // team so will be no larger than this team.
            string prefix = "";
            for (; i < battles; i++)
            {
                if (members[i].Status)
                {
                    results.Write("{0}{1}", prefix, members[i].Name);
                    if (prefix.Length == 0)
                    {
                        prefix = ", ";
                    }
                }
            }
            // If they member was not in a battle then
            // just print out their name, by definition they
            // survived.
            for (; i < members.Count; i++)
            {
                results.Write("{0}{1}", prefix, members[i].Name);
                if (prefix.Length == 0)
                {
                    prefix = ", ";
                }
            }
            results.WriteLine();
        }

        internal void Add(Robot bot)
        {
            members.Add(bot);
        }

        internal void Sort()
        {
            members.Sort((a, b) => -1 * a.CompareTo(b));
        }
    }

    /// <summary>
    /// Peform one battle between two teams of robots
    /// </summary>
    internal class Battle
    {
        bool allLose;
        int battles;
        int autobotsWins;
        int decepticonWins;
        Team autobots;
        Team decepticons;

        void AutobotWinner(Robot autobot, Robot decepticon)
        {
            autobotsWins++;
            autobot.Status = true;
            decepticon.Status = false;
        }

        void DecepticontWinner(Robot autobot, Robot decepticon)
        {
            decepticonWins++;
            autobot.Status = false;
            decepticon.Status = true;
        }

        internal Battle(Team a, Team d)
        {
            allLose = false;
            battles = 0;
            autobotsWins = 0;
            decepticonWins = 0;
            autobots = a;
            decepticons = d;
        }

        internal void DoBattle(StringWriter results)
        {
            Team winner = null;
            Team loser = null;

            if (autobots.Count == 0)
            {
                if (decepticons.Count == 0)
                {
                    // If there are no entries for either teams
                    // then there is no battle.
                    results.WriteLine("No battle");
                    return;
                }
                else
                {
                    // If there are no autobots but there are decepticons then
                    // they win, no battle is necessary
                    winner = decepticons;
                }
            }
            else if (decepticons.Count == 0)
            {
                // if there are not decepticons but there are autobots then 
                // they win, no battle is necessary
                winner = autobots;
            }
            else
            {
                // Teams are sorted by all overall ratings and matched against each other.
                // There are two special transformers,  Optimus Prime (autodbots) and Predaking (decepticons).
                // If they battle other transformers then they win, if they battle each other everyone loses
                // and the game is over
                for (; battles < autobots.Count && battles < decepticons.Count; battles++)
                {
                    // Using the two fighters see if they need to battle - autobot and decepticon only need to 
                    // battle if they are not special
                    Robot autobot = autobots.member(battles);
                    Robot decepticon = decepticons.member(battles);
                    if (autobot.Supreme)
                    {
                        if (decepticon.Supreme)
                        {
                            allLose = true;
                            break;
                        }
                        else
                        {
                            AutobotWinner(autobot, decepticon);
                        }
                    }
                    else if (decepticon.Supreme)
                    {
                        DecepticontWinner(autobot, decepticon);
                    }
                    else
                    {
                        // When true is returned then the autobot bested
                        // the decepticon, false means the decepticon bested
                        // the autobot and null beens neither won.
                        bool? result = autobot.Test(decepticon);
                        if (result == null)
                        {
                            // Tie in battle - both lose
                            autobot.Status = false;
                            decepticon.Status = false;
                        }
                        else
                        {
                            if ((bool)result)
                            {
                                AutobotWinner(autobot, decepticon);
                            }
                            else
                            {
                                DecepticontWinner(autobot, decepticon);
                            }
                        }
                    }
                }
            }

            // Write out the result from the battle
            results.WriteLine("{0} battle", battles);
            if (allLose)
            {
                results.WriteLine("No Winner");
                results.WriteLine("Survivors from the losing team({0}, {1}):", autobots.Name, decepticons.Name);
            }
            else
            {
                // If a winner has not been set then check to see who won
                // the most battles
                if (winner == null)
                {
                    // Give the tie to good instead of evil (not in the rules but adding assumption)
                    if (autobotsWins >= decepticonWins)
                    {
                        winner = autobots;
                        loser = decepticons;
                    }
                    else
                    {
                        winner = decepticons;
                        loser = autobots;
                    }
                }
                results.Write("Winning team ({0}): ", winner.Name);
                winner.WriteTeam(results, battles);
                if (loser != null)
                {
                    results.Write("Survivors from the losing team ({0}): ", loser.Name);
                    loser.WriteTeam(results, battles);
                }
            }
        }
    }


    /// <summary>
    /// Game takes a list of robots, creates teams, runs a battle and outputs the
    /// outcome to the output stream.
    /// </summary>
    public class Game
    {
        static public bool Run(string[] entries, StringWriter results)
        {
            Team autobots = new Team();
            Team decepticons = new Team();

            autobots.Name = "Autobots";
            decepticons.Name = "Decepticons";

            //
            // Requires a writer for results and a list of entries.
            if (results == null || entries == null)
            {
                return false;
            }

            string current = null;
            try
            {
                char[] delimiters = new char[] { ',', ':' };
                foreach (string entry in entries)
                {
                    current = entry;
                    string[] values = entry.Split(delimiters);
                    Robot bot;
                    Team team;
                    if (String.Compare(values[1].Trim(), "A", true) == 0)
                    {
                        bot = new Autobot(values[0].Trim());
                        team = autobots;
                    }
                    else
                    {
                        bot = new Deception(values[0].Trim());
                        team = decepticons;
                    }

                    bot.Strength = int.Parse(values[2].Trim());
                    bot.Intelligence = int.Parse(values[3].Trim());
                    bot.Speed = int.Parse(values[4].Trim());
                    bot.Endurance = int.Parse(values[5].Trim());
                    bot.Rank = int.Parse(values[6].Trim());
                    bot.Courage = int.Parse(values[7].Trim());
                    bot.Firepower = int.Parse(values[8].Trim());
                    bot.Skill = int.Parse(values[9].Trim());

                    team.Add(bot);
                }
            }
            catch (Exception e)
            {
                results.WriteLine(e.ToString());
                results.WriteLine("Bad line: {0}", current);
                return false;
            }

            // Sort the teams to match up the highest ranking 
            autobots.Sort();
            decepticons.Sort();
            Battle skirmish = new Battle(autobots, decepticons);

            skirmish.DoBattle(results);
            return true;
        }
    }
}