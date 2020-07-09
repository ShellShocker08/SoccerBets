using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Soccer.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Prism.ViewModels
{
    public class TournamentPageViewModel : ViewModelBase
    {
        public TournamentPageViewModel(INavigationService navigation)
            : base(navigation)
        {
            Title = "Soccer";
        }

        public List<TournamentResponse> Tournaments { get; set; }
    }
}
