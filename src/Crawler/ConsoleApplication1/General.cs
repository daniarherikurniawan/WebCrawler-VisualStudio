using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class General
    {
        public static List<string> reservedWords;

        public static HtmlDocument getWebPage(string link)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(link);

            return doc;
        }

        public static List<string> getListOfKeywords(HtmlDocument doc)
        {
            SortedSet<string> keyword = new SortedSet<string>();
            foreach (var node in doc.DocumentNode.SelectNodes("//text()"))
            {
                string text = node.InnerText;
                foreach (string kw in Regex.Split(text, @"\W+"))
                {
                    string trimmedKW = kw.ToLower();

                    if (niceWord(trimmedKW, keyword))
                        keyword.Add(trimmedKW);
                }
            }

            return new List<string>(keyword);
        }

        public static string getWebTitle(string URL)
        {
            //Membaca html sebagai string
            WebClient client = new WebClient();
            Stream data = client.OpenRead(URL);
            StreamReader reader = new StreamReader(data);
            string text = reader.ReadToEnd();
            data.Close();
            reader.Close();

            string title;//mengambil title sementara dari html
            Match m = Regex.Match(text, @"<title>\s*(.+?)\s*</title>");
            if (m.Success)
            {
                title = m.Groups[1].Value;
            }
            else
            {
                Match mm = Regex.Match(text, @"<h\d*>\s*(.+?)\s*</h\d*>");
                title = mm.Groups[1].Value;
                if (!mm.Success)
                {
                    Match mmm = Regex.Match(text, @"<h\d*>\s*(.+?) \s*<(.+?)>");
                    title = mmm.Groups[1].Value;
                }
            }
            //menghasilkan string yang cocok sebagai title
            return getNiceSentence(title, URL);
        }

        private static bool niceWord(string word, SortedSet<string> keyword)
        {
            if (!(word.Length > 1 &&
                  !string.IsNullOrWhiteSpace(word) &&
                  !keyword.Contains(word) &&
                  !reservedWords.Contains(word)))
                return false;

            return true;
        }

        private static string getNiceSentence(string sentence, string URL)
        {
            //menyeleksi string yang mengandung invalid char
            string title = "";
            string[] word = sentence.Split(' ', '<', '>');
            for (int a = 0; a < word.Length; a++)
            {
                if (!word[a].Contains(";") && !word[a].Contains("/"))
                {
                    if (word[a].Length > 1)
                        title = title + word[a] + " ";
                    if (word[a].Length == 1 && !char.IsLetter(word[a][0]))
                        title = title + word[a] + " ";
                }
            }
            if (title.Length > 1)
            {
                return title;
            }
            else//terpaksa mengambil judul dari URL
            {
                return URL;
            }
        }
    }
}
