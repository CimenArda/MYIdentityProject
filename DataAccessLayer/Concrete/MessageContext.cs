using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class MessageContext :IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=ARDACIMEN\\SQLEXPRESS;initial catalog=MYIdentityProject;integrated security=true");
        }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Message -> Sender ilişkisi
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender) // Gönderen
                .WithMany(u => u.SentMessages) // Kullanıcının gönderdiği mesajlar
                .HasForeignKey(m => m.SenderId) // Gönderen ID'si
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete engellendi

            // Message -> Receiver ilişkisi
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver) // Alıcı
                .WithMany(u => u.ReceivedMessages) // Kullanıcının aldığı mesajlar
                .HasForeignKey(m => m.ReceiverId) // Alıcı ID'si
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete engellendi
        }
    }
}
