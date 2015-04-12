using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace QueryMaker
{
    class Program
    {
        const string server = "localhost";
        const string dbName = "db_crawler";
        const string uid = "root";
        const string pwd = "";
        static string connection;

        static void Main(string[] args)
        {
            bool never = true;
            connection = "Server=" + server + ";Database=" + dbName + ";Uid=" + uid + ";Pwd=" + pwd;

            List<Tuple<int, int>> rankedID = new List<Tuple<int, int>>(); // -count, id
            SortedList<int, int> counter = new SortedList<int, int>(); // id, count

            string str;
            for (int i = 1; i < args.Length; ++i)
            {
                never = false;
                str = args[i].ToLower();
                List<int> ret = getMatchingUrl(str);

                foreach (int id in ret)
                    if (counter.ContainsKey(id))
                        ++counter[id];
                    else
                        counter[id] = 1;
            }
            if (never) // argumen kosong
            {
                List<int> ret = getAll();
                foreach (int id in ret)
                    rankedID.Add(new Tuple<int, int>(-1, id));
            }
            else
            {
                foreach (int key in counter.Keys)
                    rankedID.Add(new Tuple<int, int>(-counter[key], key));
            }

            rankedID.Sort();

            Console.WriteLine(rankedID.Count);
            int page = int.Parse(args[0]);
            if (isPageValid(page, rankedID.Count))
            {
                for (int i = page * 10; (i < (page + 1) * 10) && (i < rankedID.Count); ++i)
                {
                    Console.WriteLine(rankedID[i].Item2);
                }
            }
        }

        static bool isPageValid(int i, int n)
        {
            int upper = (n - 1) / 1 + 1;
            if (upper < 0) upper = 0;
            return (i >= 0 && i <= upper);
        }

        static List<int> getMatchingUrl(string keyword)
        {
            MySqlConnection myConnection = new MySqlConnection(connection);

            string str = "SELECT link_id FROM link_keywords WHERE keyword=@kw";
            MySqlCommand cmd = new MySqlCommand(str, myConnection);

            myConnection.Open();
            cmd.Parameters.AddWithValue("@kw", keyword);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<int> res = new List<int>();
            while (reader.Read())
                res.Add(reader.GetInt32("link_id"));

            myConnection.Close();

            return res;
        }

        static List<int> getAll()
        {
            MySqlConnection myConnection = new MySqlConnection(connection);

            string str = "SELECT * FROM visited_links";
            MySqlCommand cmd = new MySqlCommand(str, myConnection);

            myConnection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<int> res = new List<int>();
            while (reader.Read())
                res.Add(reader.GetInt32("id"));

            myConnection.Close();

            return res;
        }
    }
}
