using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject_Core.DTOs
{
    public class UrunDto
    {

        public int UrunID { get; set; }
        public string? Tanim { get; set; }
        public string? Barkod { get; set; }
        public int? SktGunSayisi { get; set; }
        public bool Silindi { get; set; }
        public KategoriDto? Kategori { get; set; }
        public MuhafazaKosuluDto? MuhafazaKosulu { get; set; }

        public List<ResimDto> _resimler = new List<ResimDto>();



    }
}
