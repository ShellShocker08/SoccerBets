﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soccer.Web.Data;
using Soccer.Web.Data.Entities;
using Soccer.Web.Interfaces;
using Soccer.Web.Models;
using System;
using System.Threading.Tasks;

namespace Soccer.Web.Controllers
{
    public class TeamsController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public TeamsController(
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
            return View(await _context.Teams.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamEntity teamEntity = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teamEntity == null)
            {
                return NotFound();
            }

            return View(teamEntity);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamViewModel teamViewModel)
        {
            if (ModelState.IsValid)
            {
                // Crear Imagen en Carpeta
                string path = string.Empty;
                if (teamViewModel.LogoFile != null)
                {
                    try
                    {
                        path = await _imageHelper.UploadImageAsync(teamViewModel.LogoFile, null);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        return View(teamViewModel);
                    }
                }

                TeamEntity teamEntity = _converterHelper.ToTeamEntity(teamViewModel, path, true);

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
                    {
                        ModelState.AddModelError(string.Empty, "El equipo ya existe");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }
            return View(teamViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamEntity teamEntity = await _context.Teams.FindAsync(id);

            if (teamEntity == null)
            {
                return NotFound();
            }

            TeamViewModel teamViewModel = _converterHelper.ToTeamViewModel(teamEntity);
            return View(teamViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, TeamViewModel teamViewModel)
        {
            if (id != teamViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string path = teamViewModel.LogoPath;
                if (teamViewModel.LogoFile != null)
                {
                    try
                    {
                        path = await _imageHelper.UpdateImageAsync(teamViewModel.LogoPath, teamViewModel.LogoFile, null);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        return View(teamViewModel);
                    }
                }

                TeamEntity teamEntity = _converterHelper.ToTeamEntity(teamViewModel, path, false);

                _context.Update(teamEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "El equipo ya existe");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }
            return View(teamViewModel);
        }

        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamEntity teamEntity = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teamEntity == null)
            {
                return NotFound();
            }

            // Cambiar Estatus de Equipo
            if (teamEntity.Active)
            {
                teamEntity.Active = false;
            }
            else
            {
                teamEntity.Active = true;
            }

            try
            {
                _context.Teams.Update(teamEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}