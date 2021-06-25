using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MC_JSON_Maker
{
    class FileContents
    {
        public string parent { get; set; }
        public Dictionary<String, String> textures { get; set; }
    }

    class Counter
    {
        public int count { get; set; }
    }

    class Maker
    {
        static void Main(string[] args)
        {
            //initialize needed vars
            //string filePath = "./MC_JSON_Maker_RunCounter.json";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "MC_JSON_Maker_RunCounter.json");
            Counter index = new Counter();
            index.count = 1;
            string json = "";
            bool isInt = false;
            int input = 0;

            while(isInt)
            {
                Console.WriteLine("How many files would you like to make?(please enter only whole numbers)");
                isInt = int.TryParse(Console.ReadLine(), out input);
                if(!isInt)
                {
                    Console.WriteLine("That's not a whole number, please try again.");
                }
            }

            for (int i = 0; i < input; i++)
            {
                //check if file exists to use the updated count, otherwise use 1(line 26)
                if (File.Exists(filePath))
                {
                    try
                    {
                        StreamReader sr = new StreamReader(filePath);
                        json = sr.ReadToEnd();
                    }
                    catch (Exception fileReadException)
                    {
                        Console.WriteLine("Failed to read json file:\n" + fileReadException);
                    }

                    index = JsonConvert.DeserializeObject<Counter>(json);
                }

                //generate the template
                var fileContents = new FileContents
                {
                    parent = "item/generated",
                    textures = new Dictionary<String, String>
                    {
                        ["layer0"] = "items/CI" + index.count
                    }
                };

                //create the new json file
                string fileName = Path.Combine(Directory.GetCurrentDirectory(), "CLI" + index.count + ".json");
                string jsonString = JsonConvert.SerializeObject(fileContents);
                File.WriteAllText(fileName, jsonString);

                //increment counter and write to file
                index.count += 1;
                string jstring = JsonConvert.SerializeObject(index);
                File.WriteAllText(filePath, jstring);
                Console.WriteLine("Json file created at: " + fileName);
            }
        }
    }
}
