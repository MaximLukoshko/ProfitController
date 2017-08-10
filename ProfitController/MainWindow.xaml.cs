using System;
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
        private ITreeModel _model = new TreeModel();
        private readonly IDAO _dao = new DAO();

        public ICollection<ITreeNode> Nodes
        {
            get { return GetNodes(_model.Nodes); }
        }

        public IDAO DataAcsessObject
        {
            get { return _dao; }
        }

        public MainWindow()
        {
            InitializeComponent();
            trw_Orders.ItemsSource = Nodes;
        }

        #region RefreshMethods

        private void UpdateWindow()
        {
            UpdateTreeView();
            UpdateOrdersView();
        }

        private void UpdateTreeView()
        {
            var sel = trw_Orders.SelectedItem;
            trw_Orders.ItemsSource = Nodes;
            trw_Orders.SelectedItem = sel;
        }

        private void UpdateOrdersView()
        {
            dgrd_Orders.ItemsSource = null;
            dgrd_Summary.ItemsSource = null;
            var sel = trw_Orders.SelectedItem as ITreeNode;
            if (sel != null)
            {
                dgrd_Orders.ItemsSource = sel.Orders;
                dgrd_Summary.ItemsSource = sel.Summary;
            }
        }

        #endregion RefreshMethods

        private ICollection<ITreeNode> GetNodes(ICollection<ITreeNode> nodesList)
        {
            var ret = new List<ITreeNode>();
            foreach (var child in nodesList)
                ret.AddRange(GetNodes(child));
            return ret;
        }

        private ICollection<ITreeNode> GetNodes(ITreeNode node)
        {
            var ret = new List<ITreeNode>();
            if (node != null)
            {
                ret.Add(node);
                if (node.IsExpanded)
                    ret.AddRange(GetNodes(node.ChildNodes));

            }
            return ret;
        }

        private void treeItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var sel = (ITreeNode) trw_Orders.SelectedItem;
            if (sel != null)
                UpdateOrdersView();
        }

        private void Row_Add(object sender, RoutedEventArgs e)
        {
            var sel = (ITreeNode) trw_Orders.SelectedItem;
            if (sel != null)
                _model.AddChildToNode(sel);
            UpdateWindow();
        }

        private void Row_Delete(object sender, RoutedEventArgs e)
        {
            var sel = (ITreeNode) trw_Orders.SelectedItem;
            if (sel != null)
                _model.RemoveNode(sel);
            UpdateWindow();
        }

        private void AddToGrid_Click(object sender, RoutedEventArgs e)
        {
            var sel = (ITreeNode) trw_Orders.SelectedItem;
            if (sel != null)
                _model.AddOrderToNode(sel);
            UpdateOrdersView();
        }

        private void DeleteFromGrid_Click(object sender, RoutedEventArgs e)
        {
            var selNode = (ITreeNode) trw_Orders.SelectedItem;
            var selLine = (IOrderLine) dgrd_Orders.SelectedItem;
            if (selNode != null)
                _model.RemoveOrderFromNode(selNode, selLine);
            UpdateOrdersView();
        }

        private string _filename = string.Empty;

        internal enum DlgMode
        {
            Save,
            Open
        }

        private string ChooseFile(DlgMode mode)
        {
            FileDialog dlg;
            switch (mode)
            {
                case DlgMode.Save:
                    dlg = new SaveFileDialog();
                    break;
                case DlgMode.Open:
                    dlg = new OpenFileDialog();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode", mode, null);
            }

            dlg.FileName = "";
            dlg.DefaultExt = ".pcm";
            dlg.Filter = "Profit Controller Model (.pcm)|*.pcm";

            return dlg.ShowDialog() == true ? dlg.FileName : string.Empty;
        }

        private void Open_BtnClick(object sender, RoutedEventArgs e)
        {
            if (AskConfirmationAndSave())
            {
                var path = ChooseFile(DlgMode.Open);
                if (!string.IsNullOrEmpty(path))
                {
                    _dao.LoadModelFromFile(_model, path);
                    _filename = path;
                    UpdateWindow();
                }
            }
        }

        private void Save()
        {
            var result = false;

            if (!string.IsNullOrEmpty(_filename))
                result = _dao.SaveModelToFile(_model, _filename);

            if (!result)
                SaveAs();
        }

        private void SaveAs()
        {
            var path = ChooseFile(DlgMode.Save);
            if (!string.IsNullOrEmpty(path) && !_dao.SaveModelToFile(_model, path))
                MessageBox.Show("Не сохранено", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Save_BtnClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void SaveAs_BtnClick(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }

        private bool AskConfirmationAndSave()
        {
            var dialogResult = MessageBox.Show("Все несохранённые данные будут утеряны! \nСохранить?",
                "Внимание!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (dialogResult == MessageBoxResult.Cancel)
                return false; // Action declined

            if (dialogResult == MessageBoxResult.Yes)
                Save();

            return true;
        }

        private void Close_BtnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TrwSaveAs_Click(object sender, RoutedEventArgs e)
        {
            var selNode = (ITreeNode) trw_Orders.SelectedItem;
            if (selNode != null)
            {
                var path = ChooseFile(DlgMode.Save);
                if (!string.IsNullOrEmpty(path) && !_dao.SaveNodeToFile(selNode, path))
                    MessageBox.Show("Не сохранено", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Create_BtnClick(object sender, RoutedEventArgs e)
        {
            if (AskConfirmationAndSave())
            {
                _model = new TreeModel();
                UpdateWindow();
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !AskConfirmationAndSave();
            base.OnClosing(e);
        }

        private void ExpandNode(ITreeNode node, bool? expand = null)
        {
            if (node != null)
                node.IsExpanded = expand ?? !node.IsExpanded;
        }

        private void trw_Orders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selNode = (ITreeNode) trw_Orders.SelectedItem;
            if (selNode != null)
            {
                ExpandNode(selNode);
                UpdateTreeView();
            }
        }
    }
}
