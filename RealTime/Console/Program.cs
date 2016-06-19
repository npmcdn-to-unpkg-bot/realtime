namespace Allors
{
    using System;

    using NLog;

    public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Please select an option:\n");
            foreach (var option in Enum.GetValues(typeof(Options)))
            {
                Console.WriteLine((int)option + ". " + Enum.GetName(typeof(Options), option));
            }

            Console.WriteLine();

            try
            {
                var choice = args.Length > 0 ? args[0] : Console.ReadLine();
                Options option;
                if (Enum.TryParse(choice, out option))
                {
                    Console.WriteLine("-> " + (int)option + ". " + Enum.GetName(typeof(Options), option));
                    Console.WriteLine();

                    Command command;

                    switch (option)
                    {
                        case Options.Save:
                            command = new Commands.Save();
                            break;

                        case Options.Load:
                            command = new Commands.Load();
                            break;

                        case Options.Upgrade:
                            command = new Commands.Upgrade();
                            break;

                        case Options.Populate:
                            command = new Commands.Populate();
                            break;

                        case Options.Custom:
                            command = new Commands.Custom();
                            break;

                        case Options.Investigate:
                            command = new Commands.Investigate();
                            break;

                        case Options.Performance:
                            command = new Commands.Performance();
                            break;

                        case Options.Exit:
                            return;

                        default:
                            throw new ArgumentException("Unknown option");
                    }

                    command.Execute();
                }
                else
                {
                    Console.WriteLine("Unknown option");
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                Console.ReadKey(false);
            }
            finally
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey(false);
            }
        }
    }
}
