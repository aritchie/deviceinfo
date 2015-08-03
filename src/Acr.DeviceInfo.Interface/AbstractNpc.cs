using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Acr.DeviceInfo {

    public abstract class AbstractNpc : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected virtual bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null) {
            if (Object.Equals(property, value))
                return false;

            property = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }
    }
}
