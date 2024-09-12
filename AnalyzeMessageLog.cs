using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LogAnalyzer
{
    public static class LogAnalyzer
    {
        public static void AnalyzeMessageLog()
        {
            string filePath = @"C:\Users\****\MessageLog.csv";
            var logins = new List<LoginRecord>();

            // Read the CSV file
            using (var reader = new StreamReader(filePath))
            {
                
                for (int i = 0; i < 3; i++) reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length > 7)
                    {
                        // Parse the 'Time' and 'Message' columns
                        DateTime time;
                        if (DateTime.TryParseExact(values[1].Trim('\"'), "dd-MMM-yy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                        {
                            string message = values[7].Trim();

                            // Extract user identifier from the 'Message' column
                            var userMatch = Regex.Match(message, @"CT\\(!?\w{4,5})");
                            // Extract process name from the 'Message' column
                            var processMatch = Regex.Match(message, @"Name: (\w+)");

                            if (userMatch.Success && processMatch.Success)
                            {
                                string user = userMatch.Groups[1].Value;
                                string process = processMatch.Groups[1].Value;
                                logins.Add(new LoginRecord { User = user, Time = time, Process = process });
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to parse date: {values[1]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Data couldn't find for these users (maybe (!) privileged users): {line}");
                    }
                }
            }

            // Get the latest login time for each user
            var latestLogins = logins
                .GroupBy(l => l.User)
                .Select(g => new { User = g.Key, Time = g.Max(l => l.Time), Process = g.First().Process })
                .ToList();

            // Define the cutoff date (1 month ago from the latest date in the log)
            DateTime cutoffDate = latestLogins.Max(l => l.Time).AddMonths(-2);

            // Identify inactive users who haven't logged in within the last month
            var inactiveUsers = latestLogins.Where(l => l.Time < cutoffDate).ToList();

            string ldapDomain = "LDAP://****";
            var adConnection = new ADConnection(ldapDomain);

            string outputFilePath = @"C:\Users\****\InactiveUsers.csv";
            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("CAI,Full Name,Email,Supervisor,Last Login Time,Process");

                foreach (var user in inactiveUsers)
                {
                    var adUser = adConnection.GetUserByUserID(user.User);
                    if (adUser != null)
                    {
                        writer.WriteLine($"\"{adUser.UserID}\",\"{adUser.FullName}\",\"{adUser.Email}\",\"{adUser.Supervisor}\",\"{user.Time}\",\"{user.Process}\"");
                    }
                    else
                    {
                        writer.WriteLine($"\"{user.User}\",\"\",\"\",\"\",\"{user.Time}\",\"{user.Process} - User details not found.\"");
                    }
                }
            }

            Console.WriteLine("Inactive users have been exported to the CSV file.");
        }
    }

    public class LoginRecord
    {
        public string? User { get; set; }
        public DateTime Time { get; set; }
        public string? Process { get; set; }
    }
}



// This file contains the LogAnalyzer class with the AnalyzeMessageLog method and the LoginRecord class. It handles the analysis of the exported message logs to identify inactive users.
