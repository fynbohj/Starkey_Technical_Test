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
                int redTotal = 0;
                int yellowTotal = 0;
                int blueTotal = 0;
                int greenTotal = 0;

                for (int i = 0; i < entries?.Count; i++)
                {
                    var user = entries[i].User;
                    var command = entries[i].Command;
                    var timestamp = entries[i].Timestamp;

                    // user is not in dictionary
                    if (!d.ContainsKey(user))
                    {
                        var record = new Dictionary<string, string[]> 
                        {
                            {"red", [] },
                            {"yellow", [] },
                            {"blue", [] },
                            {"green", [] },
                        };

                        // take old array, add to it, replace it in dictionary
                        var temp = record[command];
                        record[command] = temp.Append(timestamp).ToArray();
                        d.Add(user, record);
                        
                    }

                    // user is already in dictionary
                    else
                    {
                        // take old array, add to it, replace it in dictionary
                        var dictionary = d[user];
                        var temp = dictionary[command];
                        dictionary[command] = temp.Append(timestamp).ToArray();
                        d[user] = dictionary;
                    }

                    // record what command was used for record keeping
                    if (command.Equals("red"))
                    {
                        redTotal++;
                    }
                    if (command.Equals("yellow"))
                    {
                        yellowTotal++;
                    }
                    if (command.Equals("blue"))
                    {
                        blueTotal++;
                    }
                    if (command.Equals("green"))
                    {
                        greenTotal++;
                    }
                }

                // Stats displayed in json
                var stats = new Dictionary<string, int>
                {
                    {"Number of Users", d.Count },
                    {"Number of Records", entries.Count },
                    {"Times Red Was Called", redTotal },
                    {"Times Yellow Was Called", yellowTotal },
                    {"Times Blue Was Called", blueTotal },
                    {"Times Green Was Called", greenTotal },

                };

                // Write the final dictionary to the output in json
                string result = "Statistics: " + JsonConvert.SerializeObject(stats, Formatting.Indented) + "\n" + JsonConvert.SerializeObject(d, Formatting.Indented);
                File.WriteAllText("output.json", result);
            }
        }
    }
}