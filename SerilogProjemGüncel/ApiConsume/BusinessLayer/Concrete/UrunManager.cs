using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UrunManager : IUrunService
    {
        private readonly IUrunDal _urunDal;

        public UrunManager(IUrunDal urunDal)
        {
            _urunDal = urunDal;
        }

        public void TDelete(Urun t)
        {
            _urunDal.Delete(t);
        }

        public Urun TGetByID(int id)
        {
            return _urunDal.GetByID(id);
        }

        public List<Urun> TGetList()
        {
            return _urunDal.GetList();
        }

        public void TInsert(Urun t)
        {
            _urunDal.Insert(t);
        }

        public void TUpdate(Urun t)
        {
            _urunDal.Update(t);
        }
    }
}
