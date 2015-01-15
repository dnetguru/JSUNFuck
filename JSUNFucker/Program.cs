using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JSUNFuck
{
    internal class Program
    {
        readonly static Regex r = new Regex("([^\\+])\\+(?!\\+)", RegexOptions.Multiline | RegexOptions.Compiled);
        private static void Main(string[] args)
        {
            if (args.Length != 2) Exit("Usage: JSUNFuck.exe <JSFuck Encrypted File> <Output Filename>");
            else if (!File.Exists(args[0])) Exit("Cannot locate the specified source file ! :(");
            try
            {
                var srcFile = File.ReadAllText(args[0]);
                srcFile = Dictionary.JSFuckDict.Aggregate(srcFile,
                    (current, pair) => current.Replace(pair.Key, pair.Value));
                string res = r.Replace(srcFile, "$1").Replace("[][filter][constructor](", "");
                File.WriteAllText(args[1], res.Substring(res.Length - 3) == ")()" ? res.Substring(0, res.Length - 3) : res);
            }
            catch (Exception ex)
            {
                Exit("An error occurred: " + ex.Message);
            }
        }

        private static void Exit(string msg)
        {
            Console.WriteLine(msg);
            Environment.Exit(0);
        }
    }
}
