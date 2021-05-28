using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class StatistikWindow : Window
    {

        public SeriesCollection seriesCollection { get; set; }
        public DateTime startTime { get; set; }
        public Func<double, string> Formatter { get; set; }
        public SqlRepository sql = new SqlRepository();

        public class DatoModel
        {
            public DateTime dateTime { get; set; }
            public double value { get; set; }

        }

        public StatistikWindow()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            DataContext = this;

            seriesCollection = new SeriesCollection();

            myChart.Zoom = LiveCharts.ZoomingOptions.Xy;

            string start = "2021-05-03 08:00:00";
            this.startTime = DateTime.ParseExact(start, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            this.Formatter = value => new DateTime((long)value).ToString("yyyy-MM-dd HH:mm");

            var kategorier = sql.GetKategorier();
            if (kategorier != null)
            {
                int tal = 1;
                foreach (var item in MyCanvas.Children.OfType<Button>())
                {
                    if (kategorier.Contains(tal))
                    {
                        item.IsEnabled = true;
                        item.Foreground = Brushes.White;
                    }
                    tal++;
                }

            }
            else
            {
                MessageBox.Show("Kunne ikke få fat på affaldskategorierne!");
            }

        }

        public DatoModel[] MakeChartValues(int kategori, int maaleenhed)
        {
            //DateTime[] dates = sql.GetChartDates(kategori, maaleenhed, VirksomhedID);
            //double[] values = sql.GetChartPosts(kategori, maaleenhed, VirksomhedID);

            DateTime[] dates = sql.GetChartDates(kategori, maaleenhed);
            double[] values = sql.GetChartPosts(kategori, maaleenhed);

            Array.Sort(dates);

            if (dates.Length != 0 && values.Length != 0)
            {
                DatoModel[] datesAndnumbers = new DatoModel[values.Length];

                for (int i = 0; i < datesAndnumbers.Length; i++)
                {
                    datesAndnumbers[i] = new DatoModel { dateTime = dates[i], value = values[i] };
                }

                return datesAndnumbers;

            }
            else
            {
                return null;
            }

        }

        public void MakeLineSeries(int kategori)
        {
            var dayConfig = Mappers.Xy<DatoModel>().X(datoModel => datoModel.dateTime.Ticks).Y(datoModel => datoModel.value);

            DatoModel[] ChartValues1 = MakeChartValues(kategori, 1);
            DatoModel[] ChartValues2 = MakeChartValues(kategori, 2);
            DatoModel[] ChartValues3 = MakeChartValues(kategori, 3);
            DatoModel[] ChartValues4 = MakeChartValues(kategori, 4);
            DatoModel[] ChartValues5 = MakeChartValues(kategori, 5);
            DatoModel[] ChartValues6 = MakeChartValues(kategori, 6);
            DatoModel[] ChartValues7 = MakeChartValues(kategori, 7);
            DatoModel[] ChartValues8 = MakeChartValues(kategori, 8);

            #region LineSeries
            if (ChartValues1 != null)
            {
                var linesserie = new LineSeries
                {
                    Title = "Colli",
                    Values = new ChartValues<DatoModel>(ChartValues1),
                    StrokeThickness = 3,
                    Stroke = Brushes.Blue,
                    Fill = Brushes.LightBlue,
                    Configuration = dayConfig
                };

                seriesCollection.Add(linesserie);
            }

            if (ChartValues2 != null)
            {
                var linesserie = new LineSeries
                {
                    Title = "Stk",
                    Values = new ChartValues<DatoModel>(ChartValues2),
                    StrokeThickness = 3,
                    Stroke = Brushes.Purple,
                    Fill = Brushes.MediumPurple,
                    Configuration = dayConfig
                };

                seriesCollection.Add(linesserie);
            }

            if (ChartValues3 != null)
            {
                var linesserie = new LineSeries
                {
                    Title = "Ton",
                    Values = new ChartValues<DatoModel>(ChartValues3),
                    StrokeThickness = 3,
                    Stroke = Brushes.Pink,
                    Fill = Brushes.LightPink,
                    Configuration = dayConfig
                };

                seriesCollection.Add(linesserie);
            }

            if (ChartValues4 != null)
            {
                var linesserie = new LineSeries
                {
                    Title = "Kilogram",
                    Values = new ChartValues<DatoModel>(ChartValues4),
                    StrokeThickness = 3,
                    Stroke = Brushes.Red,
                    Fill = Brushes.MistyRose,
                    Configuration = dayConfig
                };

                seriesCollection.Add(linesserie);
            }

            if (ChartValues5 != null)
            {
                var linesserie = new LineSeries
                {
                    Title = "Gram",
                    Values = new ChartValues<DatoModel>(ChartValues5),
                    StrokeThickness = 3,
                    Stroke = Brushes.Orange,
                    Fill = Brushes.Khaki,
                    Configuration = dayConfig
                };

                seriesCollection.Add(linesserie);
            }

            if (ChartValues6 != null)
            {
                var linesserie = new LineSeries
                {
                    Title = "M3",
                    Values = new ChartValues<DatoModel>(ChartValues6),
                    StrokeThickness = 3,
                    Stroke = Brushes.Yellow,
                    Fill = Brushes.LightYellow,
                    Configuration = dayConfig
                };

                seriesCollection.Add(linesserie);
            }

            if (ChartValues7 != null)
            {
                var linesserie = new LineSeries
                {
                    Title = "Liter",
                    Values = new ChartValues<DatoModel>(ChartValues7),
                    StrokeThickness = 3,
                    Stroke = Brushes.Brown,
                    Fill = Brushes.SandyBrown,
                    Configuration = dayConfig
                };

                seriesCollection.Add(linesserie);
            }

            if (ChartValues8 != null)
            {
                var linesserie = new LineSeries
                {
                    Title = "Hektoliter",
                    Values = new ChartValues<DatoModel>(ChartValues8),
                    StrokeThickness = 3,
                    Stroke = Brushes.DarkGreen,
                    Fill = Brushes.LightGreen,
                    Configuration = dayConfig
                };

                seriesCollection.Add(linesserie);
            }
            #endregion
        }

        private void Batterier_Click(object sender, RoutedEventArgs e)
        {
            seriesCollection.Clear();

            MakeLineSeries(1);

            myChart.AxisX[0].MinValue = double.NaN;
            myChart.AxisX[0].MaxValue = double.NaN;
            myChart.AxisY[0].MinValue = double.NaN;
            myChart.AxisY[0].MaxValue = double.NaN;

        }

        private void Biler_Click(object sender, RoutedEventArgs e)
        {
            seriesCollection.Clear();

            MakeLineSeries(2);

            myChart.AxisX[0].MinValue = double.NaN;
            myChart.AxisX[0].MaxValue = double.NaN;
            myChart.AxisY[0].MinValue = double.NaN;
            myChart.AxisY[0].MaxValue = double.NaN;
        }

        private void Elektronik_Click(object sender, RoutedEventArgs e)
        {
            seriesCollection.Clear();

            MakeLineSeries(3);

            myChart.AxisX[0].MinValue = double.NaN;
            myChart.AxisX[0].MaxValue = double.NaN;
            myChart.AxisY[0].MinValue = double.NaN;
            myChart.AxisY[0].MaxValue = double.NaN;
        }

        private void Impraeg_Click(object sender, RoutedEventArgs e)
        {
            seriesCollection.Clear();

            MakeLineSeries(4);

            myChart.AxisX[0].MinValue = double.NaN;
            myChart.AxisX[0].MaxValue = double.NaN;
            myChart.AxisY[0].MinValue = double.NaN;
            myChart.AxisY[0].MaxValue = double.NaN;
        }

        private void Inventar_Click(object sender, RoutedEventArgs e)
        {
            seriesCollection.Clear();
 
            MakeLineSeries(5);

            myChart.AxisX[0].MinValue = double.NaN;
            myChart.AxisX[0].MaxValue = double.NaN;
            myChart.AxisY[0].MinValue = double.NaN;
            myChart.AxisY[0].MaxValue = double.NaN;
        }

        private void Organisk_Click(object sender, RoutedEventArgs e)
        {
            seriesCollection.Clear();

            MakeLineSeries(6);

            myChart.AxisX[0].MinValue = double.NaN;
            myChart.AxisX[0].MaxValue = double.NaN;
            myChart.AxisY[0].MinValue = double.NaN;
            myChart.AxisY[0].MaxValue = double.NaN;
        }

        private void Pap_Click(object sender, RoutedEventArgs e)
        {
            seriesCollection.Clear();

            MakeLineSeries(7);

            myChart.AxisX[0].MinValue = double.NaN;
            myChart.AxisX[0].MaxValue = double.NaN;
            myChart.AxisY[0].MinValue = double.NaN;
            myChart.AxisY[0].MaxValue = double.NaN;
        }

        private void Plast_Click(object sender, RoutedEventArgs e)
        {
            seriesCollection.Clear();

            MakeLineSeries(8);

            myChart.AxisX[0].MinValue = double.NaN;
            myChart.AxisX[0].MaxValue = double.NaN;
            myChart.AxisY[0].MinValue = double.NaN;
            myChart.AxisY[0].MaxValue = double.NaN;
        }

        private void PVC_Click(object sender, RoutedEventArgs e)
        {
            seriesCollection.Clear();

            MakeLineSeries(9);

            myChart.AxisX[0].MinValue = double.NaN;
            myChart.AxisX[0].MaxValue = double.NaN;
            myChart.AxisY[0].MinValue = double.NaN;
            myChart.AxisY[0].MaxValue = double.NaN;
        }

    }

}
