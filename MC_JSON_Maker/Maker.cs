using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MC_JSON_Maker
{
    class fileContents
    {
        public string parent { get; set; }
        public Dictionary<String, String> textures { get; set; }
    }
    class Maker
    {
        static void Main(string[] args)
        {
            string filePath = "./MC_JSON_Maker_RunCounter.txt";
            int count = 0;
            if(File.Exists(filePath))
            {
                try
                {
                    StreamReader sr = new StreamReader(filePath);
                    var line = sr.ReadLine();
                    sr.Close();
                    if(!int.TryParse(line, out count))
                    {
                        Console.WriteLine("Could not parse int\n");
                    }
                }
                catch(Exception fileReadError)
                {
                    Console.WriteLine("Failed to read file:\n" + fileReadError);
                }
            }
            else
            {
                try
                {
                    File.CreateText(filePath);
                }
                catch(Exception fileCreationError)
                {
                    Console.WriteLine("Filed to create counter file:\n" + fileCreationError);
                }
            }

            try
            {
                StreamWriter wr = new StreamWriter(filePath, false);
                wr.WriteLine(count + 1);
            }
            catch(Exception fileWriteError)
            {

            }

            var fileContents = new fileContents
            {
                parent = "item/generated",
                textures = new Dictionary<String, String> 
                    {
                        ["layer0"] = "items/CI" + count
                    }
            };

            string fileName = "CLI" + count + ".json";
            string jsonString = JsonConvert.SerializeObject(fileContents);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
