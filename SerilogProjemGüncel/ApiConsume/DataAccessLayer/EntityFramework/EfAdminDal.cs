using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
   public class EfAdminDal:GenericRepository<Admin>,IAdminDal//entitye özgü methodları yazmak için bu classı oluşturdum
    {
        public EfAdminDal(Context context) :base(context)//GenericRepository den miras aldım GenericRepository Contexti constructor üzerinde geçmişti bu yüzden burda da contexti constructor içinde geçiyorum. Base ile temel sınıfın yapıcısını çağırıyo yani generic repository nin
        {

        }//eğer ki harici(entitye özgü), metod olursa aşağıya bu metodları ekleyebiliriz
    }
}
