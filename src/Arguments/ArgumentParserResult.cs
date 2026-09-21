using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Arguments
{
    public class ArgumentParserResult
    {
        public string[] ArgumentList { get; set; }
        public bool HasRedirection { get; set; }
        public string? RedirectionFile { get; set; }

        public ArgumentParserResult()
        {
            ArgumentList = Array.Empty<string>();
            HasRedirection = false;
            RedirectionFile = null;
        }

        public ArgumentParserResult(string[] argumentList, bool hasRedirection, string? redirectionFile)
        {
            ArgumentList = argumentList;
            HasRedirection = hasRedirection;
            RedirectionFile = redirectionFile;
        }
    }

}
