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
    }
}