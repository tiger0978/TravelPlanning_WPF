using IoC_Container.Attributes;
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

namespace TravelPlanning.Views.Pages.SearchPlace.Comment
{
    /// <summary>
    /// CommentPage.xaml 的互動邏輯
    /// </summary>
    public partial class CommentPage : Page
    {
        public CommentPage(CommentPageContext commentPageContext)
        {
            InitializeComponent();
            DataContext = commentPageContext;
        }
        private void ExpandButton_Click(object sender, MouseButtonEventArgs e)
        {
            var button = sender as TextBlock;
            if (button == null) return;

            // 往上找到 StackPanel，再找到 ReviewText
            var parent = VisualTreeHelper.GetParent(button);
            while (!(parent is StackPanel))
                parent = VisualTreeHelper.GetParent(parent);

            var stackPanel = parent as StackPanel;
            var reviewText = stackPanel?.Children
                .OfType<TextBlock>()
                .FirstOrDefault(t => t.Name == "ReviewText");

            if (reviewText == null) return;

            if (reviewText.MaxHeight == 60)
            {
                reviewText.MaxHeight = double.PositiveInfinity;
                button.Text = "收合";
            }
            else
            {
                reviewText.MaxHeight = 60;
                button.Text = "查看更多";
            }
        }
    }
}
