using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovaTombola.Models;

namespace NovaTombola.Controllers
{
    public class HomeController : Controller
    {
        private readonly LotoizvlacenjeContext db;

        public HomeController(LotoizvlacenjeContext _db)
        {
            db = _db;
        }

        public IActionResult Index(string gr)
        {
            ViewBag.Greska = gr;
            return View();
        }
        public IActionResult Index2(string Ime, [Bind("KombinacijaId,IgracId,PrviBroj,DrugiBroj,TreciBroj,CetvrtiBroj,PetiBroj,SestiBroj")] Kombinacija kombinacija, string key, int id)
        {

            var listaKombinacija2 = db.Kombinacije.Include(p => p.Igrac);

            ViewBag.DobitnaKombinacija = "!!!!!!!!!!!";
            if (key == "!")
            {

                Random rnd = new Random();
                var gkokombinacija = Enumerable.Range(1, 36).OrderBy(x => rnd.Next()).Take(6).ToList();

                ViewBag.DobitnaKombinacija = "";
                List<int> kombinacijaizbabaze = new List<int>();
                foreach (Kombinacija kombinacijaizbaze in listaKombinacija2)
                {
                    kombinacijaizbabaze.Add(kombinacijaizbaze.PrviBroj);
                    kombinacijaizbabaze.Add(kombinacijaizbaze.DrugiBroj);
                    kombinacijaizbabaze.Add(kombinacijaizbaze.TreciBroj);
                    kombinacijaizbabaze.Add(kombinacijaizbaze.CetvrtiBroj);
                    kombinacijaizbabaze.Add(kombinacijaizbaze.PetiBroj);
                    kombinacijaizbabaze.Add(kombinacijaizbaze.SestiBroj);

                    if (kombinacijaizbabaze.SequenceEqual(gkokombinacija))
                    {
                        ViewBag.DobitnaKombinacija = gkokombinacija.ToString();
                        int idKomb = kombinacijaizbaze.KombinacijaId;
                        int idIgrac = kombinacijaizbaze.IgracId;
                        
                        return RedirectToAction("DobitnaKombinacija", new { idKomb, idIgrac });
                    }
                }
                string gkokokombinacija = "";
                foreach (int broj in gkokombinacija)
                {
                    gkokokombinacija += broj.ToString();
                    gkokokombinacija += " ";
                }
                ViewBag.gkombinacija = gkokokombinacija;

                return View(listaKombinacija2.ToList());
            }

            string xxxxx = Ime;
            int vrednost;
            if (kombinacija.PrviBroj == kombinacija.DrugiBroj || kombinacija.PrviBroj == kombinacija.TreciBroj || kombinacija.PrviBroj == kombinacija.CetvrtiBroj
                || kombinacija.PrviBroj == kombinacija.PetiBroj || kombinacija.PrviBroj == kombinacija.SestiBroj || kombinacija.DrugiBroj == kombinacija.PrviBroj || kombinacija.DrugiBroj == kombinacija.TreciBroj || kombinacija.DrugiBroj == kombinacija.CetvrtiBroj
               || kombinacija.DrugiBroj == kombinacija.PetiBroj || kombinacija.DrugiBroj == kombinacija.SestiBroj || kombinacija.TreciBroj == kombinacija.DrugiBroj || kombinacija.TreciBroj == kombinacija.PrviBroj || kombinacija.TreciBroj == kombinacija.CetvrtiBroj
               || kombinacija.TreciBroj == kombinacija.PetiBroj || kombinacija.TreciBroj == kombinacija.SestiBroj|| kombinacija.CetvrtiBroj == kombinacija.DrugiBroj || kombinacija.CetvrtiBroj == kombinacija.TreciBroj || kombinacija.CetvrtiBroj == kombinacija.PrviBroj
               || kombinacija.CetvrtiBroj == kombinacija.PetiBroj || kombinacija.CetvrtiBroj == kombinacija.SestiBroj|| kombinacija.PetiBroj == kombinacija.DrugiBroj || kombinacija.PetiBroj == kombinacija.TreciBroj || kombinacija.PetiBroj == kombinacija.CetvrtiBroj
               || kombinacija.PetiBroj == kombinacija.PrviBroj || kombinacija.PetiBroj == kombinacija.SestiBroj|| kombinacija.SestiBroj == kombinacija.DrugiBroj || kombinacija.SestiBroj == kombinacija.TreciBroj || kombinacija.SestiBroj == kombinacija.CetvrtiBroj
               || kombinacija.SestiBroj == kombinacija.PetiBroj || kombinacija.SestiBroj == kombinacija.PrviBroj)
            {
                string a = "Morate uneti razlicite brojeve";

                if (int.TryParse(xxxxx, out vrednost))
                {
                    a += " i ime ne sme biti sastavljeno od brojeva.";
                    return RedirectToAction("Index", new { gr = a });
                }
                return RedirectToAction("Index", new { gr = a });
            }

            if (int.TryParse(xxxxx, out vrednost))
            {
                string a = "Ime ne može biti broj.";
                return RedirectToAction("Index", new { gr = a });
            }

            List<Igrac> listaIgraca = db.Igraci.ToList();
            Igrac postojaniIgrac = new Igrac();

            foreach (Igrac i in listaIgraca)
            {
                if (i.Ime == Ime)
                {
                    postojaniIgrac = i;
                }
            }
            
            if (postojaniIgrac.Ime == null)
            {
                postojaniIgrac.Ime = Ime;
                try
                {
                    db.Add(postojaniIgrac);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    var listaKombinacija1 = db.Kombinacije.Include(p => p.Igrac);
                    foreach (Kombinacija komb in listaKombinacija1)
                    {
                        ViewBag.Kombinacija += komb.PrviBroj.ToString() + " " + komb.DrugiBroj.ToString() + " " + komb.TreciBroj.ToString() + " " + komb.CetvrtiBroj.ToString() + " " + komb.PetiBroj.ToString() + " " + komb.SestiBroj.ToString() + "|";

                    }
                    return View(listaKombinacija1.ToList());
                }
                
            }

            kombinacija.IgracId = postojaniIgrac.IgracId;
            ViewBag.Greska = "";
            try
            {
                db.Add(kombinacija);
                db.SaveChanges();
            }
            catch (Exception)
            {
                ViewBag.Greska = "Greska!";
            }
            var listaKombinacija = db.Kombinacije.Include(p => p.Igrac);

            foreach (Kombinacija komb in listaKombinacija)
            {
                ViewBag.Kombinacija += komb.PrviBroj.ToString() + " " + komb.DrugiBroj.ToString() + " " + komb.TreciBroj.ToString() + " " + komb.CetvrtiBroj.ToString() + " " + komb.PetiBroj.ToString() + " " + komb.SestiBroj.ToString() + "|";
                
            }
            return View(listaKombinacija.ToList());
        }
        public IActionResult DobitnaKombinacija(int idKomb, int idIgrac)
        {
            string asa = idKomb.ToString();

            if (asa != "0")
            {
                Pobednik pobednik = new Pobednik();
                pobednik.PobednickaKombinacijaId = idKomb;
                pobednik.PobednickiIgracId = idIgrac;

                if (ModelState.IsValid)
                {
                    db.Add(pobednik);
                    db.SaveChanges();
                    Igrac igra = db.Igraci.Find(pobednik.PobednickiIgracId);
                    Kombinacija kombi = db.Kombinacije.Find(pobednik.PobednickaKombinacijaId);

                    ViewBag.PobednikIme = igra.Ime;
                    ViewBag.PobednickaKombinacija = kombi.PrviBroj + " " + kombi.DrugiBroj + " " + kombi.TreciBroj + " " + kombi.CetvrtiBroj + " " + kombi.PetiBroj + " " + kombi.SestiBroj;
                }
            }

            var listaPobednika = db.Pobednici.Include(p => p.PobednickiIgrac).Include(p => p.PobednickaKombinacija);
            return View(listaPobednika.ToList());
        }

