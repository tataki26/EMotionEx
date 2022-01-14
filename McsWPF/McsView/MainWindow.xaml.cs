using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace McsView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Button 컨트롤 클릭 이벤트 핸들러
        private void cnnBtn_Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show("접속 완료!");
        }

        private void qpBtn_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show("Q접점");
        }

        private void lpBtn_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show("L접점");
        }

        private void dnnBtn_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show("접속 종료!");
        }
    }
}
