using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Interfaces
{
    public interface IComboHelper
    {
        IEnumerable<SelectListItem> GetComboTeams();
    }
}
