using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }
        public bool IsOnline { get; set; }

        // Gönderen Kullanıcı
        public int SenderId { get; set; } // Gönderen AppUser'ın ID'si
        public AppUser Sender { get; set; } // Gönderen Kullanıcı

        // Alıcı Kullanıcı
        public int ReceiverId { get; set; } // Alıcı AppUser'ın ID'si
        public AppUser Receiver { get; set; } // Alıcı Kullanıcı
    }
}
