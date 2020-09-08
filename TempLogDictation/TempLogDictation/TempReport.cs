using System;

public class TempReport
{
    public string name { get; set; }
    public string temp { get; set; }

    public TempReport(string name, string temp)
    {
        this.name = name;
        this.temp = temp;
    }

    public string Get_Report()
    {
        return (DateTime.Now + " : " + name + " " + temp);
    }
}
