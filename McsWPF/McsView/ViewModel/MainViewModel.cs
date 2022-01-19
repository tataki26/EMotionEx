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

        private string _lprAddr;
        public string LprAddr
        {
            get { return _lprAddr; }
            set
            {
                _lprAddr = value;
                OnPropertyUpdate("LprAddr");
            }
        }

        private string _ipAddr;
        public string IpAddr
        {
            get { return _ipAddr; }
            set
            {
                _ipAddr = value;
                OnPropertyUpdate("IpAddr");
            }
        }

        private string _qpData;
        public string QpData
        {
            get { return _qpData; }
            set
            {
                _qpData = value;
                OnPropertyUpdate("QpData");
            }
        }

        private string _lpwData;
        public string LpwData
        {
            get { return _lpwData; }
            set
            {
                _lpwData = value;
                OnPropertyUpdate("LpwData");
            }
        }

        private string _lprData;
        public string LprData
        {
            get { return _lprData; }
            set
            {
                _lprData = value;
                OnPropertyUpdate("LprData");
            }
        }

        private string _ipData;
        public string IpData
        {
            get { return _ipData; }
            set
            {
                _ipData = value;
                OnPropertyUpdate("IpData");
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
