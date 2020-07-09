using Prism.Commands;
using Prism.Navigation;
using Soccer.Common.Responses;
using Soccer.Prism.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Soccer.Prism.ItemViewModel
{
    public class TournamentItemViewModel : TournamentResponse
    {
        private readonly INavigationService _navigation;
        private DelegateCommand _selectTournamentCommand;

        public TournamentItemViewModel(INavigationService navigation)
        {
            _navigation = navigation;
        }

        public DelegateCommand SelectTournamentCommand =>
            _selectTournamentCommand ?? (_selectTournamentCommand = new DelegateCommand(SelectTournament));

        private async void SelectTournament()
        {
            var parameters = new NavigationParameters
            {
                { "tournament", this }
            };

            await _navigation.NavigateAsync(nameof(GroupsPage), parameters);
        }
    }
}
