using System;
using System.Collections.Generic;

public class Configuration
{
    public string path { get; set; }
    public Dictionary<string, string> contents { get; set; } //Variable Name, String in file

	public Configuration(string path)
	{
        this.path = path;
        contents = new Dictionary<string, string>();
    }

    public void Load()
    {
        string[] lines = System.IO.File.ReadAllLines(@path);

        foreach (string line in lines)
        {
            string[] value = line.Split('=');
            if (value.Length > 1) contents[value[0]] = value[1];
            else contents[value[0]] = value[0];
        }
    }
}
