using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace NCBIFilter
{
    class Cache
    {
        public static List<Basic.DnaBlock> DnaBlock = new List<Basic.DnaBlock>();
        public static List<Advanced.SameLocation> SameCountry = new List<Advanced.SameLocation>();
        public static List<Advanced.SameLocation> SameCity = new List<Advanced.SameLocation>();
        public static List<Advanced.SameLocation> SameLatLngCity = new List<Advanced.SameLocation>();
        public static List<Advanced.SameLocation> SameLatLng = new List<Advanced.SameLocation>();
        public static SQLiteConnection Connection = new SQLiteConnection();
    }
}
