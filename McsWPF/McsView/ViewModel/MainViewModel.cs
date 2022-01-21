using System.ComponentModel;
using VtpLib;
using System.Threading;

namespace McsView.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private VirtualTableProtocol vtp = new VirtualTableProtocol();

        public event PropertyChangedEventHandler PropertyChanged;

        private string _qpAddr, _lpwAddr, _lprAddr, _ipAddr;
        private string _qpData, _lpwData;

        public int lUpdatedAddr, iUpdatedAddr;
        public object lUpdatedData, iUpdatedData;

        Thread thread;
        private bool flag = true;

        public string QpAddr
        {
            get { return _qpAddr; }
            set
            {
                _qpAddr = value;
                OnPropertyUpdate("QpAddr");
            }
        }

        public string LpwAddr
        {
            get { return _lpwAddr; }
            set
            {
                _lpwAddr = value;
                OnPropertyUpdate("LpwAddr");
            }
        }

        public string LprAddr
        {
            get { return _lprAddr; }
            set
            {
                _lprAddr = value;
                OnPropertyUpdate("LprAddr");
            }
        }

        public string IpAddr
        {
            get { return _ipAddr; }
            set
            {
                _ipAddr = value;
                OnPropertyUpdate("IpAddr");
            }
        }

        public string QpData
        {
            get { return _qpData; }
            set
            {
                _qpData = value;
                OnPropertyUpdate("QpData");
            }
        }

        public string LpwData
        {
            get { return _lpwData; }
            set
            {
                _lpwData = value;
                OnPropertyUpdate("LpwData");
            }
        }

        public string LprData
        {
            get { return lUpdatedData?.ToString(); }
            set
            {
                lUpdatedData = value;
                OnPropertyUpdate("LprData");
            }
        }

        public string IpData
        {
            get { return iUpdatedData?.ToString(); }
            set
            {
                iUpdatedData = value;
                OnPropertyUpdate("IpData");
            }
        }

        public bool qpWrite(int addr, int data)
        {

            vtp.Set_Q_Var(addr, data);

            return true;
        }

        public bool lpWrite(int addr, int data)
        {

            vtp.Set_LW_Var(addr, data);

            return true;
        }

        public void Connect(string host, int port)
        {
            vtp.Connect_Udp_Client(host, port);

            thread = new Thread(new ThreadStart(ThreadGetData));
            thread.Start();

        }

        public void Disconnect()
        {
            flag = false;
            thread?.Join();
        }

        public void ThreadGetData()
        {
            // object tempObject = null;
            while (flag)
            {
                vtp.Set_LR_Var(lUpdatedAddr, ref lUpdatedData);
                LprData = lUpdatedData.ToString();

                vtp.Set_I_Var(iUpdatedAddr, ref iUpdatedData);
                IpData = iUpdatedData.ToString();

            }
        }

        private void OnPropertyUpdate(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
