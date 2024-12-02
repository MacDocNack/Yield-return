using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YieldReturn
{
    public partial class MainWindow : Window
    {
        private static ProgressBar _progressBar;
        public MainWindow()
        {
            InitializeComponent();
            _progressBar = Process;
        }
        private static async IAsyncEnumerable<int> GenerateNumbersAsync(int start, int end)
        {
            _progressBar.Minimum = start - 1;
            _progressBar.Maximum = end;
            for (int i = start; i <= end; i++)
            {
                await Task.Delay(300);
                _progressBar.Value = i;
                yield return i;
            }
        }

        private async void Generate(object sender, RoutedEventArgs e)
        {
            int.TryParse(Start.Text, out var startNumber);
            int.TryParse(End.Text, out var endNumber);

            await foreach (var number in GenerateNumbersAsync(startNumber, endNumber))
            {
                ItemList.Items.Add(number);
            }
        }
    }

}
