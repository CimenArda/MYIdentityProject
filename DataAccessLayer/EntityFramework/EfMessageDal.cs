using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfMessageDal : GenericRepository<Message>, IMessageDal
    {
        public EfMessageDal(MessageContext messageContext) : base(messageContext)
        {
        }

        public void DeleteMessagePermanently(int id)
        {
            using (var messageContext = new MessageContext())
            {
                var message = messageContext.Messages.FirstOrDefault(m => m.MessageId == id);
                if (message != null)
                {
                    messageContext.Messages.Remove(message);
                    messageContext.SaveChanges();
                }
            }
        }

        public List<Message> GetTrashMessages(int id)
        {
            using (var messageContext = new MessageContext())
            {
                return messageContext.Messages
                   .Include(m => m.Sender)
                   .Where(m => m.ReceiverId == id && m.IsDeleted) // IsDeleted = true
                   .OrderByDescending(m => m.CreatedDate)
                   .ToList();
            }
        }

        public List<Message> InboxMessages(int id)
        {
            using (var messageContext = new MessageContext())
            {
                return messageContext.Messages
                            .Include(m => m.Sender)
                            .Where(m => m.ReceiverId == id && !m.IsDeleted)
                            .OrderByDescending(m => m.CreatedDate)
                            .ToList();
            }
        }

        public void MoveToTrash(int id)
        {
            using (var messageContext = new MessageContext())
            {
                var message = messageContext.Messages.FirstOrDefault(m => m.MessageId == id);
                if (message != null)
                {
                    message.IsDeleted = true;
                    messageContext.SaveChanges();
                }
            }
        }

        public List<Message> OutboxMessages(int id)
        {
            using (var messageContext = new MessageContext())
            {
                return messageContext.Messages
                            .Include(m => m.Receiver)
                            .Where(m => m.SenderId == id)
                            .OrderByDescending(m => m.CreatedDate)
                            .ToList();
            }
        }
    }
}
