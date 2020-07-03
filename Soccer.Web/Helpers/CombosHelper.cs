using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Soccer.Web.Data;
using Soccer.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Helpers
{
    public class CombosHelper : IComboHelper
    {
        private static DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboTeams()
        {
            List<SelectListItem> list = _context.Teams
                .Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Selecciona un Equipo...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboTeams(int groupId)        
        {
            List<SelectListItem> list = _context.GroupDetails
                .Include(gd => gd.Team)
                .Where(gd => gd.Group.Id == groupId)
                .Select(gd => new SelectListItem
                {
                    Text = gd.Team.Name,
                    Value = $"{gd.Team.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a team...]",
                Value = "0"
            });

            return list;
        }

    }
}
