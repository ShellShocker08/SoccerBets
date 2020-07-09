using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Soccer.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Prism.ViewModels
{
    public class GroupsPageViewModel : ViewModelBase
    {
        private TournamentResponse _tournament;

        public GroupsPageViewModel(INavigationService navigation)
            : base(navigation)
        {
            Title = "Groups";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            _tournament = parameters.GetValue<TournamentResponse>("tournament");
            Title = _tournament.Name;
        }
    }
}
