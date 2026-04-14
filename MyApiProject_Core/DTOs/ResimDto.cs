using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject_Core.DTOs
{
    public class ResimDto
    {
        public int ResimID { get; set; }
        public int UrunID { get; set; }
        public int SiraNo { get; set; }
        public string? ResimAdi { get; set; }
        public string? DosyaYolu { get; set; }
        public bool AnaResimMi { get; set; }
        public bool Aktif { get; set; }


    }
}
