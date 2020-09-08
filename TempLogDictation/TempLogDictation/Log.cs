using System;
using System.IO;

public class Log
{
    public string path { get; set; }

    public Log() { } //Needed for initialization

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
