using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP_PRVI_PROJEKAT
{
    class Ponuda
    {
        int id_automobila;
        DateTime datum_od, datum_do;
        int cena_po_danu;
        public Ponuda(int id_automobila,DateTime datum_od,DateTime datum_do,int cena_po_danu)
        {
            this.id_automobila = id_automobila;
            this.datum_od = datum_od;
            this.datum_do = datum_do;
            this.cena_po_danu = cena_po_danu;
        }
        public int Id_automobila
        {
            get { return id_automobila; }
            set { id_automobila = value; }
        }
        public DateTime Datum_od
        {
            get { return datum_od; }
            set { datum_od = value; }

        }
        public DateTime Datum_do
        {
            get { return datum_do; }
            set { datum_do = value; }
        }
        public int Cena_po_danu
        {
            get { return cena_po_danu; }
            set { cena_po_danu = value; }
        }
        public static int NovaPonuda(StreamWriter fajl, Ponuda Ponuda, List<Ponuda> Ponude, DateTime select_od, DateTime select_do)
        {
            int i = 1;
            foreach (Ponuda ponuda in Ponude)
            {
                if (ponuda.Id_automobila.ToString().Contains(Ponuda.Id_automobila.ToString()))
                {
                    if ((ponuda.Cena_po_danu.ToString().Contains(Ponuda.Cena_po_danu + "") && ponuda.Datum_do.ToString().Contains(Ponuda.Datum_do + "") && ponuda.Datum_od.ToString().Contains(Ponuda.Datum_od + "")))
                        i = -1;
                    else if (!(select_od<ponuda.Datum_od && select_do<ponuda.Datum_od) && !(ponuda.Datum_do<select_od && ponuda.Datum_do<select_do) && (ponuda.Datum_od<ponuda.Datum_do) && (select_od <select_do))
                        i = 0;
                }
            }
                if (i == 1)
                {

                    fajl.WriteLine(Ponuda.Id_automobila + "|" + Ponuda.Datum_od + "|" + Ponuda.Datum_do + "|" + Ponuda.Cena_po_danu);
                    fajl.Flush();
                    fajl.Close();
                    ++i;
                }

                return i;
            }
        
        public static List<Ponuda> Procitaj_Ponude(StreamReader f)
        {
            List<Ponuda> Ponude= new List<Ponuda>();
            while (!f.EndOfStream)
            {
                string[] delovi_teksta = f.ReadLine().Split('|');
                Ponuda Ponuda = new Ponuda(Convert.ToInt32(delovi_teksta[0]), Convert.ToDateTime(delovi_teksta[1]),Convert.ToDateTime(delovi_teksta[2]),Convert.ToInt32(delovi_teksta[3]));
                Ponude.Add(Ponuda);

            }
            f.Close();
            return Ponude;
        }
        public static int Brisi_Ponudu(int id_ponuda, string path)
        {
            FileStream f = new FileStream(path, FileMode.Open);
            StreamReader r = new StreamReader(f);
            string text = "", ostali = "";
            int i = 0;
            while (!r.EndOfStream)
            {
                text = r.ReadLine();
                if (i!= id_ponuda)
                {
                    ostali += (text + "\r\n");
                }
                i++;
            }

            r.Close();
            f.Close();
            f = new FileStream(path, FileMode.Create);
            StreamWriter w = new StreamWriter(f);
            w.Write(ostali);
            w.Close();
            f.Close();
            return 1;
        }
        public static int Izmeni(string path, int id_automobila, DateTime datum_od, DateTime datum_do, int cena_po_danu,List<Ponuda>Ponude)
        {
            FileStream f = new FileStream(path, FileMode.Open);
            StreamReader r = new StreamReader(f);
            string text = "", ostali = "";
            string DOD = "", DDO="";
            string ID="";
            int i = 0;
            List<Ponuda> P = new List<Ponuda>();
            foreach(Ponuda ponuda in Ponude)
            {
                
               if (id_automobila == ponuda.Id_automobila && datum_od==ponuda.Datum_od && datum_do==ponuda.Datum_do)
                {
                    DOD = datum_od+"";
                    DDO = datum_do+"";
                    ID = id_automobila+"";
                    
                }
               
                
            }
                  while (!r.EndOfStream)
                  {
                text = r.ReadLine();
                if (ID==text.Split('|')[0] && text.Split('|')[1]==DOD && text.Split('|')[2]==DDO)
                {
                    
                     ostali += (id_automobila + "|" + datum_od + "|" + datum_do + "|" + cena_po_danu + "\r\n"); i++;
                }
                else
                {
                    ostali += (text + "\r\n"); 
                }
                }
            r.Close();
            f.Close();
            f = new FileStream(path, FileMode.Create);
            StreamWriter w = new StreamWriter(f);
            w.Write(ostali);
            w.Close();
            f.Close();
            return i;
        }



    }
}
