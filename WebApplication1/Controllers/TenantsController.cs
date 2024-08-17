using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(TenantFilterAttribute))]
    public class TenantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TenantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Messages()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Roles()
        {
            return View();
        }

        public IActionResult RolePermissions() { 
        
            return View();
        }

        //// GET: Tenants
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Tenants.ToListAsync());
        //}

        //// GET: Tenants/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tenants = await _context.Tenants
        //        .FirstOrDefaultAsync(m => m.TenantID == id);
        //    if (tenants == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tenants);
        //}

        //// GET: Tenants/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Tenants/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("TenantID,TenantName,Subdomain,CreatedAt,UpdatedAt")] Tenants tenants)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(tenants);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(tenants);
        //}

        //// GET: Tenants/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tenants = await _context.Tenants.FindAsync(id);
        //    if (tenants == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(tenants);
        //}

        //// POST: Tenants/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("TenantID,TenantName,Subdomain,CreatedAt,UpdatedAt")] Tenants tenants)
        //{
        //    if (id != tenants.TenantID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(tenants);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TenantsExists(tenants.TenantID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(tenants);
        //}

        //// GET: Tenants/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tenants = await _context.Tenants
        //        .FirstOrDefaultAsync(m => m.TenantID == id);
        //    if (tenants == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tenants);
        //}

        //// POST: Tenants/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var tenants = await _context.Tenants.FindAsync(id);
        //    if (tenants != null)
        //    {
        //        _context.Tenants.Remove(tenants);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TenantsExists(int id)
        //{
        //    return _context.Tenants.Any(e => e.TenantID == id);
        //}
    }
}
