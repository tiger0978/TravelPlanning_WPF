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
using TravelPlanning.Views.CreateTravel;
using Wpf.Ui.Appearance;

namespace TravelPlanning.Views
{
    /// <summary>
    /// CreateTravelWindow.xaml 的互動邏輯
    /// </summary>
    public partial class CreateTravelWindow : Window
    {
        public CreateTravelContext Context { get; set; } = new CreateTravelContext();    
        public CreateTravelWindow()
        {
            InitializeComponent();

            this.DataContext = Context;
        }
    }
}
