using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject_Core.Entities
{
    public class Urun
    {

        public int UrunID { get; set; }
        public string? Tanim { get; set; }
        public string? Barkod { get; set; }
        public int? SKTGunSayisi { get; set; }
        public bool Silindi { get; set; }
        public Kategori? Kategori { get; set; }
        public MuhafazaKosulu? MuhafazaKosulu { get; set; }
        public List<Resim> Resimler { get; set; } = new List<Resim>();

    }
}
