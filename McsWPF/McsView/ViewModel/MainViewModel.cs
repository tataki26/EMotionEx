using System.ComponentModel;

namespace McsView.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private string _qpAddr;
        public string QpAddr
        {
            get { return _qpAddr; }
            set
            {
                _qpAddr = value;
                OnPropertyUpdate("QpAddr");
            }
        }

        private string _lpwAddr;
        public string LpwAddr
        {
            get { return _lpwAddr; }
            set
            {
                _lpwAddr = value;
                OnPropertyUpdate("LpwAddr");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyUpdate(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
