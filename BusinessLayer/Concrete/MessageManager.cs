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
    public class MessageManager :IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public List<Message> OutboxMessages(int id)
        {
            return _messageDal.OutboxMessages(id);
        }

        public void TDelete(int id)
        {
           _messageDal.Delete(id);
        }

        public List<Message> TGetAllList()
        {
           return _messageDal.GetAllList();
        }

        public Message TGetById(int id)
        {
           return _messageDal.GetById(id);
        }

        public List<Message> TInboxMessages(int id)
        {
           return _messageDal.InboxMessages(id);
        }

        public void TInsert(Message entity)
        {
           _messageDal.Insert(entity);
        }

        public void TUpdate(Message entity)
        {
           _messageDal.Update(entity);
        }
    }
}
