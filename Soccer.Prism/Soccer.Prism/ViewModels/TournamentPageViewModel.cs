using Prism.Navigation;
using Soccer.Common.Interfaces;
using Soccer.Common.Responses;
using Soccer.Prism.ItemViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Prism.ViewModels
{
    public class TournamentPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private readonly INavigationService _navigation;
        private List<TournamentItemViewModel> _tournaments;
        private bool _isBusy;

        public TournamentPageViewModel(
            INavigationService navigation,
            IApiService apiService)
            : base(navigation)
        {
            Title = "Soccer";
            _apiService = apiService;
            _navigation = navigation;
            LoadTournamentsAsync();
        }

        public List<TournamentItemViewModel> Tournaments
        {
            get => _tournaments;
            set => SetProperty(ref _tournaments, value);
        }
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private async void LoadTournamentsAsync()
        {
            IsBusy = true;
            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }
            else
            {
                Response response = await _apiService.GetListAsync<TournamentResponse>(
                    url,
                    "/api",
                    "/Tournaments");
                IsBusy = false;

                if (!response.IsSuccess)
                {
                    await App.Current.MainPage.DisplayAlert(
                        "Error",
                        response.Message,
                        "Accept");
                    return;
                }

                var tournaments = (List<TournamentResponse>)response.Result;
                Tournaments = tournaments.Select(t => new TournamentItemViewModel(_navigation)
                {
                    EndDate = t.EndDate,
                    Groups = t.Groups,
                    Id = t.Id,
                    IsActive = t.IsActive,
                    LogoPath = t.LogoPath,
                    Name = t.Name,
                    StartDate = t.StartDate

                }).ToList();
            }
        }
    }
}
