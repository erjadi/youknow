using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace GetTimeStamps
{
    class Program
    {

        static void Main(string[] args)
        {
            string ffmpegScript = "#!/bin/bash\n";
            string concatlist = "";

            List<Word> words = new List<Word>();
            for (long i = 0; i < 999; i++)
            {
                string jsonText = "";
                try
                {
                    jsonText = File.ReadAllText(@"./segment" + i.ToString("000") + ".wav.json");
                }
                catch (Exception e)
                {
                    break;
                }
                Speech spobject = JsonConvert.DeserializeObject<Speech>(jsonText);
                if (spobject.NBest != null)
                {
                    var max = spobject.NBest.Max();
                    if (max.Words != null)
                    {
                        foreach (Word w in max.Words)
                        {
                            w.Offset += i * 10 * 10 * 1000 * 1000;
                            words.Add(w);
                        }
                    }
                }
                Console.Write(".");
            }
            for (int i = 0; i < words.Count(); i++)
            {
                if (words[i].WordWord.Equals("you"))
                {
                    if (i<words.Count() - 1)
                    {
                        if (words[i + 1].WordWord.Equals("know"))
                        {
                            long youknowstart = words[i].Offset;
                            long youknowlength = words[i + 1].Offset + words[i + 1].Duration - words[i].Offset;
                            //if (youknowlength < 10000000) { youknowlength = 10000000; }
                            youknowlength += 1000000; // Add space 
                            Console.WriteLine("You Know @" + ((float)(youknowstart / 100000)/100).ToString("0.00") + " - " + ((float)(youknowlength / 100000) / 100).ToString("0.00"));
                            ffmpegScript += "ffmpeg -i output.mp4 -ss " + ((float)(youknowstart / 100000) / 100).ToString("0.00") + " -t " + ((float)(youknowlength / 100000) / 100).ToString("0.00") + " yk" + i + ".mp4 &\n";
                            concatlist += "file './yk" + i +".mp4'\n";
                        }
                    }
                }
            }
            //Console.ReadLine();
            ffmpegScript += "wait\nffmpeg -f concat -safe 0 -i concat.txt youknow.mp4\n";
            File.WriteAllText(@"./script.sh", ffmpegScript);
            File.WriteAllText(@"./concat.txt", concatlist);
        }      
    }
}

