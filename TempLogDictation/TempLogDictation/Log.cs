using System;
using System.IO;

public class Log
{
    /// <summary>
    /// Specifies the path for the temperature log. Provides a mechanism to write a temperature report to the temperature log. 
    /// </summary>

    public string path { get; set; }

    public Log() { } 

    public Log (string path)
	{
        this.path = path;
	}

    public void Write_TempReport(TempReport tr)
    {
        StreamWriter file = new StreamWriter(path, true);
        file.WriteLine(tr.Get_Report());
        file.Flush();
        file.Close();
    }
}

