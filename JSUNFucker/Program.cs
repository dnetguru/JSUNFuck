using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

// ReSharper disable once AssignNullToNotNullAttribute

namespace JSUNFuck
{
    internal class Program
    {
        struct TransformResult
        {
            public string resultString;
            public int heurProbability;
        }

        private static readonly Regex r = new Regex("([^\\+])\\+(?!\\+)", RegexOptions.Multiline | RegexOptions.Compiled);

        private static void Main(string[] args)
        {
            if (args.Length < 1) Exit("Usage:  JSUNFuck.exe <JSFuck Encrypted File>\n\tJSUNFuck.exe <JSFuck Encrypted File> <Output Filename>");
            else if (!File.Exists(args[0])) Exit("Cannot locate the specified source file ! :(");
            try
            {
                int currentHeur = -1;
                string endResult = null;
                var srcFile = File.ReadAllText(args[0]);
                foreach (var crAnalysisResultCandidate in Dictionary.crAnalysisResults)
                {
                    var tRes = RunTransform(srcFile, crAnalysisResultCandidate);
                    if (tRes.heurProbability > currentHeur) endResult = tRes.resultString;
                    currentHeur = tRes.heurProbability;
                }

                string res = r.Replace(endResult, "$1").Replace("[][filter][constructor](", "");
                File.WriteAllText(args[1], res.Substring(res.Length - 3) == ")()" ? res.Substring(0, res.Length - 3) : res);
            }
            catch (Exception ex)
            {
                Exit("An error occurred: " + ex.Message);
            }
        }

        private static TransformResult RunTransform(string srcFile, ICollection crAnalysisRes)
        {
            int heurCnt = 0;
            foreach (KeyValuePair<string, string> entry in crAnalysisRes)
            {
                var rc = new Regex(entry.Key, RegexOptions.IgnoreCase);
                heurCnt += rc.Matches(srcFile).Count;
                srcFile = rc.Replace(srcFile, entry.Value);
            }
            return new TransformResult() { heurProbability = heurCnt, resultString = srcFile };
        }

        private static void Exit(string msg)
        {
            Console.WriteLine(msg);
            Environment.Exit(0);
        }
    }
}
