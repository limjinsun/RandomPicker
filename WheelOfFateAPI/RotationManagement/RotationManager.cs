using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace WheelOfFateAPI.RotationManagement
{
    public static class RotationManager
    {
        public static Queue<HashSet<Engineer>> engineerQue;
        public static Dictionary<Engineer, int> candidateDictionary;
        public static List<Engineer> engineers;

        static RotationManager()
        {
            InitializePickingEnvironment();
        }

        private static void InitializePickingEnvironment()
        {
            engineers = LoadJsonData();
            engineerQue = new Queue<HashSet<Engineer>>();

            // 15 days of dummy data. 
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[0], engineers[1] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[2], engineers[3] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[4], engineers[5] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[6], engineers[7] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[8], engineers[9] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[0], engineers[1] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[2], engineers[3] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[4], engineers[5] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[6], engineers[7] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[8], engineers[9] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[0], engineers[1] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[2], engineers[3] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[4], engineers[5] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[6], engineers[7] });
            engineerQue.Enqueue(new HashSet<Engineer> { engineers[8], engineers[9] });

            // Initializing basic value.
            candidateDictionary = new Dictionary<Engineer, int>
            {
                { engineers[0], 0 },
                { engineers[1], 0 },
                { engineers[2], 0 },
                { engineers[3], 0 },
                { engineers[4], 0 },
                { engineers[5], 0 },
                { engineers[6], 0 },
                { engineers[7], 0 },
                { engineers[8], 0 },
                { engineers[9], 0 }
            };
        }

        public static HashSet<Engineer> PickEngineers()
        {
            return RunPickingAlgorythm();
        }

        private static HashSet<Engineer> RunPickingAlgorythm()
        {
            var candidateList = new List<Engineer>();
            var rnd = new Random();
            var newPair = new HashSet<Engineer>(); // can't use clear() somehow.

            //candidateList.Clear();
            candidateDictionary.Clear();

            engineerQue.Dequeue(); // remove oldest pair from pool.

            // fill in candidate pool.
            foreach (HashSet<Engineer> set in engineerQue)
            {
                foreach (Engineer s in set.ToArray<Engineer>())
                {
                    candidateList.Add(s);
                }
            } 

            // remove yesterday's engineers from candidate pool
            candidateDictionary[candidateList[candidateList.Count - 1]] = 100; // set to max value temporary - prevent miss caculation.
            candidateList.RemoveAll(e => e == candidateList[candidateList.Count - 1]);
            candidateDictionary[candidateList[candidateList.Count - 1]] = 100; // set to max value temporary - prevent miss caculation.
            candidateList.RemoveAll(e => e == candidateList[candidateList.Count - 1]);

            // calculate how many time each member has been done rota.
            foreach (var grp in candidateList.GroupBy(e => e))
            {
                candidateDictionary[grp.Key] = grp.Count();
            }

            // if someone has done less than 2 rota, put them in a new pair.
            if (candidateDictionary.ContainsValue(1))
            {
                var dutyEngineer = candidateDictionary.FirstOrDefault(x => x.Value == 1).Key;
                newPair.Add(dutyEngineer);
                candidateDictionary[dutyEngineer] = 100; // set to max value temporary - prevent miss operation;
            }
            if (candidateDictionary.ContainsValue(1))
            {
                var dutyEngineer = candidateDictionary.FirstOrDefault(x => x.Value == 1).Key;
                newPair.Add(dutyEngineer);
                candidateDictionary[dutyEngineer] = 100; // set to max value temporary - prevent miss operation;
            }

            // if candidate has not been fulled, get random candidate.
            if (newPair.Count < 2)
            {
                newPair.Add(candidateList[rnd.Next(0, candidateList.Count)]);
            }
            while (newPair.Count < 2)
            {
                Engineer v = candidateList[rnd.Next(0, candidateList.Count)];
                newPair.Add(v);
            }

            // put them in a que.
            engineerQue.Enqueue(newPair);

            return newPair;
        }

        public static List<Engineer> LoadJsonData()
        {
            using (StreamReader r = new StreamReader("employees.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Engineer>>(json); 
            }
        }

        public static Engineer GetEngineerById (int id)
        {
            return engineers[id-1];
        }
    }

    public class Engineer
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("gender")]
        public Gender Gender { get; set; }

        [JsonProperty("employeeId")]
        public long EmployeeId { get; set; }

        [JsonProperty("ppsn")]
        public string Ppsn { get; set; }


        public override string ToString()
        {
            return "Engineer" + Id;
        }
    }

    public enum Gender { Female, Male };
}






































