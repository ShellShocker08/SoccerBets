using Soccer.Common.Models;
using Soccer.Common.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soccer.Common.Interfaces
{
    public interface ITransformHelper
    {
        List<Group> ToGroups(List<GroupResponse> groupResponses);
    }
}
