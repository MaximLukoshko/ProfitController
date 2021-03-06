﻿using System;
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
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
            TrwOrders.ItemsSource = Nodes;
        }

        #endregion Constructors

        #region PrivateFields

        private ICollection<TreeNodeWrapper> Nodes
        {
            get { return GetNodes(_model.Nodes); }
        }

        private IDao DataAccessObject
        {
            get { return _dao; }
        }

        private ITreeModel _model = new TreeModel();
        private readonly IDao _dao = new Dao();

        private string _filename = string.Empty;

        internal enum DlgMode
        {
            Save,
            Open
        }

        #endregion PrivateFields

        #region RefreshMethods

        private void UpdateWindow()
        {
            UpdateTreeView();
            UpdateOrdersView();
        }

        private void UpdateTreeView()
        {
            var sel = TrwOrders.SelectedItem as TreeNodeWrapper;
            TrwOrders.ItemsSource = Nodes;
            if (sel == null)
                return;
            foreach (var t in TrwOrders.Items)
            {
                var it = t as TreeNodeWrapper;
                if (it != null && it.Source.Equals(sel.Source))
                {
                    TrwOrders.SelectedItem = t;
                    break;
                }
            }
        }

        private void UpdateOrdersView()
        {
            DgrdOrders.ItemsSource = null;
            DgrdSummary.ItemsSource = null;
            var sel = TrwOrders.SelectedItem as TreeNodeWrapper;
            if (sel != null)
            {
                DgrdOrders.ItemsSource = sel.Source.Orders;
                DgrdSummary.ItemsSource = sel.Source.Summary;
            }
        }

        #endregion RefreshMethods

        #region Events

        #region TrwOrders

        private void treeItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var sel = (TreeNodeWrapper) TrwOrders.SelectedItem;
            if (sel != null)
                UpdateOrdersView();
        }

        private void Row_Add(object sender, RoutedEventArgs e)
        {
            var sel = (TreeNodeWrapper) TrwOrders.SelectedItem;
            if (sel != null)
                _model.AddChildToNode(sel.Source);
            UpdateWindow();
        }

        private void Row_Delete(object sender, RoutedEventArgs e)
        {
            var sel = (TreeNodeWrapper) TrwOrders.SelectedItem;
            if (sel != null)
                _model.RemoveNode(sel.Source);
            UpdateWindow();
        }

        private void TrwSaveAs_Click(object sender, RoutedEventArgs e)
        {
            var selNode = (TreeNodeWrapper) TrwOrders.SelectedItem;
            if (selNode != null)
            {
                var path = ChooseFile(DlgMode.Save);
                if (!string.IsNullOrEmpty(path) && !DataAccessObject.SaveNodeToFile(selNode.Source, path))
                    MessageBox.Show("Не сохранено", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void trw_Orders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selNode = (TreeNodeWrapper) TrwOrders.SelectedItem;
            if (selNode != null)
            {
                ExpandNode(selNode.Source);
                UpdateTreeView();
            }
        }

        #endregion TrwOrders

        #region DgrdOrders

        private void AddToGrid_Click(object sender, RoutedEventArgs e)
        {
            var sel = (TreeNodeWrapper) TrwOrders.SelectedItem;
            if (sel != null)
                _model.AddOrderToNode(sel.Source);
            UpdateOrdersView();
        }

        private void DeleteFromGrid_Click(object sender, RoutedEventArgs e)
        {
            var selNode = (TreeNodeWrapper) TrwOrders.SelectedItem;
            var selLine = (IOrderLine) DgrdOrders.SelectedItem;
            if (selNode != null)
                _model.RemoveOrderFromNode(selNode.Source, selLine);
            UpdateOrdersView();
        }

        #endregion DgrdOrders

        #region ToolBar

        private void Open_BtnClick(object sender, RoutedEventArgs e)
        {
            if (AskConfirmationAndSave())
            {
                var path = ChooseFile(DlgMode.Open);
                if (!string.IsNullOrEmpty(path))
                {
                    DataAccessObject.LoadModelFromFile(_model, path);
                    _filename = path;
                    UpdateWindow();
                }
            }
        }

        private void Save_BtnClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void SaveAs_BtnClick(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }

        private void Update_BtnClick(object sender, RoutedEventArgs e)
        {
            UpdateWindow();
        }

        private void Close_BtnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Create_BtnClick(object sender, RoutedEventArgs e)
        {
            if (AskConfirmationAndSave())
            {
                _model = new TreeModel();
                _filename = string.Empty;
                UpdateWindow();
            }
        }

        #endregion ToolBar

        #region OtherEvents

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !AskConfirmationAndSave();
            base.OnClosing(e);
        }

        #endregion OtherEvents

        #endregion Events

        #region HelpingMethods

        private void Save()
        {
            var result = false;

            if (!string.IsNullOrEmpty(_filename))
                result = DataAccessObject.SaveModelToFile(_model, _filename);

            if (!result)
                SaveAs();
        }

        private void SaveAs()
        {
            var path = ChooseFile(DlgMode.Save);
            if (string.IsNullOrEmpty(path))
                return;

            if (DataAccessObject.SaveModelToFile(_model, path))
                _filename = path;
            else
                MessageBox.Show("Не сохранено", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool AskConfirmationAndSave()
        {
            if (!IsModelChanged())
                return true;

            var dialogResult = MessageBox.Show("Все несохранённые данные будут утеряны! \nСохранить?",
                "Внимание!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (dialogResult == MessageBoxResult.Cancel)
                return false; // Action declined

            if (dialogResult == MessageBoxResult.Yes)
                Save();

            return true;
        }

        private bool IsModelChanged()
        {
            ITreeModel cmpModel = new TreeModel();
            if (!string.IsNullOrEmpty(_filename))
                DataAccessObject.LoadModelFromFile(cmpModel, _filename);
            return !cmpModel.Equals(_model);
        }

        private void ExpandNode(ITreeNode node, bool? expand = null)
        {
            if (node != null)
                node.IsExpanded = expand ?? !node.IsExpanded;
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

        private ICollection<TreeNodeWrapper> GetNodes(ICollection<ITreeNode> nodesList)
        {
            var ret = new List<TreeNodeWrapper>();
            foreach (var child in nodesList)
                ret.AddRange(GetNodes(child));
            return ret;
        }

        private ICollection<TreeNodeWrapper> GetNodes(ITreeNode node)
        {
            var ret = new List<TreeNodeWrapper>();
            if (node != null)
            {
                ret.Add(new TreeNodeWrapper(node));
                if (node.IsExpanded)
                    ret.AddRange(GetNodes(node.ChildNodes));
            }
            return ret;
        }

        #endregion HelpingMethods
    }
}