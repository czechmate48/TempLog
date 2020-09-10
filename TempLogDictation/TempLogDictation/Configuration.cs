using System;
using System.Collections.Generic;

public class Configuration
{
    /// <summary>
    /// Loads a configuration file. A configuration file should have 'variable=value' structure set on new lines of a text file
    /// Ex. car=ford
    ///     tires=firestone
    /// </summary>

    public string path { get; set; }
    public Dictionary<string, string> contents { get; set; } //Variable, Value [variable=value]

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
