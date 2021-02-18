using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NCBIFilter
{
    class Basic
    {
        /// <summary>
        /// 信息块
        /// </summary>
        public struct DnaBlock
        {
            public List<string> Content { get; set; }
            public string Version { get; set; }
            public List<string> Fwd_seq { get; set; }
            public List<string> Rev_seq { get; set; }
            public string Organism { get; set; }
            public string Country { get; set; }
            public string Location { get; set; }
            public string Lat { get; set; }
            public string Lng { get; set; }
        }

        /// <summary>
        /// 切分大信息块，返回信息块集合
        /// </summary>
        /// <param name="OriginalContent"></param>
        /// <returns></returns>
        public static List<DnaBlock> GetDnaBlock(string[] OriginalContent)
        {
            List<DnaBlock> dnablock = new List<DnaBlock>();
            int last = 0;

            for (int i = 0; i < OriginalContent.Length; i++)
            {
                if (OriginalContent[i] == "//")
                {
                    List<string> content = new List<string>();

                    while (last <= i)
                    {
                        if (OriginalContent[last].Replace(" ", "") != "") content.Add(OriginalContent[last]);
                        last++;
                    }

                    dnablock.Add(new DnaBlock()
                    {
                        Content = content,
                    });
                }
            }

            return dnablock;
        }

        /// <summary>
        /// 获取信息块ID，返回信息块
        /// </summary>
        /// <param name="dnablock"></param>
        /// <returns></returns>
        public static DnaBlock GetVersion(DnaBlock dnablock)
        {
            for (int i = 0; i < dnablock.Content.Count(); i++)
            {
                if (dnablock.Content[i].IndexOf("VERSION") != -1)
                {
                    string version = dnablock.Content[i].Replace("VERSION", "").Replace(" ", "");
                    int start = version.IndexOf("GI:");
                    version = version.Remove(start, version.Length - start);
                    dnablock.Version = version;
                }
            }
            return dnablock;
        }

        /// <summary>
        /// 获取引物信息，返回信息块
        /// </summary>
        /// <param name="dnablock"></param>
        public static DnaBlock GetPrimers(DnaBlock dnablock)
        {
            List<string> Fwd_seq = new List<string>();
            List<string> Rev_seq = new List<string>();

            for (int i = 0; i < dnablock.Content.Count(); i++)
            {
                if (dnablock.Content[i].IndexOf("/PCR_primers") != -1)
                {
                    string pcr_primers = dnablock.Content[i].Replace(" ", "");

                    while (dnablock.Content[i + 1].IndexOf("/PCR_primers") == -1 && Regex.IsMatch(dnablock.Content[i + 1], @"                     /") == false && Regex.IsMatch(dnablock.Content[i + 1], @"                     (\S+)") == true)
                    {
                        i++;
                        pcr_primers += dnablock.Content[i].Replace(" ", "");
                    }

                    //获取fwd_seq
                    while (pcr_primers.IndexOf("fwd_seq:") != -1)
                    {
                        int start = pcr_primers.IndexOf("fwd_seq:") + "fwd_seq:".Length;
                        string fwd_seq = "";

                        while (pcr_primers[start] != ',' && pcr_primers[start] != '"')
                        {
                            fwd_seq += pcr_primers[start];
                            start++;
                        }
                        Fwd_seq.Add(fwd_seq);

                        int count = "fwd_seq:".Length + fwd_seq.Length;
                        pcr_primers = pcr_primers.Remove(start - count, count);
                    }

                    //获取rev_seq
                    while (pcr_primers.IndexOf("rev_seq:") != -1)
                    {
                        int start = pcr_primers.IndexOf("rev_seq:") + "rev_seq:".Length;
                        string rev_seq = "";

                        while (pcr_primers[start] != ',' && pcr_primers[start] != '"')
                        {
                            rev_seq += pcr_primers[start];
                            start++;
                        }
                        Rev_seq.Add(rev_seq);

                        int count = "rev_seq:".Length + rev_seq.Length;
                        pcr_primers = pcr_primers.Remove(start - count, count);
                    }
                }
            }

            if (Fwd_seq.Count != Rev_seq.Count)
            {
                if (Fwd_seq.Count > Rev_seq.Count)
                {
                    while (Fwd_seq.Count != Rev_seq.Count)
                    {
                        Rev_seq.Add("Null");
                    }
                }
                else
                {
                    while (Fwd_seq.Count != Rev_seq.Count)
                    {
                        Fwd_seq.Add("Null");
                    }
                }
            }
            dnablock.Fwd_seq = Fwd_seq;
            dnablock.Rev_seq = Rev_seq;
            return dnablock;
        }

        /// <summary>
        /// 获取物种信息，返回信息块
        /// </summary>
        /// <param name="dnablock"></param>
        /// <returns></returns>
        public static DnaBlock GetOrganism(DnaBlock dnablock)
        {
            for (int i = 0; i < dnablock.Content.Count(); i++)
            {
                if (dnablock.Content[i].IndexOf("/organism") != -1)
                {
                    string Organism = dnablock.Content[i];

                    while (Regex.IsMatch(dnablock.Content[i + 1], "/(.*?)=\"(.*?)\"") == false && Regex.IsMatch(dnablock.Content[i + 1], @"                     (\S+)") == true)
                    {
                        i++;
                        Organism += dnablock.Content[i].Replace("                     ", "");
                    }

                    Organism = Organism.Remove(Organism.Length - 1, 1).Replace("                     /organism=\"", "");

                    dnablock.Organism = Organism;
                }
            }

            return dnablock;
        }

        /// <summary>
        /// 获取地理信息，返回信息块
        /// </summary>
        /// <param name="dnablock"></param>
        /// <returns></returns>
        public static DnaBlock GetLocation(DnaBlock dnablock)
        {
            for (int i = 0; i < dnablock.Content.Count(); i++)
            {
                if (dnablock.Content[i].IndexOf("/country") != -1)
                {
                    string Location = dnablock.Content[i].Replace("                     /country=", "").Replace("\"", "");
                    string Lat = "";
                    string Lng = "";
                    string Country = "";

                    while (Regex.IsMatch(dnablock.Content[i + 1], "/(.*?)=\"(.*?)\"") == false && Regex.IsMatch(dnablock.Content[i + 1], @"                     (\S+)") == true)
                    {
                        i++;
                        Location += dnablock.Content[i].Replace("\"", "").Replace("                     ", "");
                    }

                    //转换坐标点
                    if (dnablock.Content[i + 1].IndexOf("/lat_lon=") != -1)
                    {
                        string LatLng = dnablock.Content[i + 1].Replace("\"", "").Replace(" ", "").Replace("/lat_lon=", "");
                        if (LatLng.IndexOf("N") != -1)
                        {
                            Lat = LatLng.Split('N')[0];
                            if (LatLng.Split('N')[1].IndexOf("E") != -1)
                            {
                                Lng = LatLng.Split('N')[1].Replace("E", "");
                            }
                            else
                            {
                                Lng = "-" + LatLng.Split('N')[1].Replace("W", "");
                            }
                        }
                        else
                        {
                            Lat = "-" + LatLng.Split('S')[0];
                            if (LatLng.Split('S')[1].IndexOf("E") != -1)
                            {
                                Lng = LatLng.Split('S')[1].Replace("E", "");
                            }
                            else
                            {
                                Lng = "-" + LatLng.Split('S')[1].Replace("W", "");
                            }
                        }
                    }

                    //获取国家
                    try
                    {
                        if (Location.IndexOf(':') != -1)
                        {
                            Country = Location.Split(':')[0];
                            Location = Location.Replace(Country + ":", "");
                        }
                        else
                        {
                            Country = Location;
                            Location = "";
                        }
                    }
                    catch (Exception)
                    {
                    }

                    if (Country != "")
                    {
                        dnablock.Country = Country;
                        dnablock.Location = Location;
                        if (Lat == "") Lat = null;
                        if (Lng == "") Lng = null;
                        dnablock.Lat = Lat;
                        dnablock.Lng = Lng;
                    }
                }
            }
            return dnablock;
        }
    }
}
