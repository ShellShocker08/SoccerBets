using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soccer.Web.Data;
using Soccer.Web.Data.Entities;
using Soccer.Web.Interfaces;
using Soccer.Web.Models;

namespace Soccer.Web.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public TournamentsController(
            DataContext context,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _context
                .Tournaments
                .Include(t => t.Groups)
                .OrderBy(t => t.StartDate)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TournamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                if (model.LogoFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.LogoFile, null);
                }

                var tournament = _converterHelper.ToTournamentEntity(model, path, true);
                _context.Add(tournament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TournamentEntity tournamentEntity = await _context.Tournaments.FindAsync(id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            TournamentViewModel model = _converterHelper.ToTournamentViewModel(tournamentEntity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TournamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = model.LogoPath;

                if (model.LogoFile != null)
                {
                    path = await _imageHelper.UpdateImageAsync(model.LogoPath, model.LogoFile, null);
                }

                TournamentEntity tournamentEntity = _converterHelper.ToTournamentEntity(model, path, false);
                _context.Update(tournamentEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TournamentEntity tournamentEntity = await _context.Tournaments
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tournamentEntity == null)
            {
                return NotFound();
            }

            // Cambiar Estatus de Equipo
            if (tournamentEntity.IsActive)
            {
                tournamentEntity.IsActive = false;
            }
            else
            {
                tournamentEntity.IsActive = true;
            }

            try
            {
                _context.Tournaments.Update(tournamentEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentEntity = await _context.Tournaments
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Local)
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Visitor)
                .Include(t => t.Groups)
                .ThenInclude(t => t.GroupDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            return View(tournamentEntity);
        }

        public async Task<IActionResult> AddGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentEntity = await _context.Tournaments.FindAsync(id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            var model = new GroupViewModel
            {
                Tournament = tournamentEntity,
                TournamentId = tournamentEntity.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGroup(GroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var groupEntity = await _converterHelper.ToGroupEntityAsync(model, true);
                _context.Add(groupEntity);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = model.Id });
            }

            return View(model);
        }


    }
}