using System;
using System.Diagnostics;
using System.IO;

namespace LogAnalyzer
{
    public static class LogExporter
    {
        public static void ExportMessageLog()
        {
            string remoteNode = {ipAddress};
            string authenticationMethod = "-Windows";

            string startTime = "\"1-May-2024\"";
            string endTime = "\"9-Sep-2024\"";

            string messageId = "7082";

            string pigetmsgDirectory = @{path};

            string pigetmsgSavePath = @{path};

            string command = ".\\pigetmsg";
            string arguments = $"-remote -node {remoteNode} {authenticationMethod} -st {startTime} -et {endTime} -id {messageId} -of {pigetmsgSavePath} -fc";

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"cd '{pigetmsgDirectory}'; {command} {arguments}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = processStartInfo })
            {
                process.Start();

                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine(result);

                    process.WaitForExit();
                }
            }
        }
    }
}



// This file contains the LogExporter class with the ExportMessageLog method. It handles the export of message logs from a remote node to a local CSV file.
