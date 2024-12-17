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

        public List<Message> InboxMessages(int id)
        {
            using (var messageContext = new MessageContext())
            {
                return messageContext.Messages
                            .Include(m => m.Sender)
                            .Where(m => m.ReceiverId == id)
                            .OrderByDescending(m => m.CreatedDate)
                            .ToList();
            }
        }
    }
}
