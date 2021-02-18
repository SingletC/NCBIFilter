using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Data.SQLite;
using System.IO;

namespace NCBIFilter
{
    class Prolongation
    {
        /// <summary>
        /// JH式排列
        /// </summary>
        /// <param name="dnablocklist"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void JHsort(List<Basic.DnaBlock> dnablocklist, int start, int end)
        {
            int i = start, j = end;
            int x = dnablocklist[0].Fwd_seq.Count;
            for (int z = 1; z < end; ++z)
            {
                x = Math.Max(x, dnablocklist[z].Fwd_seq.Count);
            }
            double k = Math.Pow(2, Convert.ToString(x, 2).Length - 1);
            JHsort(dnablocklist, start, end, (int)k);
        }
        public static void JHsort(List<int> fail, int start, int end)
        {
            int i = start, j = end;
            int x = fail[0];
            for (int z = 1; z < end; ++z)
            {
                x = Math.Max(x, fail[z]);
            }
            double k = Math.Pow(2, Convert.ToString(x, 2).Length - 1);
            JHsort(fail, start, end, (int)k);
        }

        /// <summary>
        /// JH式排列
        /// </summary>
        /// <param name="dnablocklist"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="k"></param>
        public static void JHsort(List<Basic.DnaBlock> dnablocklist, int start, int end, int k)
        {
            int i = start, j = end, x;
            while (i < j)
            {
                while ((dnablocklist[j].Fwd_seq.Count & k) != 0 && i < j)
                {
                    --j;
                }

                while ((dnablocklist[i].Fwd_seq.Count & k) == 0 && i < j)
                {
                    ++i;
                }
                if (i < j)
                {
                    x = dnablocklist[j].Fwd_seq.Count;
                    var temp = dnablocklist[j];
                    dnablocklist[j] = dnablocklist[i];
                    dnablocklist[i] = temp;
                }
                else
                {
                    if ((dnablocklist[j].Fwd_seq.Count & k) != 0)
                    {
                        --i;
                    }
                    else
                    {
                        ++j;
                    }
                    break;
                }
            }
            if (k > 1)
            {
                if (start < i) JHsort(dnablocklist, start, i, k >> 1);
                if (j < end) JHsort(dnablocklist, j, end, k >> 1);
            }
        }
        public static void JHsort(List<int> fail, int start, int end, int k)
        {
            int i = start, j = end, x;
            while (i < j)
            {
                while ((fail[j] & k) != 0 && i < j)
                {
                    --j;
                }

                while ((fail[i] & k) == 0 && i < j)
                {
                    ++i;
                }
                if (i < j)
                {
                    x = fail[j];
                    fail[j] = fail[i];
                    fail[i] = x;
                }
                else
                {
                    if ((fail[j] & k) != 0)
                    {
                        --i;
                    }
                    else
                    {
                        ++j;
                    }
                    break;
                }
            }
            if (k > 1)
            {
                if (start < i) JHsort(fail, start, i, k >> 1);
                if (j < end) JHsort(fail, j, end, k >> 1);
            }
        }

        /// <summary>
        /// JH连接模块
        /// </summary>
        public class JHWebClient : WebClient
        {
            private int timeout;
            private CookieContainer cookiecontainer;

            public JHWebClient()
            {
                this.Encoding = System.Text.Encoding.UTF8;
                this.timeout = 15000;
                this.cookiecontainer = new CookieContainer();
            }

            public JHWebClient(int timeout)
            {
                this.timeout = timeout;
            }

            public JHWebClient(CookieContainer cookies)
            {
                this.cookiecontainer = cookies;
            }

            public CookieContainer Cookies
            {
                get { return this.cookiecontainer; }
                set { this.cookiecontainer = value; }
            }

            public int Timeout
            {
                get { return this.timeout; }
                set { this.timeout = value; }
            }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var result = base.GetWebRequest(address);
                if (result is HttpWebRequest)
                {
                    HttpWebRequest httpRequest = result as HttpWebRequest;
                    httpRequest.CookieContainer = cookiecontainer;
                    httpRequest.Timeout = this.timeout;
                    httpRequest.ServicePoint.Expect100Continue = false;
                }
                return result;
            }
        }

        /// <summary>
        /// Sql模块
        /// </summary>
        public class Sql
        {
            static public SQLiteConnection OpenSql()
            {
                SQLiteConnection connection = new SQLiteConnection("Data Source = Local.db");
                connection.Open();
                return connection;
            }

            static public bool AddCountry(Advanced.SameLocation samelocation, SQLiteConnection connection)
            {
                if (samelocation.Lat != "NetworkError" && samelocation.Lat != "DataError")
                {
                    SQLiteCommand sqlcommand = new SQLiteCommand("INSERT INTO CountryTable VALUES('" + samelocation.Location + "', '" + samelocation.Lat + "', '" + samelocation.Lng + "')", connection);

                    if (sqlcommand.ExecuteNonQuery() != 0)
                        return true;

                }
                return false;
            }

            static public Advanced.SameLocation MatchCountry(Advanced.SameLocation samelocation, SQLiteConnection connection)
            {
                SQLiteCommand sqlcommand = new SQLiteCommand("SELECT * FROM CountryTable WHERE Country = '" + samelocation.Location + "'", connection);
                if (sqlcommand.ExecuteScalar() != null)
                {
                    SQLiteDataReader reader = sqlcommand.ExecuteReader();
                    while (reader.Read())
                    {
                        samelocation.Lng = reader.GetString(reader.GetOrdinal("Lng"));
                        samelocation.Lat = reader.GetString(reader.GetOrdinal("Lat"));
                    }
                }
                else
                {
                    samelocation = Advanced.GetLatLng(samelocation,0);
                    AddCountry(samelocation, connection);
                }

                return samelocation;
            }
        }

        /// <summary>
        /// 创立文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void NewFile(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                Directory.Delete(path, true);
                System.Threading.Thread.Sleep(100);
                Directory.CreateDirectory(path);
            }
        }
    }
}
