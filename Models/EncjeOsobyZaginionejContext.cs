using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RejOsZag.Models
{
    public class EncjeOsobyZaginionejContext : DbContext
    {
        public EncjeOsobyZaginionejContext()
        {

        }

        public DbSet<EncjaOsobyZaginionej> osobyZaginione { get; set; }
    }
}