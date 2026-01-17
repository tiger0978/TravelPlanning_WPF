using GoogleMap.SDK.Contract;
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

namespace TravelPlanning.Components.GMap
{
    /// <summary>
    /// GoogleMap.xaml 的互動邏輯
    /// </summary>
    public partial class GoogleMapComponent : UserControl
    {
        public GoogleMapComponent(IGMap map)
        {
            InitializeComponent();
            GoolgeMapContext model = new GoolgeMapContext();
            DataContext = model;
            MapContainer.Children.Add((UserControl)map);
        }

    }
}
