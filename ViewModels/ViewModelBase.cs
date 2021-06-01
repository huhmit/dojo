using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Public Properties

        public event PropertyChangedEventHandler PropertyChanged;

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

        #endregion Public Properties

        #region Protected Methods

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Protected Methods

        #region Public Methods

        public static void NotifyStaticPropertyChanged(string PropertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(PropertyName));
        }

        #endregion Public Methods
    }
}
