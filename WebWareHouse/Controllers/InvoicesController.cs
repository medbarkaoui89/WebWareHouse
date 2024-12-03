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
    public class InvoicesController : Controller
    {
        private readonly WareHouseContext _context;

        public InvoicesController(WareHouseContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var wareHouseContext = _context.Invoices.Include(i => i.From1).Include(i => i.FromNavigation).Include(i => i.IdShipNavigation).Include(i => i.To1).Include(i => i.ToNavigation);
            return View(await wareHouseContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.From1)
                .Include(i => i.FromNavigation)
                .Include(i => i.IdShipNavigation)
                .Include(i => i.To1)
                .Include(i => i.ToNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["From"] = new SelectList(_context.Warehouses, "Id", "Id");
            ViewData["From"] = new SelectList(_context.Suppliers, "Id", "Id");
            ViewData["IdShip"] = new SelectList(_context.Shipments, "Id", "Id");
            ViewData["To"] = new SelectList(_context.Warehouses, "Id", "Id");
            ViewData["To"] = new SelectList(_context.Consumers, "Id", "Id");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,From,To,TypeInvo,IdShip")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["From"] = new SelectList(_context.Warehouses, "Id", "Id", invoice.From);
            ViewData["From"] = new SelectList(_context.Suppliers, "Id", "Id", invoice.From);
            ViewData["IdShip"] = new SelectList(_context.Shipments, "Id", "Id", invoice.IdShip);
            ViewData["To"] = new SelectList(_context.Warehouses, "Id", "Id", invoice.To);
            ViewData["To"] = new SelectList(_context.Consumers, "Id", "Id", invoice.To);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["From"] = new SelectList(_context.Warehouses, "Id", "Id", invoice.From);
            ViewData["From"] = new SelectList(_context.Suppliers, "Id", "Id", invoice.From);
            ViewData["IdShip"] = new SelectList(_context.Shipments, "Id", "Id", invoice.IdShip);
            ViewData["To"] = new SelectList(_context.Warehouses, "Id", "Id", invoice.To);
            ViewData["To"] = new SelectList(_context.Consumers, "Id", "Id", invoice.To);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,From,To,TypeInvo,IdShip")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
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
            ViewData["From"] = new SelectList(_context.Warehouses, "Id", "Id", invoice.From);
            ViewData["From"] = new SelectList(_context.Suppliers, "Id", "Id", invoice.From);
            ViewData["IdShip"] = new SelectList(_context.Shipments, "Id", "Id", invoice.IdShip);
            ViewData["To"] = new SelectList(_context.Warehouses, "Id", "Id", invoice.To);
            ViewData["To"] = new SelectList(_context.Consumers, "Id", "Id", invoice.To);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.From1)
                .Include(i => i.FromNavigation)
                .Include(i => i.IdShipNavigation)
                .Include(i => i.To1)
                .Include(i => i.ToNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Invoices == null)
            {
                return Problem("Entity set 'WareHouseContext.Invoices'  is null.");
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
          return (_context.Invoices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
