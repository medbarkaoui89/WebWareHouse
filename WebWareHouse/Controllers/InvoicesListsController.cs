using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebWareHouse.Data;
using WebWareHouse.Models;

namespace WebWareHouse.Controllers
{
    public class InvoicesListsController : Controller
    {
        private readonly WareHouseContext _context;

        public InvoicesListsController(WareHouseContext context)
        {
            _context = context;
        }

        // GET: InvoicesLists
        public async Task<IActionResult> Index()
        {
            var wareHouseContext = _context.InvoicesLists.Include(i => i.IdGoodsNavigation).Include(i => i.IdInvoNavigation);
            return View(await wareHouseContext.ToListAsync());
        }

        // GET: InvoicesLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InvoicesLists == null)
            {
                return NotFound();
            }

            var invoicesList = await _context.InvoicesLists
                .Include(i => i.IdGoodsNavigation)
                .Include(i => i.IdInvoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoicesList == null)
            {
                return NotFound();
            }

            return View(invoicesList);
        }

        // GET: InvoicesLists/Create
        public IActionResult Create()
        {
            ViewData["IdGoods"] = new SelectList(_context.Goods, "Id", "Id");
            ViewData["IdInvo"] = new SelectList(_context.Invoices, "Id", "Id");
            return View();
        }

        // POST: InvoicesLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdGoods,IdInvo,Number")] InvoicesList invoicesList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoicesList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGoods"] = new SelectList(_context.Goods, "Id", "Id", invoicesList.IdGoods);
            ViewData["IdInvo"] = new SelectList(_context.Invoices, "Id", "Id", invoicesList.IdInvo);
            return View(invoicesList);
        }

        // GET: InvoicesLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InvoicesLists == null)
            {
                return NotFound();
            }

            var invoicesList = await _context.InvoicesLists.FindAsync(id);
            if (invoicesList == null)
            {
                return NotFound();
            }
            ViewData["IdGoods"] = new SelectList(_context.Goods, "Id", "Id", invoicesList.IdGoods);
            ViewData["IdInvo"] = new SelectList(_context.Invoices, "Id", "Id", invoicesList.IdInvo);
            return View(invoicesList);
        }

        // POST: InvoicesLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdGoods,IdInvo,Number")] InvoicesList invoicesList)
        {
            if (id != invoicesList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoicesList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoicesListExists(invoicesList.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGoods"] = new SelectList(_context.Goods, "Id", "Id", invoicesList.IdGoods);
            ViewData["IdInvo"] = new SelectList(_context.Invoices, "Id", "Id", invoicesList.IdInvo);
            return View(invoicesList);
        }

        // GET: InvoicesLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InvoicesLists == null)
            {
                return NotFound();
            }

            var invoicesList = await _context.InvoicesLists
                .Include(i => i.IdGoodsNavigation)
                .Include(i => i.IdInvoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoicesList == null)
            {
                return NotFound();
            }

            return View(invoicesList);
        }

        // POST: InvoicesLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InvoicesLists == null)
            {
                return Problem("Entity set 'WareHouseContext.InvoicesLists'  is null.");
            }
            var invoicesList = await _context.InvoicesLists.FindAsync(id);
            if (invoicesList != null)
            {
                _context.InvoicesLists.Remove(invoicesList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoicesListExists(int id)
        {
          return (_context.InvoicesLists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
