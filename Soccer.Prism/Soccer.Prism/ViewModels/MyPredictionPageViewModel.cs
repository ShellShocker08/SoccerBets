using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Prism.ViewModels
{
    public class MyPredictionPageViewModel : ViewModelBase
    {
        public MyPredictionPageViewModel(INavigationService navigation)
            : base(navigation)
        {
            Title = "My Predictions";
        }
    }
}
