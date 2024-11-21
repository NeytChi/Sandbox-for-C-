using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace HtmlParserExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parser = new HtmlParser();
            var html = parser.GetHTMLByURL("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-8.0");
            parser.ParseAndPrintHtmlWithRegex(html);
            parser.ExtractTextFromHtml(html);
        }
    }
    internal class HtmlParser
    {
        public string GetHTMLByURL(string url)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync(url);
            var result = response.Result.Content.ReadAsStringAsync().Result;
            return result;
        }
        public void ParseAndPrintHtmlWithRegex(string htmlContent)
        {
            string pattern = @"<.*?>(.*?)<\/.*?>";
            Regex regex = new Regex(pattern, RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(htmlContent);
            foreach (Match match in matches)
            {

                string text = match.Value.Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    Console.WriteLine(text);
                }
            }
        }
        public string ExtractTextFromHtml(string html)
        {
            html = Regex.Replace(html, @"<br\s*/?>", "\n", RegexOptions.IgnoreCase); 
            html = Regex.Replace(html, @"<p\s*/?>", "\n", RegexOptions.IgnoreCase); 
            string pattern = "<.*?>"; 
            string cleanText = Regex.Replace(html, pattern, String.Empty); 
            cleanText = Regex.Replace(cleanText, @"\s+", " ").Trim(); 
            cleanText = Regex.Replace(cleanText, @"\n", Environment.NewLine);
            Console.WriteLine(cleanText);
            return cleanText;
        }
    }
}
