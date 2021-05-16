using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RejOsZag.Models
{
    public class EncjaOsobyZaginionej
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Plec { get; set; }
        public string MiejscowoscZaginiecia { get; set; }
        public DateTime DataUrodzenia { get; set; }
    }
}