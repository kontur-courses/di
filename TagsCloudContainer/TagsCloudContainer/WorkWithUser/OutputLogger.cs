using System.Collections.Generic;

namespace TagsCloudContainer
{
    public static class OutputLogger
    {
        private readonly static List<string> AllLogs = new List<string>();

        public static void AddLog(string s) => AllLogs.Add(s);

        public static List<string> GetAllLogs() => AllLogs;
    }
}