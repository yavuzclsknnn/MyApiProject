using MyApiProject_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject_Core.Interfaces
{
    public interface IUrunRepository
    {
        List<Urun> GetAll();
        Urun? GetByID(int id);
        int Insert(Urun u);
        bool Update(Urun u);
        bool Delete(int id);
    }
}
