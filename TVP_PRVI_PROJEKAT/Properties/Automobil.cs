using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP_PRVI_PROJEKAT
{
    class Automobil
    {
        int id_auto,kubikaza,godiste,br_vrata;
        string marka,model, pogon, vrsta_menjaca, karoserija,gorivo;
        public Automobil(int id_auto,string marka,string model,int godiste,int kubikaza,string pogon,string vrsta_menjaca,string karoserija,string gorivo,int br_vrata)
        {
            this.id_auto = id_auto;
            this.marka = marka;
            this.model = model;
            this.godiste = godiste;
            this.kubikaza = kubikaza;
            this.pogon = pogon;
            this.vrsta_menjaca = vrsta_menjaca;
            this.karoserija = karoserija;
            this.gorivo = gorivo;
            this.br_vrata = br_vrata;

        }
        public int Id_auto
        {
            get { return id_auto; }
            set { id_auto = value; }
        }
        public int Kubikaza
        {
            get { return kubikaza; }
            set { kubikaza = value; }
        }
        public int Godiste
        {
            get { return godiste; }
            set { godiste = value; }
        }
        public int Br_vrata
        {
            get { return br_vrata; }
            set { br_vrata = value; }
        }
        public string Marka
        {
            get { return marka; }
            set { marka = value; }
        }
        public string Model
        {
            get { return model; }
            set { model = value; }
        }
        public string Pogon
        {
            get { return pogon; }
            set { pogon = value; }
        }
        public string Vrsta_menjaca
        {
            get { return vrsta_menjaca; }
            set { vrsta_menjaca = value; }
        }
        public string Karoserija
        {
            get { return karoserija; }
            set { karoserija = value; }
        }
        public string Gorivo
        {
            get { return gorivo; }
            set { gorivo = value; }
        }
        public static List<Automobil> Procitaj_Automobil(StreamReader f)
        {
            List<Automobil> Automobili = new List<Automobil>();
            while (!f.EndOfStream)
            {
                string[] delovi_teksta = f.ReadLine().Split('|');
                 Automobil Automobil = new Automobil(Convert.ToInt32(delovi_teksta[0]), delovi_teksta[1], delovi_teksta[2], Convert.ToInt32(delovi_teksta[3]),Convert.ToInt32( delovi_teksta[4]), delovi_teksta[5],delovi_teksta[6],delovi_teksta[7],delovi_teksta[8],Convert.ToInt32(delovi_teksta[9]));
                Automobili.Add(Automobil);

            }
            f.Close();
            return Automobili;
        }
        public static int UpsiNovogAutomobila(StreamWriter fajl, Automobil Auto, List<Automobil> Automobili)
        {
            int i = 1;
            foreach (Automobil AUTO in Automobili)
            {
                if (AUTO.Marka.Contains(Auto.Marka) && AUTO.Model.Contains(Auto.Model) && AUTO.Godiste.ToString().Contains(Auto.Godiste.ToString())&& AUTO.Gorivo.Contains(Auto.Gorivo))
                {
                    i = 0;
                }
                if (AUTO.Id_auto.ToString().Contains(Auto.Id_auto.ToString()))
                {
                    i = -1;
                }


            }
            if (i == 1)
            {

                fajl.WriteLine(Auto.Id_auto + "|" + Auto.Marka+ "|" + Auto.Model+ "|" + Auto.Godiste + "|" + Auto.Kubikaza+ "|" + Auto.Pogon+"|"+Auto.vrsta_menjaca+"|"+Auto.Karoserija+"|"+Auto.Gorivo+"|"+Auto.br_vrata);
                fajl.Flush();
                fajl.Close();
                ++i;
            }

            return i;
        }
        public static int Brisi_Automobil(int id_automobil, string path)
        {
            FileStream f = new FileStream(path, FileMode.Open);
            StreamReader r = new StreamReader(f);
            string text = "", ostali = "";
            while (!r.EndOfStream)
            {
                text = r.ReadLine();
                if (text.Split('|')[0] != id_automobil + "")
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
            return 1;
        }
        public static int izmeni(string path, string id_auto, string marka, string model, string godiste, string kubikaza, string pogon, string vrsta_menjaca, string karoserija, string gorivo, string br_vrata)
        {
            FileStream f = new FileStream(path, FileMode.Open);
            StreamReader r = new StreamReader(f);
            string text = "", ostali = "";
            while (!r.EndOfStream)
            {
                text = r.ReadLine();
                if (text.Split('|')[0] != id_auto)
                {
                    ostali += (text + "\r\n");
                }
                else
                {
                    ostali += (id_auto + "|" + marka + "|" + model + "|" + godiste + "|" + kubikaza + "|" + pogon + "|" + vrsta_menjaca + "|" + karoserija + "|" + gorivo + "|" + br_vrata + "\r\n");
                }
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











    }
}
