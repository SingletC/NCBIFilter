using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace NCBIFilter
{
    public partial class Main : Form
    {
        static public string FileName = "";
        static public string ParentDirectory = "";
        static public string name = "";
        static public string Format = "";
        static public string Check = "";

        public Main()
        {
            InitializeComponent();
            Floder_Combobox.SelectedIndex = 0;
            Format_Combobox.SelectedIndex = 0;
            Cache.Connection = Prolongation.Sql.OpenSql();
        }

        private void LoadFile_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.Filter = "All Files Surport(*.*)|*.*";
            FileDialog.Title = "Choose";
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                File_Textbox.Text = FileDialog.FileName;
                FileName = FileDialog.FileName;
            }

            Cache.DnaBlock = Basic.GetDnaBlock(File.ReadAllLines(FileName, Encoding.Default));
            System.Threading.Tasks.Parallel.For(0, Cache.DnaBlock.Count, (i) => { Cache.DnaBlock[i] = Basic.GetVersion(Cache.DnaBlock[i]); });

            MessageBox.Show("Load Success");
        }

        private void Output_Button_Click(object sender, EventArgs e)
        {
            if (FileName != "")
            {
                if (SamePrimers_Check.Checked == true)
                {
                    Check += "1";
                }

                if (SameOrganism_Check.Checked == true)
                {
                    Check += "2";
                }

                if (SameCountry_Check.Checked == true)
                {
                    Check += "3";
                }

                if (SameCity_Check.Checked == true)
                {
                    Check += "4";
                }

                if (SameLatLon_Check.Checked == true)
                {
                    Check += "5";
                }

                Thread Thread = new Thread(Output);
                Thread.SetApartmentState(ApartmentState.STA);
                Thread.IsBackground = true;
                Thread.Start();
            }
            else
            {
                MessageBox.Show("Please choose the file first");
            }
        }

        private void Output()
        {
            int error = 0;

            try
            {
                ParentDirectory = FileName.Substring(0, FileName.TrimEnd('\\').LastIndexOf('\\'));
                name = FileName.Replace(ParentDirectory, "").Split('.')[0];

                foreach (var a in Check)
                {
                    if (a == '1')
                    {
                        string SamePrimersPath = ParentDirectory + name + "_SamePrimers\\";
                        Prolongation.NewFile(SamePrimersPath);

                        System.Threading.Tasks.Parallel.For(0, Cache.DnaBlock.Count, (i) => { Cache.DnaBlock[i] = Basic.GetPrimers(Cache.DnaBlock[i]); });
                        List<Advanced.SamePrimers> SamePrimers = Advanced.SelectPrimers(new List<Basic.DnaBlock>(Cache.DnaBlock.ToArray()));

                        int Null = -1;

                        FileStream File1 = new FileStream(SamePrimersPath + "PrimersTable.txt", FileMode.Create);
                        StreamWriter Writer1 = new StreamWriter(File1);
                        for (int i = 0; i < SamePrimers.Count; i++)
                        {
                            if (SamePrimers[i].Fwd_seq != null)
                            {
                                for (int j = 0; j < SamePrimers[i].Fwd_seq.Count; j++)
                                {
                                    if (j == 0)
                                    {
                                        Writer1.WriteLine(i + "    " + SamePrimers[i].Fwd_seq[j] + "    " + SamePrimers[i].Rev_seq[j]);
                                    }
                                    else
                                    {
                                        Writer1.WriteLine("     " + SamePrimers[i].Fwd_seq[j] + "    " + SamePrimers[i].Rev_seq[j]);
                                    }
                                }
                                Writer1.WriteLine();
                            }
                            else
                            {
                                Null = i;
                            }
                        }
                        Writer1.Flush();
                        Writer1.Close();
                        File1.Close();

                        System.Threading.Tasks.Parallel.For(0, SamePrimers.Count, (i) =>
                        {
                            FileStream File;
                            if (i != Null)
                            {
                                File = new FileStream(SamePrimersPath + i + ".txt", FileMode.Create);
                            }
                            else
                            {
                                File = new FileStream(SamePrimersPath + "Null.txt", FileMode.Create);
                            }
                            StreamWriter Writer = new StreamWriter(File);
                            foreach (string Version in SamePrimers[i].Version)
                            {
                                Writer.WriteLine(Version);
                            }
                            Writer.Flush();
                            Writer.Close();
                            File.Close();
                        });

                    }

                    if (a == '2')
                    {
                        string SameOrganismPath = ParentDirectory + name + "_SameOrganism\\";
                        Prolongation.NewFile(SameOrganismPath);

                        System.Threading.Tasks.Parallel.For(0, Cache.DnaBlock.Count, (i) => { Cache.DnaBlock[i] = Basic.GetOrganism(Cache.DnaBlock[i]); });
                        List<Advanced.SameOrganism> SameOrganism = Advanced.SelectOrganism(new List<Basic.DnaBlock>(Cache.DnaBlock.ToArray()));

                        System.Threading.Tasks.Parallel.For(0, SameOrganism.Count, (i) =>
                        {
                            FileStream File = new FileStream(SameOrganismPath + SameOrganism[i].Organism.Replace("/", " ").Replace(":", "-") + ".txt", FileMode.Create);
                            StreamWriter Writer = new StreamWriter(File);
                            foreach (string Version in SameOrganism[i].Version)
                            {
                                Writer.WriteLine(Version);
                            }
                            Writer.Flush();
                            Writer.Close();
                            File.Close();
                        });
                    }

                    if (a == '3')
                    {
                        string SameCountryPath = ParentDirectory + name + "_SameCountry\\";
                        Prolongation.NewFile(SameCountryPath);

                        System.Threading.Tasks.Parallel.For(0, Cache.DnaBlock.Count, (i) => { Cache.DnaBlock[i] = Basic.GetLocation(Cache.DnaBlock[i]); });

                        OutputLocation(SameCountryPath, Cache.DnaBlock);
                        List<Advanced.SameLocation> SameCountry = Advanced.SelectCountry(new List<Basic.DnaBlock>(Cache.DnaBlock.ToArray()));

                        System.Threading.Tasks.Parallel.For(0, SameCountry.Count, (i) =>
                        {
                            FileStream File;
                            if (SameCountry[i].Location != null)
                            {
                                File = new FileStream(SameCountryPath + SameCountry[i].Location.Replace("/", " ").Replace(":", "-") + ".txt", FileMode.Create);
                            }
                            else
                            {
                                File = new FileStream(SameCountryPath + "Null.txt", FileMode.Create);
                            }
                            StreamWriter Writer = new StreamWriter(File);
                            foreach (string Version in SameCountry[i].Version)
                            {
                                Writer.WriteLine(Version);
                            }
                            Writer.Flush();
                            Writer.Close();
                            File.Close();
                        });
                    }

                    if (a == '4')
                    {
                        string SameCityPath = ParentDirectory + name + "_SameCity\\";
                        Prolongation.NewFile(SameCityPath);

                        System.Threading.Tasks.Parallel.For(0, Cache.DnaBlock.Count, (i) => { Cache.DnaBlock[i] = Basic.GetLocation(Cache.DnaBlock[i]); });
                        OutputLocation(SameCityPath, Cache.DnaBlock);

                        List<Advanced.SameLocation> SameCity = Advanced.SelectLocation(new List<Basic.DnaBlock>(Cache.DnaBlock.ToArray()));
                        List<Advanced.SameLocation> SameLatLngCity = Advanced.SelectLatLng(new List<Basic.DnaBlock>(Cache.DnaBlock.ToArray()));

                        System.Threading.Tasks.Parallel.For(0, SameCity.Count, (i) =>
                        {
                            if (SameCity[i].Location != null && error <= 0)
                            {
                                SameCity[i] = Advanced.GetCityLatLng(SameCity[i]);
                                if (SameCity[i].Lat == "NetworkError")
                                {
                                    error++;
                                }
                            }
                        });

                        System.Threading.Tasks.Parallel.For(0, SameLatLngCity.Count, (i) =>
                        {
                            if (SameLatLngCity[i].Location != null && error <= 0)
                            {
                                SameLatLngCity[i] = Advanced.GetLatLngCityLatLng(SameLatLngCity[i]);
                                if (SameLatLngCity[i].Lat == "NetworkError")
                                {
                                    error++;
                                }
                            }
                        });



                        if (error <= 0)
                        {
                            for (int i = SameCity.Count() - 1; i >= 0; i--)
                            {
                                for (int j = SameLatLngCity.Count() - 1; j >= 0; j--)
                                {
                                    if (SameCity[i].Location == SameLatLngCity[j].Location)
                                    {
                                        var Temp = SameCity[i];
                                        Temp.Version = SameCity[i].Version.Union(SameLatLngCity[j].Version).ToList<string>();
                                        SameCity[i] = Temp;
                                        SameLatLngCity.RemoveAt(j);
                                    }
                                }
                            }

                            if (SameCity.Count() > 1)
                            {
                                for (int i = SameCity.Count() - 1; i > 0; i--)
                                {
                                    int j = i - 1;

                                    while (j >= 0)
                                    {
                                        if (SameCity[i].Location == SameCity[j].Location)
                                        {
                                            var Temp = SameCity[i];
                                            Temp.Version = SameCity[i].Version.Union(SameCity[j].Version).ToList<string>();
                                            SameCity[i] = Temp;
                                            SameCity.RemoveAt(j);
                                            i--;
                                        }
                                        j--;
                                    }
                                }
                            }

                            System.Threading.Tasks.Parallel.For(0, SameCity.Count, (i) =>
                            {
                                FileStream File;
                                if (SameCity[i].Lat != null)
                                {
                                    File = new FileStream(SameCityPath + SameCity[i].Location.Replace("/", " ").Replace(":", "-") + ".txt", FileMode.Create);
                                    StreamWriter Writer = new StreamWriter(File);
                                    foreach (string Version in SameCity[i].Version)
                                    {
                                        Writer.WriteLine(Version);
                                    }
                                    Writer.Flush();
                                    Writer.Close();
                                    File.Close();
                                }

                            });
                        }
                    }

                    if (a == '5')
                    {
                        string SameLatLonPath = ParentDirectory + name + "_SameLatLon\\";
                        Prolongation.NewFile(SameLatLonPath);

                        System.Threading.Tasks.Parallel.For(0, Cache.DnaBlock.Count, (i) => { Cache.DnaBlock[i] = Basic.GetLocation(Cache.DnaBlock[i]); });
                        OutputLocation(SameLatLonPath, Cache.DnaBlock);

                        List<Advanced.SameLocation> SameLatLon = Advanced.SelectLatLng(new List<Basic.DnaBlock>(Cache.DnaBlock.ToArray()));

                        System.Threading.Tasks.Parallel.For(0, SameLatLon.Count, (i) =>
                        {
                            FileStream File;
                            if (SameLatLon[i].Lat != null)
                            {
                                File = new FileStream(SameLatLonPath + SameLatLon[i].Lat + "," + SameLatLon[i].Lng + ".txt", FileMode.Create);
                            }
                            else
                            {
                                File = new FileStream(SameLatLonPath + "Null.txt", FileMode.Append);
                            }
                            StreamWriter Writer = new StreamWriter(File);
                            foreach (string Version in SameLatLon[i].Version)
                            {
                                Writer.WriteLine(Version);
                            }
                            Writer.Flush();
                            Writer.Close();
                            File.Close();
                        });
                    }
                }

                if (error > 0)
                {
                    MessageBox.Show("Classify City Failed,Classify Others Success");
                }
                else
                {
                    MessageBox.Show("ClassifySuccess");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There is something wrong. Please try again");
            }
        }

        private void OutputLocation(string file, List<Basic.DnaBlock> dnablock)
        {
            FileStream File = new FileStream(file + "LocationTable.txt", FileMode.Create);
            StreamWriter Writer = new StreamWriter(File);

            for (int i = 0; i < dnablock.Count; i++)
            {
                if (dnablock[i].Lat != null && dnablock[i].Lng != null)
                {
                    Writer.WriteLine(dnablock[i].Version + "    " + dnablock[i].Country + "    " + dnablock[i].Location + "    " + dnablock[i].Lat + "," + dnablock[i].Lng);
                }
                else
                {
                    Writer.WriteLine(dnablock[i].Version + "    " + dnablock[i].Country + "    " + dnablock[i].Location);
                }
            }

            Writer.Flush();
            Writer.Close();
            File.Close();
        }

        private void Download_Button_Click(object sender, EventArgs e)
        {
            if (FileName != "")
            {
                ParentDirectory = FileName.Substring(0, FileName.TrimEnd('\\').LastIndexOf('\\'));
                name = FileName.Replace(ParentDirectory, "").Split('.')[0];
                ParentDirectory = ParentDirectory + name + "_" + Floder_Combobox.SelectedItem.ToString();
                Format = Format_Combobox.SelectedItem.ToString();

                Thread Thread = new Thread(Download);
                Thread.SetApartmentState(ApartmentState.STA);
                Thread.IsBackground = true;
                Thread.Start();
            }
            else
            {
                MessageBox.Show("Please choose the file first");
            }
        }

        private void Download()
        {
            try
            {
                string Fail = "";

                if (Directory.Exists(ParentDirectory))
                {
                    DirectoryInfo Folder = new DirectoryInfo(ParentDirectory);
                    FileInfo[] Child = Folder.GetFiles("*");

                    Prolongation.NewFile(ParentDirectory + "\\" + Format);



                    System.Threading.Tasks.Parallel.For(0, Child.Count(), (i) =>
                    {
                        if (Child[i].Name != "PrimersTable.txt" && Child[i].Name != "Null.txt" && Child[i].Name != "LocationTable.txt")
                        {
                            if (!Advanced.GetFile(ParentDirectory + "\\" + Child[i].Name, ParentDirectory + "\\" + Format + "\\" + Child[i].Name, Format))
                            {
                                Fail += Child[i].Name + ",";
                            }
                        }
                    });

                    if (Fail != "")
                    {
                        MessageBox.Show("Number: " + Fail + "download failed");
                    }
                    else
                    {
                        MessageBox.Show("Download Success");
                    }
                }
                else
                {
                    MessageBox.Show("Can't find " + Format + " Floder");
                }
    
            }
            catch (Exception )
            {
                MessageBox.Show("There is something wrong. Please try again");
            }
        }

        private void BuildMap_Button_Click(object sender, EventArgs e)
        {
            if (FileName != "")
            {
                Thread Thread = new Thread(BuildMap);
                Thread.SetApartmentState(ApartmentState.STA);
                Thread.IsBackground = true;
                Thread.Start();
            }
            else
            {
                MessageBox.Show("Please choose the file first");
            }
        }

        private void BuildMap()
        {
            try
            {
                System.Threading.Tasks.Parallel.For(0, Cache.DnaBlock.Count, (i) => { Cache.DnaBlock[i] = Basic.GetLocation(Cache.DnaBlock[i]); });
                Cache.SameCountry = Advanced.SelectCountry(new List<Basic.DnaBlock>(Cache.DnaBlock.ToArray()));
                Cache.SameCity = Advanced.SelectLocation(new List<Basic.DnaBlock>(Cache.DnaBlock.ToArray()));
                Cache.SameLatLng = Advanced.SelectLatLng(new List<Basic.DnaBlock>(Cache.DnaBlock.ToArray()));
                Cache.SameLatLngCity = Advanced.SelectLatLng(new List<Basic.DnaBlock>(Cache.DnaBlock.ToArray())); 

                int error = 0;

                System.Threading.Tasks.Parallel.For(0, Cache.SameCountry.Count, (i) =>
                {
                    if (Cache.SameCountry[i].Location != null)
                    {
                        Cache.SameCountry[i] = Prolongation.Sql.MatchCountry(Cache.SameCountry[i], Cache.Connection);
                        if(Cache.SameCountry[i].Lat == "NetworkError")
                        {
                            error++;
                        }
                    }
                });

                System.Threading.Tasks.Parallel.For(0, Cache.SameCity.Count, (i) =>
                {
                    if (Cache.SameCity[i].Location != null)
                    {
                        Cache.SameCity[i] = Advanced.GetCityLatLng(Cache.SameCity[i]);
                        if (Cache.SameCity[i].Lat == "NetworkError")
                        {
                            error++;
                        }
                    }
                });

                System.Threading.Tasks.Parallel.For(0, Cache.SameLatLngCity.Count, (i) =>
                {
                    if (Cache.SameLatLngCity[i].Location != null)
                    {
                        Cache.SameLatLngCity[i] = Advanced.GetLatLngCityLatLng(Cache.SameLatLngCity[i]);
                        if (Cache.SameLatLngCity[i].Lat == "NetworkError")
                        {
                            error++;
                        }
                    }
                });

                if (error > 0)
                {
                    MessageBox.Show("Network error. Please try again");
                    Cache.SameCountry = null;
                    Cache.SameLatLng = null;
                    return;
                }

                

                MessageBox.Show("Build Success");
            }
            catch (Exception)
            {
                MessageBox.Show("There is something wrong. Please try again");
                Cache.SameCountry = null;
                Cache.SameLatLng = null;
            }
        }

        private void ShowMap_Button_Click(object sender, EventArgs e)
        {
            if (Cache.SameCountry.Count == 0 || Cache.SameLatLng.Count == 0)
            {
                MessageBox.Show("Please builds the map first");
            }
            else
            {
                MethodInvoker MethInvo = new MethodInvoker(ShowMap);
                BeginInvoke(MethInvo);
            }
        }

        private void ShowMap()
        {
            Map Map = new Map();
            Map.Show();
        }



    }
}
