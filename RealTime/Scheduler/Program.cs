namespace Allors
{
    using System;

    using NLog;

    public class Program
    {
        private const string UsageMessage = "Invalid Syntax.\nUsage: scheduler {a | h | d | w | m}";

        private const int Success = 0;
        private const int Failure = 1;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static int Main(string[] args)
        {
            try
            {
                var argument = (args.Length == 0 || string.IsNullOrWhiteSpace(args[0])) ? "d" : args[0].ToLower();

                Scheduler scheduler;
                switch (argument)
                {
                    case "i":
                        scheduler = new ImmediateScheduler();
                        break;

                    case "h":
                        scheduler = new HourlyScheduler();
                        break;

                    case "d":
                        scheduler = new DailyScheduler();
                        break;

                    case "w":
                        scheduler = new WeeklyScheduler();
                        break;

                    case "m":
                        scheduler = new MonthlyScheduler();
                        break;

                    case "a":
                        scheduler = new AsyncScheduler();
                        break;

                    default:
                        throw new ArgumentException();
                }

                scheduler.Schedule();
            }
            catch (ArgumentException)
            {
                Logger.Error(UsageMessage);
                return Failure;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return Failure;
            }

            return Success;
        }
    }
}
