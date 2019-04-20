using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP_PRVI_PROJEKAT
{
    class Rezervacija
    {
        int id_automobil, id_kupac, cena;
        DateTime datum_od, datum_do;

        public Rezervacija(int id_automobil, int id_kupac, DateTime datum_od, DateTime datum_do) : base()
        {
            this.id_automobil = id_automobil;
            this.id_kupac = id_kupac;
            this.datum_do = datum_do;
            this.datum_od = datum_od;

        }
        public Rezervacija(int id_automobil, int id_kupac, DateTime datum_od, DateTime datum_do, int cena) : this(id_automobil, id_kupac, datum_od, datum_do)
        {
            this.cena = cena;

        }

        public int Id_automobil
        {
            get { return id_automobil; }
            set { id_automobil = value; }
        }
        public int Id_kupac
        {
            get { return id_kupac; }
            set { id_kupac = value; }
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
        public int Cena
        {
            get { return cena; }
            set { cena = value; }
        }
        public static List<Rezervacija> Procitaj_Rezervacije(StreamReader f)
        {
            List<Rezervacija> Rezervacije = new List<Rezervacija>();
            while (!f.EndOfStream)
            {
                string[] delovi_teksta = f.ReadLine().Split('|');
                Rezervacija Rezervacija = new Rezervacija(Convert.ToInt32(delovi_teksta[0]), Convert.ToInt32(delovi_teksta[1]), Convert.ToDateTime(delovi_teksta[2]), Convert.ToDateTime(delovi_teksta[3]), Convert.ToInt32(delovi_teksta[4]));
                Rezervacije.Add(Rezervacija);

            }
            f.Close();
            return Rezervacije;
        }
        public static int Racunaj_cenu(int id_automobila, DateTime selectovan_od, DateTime selectovan_do, List<Ponuda> Nudjenje)
        {
            int razlika_dana = 0;
            int kosta = 0;
            bool postoji = false;
            foreach (Ponuda Y in Nudjenje)
            {
                if (id_automobila == Y.Id_automobila)
                {
                    postoji = true;
                    if (selectovan_od >= Y.Datum_od && selectovan_do <= Y.Datum_do)
                    {
                        razlika_dana = (selectovan_do - selectovan_od).Days+1;
                        kosta += Y.Cena_po_danu*razlika_dana;
                    }
                 else if (selectovan_od <= Y.Datum_od && selectovan_do >= Y.Datum_do)
                    {
                        razlika_dana = (Y.Datum_do - Y.Datum_od).Days+1;
                        kosta+= Y.Cena_po_danu*razlika_dana;
                    }
                  else if (selectovan_do >= Y.Datum_od && selectovan_do <= Y.Datum_do && Y.Datum_od >= selectovan_od)
                    {
                        razlika_dana = (selectovan_do - Y.Datum_od).Days+1;
                        kosta += Y.Cena_po_danu*razlika_dana;
                    }
                    else if (selectovan_do >= Y.Datum_do && Y.Datum_do >= selectovan_od && selectovan_od >= Y.Datum_od)
                    {
                        razlika_dana = (Y.Datum_do - selectovan_od).Days+1;
                       kosta += Y.Cena_po_danu*razlika_dana;
                    }
                   
                }

            }
            if (postoji)
            {
                return kosta;
            }
            else
                return -1;
        }
        public static int Rezervisi(StreamWriter fajl,List<Rezervacija> Rezervacije,Rezervacija Rezervacija)
        {
            int i = 1;
            foreach (Rezervacija x in Rezervacije)
            {
            if(x.id_automobil.ToString().Contains(Rezervacija.Id_automobil+"") && x.Id_kupac.ToString().Contains(Rezervacija.Id_kupac + "") && x.Datum_do.ToString().Contains(Rezervacija.Datum_do + "")&& x.datum_od.ToString().Contains(Rezervacija.datum_od + ""))
                {
                    i = -1;
                }
               
            }

            if (i==1)
            {

                fajl.WriteLine(Rezervacija.Id_automobil + "|" +Rezervacija.Id_kupac+"|" + Rezervacija.Datum_od + "|" + Rezervacija.Datum_do+"|"+Rezervacija.Cena);
                fajl.Flush();
                fajl.Close();
                ++i;
            }

            return i;
        }
        public static int Brisi_Rezervaciju(string id_automobila,string id_korisnika, string path)
        {
            FileStream f = new FileStream(path, FileMode.Open);
            StreamReader r = new StreamReader(f);
            string text = "", ostali = "";
            while (!r.EndOfStream)
            {
                text = r.ReadLine();
                if (text.Split('|')[0]+"|"+text.Split('|')[1] != id_automobila + "|"+id_korisnika)
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
        public static int izmeni(string path,string id_automobila  ,string id_korisnika, string datum_od, string datum_do, string cena)
        {
            FileStream f = new FileStream(path, FileMode.Open);
            StreamReader r = new StreamReader(f);
            string text = "", ostali = "";
            while (!r.EndOfStream)
            {
                text = r.ReadLine();
                if (text.Split('|')[0]+"|"+text.Split('|')[1] != id_automobila+"|"+id_korisnika)
                {
                    ostali += (text + "\r\n");
                }
                else
                {
                    ostali += (id_automobila + "|" + id_korisnika + "|" + datum_od + "|" + datum_do + "|" +cena+ "\r\n");
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
