using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Transactions;

namespace Class1Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file = "ticket.csv";
            string userInput;

            StreamWriter sw = new StreamWriter(file, true);
            sw.Close();

            StreamReader sr = new StreamReader(file);
            if (sr.ReadLine() == null)
            {
                sr.Close();
                sw = new StreamWriter(file);
                sw.WriteLine("TicketID,Summary,Status,Priority,Submitter,Assigned,Watching");

                sw.Close();
            }
            else
            {
                sr.Close();
            }

            do
            {
                Console.WriteLine("1. View tickets");
                Console.WriteLine("2. Create ticket");
                Console.WriteLine("Enter any other key to exit");

                userInput = Console.ReadLine();

                if (userInput == "1") // Read file
                {
                    sr = new StreamReader(file);

                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        string[] arr = line.Split(',');

                        Console.WriteLine($"TicketID: {arr[0]}\nSummary: {arr[1]}\nStatus: {arr[2]}\nPriority: {arr[3]}\n" +
                            $"Submitter: {arr[4]}\nAssigned: {arr[5]}\nWatchers: {arr[6]}\n");
                    }

                    sr.Close();
                }
                else if (userInput == "2") // User input to write file
                {
                    sw = new StreamWriter(file, true);

                    Console.WriteLine("Enter TicketID:");
                    string ticketID = Console.ReadLine();

                    Console.WriteLine("Enter a summary of issue:");
                    string summary = Console.ReadLine();

                    Console.WriteLine("Enter Status:");
                    string status = Console.ReadLine();

                    Console.WriteLine("Enter Priority:");
                    string priority = Console.ReadLine();

                    Console.WriteLine("Enter person who submitted ticket:");
                    string submitter = Console.ReadLine();

                    Console.WriteLine("Enter assigned person name:");
                    string assigned = Console.ReadLine();

                    List<string> watchers = new List<string>();

                    bool end = false;

                    do
                    {
                        Console.WriteLine("Add watcher? Y/N:");
                        string inputWatcher = Console.ReadLine();
                        if (inputWatcher == "Y")
                        {
                            Console.WriteLine("Enter name of watchers:");
                            watchers.Add(Console.ReadLine());
                        }
                        else if (inputWatcher == "N")
                        {
                            end = true;
                        }
                    } while (!end);

                    string watchersList = "None";
                    var watchersArr = watchers.ToArray();

                    if (watchers.Count > 1)
                    {
                        watchersList = watchers[0];
                        int counter = 0;

                        foreach (string i in watchersArr)
                        {
                            watchersList = watchersList + "|" + watchers[counter + 1];
                            counter++;
                            if (counter + 1 == watchersArr.Length)
                            {
                                break;
                            }
                        }
                    }
                    else if (watchers.Count == 1)
                    {
                        watchersList= watchers[0];
                    }

                    sw.WriteLine($"{ticketID},{summary},{status},{priority},{submitter},{assigned},{watchersList}");

                    sw.Close();
                }
            } while (userInput == "1" || userInput == "2");
        }
    }
}