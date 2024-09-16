using System;
using System.Net.Http.Json;
using System.Text.Json;

public class JsonReader
{
	public JsonReader()
	{

	}

    public void LoadJson()
    {
        using (StreamReader r = new StreamReader("input.json"))
        {
            string json = r.ReadToEnd();
                       
        }
    }


}