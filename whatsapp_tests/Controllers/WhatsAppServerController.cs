using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace whatsapp_tests.Controllers
{
    public class WhatsAppServerController : Controller
    {
        // GET: WhatsAppServerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WhatsAppServerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WhatsAppServerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WhatsAppServerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WhatsAppServerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WhatsAppServerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WhatsAppServerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WhatsAppServerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