/**
 *
 *
 *
 *
 * public static HashSet<Engineer> PickEngineers()
        {
            var queList = new List<Engineer>();
            var candidateList = new List<Engineer>();
            var newPair = new HashSet<Engineer>();
            var rnd = new Random();

            return RunPickingAlgorythm(queList, candidateList, newPair, rnd);
        }





 * private static HashSet<Engineer> RunPickingAlgorythm(List<Engineer> queList, List<Engineer> candidateList, HashSet<Engineer> newPair, Random rnd)
        {
            // Before run algorythm, reset the temporary data structures for candidates.
            queList.Clear();
            candidateList.Clear();
            newPair = new HashSet<Engineer>(); // can't use clear() somehow.
            candidateDictionary.Clear();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("This is before Dequeue first member ====================");
            foreach (HashSet<Engineer> set in engineerQue)
            {
                foreach (Engineer s in set.ToArray<Engineer>())
                {
                    Console.WriteLine(s);
                }
            }
            Console.WriteLine("count : " + engineerQue.Count);
            Console.WriteLine("====================");

            engineerQue.Dequeue();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("This is after Dequeue first member ====================");
            foreach (HashSet<Engineer> set in engineerQue)
            {
                foreach (Engineer s in set.ToArray<Engineer>())
                {
                    Console.WriteLine(s);
                }
            }
            Console.WriteLine("count : " + engineerQue.Count);
            Console.WriteLine("====================");

            foreach (HashSet<Engineer> set in engineerQue)
            {
                foreach (Engineer s in set.ToArray<Engineer>())
                {
                    candidateList.Add(s);
                }
            }

            candidateDictionary[candidateList[candidateList.Count - 1]] = 100; // set to max value temporary - prevent miss caculation.
            candidateList.RemoveAll(e => e == candidateList[candidateList.Count - 1]);
            candidateDictionary[candidateList[candidateList.Count - 1]] = 100; // set to max value temporary - prevent miss caculation.
            candidateList.RemoveAll(e => e == candidateList[candidateList.Count - 1]);

            Console.WriteLine("candidate dictionary details");
            foreach (KeyValuePair<Engineer, int> entry in candidateDictionary)
            {
                // do something with entry.Value or entry.Key
                Console.Write("{0}-{1} | ", entry.Key, entry.Value);
            }
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Lastmember removed from candidate");
            Console.WriteLine(candidateList.Count);
            foreach (Engineer s in candidateList)
            {
                Console.Write(s);
            }
            Console.WriteLine();

            foreach (var grp in candidateList.GroupBy(e => e))
            {
                Console.Write("{0}-{1} | ", grp.Key, grp.Count());
                candidateDictionary[grp.Key] = grp.Count();
            }
            Console.WriteLine();


            Console.WriteLine("candidate dictionary details after update");
            foreach (KeyValuePair<Engineer, int> entry in candidateDictionary)
            {
                // do something with entry.Value or entry.Key
                Console.Write("{0}-{1} | ", entry.Key, entry.Value);
            }
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("This is before Enqueue new Pair ====================");
            foreach (HashSet<Engineer> set in engineerQue)
            {
                foreach (Engineer s in set.ToArray<Engineer>())
                {
                    Console.WriteLine(s);
                }
            }
            Console.WriteLine("count : " + engineerQue.Count);
            Console.WriteLine("====================");

            Console.WriteLine();
            Console.WriteLine("Check if engineer who have shift less than 2, if exist, put them in a newPair");
            if (candidateDictionary.ContainsValue(1))
            {
                var dutyEngineer = candidateDictionary.FirstOrDefault(x => x.Value == 1).Key;
                newPair.Add(dutyEngineer);
                candidateDictionary[dutyEngineer] = 100; // set to max value temporary - prevent miss operation;
                Console.WriteLine("dutyEngineer : " + dutyEngineer);
            }
            if (candidateDictionary.ContainsValue(1))
            {
                var dutyEngineer = candidateDictionary.FirstOrDefault(x => x.Value == 1).Key;
                newPair.Add(dutyEngineer);
                candidateDictionary[dutyEngineer] = 100; // set to max value temporary - prevent miss operation;
                Console.WriteLine("dutyEngineer : " + dutyEngineer);
            }
            Console.WriteLine("====================");

            Console.WriteLine();
            Console.WriteLine();
            if (newPair.Count < 2)
            {
                newPair.Add(candidateList[rnd.Next(0, candidateList.Count)]);
            }
            while (newPair.Count < 2)
            {
                Engineer v = candidateList[rnd.Next(0, candidateList.Count)];
                newPair.Add(v);
            }
            Console.WriteLine("NewPair is : ");
            foreach (var e in newPair)
            {
                Console.Write(e);
            }
            Console.WriteLine();
            engineerQue.Enqueue(newPair);

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("This is after Enqueue new Pair ====================");
            foreach (HashSet<Engineer> set in engineerQue)
            {
                foreach (Engineer s in set.ToArray<Engineer>())
                {
                    Console.WriteLine(s);
                    queList.Add(s);
                }
            }
            Console.WriteLine("count : " + engineerQue.Count);
            Console.WriteLine("====================");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("This is current que by group");

            foreach (var grp in queList.GroupBy(e => e))
            {
                Console.Write("{0}-{1} | ", grp.Key, grp.Count());
            }
            Console.WriteLine();
           
            return newPair;
        }



    */
