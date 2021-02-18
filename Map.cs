using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace NCBIFilter
{
    public partial class Map : Form
    {   
        private int Count = 0;
        private string TempFileNamee = "";

        public Map()
        {
            InitializeComponent();

            MapControl.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            MapControl.MaxZoom = 18;
            MapControl.MinZoom = 2;
            MapControl.Zoom = 2;
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            if (Main.FileName != "")
            {
                string path = Main.FileName.Substring(0, Main.FileName.TrimEnd('\\').LastIndexOf('\\'));
                if (TempFileNamee != Main.FileName)
                {
                    if (!Directory.Exists(path + "\\" + Main.FileName.Replace(path, "").Split('.')[0] + "_Image"))
                    {
                        Directory.CreateDirectory(path + "\\" + Main.FileName.Replace(path, "").Split('.')[0] + "_Image");
                    }
                    TempFileNamee = Main.FileName;
                    Count = 0;
                }
                Image MapImage = MapControl.ToImage();
                MapImage.Save(path + "\\" + Main.FileName.Replace(path, "").Split('.')[0] + "_Image\\" + Count + ".png");
                Count++;
            }
        }

        private void ShowCountry_Button_Click(object sender, EventArgs e)
        {
            if (Cache.SameCountry.Count > 0)
            {
                MapControl.Overlays.Clear();
                DrawMap(Cache.SameCountry, GMarkerGoogleType.blue);
            }
        }

        private void ShowCity_Button_Click(object sender, EventArgs e)
        {
            if (Cache.SameCity.Count > 0)
            {
                MapControl.Overlays.Clear();
                DrawMap(Cache.SameCity, GMarkerGoogleType.yellow);
                DrawMap(Cache.SameLatLngCity, GMarkerGoogleType.yellow);
            }
        }

        private void LatLng_Button_Click(object sender, EventArgs e)
        {
            if (Cache.SameLatLng.Count > 0)
            {
                MapControl.Overlays.Clear();
                DrawMap(Cache.SameLatLng, GMarkerGoogleType.red_small);
            }
        }

        private void All_Button_Click(object sender, EventArgs e)
        {
            MapControl.Overlays.Clear();
            if (Cache.SameCountry.Count > 0)
            {
                DrawMap(Cache.SameCountry, GMarkerGoogleType.blue);
            }
            if (Cache.SameCity.Count > 0)
            {
                DrawMap(Cache.SameCity, GMarkerGoogleType.yellow);
                DrawMap(Cache.SameLatLngCity, GMarkerGoogleType.yellow);
            }
            if (Cache.SameLatLng.Count > 0)
            {
                DrawMap(Cache.SameLatLng, GMarkerGoogleType.red_small);
            }
        }

        private void DrawMap(List<Advanced.SameLocation> TempList, GMarkerGoogleType Color)
        {
            GMapOverlay MapOverlay = new GMapOverlay("mark");
            for (int i = 0; i < TempList.Count(); i++)
            {
                if (TempList[i].Lat != null)
                {
                    try
                    {
                        GMapMarker MapMarker = new GMarkerGoogle(new PointLatLng(double.Parse(TempList[i].Lat), double.Parse(TempList[i].Lng)), Color);
                        MapOverlay.Markers.Add(MapMarker);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            MapControl.Overlays.Add(MapOverlay);
            MapControl.ZoomAndCenterMarkers("mark");
        }



        




    }
}
