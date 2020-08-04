using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Interfaces
{
    public interface IMatchHelper
    {
        Task CloseMatchAsync(int matchId, int goalsLocal, int goalsVisitor);
    }
}
