using Microcharts;
using ParticipanteApp.ViewModel;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace ParticipanteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        private DashboardViewModel ViewModel;
        private bool dadosCarregados = false;

        #region Construtor
        public Dashboard()
        {
            InitializeComponent();
        }
        #endregion

        #region Overrides
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!dadosCarregados)
            {
                ViewModel = new DashboardViewModel();
                
                this.BindingContext = ViewModel;
                ViewModel.PreencherGrafico += ViewModel_PreencherGrafico;

                dadosCarregados = true;
            }
        }

        private void ViewModel_PreencherGrafico(object sender, EventArgs e)
        {
            List<Entry> entries = new List<Entry>();

            var rendimentosParciais = ViewModel.Dashboard.RendimentosParciais.OrderByDescending(rend => rend.DataReferencia).Take(6);
            foreach (var rendimento in rendimentosParciais.OrderBy(rend => rend.DataReferencia))
            {
                entries.Add(new Entry((float)rendimento.PercentualRendimento)
                {
                    Label = rendimento.DataReferencia.ToString("MMM", new CultureInfo("pt-BR")),
                    ValueLabel = $"{rendimento.PercentualRendimento}%",
                    TextColor = SKColor.Parse("#FFFFFF"),
                    Color = SKColor.Parse("#FFFFFF")
                });
            }

            var lineChart = new LineChart { Entries = entries };
            lineChart.LineMode = LineMode.Straight;
            lineChart.LabelTextSize = 35;
            lineChart.PointSize = 10;
            lineChart.BackgroundColor = SKColor.Parse("#346FB7");

            this.lineView.Chart = lineChart;
        }
        #endregion
    }
}