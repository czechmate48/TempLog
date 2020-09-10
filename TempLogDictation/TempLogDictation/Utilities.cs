using System;
using System.IO;

public class Utilities
{
    /// <summary>
    /// Used to create Temperature Commands
    /// </summary>
    /// <param name="filePath"></param>

    public static void Create_Temp_File(string filePath)
    {
        StreamWriter file2 = new StreamWriter(filePath, true);

        for (float i = 0; i < 125; i += .1f)
        {
            file2.WriteLine(Math.Round(i, 1));
        }

        file2.Flush();
        file2.Close();
    }
}