        public PartialViewResult _Unesi(string Ime, [Bind("KombinacijaId,IgracId,PrviBroj,DrugiBroj,TreciBroj,CetvrtiBroj,PetiBroj,SestiBroj")] Kombinacija kombinacija)
        {
            List<Igrac> listaIgraca = db.Igraci.ToList();
            Igrac postojaniIgrac = new Igrac();

            foreach (Igrac i in listaIgraca)
            {
                if (i.Ime == Ime)
                {
                    postojaniIgrac = i;
                }
            }


            if (postojaniIgrac == null)
            {
                postojaniIgrac.Ime = Ime;

                db.Add(postojaniIgrac);
                db.SaveChanges();
            }

            kombinacija.IgracId = postojaniIgrac.IgracId;

            try
            {
                db.Add(kombinacija);
                db.SaveChanges();
            }
            catch (Exception)
            {
                ViewBag.Greska = "Greska!";
            }

            var listaKombinacija = db.Kombinacije.Include(p => p.Igrac);
            return PartialView(listaKombinacija.ToList());
        }

        public IActionResult Izvlacenje()
        {
            var listaKombinacija = db.Kombinacije.Include(p => p.Igrac);
            return View(listaKombinacija.ToList());
        }

        public IActionResult IzvlacenjeBrojeva()
        {
            
            string key = "!";
            return RedirectToAction("Index2", new { key });
        }

        public PartialViewResult _Izvlacenje()
        {
            var a = db.Kombinacije;
            //var rnd = new Random();
            //var rndBrijevi = Enumerable.Range(1, 36).OrderBy(x => rnd.Next()).Take(6).ToList();

            return PartialView(a);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
