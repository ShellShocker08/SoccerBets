using Prism;
using Prism.Ioc;
using Soccer.Common.Interfaces;
using Soccer.Common.Services;
using Soccer.Prism.ViewModels;
using Soccer.Prism.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Soccer.Prism
{
    public partial class App
    {       
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/TournamentPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // ApiService
            containerRegistry.Register<IApiService, ApiService>();

            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<TournamentPage, TournamentPageViewModel>();
        }
    }
}
