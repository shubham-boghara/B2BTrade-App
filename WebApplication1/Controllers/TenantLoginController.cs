using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class TenantLoginController : Controller
    {
        // GET: TenantLoginController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TenantLoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TenantLoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TenantLoginController/Create
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

        // GET: TenantLoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TenantLoginController/Edit/5
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

        // GET: TenantLoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TenantLoginController/Delete/5
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
