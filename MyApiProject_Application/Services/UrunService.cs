using MyApiProject_Core.Entities;
using MyApiProject_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject_Application.Services
{
    public class UrunService
    {
        private readonly IUrunRepository _repo;

        public UrunService(IUrunRepository repo)
        {
            _repo = repo;
        }

        public List<Urun> GetAll() => _repo.GetAll();
        public Urun? GetByID(int id) => _repo.GetByID(id);
        public int Insert(Urun u) => _repo.Insert(u);
        public bool Update(Urun u) => _repo.Update(u);
        public bool Delete(int id) => _repo.Delete(id);


    }
}
