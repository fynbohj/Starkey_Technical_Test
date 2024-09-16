using Newtonsoft.Json;

namespace Starkey_Technical
{
    internal class Entry()
    {
        public string Command { get; set; }
        public string Timestamp { get; set; }
        public string User { get; set; }
    }

    public class Program()
    {
        public static void Main()
        {
            using (StreamReader r = new StreamReader("input.json"))
            {
                // Deserialize the json file into a list of entries
                string json = r.ReadToEnd();
                List<Entry>? entries = JsonConvert.DeserializeObject<List<Entry>>(json);
                var d = new Dictionary<string, Dictionary<string, string[]>> { };

                // Declarations for statistics
                int redCount = 0;
                int yellowCount = 0;
                int blueCount = 0;
                int greenCount = 0;

                // Get list of all unique users in json file
                List<string>? users = new List<string> { };
                for (int i = 0; i < entries?.Count; i++)
                {
                    if (!users.Contains(entries[i].User))
                    {
                        users.Add(entries[i].User);
                    }
                }
                users.Sort();

                // Create a dictionary for each user
                for (int i = 0; i < users.Count; i++)
                {
                    List<string> red = new List<string>();
                    List<string> yellow = new List<string>();
                    List<string> blue = new List<string>();
                    List<string> green = new List<string>();

                    for (int j = 0; j < entries?.Count; j++)
                    {
                        Entry entry = entries[j];

                        if (entry.User.Equals(users[i]))
                        {
                            if (entry.Command.Equals("red"))
                            {
                                red.Add(entry.Timestamp);
                                redCount++;
                            }
                            if (entry.Command.Equals("yellow"))
                            {
                                yellow.Add(entry.Timestamp);
                                yellowCount++;
                            }
                            if (entry.Command.Equals("blue"))
                            {
                                blue.Add(entry.Timestamp);
                                blueCount++;
                            }
                            if (entry.Command.Equals("green"))
                            {
                                green.Add(entry.Timestamp);
                                greenCount++;
                            }
                        }
                    }

                    // Create dictionary and add it to main dictionary
                    var record = new Dictionary<string, string[]>
                {
                    {"red", red.ToArray() },
                    {"yellow", yellow.ToArray() },
                    {"blue", blue.ToArray() },
                    {"green", green.ToArray() },
                };
                    d.Add(users[i], record);
                }

                // Stats displayed in json
                var stats = new Dictionary<string, int>
                {
                    {"Number of Users", users.Count },
                    {"Times Red Was Called", redCount },
                    {"Times Yellow Was Called", yellowCount },
                    {"Times Blue Was Called", blueCount },
                    {"Times Green Was Called", greenCount },

                };

                string result = "Statistics: " + JsonConvert.SerializeObject(stats, Formatting.Indented) + "\n" + JsonConvert.SerializeObject(d, Formatting.Indented);

                // Write the final dictionary to the output in json
                 File.WriteAllText("output.json", result);
            }
        }
    }
}