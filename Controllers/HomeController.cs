using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RejOsZag.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RejOsZag.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext dbContext;
        private EncjeOsobyZaginionejContext encjeOsobyZaginionejContext;

        public HomeController()
        {
            dbContext = new ApplicationDbContext();
            encjeOsobyZaginionejContext = new EncjeOsobyZaginionejContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewData["listaOsobZaginionych"] = podajAktualnąListeOsobZaginionych();

            if (czyJestAdminem())
            {
                var users = dbContext.Users.ToList();
                ViewData["czyAdmin"] = "T";
                ViewData["uzytkownicy"] = users;
            }
            else
            {
                ViewData["czyAdmin"] = "N";
            }

            return View();
        }

        [HttpGet]
        public ActionResult FiltrujOsobyZaginione(string plecZFormularza)
        {
            if(plecZFormularza == null || plecZFormularza == "")
            {
                ViewData["listaOsobZaginionych"] = podajAktualnąListeOsobZaginionych();
            }
            else
            {
                ViewData["listaOsobZaginionych"] = podajAktualnąListeOsobZaginionych().filtrujPoPlci(plecZFormularza);
            }
            
            if (czyJestAdminem())
            {
                var users = dbContext.Users.ToList();
                ViewData["czyAdmin"] = "T";
                ViewData["uzytkownicy"] = users;
            }
            else
            {
                ViewData["czyAdmin"] = "N";
            }

            return View("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UsunOsobeZaginiona(OsobaZaginiona osobaZaginiona)
        {
            var osobyZaginioneZBazyDanych = encjeOsobyZaginionejContext.osobyZaginione.ToList();
            foreach(EncjaOsobyZaginionej encjaOsobyZaginionej in osobyZaginioneZBazyDanych)
            {
                if(encjaOsobyZaginionej.Id == osobaZaginiona.id)
                {
                    encjeOsobyZaginionejContext.osobyZaginione.Remove(encjaOsobyZaginionej);
                    break;
                }
            }

            encjeOsobyZaginionejContext.SaveChanges();
            ViewData["listaOsobZaginionych"] = podajAktualnąListeOsobZaginionych();

            if (czyJestAdminem())
            {
                var users = dbContext.Users.ToList();
                ViewData["czyAdmin"] = "T";
                ViewData["uzytkownicy"] = users;
            }
            else
            {
                ViewData["czyAdmin"] = "N";
            }

            return View("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult ManageUsers()
        {
            if (czyJestAdminem())
            {
                var users = dbContext.Users.ToList();
                ViewData["czyAdmin"] = "T";
                ViewData["uzytkownicy"] = users;
            }
            else
            {
                ViewData["czyAdmin"] = "N";
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ManageUsers(UzytkownikDoUsuniecia uzytkownikDoUsuniecia)
        {
            if (czyJestAdminem())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));
                var uzytkownikZDBDoUsuniecia = userManager.FindByEmail(uzytkownikDoUsuniecia.email);
                userManager.Delete(uzytkownikZDBDoUsuniecia);
                var users = dbContext.Users.ToList();
                ViewData["czyAdmin"] = "T";
                ViewData["uzytkownicy"] = users;
            }
            else
            {
                ViewData["czyAdmin"] = "N";
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(OsobaZaginiona osobaZaginiona)
        {
            this.encjeOsobyZaginionejContext.osobyZaginione.Add(new EncjaOsobyZaginionej() {
                Imie = osobaZaginiona.imie,
                Nazwisko = osobaZaginiona.nazwisko,
                Plec = osobaZaginiona.plec, 
                MiejscowoscZaginiecia = osobaZaginiona.miejscowoscZaginiecia,
                DataUrodzenia = osobaZaginiona.dataUrodzenia
            });
            encjeOsobyZaginionejContext.SaveChanges();
            ViewData["listaOsobZaginionych"] = podajAktualnąListeOsobZaginionych();

            if (czyJestAdminem())
            {
                var users = dbContext.Users.ToList();
                ViewData["czyAdmin"] = "T";
                ViewData["uzytkownicy"] = users;
            }
            else
            {
                ViewData["czyAdmin"] = "N";
            }

            return View();
        }

        public Boolean czyJestAdminem()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s.Count == 0)
                {
                    return false;
                }
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private ListaOsobZaginionych podajAktualnąListeOsobZaginionych()
        {
            ListaOsobZaginionych listaOsobZaginionych = new ListaOsobZaginionych();
            var osobyZagionioneZBazyDanych = encjeOsobyZaginionejContext.osobyZaginione.ToList();

            foreach (EncjaOsobyZaginionej osobaZaginionaZBazyDanych in osobyZagionioneZBazyDanych)
            {
                OsobaZaginiona osobaZaginiona = new OsobaZaginiona();
                osobaZaginiona.id = osobaZaginionaZBazyDanych.Id;
                osobaZaginiona.imie = osobaZaginionaZBazyDanych.Imie;
                osobaZaginiona.nazwisko = osobaZaginionaZBazyDanych.Nazwisko;
                osobaZaginiona.plec = osobaZaginionaZBazyDanych.Plec;
                osobaZaginiona.miejscowoscZaginiecia = osobaZaginionaZBazyDanych.MiejscowoscZaginiecia;
                osobaZaginiona.dataUrodzenia = osobaZaginionaZBazyDanych.DataUrodzenia;

                listaOsobZaginionych.dodajOsobe(osobaZaginiona);
            }

            return listaOsobZaginionych;

        }

    }
}