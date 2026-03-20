using System;
using System.Text.RegularExpressions;
namespace LogProcessing
{
    class LogParser
    {
        private readonly string validLineRegexPattern = @"^([TRC]|[DBG]|[INF]|[WRN]|[ERR]|[FTL])";
        private readonly string splitLineRegexPattern = @"\*{3}|={3}|\^\*";
        private readonly string quotedPasswordRegexPattern = @"""[^\""]\bpassword\b[^\""]""";
        private readonly string endOfLineRegexPattern = @"end-of-line\d+";
        private readonly string weakPasswordRegexPattern;

        public bool IsValidLine(string text)
        {
            return Regex.IsMatch(text, validLineRegexPattern);
        }
        public string[] SplitLogLine(string text)
        {
            return Regex.Split(text, splitLineRegexPattern);
        }
        public int CountQuotedPasswords(string lines)
        {
            return Regex.Matches(lines, quotedPasswordRegexPattern, RegexOptions.IgnoreCase).Count();
        }
        public string RemoveEndOfLineText(string line)
        {

            return Regex.Replace(line, endOfLineRegexPattern, "");
        }
        public string[] ListLinesWithPasswords(string[] lines)
        {
            if (lines == null)
                return Array.Empty<string>();

            string pattern = @"\b(password[a-zA-Z0-9]+)\b";
            string[] result = new string[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                Match match = Regex.Match(lines[i], pattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    result[i] = match.Value + ": " + lines[i];
                }
                else
                {
                    result[i] = "-------- " + lines[i];
                }
            }
            return result;
        }
        public void func1()
        {
            Console.WriteLine(IsValidLine("[INF] Application started"));
            Console.WriteLine(IsValidLine("INF Application started"));


            string[] str = SplitLogLine("[INF] User login<***>Session created<====>Access granted");
            foreach (string s in str)
            {
                Console.WriteLine(s);
            }

            string str1 = "User said \"password123 is weak\"\n" +
                   "Admin noted \"PASSWORD456 expired\"\n" +
                   "No issue found";
            Console.WriteLine(CountQuotedPasswords(str1));

            Console.WriteLine(RemoveEndOfLineText("Transaction completed successfully end-of-line456"));
            string[] lines =
            {
                "User entered password123 during login",
                "System startup completed",
                "Admin reset passwordABC",
                "Backup process finished"
            };
            string[] result = ListLinesWithPasswords(lines);
            foreach (string s in result)
            {
                Console.WriteLine(s);
            }
        }
    }
}