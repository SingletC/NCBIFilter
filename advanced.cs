using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;


namespace NCBIFilter
{
    class Advanced
    {
        /// <summary>
        /// 相同引物存放类
        /// </summary>
        public struct SamePrimers
        {
            public List<string> Version { get; set; }
            public List<string> Fwd_seq { get; set; }
            public List<string> Rev_seq { get; set; }
        }

        /// <summary>
        /// 筛选相同引物
        /// </summary>
        /// <param name="dnablocklist"></param>
        public static List<SamePrimers> SelectPrimers(List<Basic.DnaBlock> dnablocklist)
        {
            Prolongation.JHsort(dnablocklist, 0, dnablocklist.Count - 1);
            List<SamePrimers> sameprimers = new List<SamePrimers>();

            for (int i = dnablocklist.Count() - 1; i >= 0; i--)
            {
                List<string> Version = new List<string>();

                if (dnablocklist[i].Fwd_seq.Count == 0 || i == 0)
                {

                    foreach (Basic.DnaBlock dnablock in dnablocklist)
                    {
                        Version.Add(dnablock.Version);
                    }

                    sameprimers.Add(new SamePrimers()
                    {
                        Version = Version
                    });

                    break;
                }

                int j = i - 1;
                Version.Add(dnablocklist[i].Version);
                while (j >= 0 && dnablocklist[i].Fwd_seq.Count == dnablocklist[j].Fwd_seq.Count)
                {
                    int count = 0;
                    for (int z = 0; z < dnablocklist[i].Fwd_seq.Count; z++)
                    {
                        for (int m = dnablocklist[j].Fwd_seq.Count - 1; m >= 0; m--)
                        {
                            if (dnablocklist[i].Fwd_seq[z] + ";" + dnablocklist[i].Rev_seq[z] == dnablocklist[j].Fwd_seq[m] + ";" + dnablocklist[j].Rev_seq[m])
                            {
                                count++;
                            }
                        }
                    }

                    if (dnablocklist[j].Fwd_seq.Count == count)
                    {
                        Version.Add(dnablocklist[j].Version);
                        dnablocklist.RemoveAt(j);
                        i--;
                    }

                    j--;
                }

                sameprimers.Add(new SamePrimers()
                {
                    Version = Version,
                    Fwd_seq = dnablocklist[i].Fwd_seq,
                    Rev_seq = dnablocklist[i].Rev_seq
                });
                dnablocklist.Remove(dnablocklist[i]);
            }

            return sameprimers;
        }

        /// <summary>
        /// 相同物种存放类
        /// </summary>
        public struct SameOrganism
        {
            public List<string> Version { get; set; }
            public string Organism { get; set; }
        }

        /// <summary>
        /// 筛选相同物种
        /// </summary>
        /// <param name="dnablocklist"></param>
        /// <returns></returns>
        public static List<SameOrganism> SelectOrganism(List<Basic.DnaBlock> dnablocklist)
        {
            List<SameOrganism> sameorganism = new List<SameOrganism>();

            for (int i = dnablocklist.Count - 1; i > 0; i--)
            {
                List<string> Version = new List<string>();
                int j = i - 1;

                Version.Add(dnablocklist[i].Version);
                while (j > 0)
                {
                    if (dnablocklist[i].Organism == dnablocklist[j].Organism)
                    {
                        Version.Add(dnablocklist[j].Version);
                        dnablocklist.RemoveAt(j);
                        i--;
                    }
                    j--;
                }

                sameorganism.Add(new SameOrganism()
                {
                    Version = Version,
                    Organism = dnablocklist[i].Organism,
                });
                dnablocklist.RemoveAt(i);
            }

            return sameorganism;
        }

        /// <summary>
        /// 存放相同地理信息类
        /// </summary>
        public struct SameLocation
        {
            public List<string> Version { get; set; }
            public string Location { get; set; }
            public string Lat { get; set; }
            public string Lng { get; set; }
        }

