using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KeletsoMoselakgomoAssessment.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderName = @"C:\Temp\";
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            string path = "BDG_Input.txt";
            var jsonString = convertCsvFileToJsonObject(path);
            Console.WriteLine(jsonString);
            createJsonFile(jsonString);
        }

        static string convertCsvFileToJsonObject(string path)
        {
            var csv = new List<string[]>();
            string headers = "Id|Name|Surname|Email|Gender|Salary";

            List<string> ltLines = File.ReadAllLines(path).ToList();
            ltLines.Insert(0,headers);

            foreach (string line in ltLines)
                csv.Add(line.Split('|'));

            var properties = ltLines[0].Split('|');

            var listObjResult = new List<Dictionary<string, string>>();

            for (int i = 1; i < ltLines.Count; i++)
            {
                var objResult = new Dictionary<string, string>();
                for (int j = 0; j < properties.Length; j++)
                    objResult.Add(properties[j], csv[i][j]);

                listObjResult.Add(objResult);
            }

            return JsonConvert.SerializeObject(listObjResult);
        }

        static void createJsonFile(string jsonData)
        {
            string fileName = @"C:\Temp\BDG_Output.json";
            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file    
                    Byte[] data = new UTF8Encoding(true).GetBytes(jsonData);
                    fs.Write(data, 0, data.Length);
                }

                // Open the stream and read it back.    
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}
