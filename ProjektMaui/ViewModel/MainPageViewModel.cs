using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektMaui.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IMap map;
        private readonly IGeolocation geolocation;
        private readonly IConnectivity connectivity;

        public MainPageViewModel(IMap map, IGeolocation geolocation, IConnectivity connectivity) 
        {
            this.map = map;
            this.geolocation = geolocation;
            this.connectivity = connectivity;
        }

        public IGeolocation Geolocation { get; }

        [RelayCommand]
        public async Task Location() 
        {
          if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet) 
            {
                await Shell.Current.DisplayAlert("Ops!", "You need internet connection/ Du behöver internet kontakt", "Okey");
                return;
            
            }
          if(connectivity.NetworkAccess != NetworkAccess.Internet) 
            {
                await Shell.Current.DisplayAlert("Ops!", "You need internet connection/ Du behöver internet kontakt", "Okey");
                return;
            }
          // Get Location /hitta plats
            var location = await geolocation.GetLastKnownLocationAsync();
            if (location == null) 
            {
                location = await geolocation.GetLocationAsync(
                    new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Best,
                        Timeout = TimeSpan.FromSeconds(30),
                        RequestFullAccuracy = true

                    }


                    );
              
            
            }
          


            if (location == null) 
            {
                await Shell.Current.DisplayAlert("Ops!", "Sorry we couldnt get your location/ Vi kunde inte hitta din plats", "Okey");
                return;
            }



            //Open map - öppna karta
            await map.OpenAsync(location.Latitude, location.Longitude, new MapLaunchOptions
            {
                Name = "My Current Location",
                NavigationMode=NavigationMode.None
               
            }) ;
        }
    }
}
