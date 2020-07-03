using Microsoft.AspNetCore.Mvc.Rendering;
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
            List<SelectListItem> list = _context.Teams.Select(t => new SelectListItem
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
    }
}
