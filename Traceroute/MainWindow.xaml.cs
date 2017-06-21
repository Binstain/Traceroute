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
        private static string lastHopIP;
        private static int hopsCount;
        private  const int maxHopsCount = 20;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                traceroute = new MyTraceroute(textBox_IPOrDomain.Text);
            }
            catch (Exception)
            {
                textBlock_start.Text = "无法解析输入的IP或者域名！";
                return;
            }
            textBlock_start.Text = "通过最多20个跃点跟踪";
            textBlock_start.Text += $"到{textBox_IPOrDomain.Text} [{traceroute.ip}]的路由：";
            Grid_result.Children.Clear();
            Grid_result.RowDefinitions.Clear();          

            button_next.IsEnabled = true;

            hopsCount = 0;
            lastHopIP = "";
        }

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            button_next.IsEnabled = false;

            int rowCount = Grid_result.RowDefinitions.Count;
            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;
            Grid_result.RowDefinitions.Add(row);

            traceroute.InitToSend();

            addTextBlock(0, rowCount, $"  {rowCount + 1}");
            for (int i = 1; i <= 3; i++)
            {
                addTextBlock(i, rowCount, traceroute.sendAndReceive());
            }
            if (lastHopIP == traceroute.hopIP)
                addTextBlock(4, rowCount, "请求超时");
            else
            {
                addTextBlock(4, rowCount, traceroute.hopIP);
                lastHopIP = traceroute.hopIP;
            }

            hopsCount++;
            if (traceroute.hopIP == traceroute.ip.ToString())
            {                
                textBlock_done.Text = " 跟踪完成！";
            }
            else if(hopsCount >= maxHopsCount)
            {
                textBlock_done.Text = "已到达最大跳数！";
            }
            else
                button_next.IsEnabled = true;
        }
       
        private void addTextBlock(int colNum, int rowNum, string text)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 15;
            textBlock.Text = text;
            Grid_result.Children.Add(textBlock);
            Grid.SetColumn(textBlock, colNum);
            Grid.SetRow(textBlock, rowNum);
        }
    }
}
