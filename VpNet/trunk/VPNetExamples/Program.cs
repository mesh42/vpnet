

using System;
using System.Configuration;
using VPNetExamples.Common;
using VpNet.Core;

namespace VPNetExamples
{
    class Program
    {
        private static BaseExampleBot _bot;

        static void Main(string[] args)
        {
            try {
menu:
                Console.WriteLine("Bot Examples");
                Console.WriteLine("0. Run all Bots (from a single vp sdk instance)");
                Console.WriteLine("1. Hello World! Bot");
                Console.WriteLine("2. Greeter Bot");
                Console.WriteLine("3. Keyword Bot");
                Console.WriteLine("4. Event Display Bot");
                Console.WriteLine("5. Text Rotator Bot");
                Console.WriteLine("6. Query Bot");
                Console.WriteLine("7. Weather Bot");
                Console.WriteLine("8. Teleport Bot");
                Console.WriteLine("---- UNIT TESTS ---");
                Console.WriteLine("A. Create Object Reference Number Test.");
                Console.Write("Please enter a numer (0-7): ");
                string read = Console.ReadLine();

                switch (read)
                {
                    case "0":
                        _bot = new GreeterBot();
                        _bot.AttachBot<HelloWorldBot>();
                        _bot.AttachBot<KeywordBot.KeywordBot>();
                        _bot.AttachBot<EventDisplayBot>();
                        _bot.AttachBot<TextRotatorBot.TextRotatorBot>();
                        _bot.AttachBot<WeatherBot.WeatherBot>();
                        break;
                    case "1":
                        _bot = new HelloWorldBot();
                        break;
                    case "2":
                        _bot = new GreeterBot();
                        break;
                    case "3":
                        _bot = new KeywordBot.KeywordBot();
                        break;
                    case "4":
                        _bot = new EventDisplayBot();
                        break;
                    case "5":
                        _bot = new TextRotatorBot.TextRotatorBot();
                        break;
                    case "6":
                        _bot = new QueryBot();
                        break;
                    case "7":
                        _bot = new WeatherBot.WeatherBot();
                        break;
                    case "8":
                        _bot = new TeleportBot.TeleportBot(); 
                        break;
                    case "A" :
                        _bot = new EventDisplayBot();
                        _bot.AttachBot<UnitTestsBot.UnitTestsBot>();
                        break;
                    default:
                        Console.WriteLine("Please enter an existing number");
                        goto menu;
                }
                _bot.Initialize();
                var unitTestBot = _bot.GetAttachedBot<UnitTestsBot.UnitTestsBot>();
                Console.WriteLine(string.Format("Running Example {0}. Press Enter to exit and choose another example.", read));
                if (unitTestBot != null)
                {
                    Console.WriteLine(string.Format("Press enter to Execute Unit Tests.", read));
                    Console.ReadLine();
                    unitTestBot.UnitTestA();
                    Console.ReadLine();
                    Console.ReadLine();
                }
                Console.ReadLine();
                _bot.Dispose();
                goto menu;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Have you entered your credentials in the app.config file?");
                Console.ReadLine();
            }
        }
    }
}