        /// <summary>
        /// 筛选相同地理信息
        /// </summary>
        /// <param name="dnablocklist"></param>
        /// <returns></returns>
        public static List<SameLocation> SelectLocation(List<Basic.DnaBlock> dnablocklist)
        {
            List<SameLocation> samelocation = new List<SameLocation>();

            for (int i = dnablocklist.Count - 1; i >= 0; i--)
            {
                List<string> Version = new List<string>();

                if (i == 0)
                {                      
                    Version.Add(dnablocklist[0].Version);
                    samelocation.Add(new SameLocation()
                    {
                        Version = Version,
                        Location = dnablocklist[0].Country + ":" + dnablocklist[0].Location,
                    });
                }
                else
                {
                int j = i - 1;

                Version.Add(dnablocklist[i].Version);
                while (j >= 0)
                {
                    if (dnablocklist[i].Country == dnablocklist[j].Country && dnablocklist[i].Location == dnablocklist[j].Location && dnablocklist[i].Location != null && dnablocklist[i].Location != "")
                    {
                        Version.Add(dnablocklist[j].Version);
                        dnablocklist.RemoveAt(j);
                        i--;
                    }
                    j--;
                }

                samelocation.Add(new SameLocation()
                {
                    Version = Version,
                    Location = dnablocklist[i].Country + ":" + dnablocklist[i].Location,
                });
                dnablocklist.RemoveAt(i);
                }
            }

            return samelocation;
        }

        /// <summary>
        /// 筛选相同国家
        /// </summary>
        /// <param name="dnablocklist"></param>
        /// <returns></returns>
        public static List<SameLocation> SelectCountry(List<Basic.DnaBlock> dnablocklist)
        {
            List<SameLocation> samecountry = new List<SameLocation>();

            for (int i = dnablocklist.Count - 1; i >= 0; i--)
            {
                List<string> Version = new List<string>();
                if (i == 0)
                {
                    Version.Add(dnablocklist[0].Version);
                    samecountry.Add(new SameLocation()
                    {
                        Version = Version,
                        Location = dnablocklist[0].Country,
                    });
                }
                else
                {
                    int j = i - 1;

                    Version.Add(dnablocklist[i].Version);
                    while (j >= 0)
                    {
                        if (dnablocklist[i].Country == dnablocklist[j].Country)
                        {
                            Version.Add(dnablocklist[j].Version);
                            dnablocklist.RemoveAt(j);
                            i--;
                        }
                        j--;
                    }

                    samecountry.Add(new SameLocation()
                    {
                        Version = Version,
                        Location = dnablocklist[i].Country,
                    });
                    dnablocklist.RemoveAt(i);
                }
            }

            return samecountry;
        }

        /// <summary>
        /// 筛选相同经纬度
        /// </summary>
        /// <param name="dnablocklist"></param>
        /// <returns></returns>
        public static List<SameLocation> SelectLatLng(List<Basic.DnaBlock> dnablocklist)
        {
            List<SameLocation> samelatlng = new List<SameLocation>();

            for (int i = dnablocklist.Count - 1; i > 0; i--)
            {
                List<string> Version = new List<string>();
                if (i == 0)
                {
                    Version.Add(dnablocklist[0].Version);
                    samelatlng.Add(new SameLocation()
                    {
                        Version = Version,
                        Location = dnablocklist[0].Lat + "," + dnablocklist[0].Lng,
                        Lat = dnablocklist[0].Lat,
                        Lng = dnablocklist[0].Lng
                    });
                }
                else
                {
                    int j = i - 1;

                    Version.Add(dnablocklist[i].Version);
                    while (j >= 0)
                    {
                        if (dnablocklist[i].Lat == dnablocklist[j].Lat && dnablocklist[i].Lng == dnablocklist[j].Lng)
                        {
                            Version.Add(dnablocklist[j].Version);
                            dnablocklist.RemoveAt(j);
                            i--;
                        }
                        j--;
                    }

                    samelatlng.Add(new SameLocation()
                    {
                        Version = Version,
                        Location = dnablocklist[i].Lat + "," + dnablocklist[i].Lng,
                        Lat = dnablocklist[i].Lat,
                        Lng = dnablocklist[i].Lng
                    });
                    dnablocklist.RemoveAt(i);
                }
            }

            return samelatlng;
        }

