using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RejOsZag.Models
{
    public class OsobaZaginiona
    {
        public int id { set; get; }
        public string imie { set; get; }
        public string nazwisko { set; get; }
        public string miejscowoscZaginiecia { set; get; }
        public string plec { set; get; }
        public DateTime dataUrodzenia { set; get; }
        public OsobaZaginiona() { }

    }
}