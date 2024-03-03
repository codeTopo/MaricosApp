using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MaricosApp.Serivices
{
    public class BaseViewModel
    {
        public static event PropertyChangedEventHandler PropertyChanged;

        protected static void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        protected static bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected static Task<bool> IsConnected()
        {
            var current = Connectivity.NetworkAccess;
            return Task.FromResult(current == NetworkAccess.Internet);
        }
    }

}
