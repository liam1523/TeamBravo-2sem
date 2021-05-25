using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBravo___2.Semester___Eksamensopgave
{
    public class Affaldspost
    {
        public int ID { get; set; }

        public decimal Maengde { get; set; }

        public int Maaleenhed { get; set; }

        public int Kategori { get; set; }

        public string Beskrivelse { get; set; }

        public string Ansvarlig { get; set; }

        public int VirksomhedID { get; set; }

        public DateTime Dato { get; set; }

    }

}
