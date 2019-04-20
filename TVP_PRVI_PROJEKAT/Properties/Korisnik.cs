using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP_PRVI_PROJEKAT
{
    public class Korisnik
    {
        protected int id_korisnik;
        protected string ime, prezime, jmbg, telefon, datum_rodjenja;
        public Korisnik(int id_korisnik, string ime, string prezime, string jmbg, string datum_rodjenja, string telefon)
        {
            this.id_korisnik = id_korisnik;
            this.jmbg = jmbg;
            this.telefon = telefon;
            this.ime = ime;
            this.prezime = prezime;
            this.datum_rodjenja = datum_rodjenja;
        }
        public int Id_korisnik
        {
            get { return id_korisnik; }
            set { id_korisnik = value; }
        }
        public string Jmbg
        {
            get { return jmbg; }
            set { jmbg = value; }
        }
        public string Telefon
        {
            get { return telefon; }
            set { telefon = value; }
        }
        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }
        public string Prezime
        {
            get { return prezime; }
            set { prezime = value; }
        }
        public string Datum_rodjenja
        {
            get { return datum_rodjenja; }
            set { datum_rodjenja = value; }
        }

        public static List<Korisnik> Procitaj_korisnike(StreamReader f)
        {
            List<Korisnik> Korisnici = new List<Korisnik>();
            while (!f.EndOfStream)
            {
                string[] delovi_teksta = f.ReadLine().Split('|');
                Korisnik korisnik = new Korisnik(Convert.ToInt32(delovi_teksta[0]), delovi_teksta[1], delovi_teksta[2], delovi_teksta[3], delovi_teksta[4], delovi_teksta[5]);
                Korisnici.Add(korisnik);

            }
            f.Close();
            return Korisnici;
        }
        public static int UpsiNovogKorisnika(StreamWriter fajl, Korisnik Korisnik, List<Korisnik> ListaKorisnika)
        {
            int i = 1;
            foreach (Korisnik x in ListaKorisnika)
            {
                if (x.Ime.Contains(Korisnik.Ime) && x.Prezime.Contains(Korisnik.Prezime) && x.Jmbg.Contains(Korisnik.Jmbg))
                {
                    i = 0;
                }
                if (x.Id_korisnik.ToString().Contains(Korisnik.Id_korisnik.ToString()))
                {
                    i = -1;
                }


            }
            if (i == 1)
            {

                fajl.WriteLine(Korisnik.Id_korisnik + "|" + Korisnik.Ime + "|" + Korisnik.Prezime + "|" + Korisnik.Jmbg + "|" + Korisnik.Datum_rodjenja + "|" + Korisnik.Telefon.Substring(1, 3) + Korisnik.Telefon.Substring(5, 3) + Korisnik.Telefon.Substring(9, 4));
                fajl.Flush();
                fajl.Close();
                ++i;
            }

            return i;
        }
        public static int Brisi_Korisnika(int id_korisnika, string path)
        {
            FileStream f = new FileStream(path, FileMode.Open);
            StreamReader r = new StreamReader(f);
            string text = "", ostali = "";
            while (!r.EndOfStream)
            {
                text = r.ReadLine();
                if (text.Split('|')[0] != id_korisnika + "")
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
        public static int izmeni(string path, string id_korisnika, string ime, string prezime, string jmbg, string godiste, string telefon)
        {
            FileStream f = new FileStream(path, FileMode.Open);
            StreamReader r = new StreamReader(f);
            string text = "", ostali = "";
            while (!r.EndOfStream)
            {
                text = r.ReadLine();
                if (text.Split('|')[0] != id_korisnika)
                {
                    ostali += (text + "\r\n");
                }
                else
                {
                    ostali += (id_korisnika + "|" + ime + "|" + prezime + "|" + jmbg + "|" + godiste + "|" + telefon + "\r\n");
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
        public class Kupac : Korisnik
        {
            string sifra_kupca;
            string kor_ime_kupca;
            public Kupac(int id_korisnik, string ime, string prezime, string jmbg, string datum_rodjenja, string telefon) : base(id_korisnik, ime, prezime, jmbg, datum_rodjenja, telefon)
            {
            }


            public string Sifra_kupca
            {
                get { return Jmbg.ToString().Substring(0, 4)+Ime[0].ToString().ToLower()+Ime[1].ToString().ToLower(); }
                set { sifra_kupca = value; }
            }
            public string Kor_ime_kupca
            {
                get { return Ime[0].ToString().ToLower() + "_" + Prezime[0].ToString().ToLower() + "_" + Jmbg.ToString().Substring(0, 4); }
                set { kor_ime_kupca = value; }
            }
        public static List<Kupac> Procitaj_Kupce(StreamReader f)
        {
            List<Kupac> Kupci = new List<Kupac>();
            while (!f.EndOfStream)
            {
                string[] delovi_teksta = f.ReadLine().Split('|');
                Kupac admin = new Kupac(Convert.ToInt32(delovi_teksta[0]), delovi_teksta[1], delovi_teksta[2], delovi_teksta[3], delovi_teksta[4], delovi_teksta[5]);
                Kupci.Add(admin);

            }
            f.Close();
            return Kupci;
        }

    }
        public class Administrator : Korisnik
        {
            string sifra_administratora;
            string kor_ime_administratora;
            public Administrator(int id_korisnik, string ime, string prezime, string jmbg, string datum_rodjenja, string telefon) : base(id_korisnik, ime, prezime, jmbg, datum_rodjenja, telefon)
            {
            }

            public string Sifra_adminstratora
            {
                get { return Jmbg.ToString().Substring(0, 4) + Ime.Substring(0, 2).ToUpper(); }
                set { sifra_administratora = value; }
            }
            public string Kor_ime_administratora
            {
                get { return Ime[0].ToString().ToUpper() + "_" + Prezime[0].ToString().ToUpper() + "_" + Jmbg.ToString().Substring(0, 4); }
                set { kor_ime_administratora = value; }
            }
            public static List<Administrator> Procitaj_Admine(StreamReader f)
            {
                List<Administrator> Admini = new List<Administrator>();
                while (!f.EndOfStream)
                {
                    string[] delovi_teksta = f.ReadLine().Split('|');
                    Administrator admin = new Administrator(Convert.ToInt32(delovi_teksta[0]), delovi_teksta[1], delovi_teksta[2], delovi_teksta[3], delovi_teksta[4], delovi_teksta[5]);
                    Admini.Add(admin);

                }
            f.Close();
            return Admini;
            }
        }
    }

