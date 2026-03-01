using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using TravelPlanning.Components.SaveList.Models;

namespace TravelPlanning.Components.SaveList
{
    /// <summary>
    /// SaveListComponent.xaml 的互動邏輯
    /// </summary>
    public partial class SaveListComponent : UserControl
    {
        private readonly SaveListContext _context;
        public SaveListComponent()
        {
            InitializeComponent();
            _context = new SaveListContext();
            DataContext = _context;
        }

        public static readonly DependencyProperty SelectedItemProperty =
          DependencyProperty.Register(
               nameof(SelectedItem),
               typeof(ICommand),
               typeof(SaveListComponent),
               new PropertyMetadata(
                   (d, e) =>
                   {
                       SaveListComponent saveListComponent = (SaveListComponent)d;
                    //   saveListComponent._context.SelectedItemCommand = (ICommand)e.NewValue; 
                   }
               ));

        // saveListComponent.SelectedItem  = new RelayCommand();
        public ICommand SelectedItem
        {
            get => (ICommand)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
    }
}
