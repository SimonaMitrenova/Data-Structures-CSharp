namespace EventsInGivenDateRange
{
    using System;
    using System.Globalization;
    using System.Threading;
    using Wintellect.PowerCollections;

    public class EventsInGivenDateRange
    {
        public static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var events = new OrderedMultiDictionary<DateTime, string>(true);

            int numberOfEvents = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberOfEvents; i++)
            {
                string[] parts = Console.ReadLine().Split('|');
                string eventName = parts[0].Trim();
                DateTime eventTime = DateTime.Parse(parts[1].Trim());
                events.Add(eventTime, eventName);
            }

            int durationNumber = int.Parse(Console.ReadLine());
            for (int i = 0; i < durationNumber; i++)
            {
                string[] startOtEnd = Console.ReadLine().Split('|');
                DateTime start = DateTime.Parse(startOtEnd[0].Trim());
                DateTime end = DateTime.Parse(startOtEnd[1].Trim());
                var eventsInRange = events.Range(start,true, end, true);
                Console.WriteLine(eventsInRange.KeyValuePairs.Count);
                foreach (var currentEvent in eventsInRange)
                {
                    foreach (var value in currentEvent.Value)
                    {
                        Console.WriteLine("{0} | {1:dd-MMM-yyy}", value, currentEvent.Key);
                    }
                }
            }
        }
    }
}