        /// <summary>
        /// 获取指定位置经纬度
        /// </summary>
        /// <param name="samelocation"></param>
        /// <param name="errortimes">0</param>
        /// <returns></returns>
        public static SameLocation GetLatLng(SameLocation samelocation, int errortimes)
        {
            try
            {
                Prolongation.JHWebClient client = new Prolongation.JHWebClient();

                System.Threading.Thread.Sleep(100);
                string locationinfo = client.DownloadString("http://maps.google.com/maps/api/geocode/json?sensor=false&address=" + samelocation.Location);
                if (locationinfo.IndexOf("\"location\"") != -1)
                {
                    string LatLng = new Regex("\"location\":{(.*?)},").Matches(locationinfo.Replace(" ", "").Replace("\n", ""))[0].Groups[1].ToString();
                    string Lat = LatLng.Split(',')[0].Replace("\"lat\":", "");
                    string Lng = LatLng.Split(',')[1].Replace("\"lng\":", "");
                    samelocation.Lat = Lat;
                    samelocation.Lng = Lng;
                }
                else
                {
                    if (errortimes < 3)
                    {
                        samelocation = GetLatLng(samelocation, errortimes + 1);
                    }
                    else
                    {
                        samelocation.Lat = "DataError";
                    }
                }
            }
            catch (Exception)
            {
                if (errortimes < 3)
                {
                    samelocation = GetLatLng(samelocation, errortimes + 1);
                }
                else
                {
                    samelocation.Lat = "NetworkError";
                }
            }

            return samelocation;
        }

        /// <summary>
        /// 获取指定位置城市经纬度
        /// </summary>
        /// <param name="samelocation"></param>
        /// <param name="errortimes">0</param>
        /// <returns></returns>
        public static SameLocation GetCityLatLng(SameLocation samelocation)
        {
            if (samelocation.Location != ":")
            {
                string[] locationgroup = samelocation.Location.Split(',');
                string location = samelocation.Location;
                for (int i = locationgroup.Length - 1; i >= 0; i--)
                {
                    string city = SelectCity(location, 0);
                    if (city != null)
                    {
                        samelocation.Location = city;
                        samelocation = GetLatLng(samelocation, 0);
                        break;
                    }
                    if (i > 0)
                    {
                        location = location.Remove(location.Length - locationgroup[i].Length - 1);
                    }
                }                
            }
            else
            {
                samelocation.Lat = null;
            }

            return samelocation;
        }

        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <param name="location"></param>
        /// <param name="errortimes"></param>
        /// <returns></returns>
        public static string SelectCity(string location, int errortimes)
        {
            string city = null;
            try
            {
                Prolongation.JHWebClient client = new Prolongation.JHWebClient();

                System.Threading.Thread.Sleep(100);
                string locationinfo = client.DownloadString("http://maps.google.com/maps/api/geocode/json?sensor=false&address=" + location).Replace(" ", "");
                string[] a = locationinfo.Split('\n');
                if (locationinfo.IndexOf("\"status\":\"OK\"") != -1 && locationinfo.IndexOf("\"administrative_area_level") != -1)
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a[i].IndexOf("\"administrative_area_level") != -1)
                        {
                            city = new Regex("\"long_name\":\"(.*?)\"").Matches(a[i - 2])[0].Groups[1].ToString();
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                if (errortimes < 3)
                {
                    city = SelectCity(location, errortimes + 1);
                }
            }

            return city;
        }

