using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IMessageDal :IGenericDal<Message>
    {
        List<Message> InboxMessages(int id);
        List<Message> OutboxMessages(int id);

        void MoveToTrash(int id);

        List<Message> GetTrashMessages(int id);//receiver

        void DeleteMessagePermanently(int id); //message

    }
}
