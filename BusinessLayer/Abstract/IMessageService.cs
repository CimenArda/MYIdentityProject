using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMessageService :IGenericService<Message>
    {
        List<Message> TInboxMessages(int id);
        List<Message> OutboxMessages(int id);

        void TMoveToTrash(int id);

        List<Message> TGetTrashMessages(int id);//receiver

        void TDeleteMessagePermanently(int id); //message

        Message GetByIdWithSender(int id);


    }
}