        /// <summary>
        /// 获取坐标城市经纬度
        /// </summary>
        /// <param name="samelocation"></param>
        /// <param name="errortimes">0</param>
        /// <returns></returns>
        public static SameLocation GetLatLngCityLatLng(SameLocation samelocation)
        {
            if (samelocation.Location != ",")
            {
                string location = samelocation.Location;
                string city = SelectLatLngCity(location, 0);
                if (city != null)
                {
                    samelocation.Location = city;
                    samelocation = GetLatLng(samelocation, 0);
                }
                else
                {
                    samelocation.Lat = null;
                }
            }
            else
            {
                samelocation.Lat = null;
            }

            return samelocation;
        }

        /// 获取坐标城市信息
        /// </summary>
        /// <param name="location"></param>
        /// <param name="errortimes"></param>
        /// <returns></returns>
        public static string SelectLatLngCity(string location, int errortimes)
        {
            string city = null;
            try
            {
                Prolongation.JHWebClient client = new Prolongation.JHWebClient();

                System.Threading.Thread.Sleep(100);
                string locationinfo = client.DownloadString("http://maps.google.com/maps/api/geocode/json?sensor=false&address=" + location).Replace(" ", "");
                string[] a = locationinfo.Split('\n');
                if (locationinfo.IndexOf("\"status\":\"OK\"") != -1 && locationinfo.IndexOf("\"administrative_area_level") != -1)
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a[i].IndexOf("\"administrative_area_level") != -1)
                        {
                            city = new Regex("\"long_name\":\"(.*?)\"").Matches(a[i - 2])[0].Groups[1].ToString();
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                if (errortimes < 3)
                {
                    city = SelectLatLngCity(location, errortimes + 1);
                }
            }

            return city;
        }

        /// <summary>
        /// 获取File
        /// </summary>
        /// <param name="file"></param>
        /// <param name="format"></param>
        public static bool GetFile(string parentfile, string file, string format)
        {
            try
            {
                Prolongation.JHWebClient client = new Prolongation.JHWebClient();

                client.Headers.Add("Content-Type: multipart/form-data; boundary=-------------------------idiot");

                string poststring = "---------------------------idiot\r\n"
                    + "Content-Disposition: form-data; name=\"db\"\r\n\r\n"
                    + "nuccore\r\n"
                    + "---------------------------idiot\r\n"
                    + "Content-Disposition: form-data; name=\"file\"; filename=\"1.txt\"\r\n"
                    + "Content-Type: text/plain\r\n\r\n";

                foreach (string version in File.ReadAllLines(parentfile, Encoding.Default))
                {
                    poststring += version + "\r\n";
                }

                poststring += "\r\n" + "---------------------------idiot--\r\n";

                byte[] response;

                try
                {
                    response = client.UploadData("http://www.ncbi.nlm.nih.gov/portal/utils/batchentrez_p.cgi", "POST", Encoding.UTF8.GetBytes(poststring));
                }
                catch (Exception a)
                {
                    try
                    {
                        System.Threading.Thread.Sleep(100);
                        response = client.UploadData("http://www.ncbi.nlm.nih.gov/portal/utils/batchentrez_p.cgi", "POST", Encoding.UTF8.GetBytes(poststring));
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }

                string responsestring = Encoding.UTF8.GetString(response);
                string key = new Regex("QueryKey=(.*?)\"").Matches(responsestring.Replace(" ", "").Replace("\n", ""))[0].Groups[1].ToString();
                string qty = new Regex("for(.*?)UID").Matches(responsestring.Replace(" ", "").Replace("\n", ""))[0].Groups[1].ToString();

                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        client.DownloadString("http://www.ncbi.nlm.nih.gov/sites/entrez?db=nuccore&cmd=HistorySearch&QueryKey=" + key); 
                        break;
                    }
                    catch (Exception)
                    {
                        System.Threading.Thread.Sleep(100);
                        if (i == 4) return false; ;
                    }
                }

                //格式筛选
                switch (format)
                {
                    case "GenBank":
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                client.DownloadFile("http://www.ncbi.nlm.nih.gov/sviewer/viewer.cgi?tool=portal&sendto=on&log$=seqview&db=nuccore&dopt=genbank&sort=&query_key=" + key + "&qty=" + qty + "&filter=all", file + ".gb");
                                break;
                            }
                            catch (Exception)
                            {
                                System.Threading.Thread.Sleep(100);
                                if (i == 4) return false; ;
                            }
                        }
                        break;

