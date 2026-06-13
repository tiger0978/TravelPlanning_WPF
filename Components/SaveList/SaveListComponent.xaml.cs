using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                   }
               ));

        public ICommand SelectedItem
        {
            get => (ICommand)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        private void MoreMenu_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var menu = button.ContextMenu;
            menu.PlacementTarget = button;
            menu.IsOpen = true;
        }
    }
}
