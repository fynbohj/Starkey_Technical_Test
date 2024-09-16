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

                // create a dictionary for each user
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
                            }
                            if (entry.Command.Equals("yellow"))
                            {
                                yellow.Add(entry.Timestamp);
                            }
                            if (entry.Command.Equals("blue"))
                            {
                                blue.Add(entry.Timestamp);
                            }
                            if (entry.Command.Equals("green"))
                            {
                                green.Add(entry.Timestamp);
                            }
                        }
                    }

                    // create dictionary and add it to main dictionary
                    var record = new Dictionary<string, string[]>
                {
                    {"red", red.ToArray() },
                    {"yellow", yellow.ToArray() },
                    {"blue", blue.ToArray() },
                    {"green", green.ToArray() },
                };
                    d.Add(users[i], record);

                }
                File.WriteAllText("output.json", JsonConvert.SerializeObject(d, Formatting.Indented));

            }

        }
    }
}