                    case "GenBank(Full)":
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                client.DownloadFile("http://www.ncbi.nlm.nih.gov/sviewer/viewer.cgi?tool=portal&sendto=on&log$=seqview&db=nuccore&dopt=gbwithparts&sort=&query_key=" + key + "&qty=" + qty + "&filter=all", file + ".gb");
                                break;
                            }
                            catch (Exception)
                            {
                                System.Threading.Thread.Sleep(100);
                                if (i == 4) return false; ;
                            }
                        }
                        break;

                    case "FASTA":
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                client.DownloadFile("http://www.ncbi.nlm.nih.gov/sviewer/viewer.cgi?tool=portal&sendto=on&log$=seqview&db=nuccore&dopt=fasta&sort=&query_key=" + key + "&qty=" + qty + "&filter=all", file + ".fasta");
                                break;
                            }
                            catch (Exception e)
                            {
                                string a = e.ToString();
                                System.Threading.Thread.Sleep(100);
                                if (i == 4) return false; ;
                            }
                        }
                        break;

                    case "XML":
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                client.DownloadFile("http://www.ncbi.nlm.nih.gov/sviewer/viewer.cgi?tool=portal&sendto=on&log$=seqview&db=nuccore&dopt=xml&sort=&query_key=" + key + "&qty=" + qty + "&filter=all", file + ".xml");
                                break;
                            }
                            catch (Exception)
                            {
                                System.Threading.Thread.Sleep(100);
                                if (i == 4) return false; ;
                            }
                        }
                        break;

                    case "INSDSeq XML":
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                client.DownloadFile("http://www.ncbi.nlm.nih.gov/sviewer/viewer.cgi?tool=portal&sendto=on&log$=seqview&db=nuccore&dopt=gbc_xml&sort=&query_key=" + key + "&qty=" + qty + "&filter=all", file + ".gbc.xml");
                                break;
                            }
                            catch (Exception)
                            {
                                System.Threading.Thread.Sleep(100);
                                if (i == 4) return false; ;
                            }
                        }
                        break;

                    case "TinySeq XML":
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                client.DownloadFile("http://www.ncbi.nlm.nih.gov/sviewer/viewer.cgi?tool=portal&sendto=on&log$=seqview&db=nuccore&dopt=fasta_xml&sort=&query_key=" + key + "&qty=" + qty + "&filter=all", file + ".fasta.xml");
                                break;
                            }
                            catch (Exception)
                            {
                                System.Threading.Thread.Sleep(100);
                                if (i == 4) return false; ;
                            }
                        }
                        break;

                    case "Feature Table":
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                client.DownloadFile("http://www.ncbi.nlm.nih.gov/sviewer/viewer.cgi?tool=portal&sendto=on&log$=seqview&db=nuccore&dopt=ft&sort=&query_key=" + key + "&qty=" + qty + "&filter=all", file + ".txt");
                                break;
                            }
                            catch (Exception)
                            {
                                System.Threading.Thread.Sleep(100);
                                if (i == 4) return false; ;
                            }
                        }
                        break;

                    case "GI List":
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                client.DownloadFile("http://www.ncbi.nlm.nih.gov/sviewer/viewer.cgi?tool=portal&sendto=on&log$=seqview&db=nuccore&dopt=gilist&sort=&query_key=" + key + "&qty=" + qty + "&filter=all", file + ".txt");
                                break;
                            }
                            catch (Exception)
                            {
                                System.Threading.Thread.Sleep(100);
                                if (i == 4) return false; ;
                            }
                        }
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                string b = e.ToString();
                return false;
            }
        }
    }
}
