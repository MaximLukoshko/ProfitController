using DAOLayer.Implementations;
using DAOLayer.Interfaces;
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
        private IDAO _dao = new DAO();
        public ICollection<ITreeNode> Nodes 
        { 
            get
            {
                return _model.Nodes;
            }
        }

        public IDAO DataAcsessObject
        {
            get
            {
                return _dao;
            }
        }
           
        public MainWindow()
        {
            //_dao.SaveModelToFile(_model, @"Test.xml");
            //_dao.LoadModelFromFile(_model, @"Test.xml");

            InitializeComponent();

            trw_Orders.ItemsSource = Nodes;
        }

        private void UpdateWindow()
        {
            trw_Orders.ItemsSource = null;
            trw_Orders.ItemsSource = _model.Nodes;

            UpdateOrdersView();
        }

        private void UpdateOrdersView()
        {
            dgrd_Orders.ItemsSource = null;
            var sel = trw_Orders.SelectedItem as ITreeNode;
            if (sel != null)
                dgrd_Orders.ItemsSource = sel.Orders;
        }

        private void treeItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        { 
            var sel = (ITreeNode)trw_Orders.SelectedItem;
            if(sel!=null)
                dgrd_Orders.ItemsSource = sel.Orders;
        }

        private void Row_Add(object sender, RoutedEventArgs e)
        {
            var sel = (ITreeNode)trw_Orders.SelectedItem;
            if (sel != null)
                _model.AddChildToNode(sel);
            UpdateWindow();
        }

        private void Row_Delete(object sender, RoutedEventArgs e)
        {
            var sel = (ITreeNode)trw_Orders.SelectedItem;
            if(sel!=null)
                _model.RemoveNode(sel);
            UpdateWindow();
        }

        private void AddToGrid_Click(object sender, RoutedEventArgs e)
        {
            var sel = (ITreeNode)trw_Orders.SelectedItem;
            if(sel!=null)
                _model.AddOrderToNode(sel);
            UpdateOrdersView();
        }

        private void DeleteFromGrid_Click(object sender, RoutedEventArgs e)
        {
            var selNode = (ITreeNode)trw_Orders.SelectedItem;
            var selLine = (IOrderLine)dgrd_Orders.SelectedItem;
            if (selNode != null && selNode!=null)
                _model.RemoveOrderFromNode(selNode, selLine);
            UpdateOrdersView();
        }

        private void Open_BtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void Save_BtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void SaveAs_BtnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
