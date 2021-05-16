using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RejOsZag.Models
{
    public class ListaOsobZaginionych
    {
        public List<OsobaZaginiona> osobyZaginione { get; }

        public ListaOsobZaginionych(List<OsobaZaginiona> osobyZaginione)
        {
            this.osobyZaginione = osobyZaginione;
        }

        public void dodajOsobe(OsobaZaginiona osoba)
        {
            this.osobyZaginione.Add(osoba);
        }
     
        public ListaOsobZaginionych()
        {
            this.osobyZaginione = new List<OsobaZaginiona>();
        }

        public ListaOsobZaginionych filtrujPoPlci(string plec)
        {
            var przefiltrowanePoPlciOsoby = this.osobyZaginione.Where(osZag => osZag.plec == plec).ToList();
            return new ListaOsobZaginionych(przefiltrowanePoPlciOsoby);
        }
    }
}