using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using McsView.ViewModel;

namespace McsView
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
            this.DataContext = new MainViewModel();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this.qpAddr);
        }

        private void cnnBtn_Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Button btn = sender as Button;
            MessageBox.Show("접속 완료!");
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
            MessageBox.Show(string.Format("Q접점: {0}", viewModel.QpAddr));
            return true;
        }

        private bool lpWrite()
        {
            var viewModel = this.DataContext as MainViewModel;
            MessageBox.Show(string.Format("L접점: {0}", viewModel.LpwAddr));
            return true;
        }

        private void dnnBtn_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show("접속 종료!");
        }
    }
}
