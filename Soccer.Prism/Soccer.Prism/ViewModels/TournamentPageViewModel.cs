using Prism.Navigation;
using Soccer.Common.Interfaces;
using Soccer.Common.Responses;
using System.Collections.Generic;

namespace Soccer.Prism.ViewModels
{
    public class TournamentPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private List<TournamentResponse> _tournaments;

        public TournamentPageViewModel(
            INavigationService navigation,
            IApiService apiService)
            : base(navigation)
        {
            Title = "Soccer";
            _apiService = apiService;
            LoadTournamentsAsync();
        }

        public List<TournamentResponse> Tournaments
        {
            get => _tournaments;
            set => SetProperty(ref _tournaments, value);
        }

        private async void LoadTournamentsAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<TournamentResponse>
                (url,
                "/api",
                "/Tournaments");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Tournaments = (List<TournamentResponse>)response.Result;
        }

    }
}
