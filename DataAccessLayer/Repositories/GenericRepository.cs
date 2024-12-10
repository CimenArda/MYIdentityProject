using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
       private readonly MessageContext _messageContext;

        public GenericRepository(MessageContext messageContext)
        {
            _messageContext = messageContext;
        }


        public List<T> GetAllList()
        {
            return _messageContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _messageContext.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            _messageContext.Set<T>().Add(entity);
            _messageContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var values = _messageContext.Set<T>().Find(id);
            _messageContext.Set<T>().Remove(values);
            _messageContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _messageContext.Set<T>().Update(entity);
            _messageContext.SaveChanges();
        }
    }
}
