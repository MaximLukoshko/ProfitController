using DAOLayer.Implementations;
using DAOLayer.Interfaces;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Tree.Implementations;
using Tree.Interfaces;

namespace ProfitController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ITreeModel _model = new TreeModel();
        private readonly IDAO _dao = new DAO();
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
            if (selNode != null)
                _model.RemoveOrderFromNode(selNode, selLine);
            UpdateOrdersView();
        }

        private string _filename = string.Empty;
        private string ChooseOpenFile_dlg()
        {
            var dlg = new OpenFileDialog
            {
                FileName = "",
                DefaultExt = ".pcm",
                Filter = "(.pcm)|*.pcm"
            };
            if (dlg.ShowDialog() == true)
                return dlg.FileName;
            else return string.Empty;
        }
        private string ChooseSaveFile_dlg()
        {
            var dlg = new SaveFileDialog
            {
                FileName = "",
                DefaultExt = ".pcm",
                Filter = "(.pcm)|*.pcm"
            };
            if (dlg.ShowDialog() == true)
                return dlg.FileName;
            else return string.Empty;
        }

        private void Open_BtnClick(object sender, RoutedEventArgs e)
        {
            var path = ChooseOpenFile_dlg();
                if (!string.IsNullOrEmpty(path))
            {
                _dao.LoadModelFromFile(_model, path);
                _filename = path;
                UpdateWindow();
            }
        }

        private void Save_BtnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_filename))
            {
                if (_dao.SaveModelToFile(_model, _filename))
                    MessageBox.Show("Сохранено", "", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Не сохранено", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else SaveAs_BtnClick(sender, e);
        }

        private void SaveAs_BtnClick(object sender, RoutedEventArgs e)
        {
            var path = ChooseSaveFile_dlg();
            {
                if (_dao.SaveModelToFile(_model,path))
                    MessageBox.Show("Сохранено", "", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Не сохранено", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Close_BtnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Все несохранённые данные будут утеряны! \nВыйти?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (dialogResult == MessageBoxResult.Yes)
                Close();
        }

        private void TrwSaveAs_Click(object sender, RoutedEventArgs e)
        {
            var path = ChooseSaveFile_dlg();
            var selNode = (ITreeNode)trw_Orders.SelectedItem;
            {
                if (_dao.SaveNodeToFile(selNode, path))
                    MessageBox.Show("Сохранено", "", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Не сохранено", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
