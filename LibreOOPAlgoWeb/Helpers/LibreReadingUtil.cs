using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LibreOOPWeb.Helpers
{
    public class LibreReadingUtil
    {
        public static Double? ReadingResultToNumber(string result)
        {
            Double res;
            // Get first match.
            Match match = Regex.Match(result, @"currentBg: (\d+)");
            if (match.Success)
            {
                try
                {
                    Double.TryParse(match.Groups[1]?.Value ?? "", out res);
                    return res;
                }
                catch
                {
                    return null;
                }
                
            }

            return null;

        }
    }
}