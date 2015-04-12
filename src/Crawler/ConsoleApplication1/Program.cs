using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                int mode = int.Parse(args[0]);
                int depth = int.Parse(args[1]);
                List<string> siteList = new List<string>();
                for (int i = 2; i < args.Length; ++i)
                    siteList.Add(args[i]);

                if (0 == mode)
                    BFS(siteList, depth);
                else
                    DFS(siteList, depth);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("usage: {0} <mode> <depth> <links>", System.AppDomain.CurrentDomain.FriendlyName);
                Console.WriteLine("<mode> = 0 for BFS");
                Console.WriteLine("<mode> = 1 for DFS");
            }
        }

        public static void BFS(List<string> urlList, int maxDepth)
        {
            SQLFunction mySQL = new SQLFunction();
            mySQL.resetDB();

            General.reservedWords = mySQL.getReservedWords();
            SortedSet<string> visitedLinks = new SortedSet<string>(mySQL.getVisitedLinks());

            // Buat Queue berisi semua url yang belum pernah dikunjungi
            Queue<Tuple<string, int>> Q = new Queue<Tuple<string, int>>();
            foreach (string url in urlList)
                if (!visitedLinks.Contains(url))
                    Q.Enqueue(new Tuple<string, int>(url, 0));

            while (Q.Count != 0)
            {
                Tuple<string, int> front = Q.Dequeue();
                string URL = front.Item1;
                int depth = front.Item2;

                if (depth <= maxDepth && !visitedLinks.Contains(URL))
                {
                    try
                    {
                        HtmlDocument doc = General.getWebPage(URL);

                        string title = General.getWebTitle(URL);
                        List<string> keyword = General.getListOfKeywords(doc);

                        mySQL.updateLinksAndTitle(URL, title);
                        int id = mySQL.getLinkId(URL);
                        mySQL.updateKeywords(id, keyword);

                        Uri baseURL = new Uri(URL), myURL;

                        List<string> linkList = new List<string>();
                        var links = doc.DocumentNode.SelectNodes("//a[@href]");
                        if (links != null)
                        {
                            foreach (HtmlNode link in links)
                            {
                                string hrefValue = link.GetAttributeValue("href", "");
                                myURL = new Uri(baseURL, hrefValue);
                                if (!linkList.Contains(myURL.ToString()))
                                    linkList.Add(myURL.ToString());
                            }
                        }

                        // Proses setiap link
                        foreach (string link in linkList)
                            if (depth < maxDepth && !visitedLinks.Contains(link))
                                Q.Enqueue(new Tuple<string, int>(link, depth + 1));

                        Console.WriteLine("Selesai memproses {0}", URL);
                    }
                    catch (Exception)
                    {
                        // website tidak bisa dikunjungi dengan berbagai alasan
                    }

                    visitedLinks.Add(URL);
                }
            }
        } //End of BFS


        public static void DFS(List<string> urlList, int maxDepth)
        {
            SQLFunction mySQL = new SQLFunction();
            mySQL.resetDB();

            General.reservedWords = mySQL.getReservedWords();
            SortedSet<string> visitedLinks = new SortedSet<string>(mySQL.getVisitedLinks());

            // Buat Stack berisi semua url yang belum pernah dikunjungi
            Stack<Tuple<string, int>> S = new Stack<Tuple<string, int>>();
            foreach (string url in urlList)
                if (!visitedLinks.Contains(url))
                    S.Push(new Tuple<string, int>(url, 0));

            while (S.Count != 0)
            {
                Tuple<string, int> front = S.Pop();
                string URL = front.Item1;
                int depth = front.Item2;

                if (depth <= maxDepth && !visitedLinks.Contains(URL))
                {
                    try
                    {
                        HtmlDocument doc = General.getWebPage(URL);

                        string title = General.getWebTitle(URL);
                        List<string> keyword = General.getListOfKeywords(doc);

                        mySQL.updateLinksAndTitle(URL, title);
                        int id = mySQL.getLinkId(URL);
                        mySQL.updateKeywords(id, keyword);

                        Uri baseURL = new Uri(URL), myURL;

                        List<string> linkList = new List<string>();
                        var links = doc.DocumentNode.SelectNodes("//a[@href]");
                        if (links != null)
                        {
                            foreach (HtmlNode link in links)
                            {
                                string hrefValue = link.GetAttributeValue("href", "");
                                myURL = new Uri(baseURL, hrefValue);
                                if (!linkList.Contains(myURL.ToString()))
                                    linkList.Add(myURL.ToString());
                            }
                        }

                        // Proses setiap link
                        foreach (string link in linkList)
                            if (depth < maxDepth && !visitedLinks.Contains(link))
                                S.Push(new Tuple<string, int>(link, depth + 1));

                        Console.WriteLine("Selesai memproses {0}", URL);
                    }
                    catch (Exception)
                    {
                        // website tidak bisa dikunjungi dengan berbagai alasan
                    }

                    visitedLinks.Add(URL);
                }
            }
        } //End of DFS
    }
}
