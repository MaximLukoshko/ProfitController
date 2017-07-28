//using DAOLayer.Implementations;
//using DAOLayer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Tree.Implementations;
using Tree.Implementations.TreeNode;
using Tree.Interfaces;

namespace ProfitController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ITreeModel _model = new TreeModel();
   //     private IDAO _dao = new DAO();
        public ICollection<ITreeNode> Nodes 
        { 
            get
            {
                return _model.Nodes;
            }
        }

     /*   public IDAO DataAcsessObject
        {
            get
            {
                return _dao;
            }
        }
        */
        public MainWindow()
        {
            InitializeComponent();

            trw_Orders.ItemsSource = Nodes;
        }

        private void UpdateWindow()
        {

        }

        private void treeItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        { 
            var sel = (ITreeNode)trw_Orders.SelectedItem;
            dgrd_Orders.ItemsSource = sel.Orders;
        }

        private void Row_Add(object sender, RoutedEventArgs e)
        {
            var sel = (ITreeNode)trw_Orders.SelectedItem;
            if(sel!=null)
                sel.AddNewChild();
            UpdateWindow();
        }

        private void Row_Delete(object sender, RoutedEventArgs e)
        {
            var sel = (ITreeNode)trw_Orders.SelectedItem;
            sel.RemoveThis();
            UpdateWindow();
        }

        private void AddToGrid_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteFromGrid_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
