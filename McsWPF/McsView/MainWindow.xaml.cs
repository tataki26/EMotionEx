using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using McsView.ViewModel;
using System.Windows.Threading;
using VtpLib;
using TdLib;

namespace McsView
{
    public partial class MainWindow : Window
    {
        private MainViewModel mvm = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
            this.DataContext = mvm;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this.qpAddr);

            lprAddr.SelectedIndex = 0;
            ipAddr.SelectedIndex = 0;
        }

        private void cnnBtn_Button_Click_1(object sender, RoutedEventArgs e)
        {
            string host = "192.168.240.2";
            // string host = "192.168.140.2"; // 연결 오류 확인용 코드
            int port = 2025;

            mvm.Connect(host, port);
        }

        int addr, data;

        private void qpBtn_Button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as MainViewModel;

            if (string.IsNullOrEmpty(viewModel.QpAddr))
            {
                MessageBox.Show("Q접점 주소를 입력해주세요.");
                Keyboard.Focus(this.qpAddr);
                return;
            }

            int.TryParse(viewModel.QpAddr, out addr);
            int.TryParse(viewModel.QpData, out data);

            mvm.qpWrite(addr, data);

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

            int.TryParse(viewModel.LpwAddr, out addr);
            int.TryParse(viewModel.LpwData, out data);

            mvm.lpWrite(addr, data);
        }

        private void lprAddr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedDt = lprAddr.SelectedIndex.ToString();
            
            int.TryParse(selectedDt, out addr);

            mvm.lUpdatedAddr = addr;

        }

        private void ipAddr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedDt = ipAddr.SelectedIndex.ToString();
            
            int.TryParse(selectedDt, out addr);

            mvm.iUpdatedAddr = addr;

        }

        private void dnnBtn_Button_Click(object sender, RoutedEventArgs e)
        {
            mvm.Disconnect();
            MessageBox.Show("접속 종료!");
        }
    }
}
