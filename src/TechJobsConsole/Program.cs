using System;
using System.Collections.Generic;
using System.Linq;

namespace TechJobsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            // Column options
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            // Allow user to search/list until they manually quit with ctrl+c
            while (true)
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices);

                if (actionChoice.Equals("list"))
                {
                    Console.Clear();
                    string columnChoice = GetUserSelection("List", columnChoices);

                    if (columnChoice.Equals("all"))
                    {
                        Console.Clear();
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice);
                        Console.Clear();
                        results.Sort();
                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        { 
                            Console.WriteLine(item);
                        }
                    }
                }
                else // choice is "search"
                {
                    // How does the user want to search (e.g. by skill or employer)
                    string columnChoice = GetUserSelection("Search", columnChoices);
                    // What is their search term?
                    Console.WriteLine("\nSearch term: ");
                    string searchTerm = Console.ReadLine();
                    List<Dictionary<string, string>> searchResults;
                    // Fetch results
                    if (columnChoice.Equals("all"))
                    {  Console.Clear();

                        searchResults = JobData.FindByValue(searchTerm);
                        if (searchResults.Count == 0)
                        {
                            Console.WriteLine("\n No results were found for your search!");
                        }
                        else
                           PrintJobs(searchResults);
                        
                    }
                    else
                    {   
                        searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        if (searchResults.Count == 0)
                        {
                            Console.WriteLine("\n No results were found for your search!");
                        }
                        else
                            PrintJobs(searchResults);
                    }
                }
            }
        }

        /*
         * Returns the key of the selected item from the choices Dictionary
         */
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx = -1, value, i=0;
            bool isValidChoice = false;
            string input;
            string[] choiceKeys = new string[choices.Count];

         
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                Console.WriteLine("\n" + choiceHeader + " by:");

                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                }
                input = Console.ReadLine();
                if (!int.TryParse(input, out value))
                    Console.WriteLine("Invalid choices. Please try again.");
                else
                {
                    choiceIdx = int.Parse(input);
                    if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                    {
                        Console.WriteLine("Invalid choices. Please try again.");
                    }
                    else
                    {
                        isValidChoice = true;
                    }
                }
                

            } while (!isValidChoice);

            return choiceKeys[choiceIdx];
        }

        private static void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            
            foreach (Dictionary<string, string> jobs in someJobs)
            {
                Console.WriteLine("\t*****");
                foreach (KeyValuePair<string, string> job in jobs)
                {
                    Console.WriteLine(string.Format("\t{0}: {1}", job.Key, job.Value));
                }
                Console.WriteLine("\t*****\n");
                
            }
            Console.ReadKey();

        }
    }
}
