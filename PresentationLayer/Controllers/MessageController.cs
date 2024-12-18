using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(IMessageService messageService, UserManager<AppUser> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Send()
        {
            var users = await _userManager.Users.ToListAsync();
            ViewBag.Receivers = users.Select(u => new SelectListItem
            {
                Text = $"{u.NameSurname}",
                Value = u.Id.ToString()
            }).ToList();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Send(Message message)
        {
            var sender = await _userManager.GetUserAsync(User);

            if (sender != null)
            {
                message.SenderId = sender.Id;
                message.IsRead = false;
                message.CreatedDate = DateTime.Now;
                _messageService.TInsert(message);
                return RedirectToAction("Inbox");
            }

            ModelState.AddModelError("", "Mesaj gönderilemedi.");
            return View(message);
        }


        public async Task<IActionResult> Inbox()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return RedirectToAction("Index", "Login");

            var messages = _messageService.TInboxMessages(currentUser.Id);
            return View(messages);
        }

        public async Task<IActionResult> Outbox()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return RedirectToAction("Index", "Login");

            var messages = _messageService.OutboxMessages(currentUser.Id);
            return View(messages);
        }



        [HttpPost]
        public async Task<IActionResult> MoveToTrash(int[] selectedMessages)
        {
            if (selectedMessages != null && selectedMessages.Length > 0)
            {
                foreach (var id in selectedMessages)
                {
                    _messageService.TMoveToTrash(id);
                }
            }
            return RedirectToAction("Inbox");
        }


        public async Task<IActionResult> Trash()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return RedirectToAction("Index", "Login");

            // Çöp kutusundaki mesajları getir
            var messages = _messageService.TGetTrashMessages(currentUser.Id);
            return View(messages);
        }


        [HttpPost]
        public async Task<IActionResult> DeletePermanently(int[] selectedMessages)
        {
            if (selectedMessages != null && selectedMessages.Length > 0)
            {
                foreach (var id in selectedMessages)
                {
                    _messageService.TDeleteMessagePermanently(id);
                }
            }
            return RedirectToAction("Trash");
        }


        public IActionResult MessageDetail(int id)
        {
         var value = _messageService.GetByIdWithSender(id);
            return View(value);
        }
    }
}
