using AhpDemo.ViewModels;
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

namespace AhpDemo.Views
{
    /// <summary>
    /// Interaction logic for TreeView.xaml
    /// </summary>
    public partial class HierarchyView : UserControl
    {
        public HierarchyView()
        {
            InitializeComponent();
        }

        private void TreeItem_RightClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as TreeViewItem;
            if (item == null)
            {
                return;
            }

            var node = item.DataContext as HierarchyNodeViewModel;
            if (node != null)
            {
                node.IsSelected = true;
                e.Handled = true;
            }
        }
    }
}
