using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace SpeechToWords
{
    class Program
    {
        async static Task FromFile(SpeechConfig speechConfig, string fileName)
        {
            using var audioConfig = AudioConfig.FromWavFileInput(fileName);
            using var recognizer = new SpeechRecognizer(speechConfig, audioConfig);

            var result = await recognizer.RecognizeOnceAsync();
            
            Console.WriteLine($"RECOGNIZED: Text={result}");
        }

        async static Task Main(string[] args)
        {
            var speechConfig = SpeechConfig.FromSubscription(Environment.GetEnvironmentVariable("speechkey"), Environment.GetEnvironmentVariable("speechregion"));
            speechConfig.RequestWordLevelTimestamps();
            await FromFile(speechConfig, args[0]);
        }
    }
}
