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
using System.Windows.Shapes;

namespace TeamBravo___2.Semester___Eksamensopgave
{
    /// <summary>
    /// Interaction logic for NyFilWindow.xaml
    /// </summary>
    public partial class NyFilWindow : Window
    {
        public NyFilWindow(List<Affaldspost> affaldsposts)
        {
            InitializeComponent();
            Dispatcher.Invoke(() => nyfilGrid.ItemsSource = affaldsposts);
        }

    }

}
