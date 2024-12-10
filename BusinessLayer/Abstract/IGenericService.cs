using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        List<T> TGetAllList();
        T TGetById(int id);
        void TInsert(T entity);
        void TDelete(int id);
        void TUpdate(T entity);
    
    }
}
