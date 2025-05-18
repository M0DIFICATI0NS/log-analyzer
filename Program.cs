namespace LogAnalyzer
{
    class Program
    {
        static void Main()
        {
            // Export the message log
            LogExporter.ExportMessageLog();

            // Analyze the message log
            LogAnalyzer.AnalyzeMessageLog();
        }
    }
}


// Main Program class of the execution of the whole script.