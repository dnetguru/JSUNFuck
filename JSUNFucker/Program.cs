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
        private static readonly Regex r = new Regex("([^\\+])\\+(?!\\+)", RegexOptions.Multiline | RegexOptions.Compiled);

        private static void Main(string[] args)
        {
#if DEBUG
            args = new[] { "..\\..\\Test Files\\AlertOne.alt.ascii" };
            //args = new[] { "..\\..\\Test Files\\AlertOne.ascii" };
#endif
            if (args.Length < 1)
                Exit(
                    "Usage:  JSUNFuck.exe <JSFuck Encrypted File>\n\tJSUNFuck.exe <JSFuck Encrypted File> <Output Filename>");
            else if (!File.Exists(args[0])) Exit("Cannot locate the specified source file ! :(");
            try
            {
                var currentHeur = 0.0f;
                string endResult = null;
                var srcFile = File.ReadAllText(args[0]);
                foreach (var crAnalysisResultCandidate in Dictionary.crAnalysisResults)
                {
                    var tRes = RunTransform(srcFile, crAnalysisResultCandidate);
                    if (tRes.heurProbability > currentHeur) endResult = tRes.resultString;
                    currentHeur = tRes.heurProbability;
                }

                string res = r.Replace(endResult, "$1").Replace("[][filter][constructor](", "");
                string contents = res.Substring(res.Length - 3) == ")()" ? res.Substring(0, res.Length - 3) : res;
                if (args.Length == 2) File.WriteAllText(args[1], contents);
                else Console.WriteLine(contents);
            }
            catch (Exception ex)
            {
                Exit("An error occurred: " + ex.Message);
            }
        }

        private static TransformResult RunTransform(string srcFile, ICollection crAnalysisRes)
        {
            float heurCnt = 0;
            foreach (KeyValuePair<string, string> entry in crAnalysisRes)
            {
                // Relative probability of the correct dialect being used
                heurCnt += srcFile.Length - srcFile.Replace(entry.Key, string.Empty).Length;
                srcFile = srcFile.Replace(entry.Key, entry.Value);
            }
            return new TransformResult { heurProbability = heurCnt, resultString = srcFile };
        }

        private static void Exit(string msg)
        {
            Console.WriteLine(msg);
            Environment.Exit(0);
        }

        private struct TransformResult
        {
            public float heurProbability;
            public string resultString;
        }
    }
}
