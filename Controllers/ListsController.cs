using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Board.Models;

namespace Board.Controllers
{
    public class ListsController : Controller
    {
        private readonly Context _context;

        public ListsController(Context context)
        {
            _context = context;
        }

        // GET: Lists
        public async Task<IActionResult> Index()
        {
            var context = _context.Lists.Include(l => l.Workspace);
            return View(await context.ToListAsync());
        }

        // GET: Lists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lists == null)
            {
                return NotFound();
            }

            var list = await _context.Lists
                .Include(l => l.Workspace)
                .FirstOrDefaultAsync(m => m.ListId == id);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);
        }

        // GET: Lists/Create
        public IActionResult Create()
        {
            ViewData["WorkspaceId"] = new SelectList(_context.Workspaces, "WorkspaceId", "WorkspaceId");
            return View();
        }

        // POST: Lists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListId,WorkspaceId,Name,Order,CreatedAt,UpdatedAt")] List list)
        {
            if (ModelState.IsValid)
            {
                _context.Add(list);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkspaceId"] = new SelectList(_context.Workspaces, "WorkspaceId", "WorkspaceId", list.WorkspaceId);
            return View(list);
        }

        // GET: Lists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lists == null)
            {
                return NotFound();
            }

            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }
            ViewData["WorkspaceId"] = new SelectList(_context.Workspaces, "WorkspaceId", "WorkspaceId", list.WorkspaceId);
            return View(list);
        }

        // POST: Lists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListId,WorkspaceId,Name,Order,CreatedAt,UpdatedAt")] List list)
        {
            if (id != list.ListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(list);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListExists(list.ListId))
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
            ViewData["WorkspaceId"] = new SelectList(_context.Workspaces, "WorkspaceId", "WorkspaceId", list.WorkspaceId);
            return View(list);
        }

        // GET: Lists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lists == null)
            {
                return NotFound();
            }

            var list = await _context.Lists
                .Include(l => l.Workspace)
                .FirstOrDefaultAsync(m => m.ListId == id);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);
        }

        // POST: Lists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lists == null)
            {
                return Problem("Entity set 'Context.Lists'  is null.");
            }
            var list = await _context.Lists.FindAsync(id);
            if (list != null)
            {
                _context.Lists.Remove(list);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListExists(int id)
        {
            return _context.Lists.Any(e => e.ListId == id);
        }
    }
}
