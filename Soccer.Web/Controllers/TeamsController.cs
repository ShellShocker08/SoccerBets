using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soccer.Web.Data;
using Soccer.Web.Data.Entities;

namespace Soccer.Web.Controllers
{
    public class TeamsController : Controller
    {
        private readonly DataContext _context;

        public TeamsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            TeamEntity teamEntity = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teamEntity == null) return NotFound();

            return View(teamEntity);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamEntity teamEntity)
        {
            if (ModelState.IsValid)
            {
                teamEntity.Active = true;
                _context.Add(teamEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                        ModelState.AddModelError(string.Empty, "El equipo ya existe");
                    else
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }
            return View(teamEntity);
        }
    
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            TeamEntity teamEntity = await _context.Teams.FindAsync(id);
            
            if (teamEntity == null) return NotFound();
            return View(teamEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, TeamEntity teamEntity)
        {
            if (id != teamEntity.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(teamEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                        ModelState.AddModelError(string.Empty, "El equipo ya existe");
                    else
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }
            return View(teamEntity);
        }
        
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null) return NotFound();

            TeamEntity teamEntity = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teamEntity == null) return NotFound();
            
            // Cambiar Estatus de Equipo
            if (teamEntity.Active) teamEntity.Active = false;
            else teamEntity.Active = true;

            try
            {
                _context.Teams.Update(teamEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
            }

            return RedirectToAction(nameof(Index));
        }
    }
}