using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace Traceroute
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MyTraceroute traceroute;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_start_Click(object sender, RoutedEventArgs e)
        {
            traceroute = new MyTraceroute(textBox_IPOrDomain.Text);
            textBox_result.Text = "通过最多30个跃点跟踪\n";
            textBox_result.Text += $"到{textBox_IPOrDomain.Text} [{traceroute.ip}]的路由：\n\n";
            button_next.IsEnabled = true;
        }

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            textBox_result.Text += "Is tracing route...\n";
            textBox_result.ScrollToEnd();
        }
    }
}
