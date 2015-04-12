using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    class SQLFunction
    {
        private string myConnectionString;
        public SQLFunction()
        {
            init(defaultServer, defaultDBName, defauldUid, defaultPwd);
        }

        public SQLFunction(string server, string dbName, string uid, string pwd)
        {
            init(server, dbName, uid, pwd);
        }

        public void resetDB()
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);

            string sql = "TRUNCATE TABLE link_keywords;ALTER TABLE link_keywords auto_increment = 1;" +
                        "TRUNCATE TABLE visited_links;ALTER TABLE visited_links auto_increment = 1;";
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public List<string> getReservedWords()
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);

            string sql = "SELECT word FROM reserved_words";
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            List<string> reservedWords = new List<string>();
            while (reader.Read())
                reservedWords.Add(reader.GetString("word"));
            connection.Close();

            return reservedWords;
        }

        public List<string> getVisitedLinks()
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);

            string sql = "SELECT link FROM visited_links";
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            List<string> visitedLinks = new List<string>();
            while (reader.Read())
                visitedLinks.Add(reader.GetString("link"));
            connection.Close();

            return visitedLinks;
        }

        public void updateLinksAndTitle(string link, string title)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);

            string sql = "INSERT INTO visited_links(id, link, title) VALUES('', @link, @title)";
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            connection.Open();
            cmd.Parameters.AddWithValue("@link", link);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public int getLinkId(string link)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);

            string sql2 = "SELECT id FROM visited_links WHERE link=@link";
            MySqlCommand cmd2 = new MySqlCommand(sql2, connection);

            connection.Open();
            cmd2.Parameters.AddWithValue("@link", link);
            MySqlDataReader Reader = cmd2.ExecuteReader();
            int id = 0;
            while (Reader.Read())
            {
                id = Reader.GetInt32("id");
            }
            connection.Close();

            return id;
        }

        public void updateKeywords(int id, List<string> keyword)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);

            // Insert Keywords
            connection.Open();
            foreach (string S in keyword)
            {
                string sql3 = "INSERT INTO link_keywords(keyword_id, link_id, keyword) VALUES('',@link_id, @keyword)";
                MySqlCommand cmd3 = new MySqlCommand(sql3, connection);
                cmd3.Parameters.AddWithValue("@link_id", id);
                cmd3.Parameters.AddWithValue("@keyword", S);
                cmd3.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void init(string server, string dbName, string uid, string pwd)
        {
            myConnectionString = "Server=" + server + ";Database=" + dbName + ";Uid=" + uid + ";Pwd=" + pwd + ";";
        }

        private const string defaultServer = "localhost",
            defaultDBName = "db_crawler", defauldUid = "root", defaultPwd = "";
    }
}
