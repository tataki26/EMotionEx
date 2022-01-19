using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using McsView.ViewModel;
using NetworkFrame01;
using System.Windows.Threading;

namespace McsView
{
    public partial class MainWindow : Window
    {
        private VirtualTableProtocol vtp = new VirtualTableProtocol();
        private ThreadingData td = new ThreadingData();

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromTicks(1000000);
            timer.Tick += new EventHandler(timerTick);
            timer.Start();

            this.Loaded += OnLoaded;
            this.DataContext = new MainViewModel();
        }

        int addr;
        int data;

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this.qpAddr);

            lprAddr.SelectedIndex = 0;
            ipAddr.SelectedIndex = 0;
        }

        private void timerTick(object sender, EventArgs e)
        {
            var viewModel = this.DataContext as MainViewModel;

            if (td.lresult != null) viewModel.LprData = td.lresult.ToString();
            if (td.iresult != null) viewModel.IpData = td.iresult.ToString();
        }

        private void cnnBtn_Button_Click_1(object sender, RoutedEventArgs e)
        {
            string host = "192.168.240.2";
            // string host = "192.168.140.2"; // 연결 오류 확인용 코드
            int port = 2025;

            vtp.Connect_Udp_Client(host, port);
            td.Connect(host, port);
        }

        private void qpBtn_Button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as MainViewModel;

            if (string.IsNullOrEmpty(viewModel.QpAddr))
            {
                MessageBox.Show("Q접점 주소를 입력해주세요.");
                Keyboard.Focus(this.qpAddr);
                return;
            }

            qpWrite();

        }

        private void lpBtn_Button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as MainViewModel;

            if (string.IsNullOrEmpty(viewModel.LpwAddr))
            {
                MessageBox.Show("L접점 주소를 입력해주세요.");
                Keyboard.Focus(this.lpwAddr);
                return;
            }

            lpWrite();
        }

        private bool qpWrite()
        {
            var viewModel = this.DataContext as MainViewModel;

            // MessageBox.Show(string.Format("Q접점: {0}", viewModel.QpAddr));
            int.TryParse(viewModel.QpAddr, out addr);
            int.TryParse(viewModel.QpData, out data);

            vtp.Set_Q_Var(addr, data);

            return true;
        }

        private bool lpWrite()
        {
            var viewModel = this.DataContext as MainViewModel;

            // MessageBox.Show(string.Format("L접점: {0}", viewModel.LpwAddr));
            int.TryParse(viewModel.LpwAddr, out addr);
            int.TryParse(viewModel.LpwData, out data);

            vtp.Set_LW_Var(addr, data);

            return true;
        }

        private void lprAddr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = this.DataContext as MainViewModel;

            string selectedDt = lprAddr.SelectedIndex.ToString();
            int.TryParse(selectedDt, out addr);

            td.lVarAddress = addr;

            if (viewModel != null) viewModel.LprData = data.ToString();

        }

        private void ipAddr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = this.DataContext as MainViewModel;

            string selectedDt = ipAddr.SelectedIndex.ToString();
            int.TryParse(selectedDt, out addr);

            td.lVarAddress = addr;

            if (viewModel != null) viewModel.IpData = data.ToString();
        }

        private void dnnBtn_Button_Click(object sender, RoutedEventArgs e)
        {
            td.Disconnect();
            MessageBox.Show("접속 종료!");
        }
    }
}
