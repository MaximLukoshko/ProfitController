using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Tree.Implementations.TreeNode;
using Tree.Interfaces;

namespace ProfitController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ITreeNode> Nodes { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Nodes = new List<ITreeNode>();
            Nodes.Add(new Year(2016));
            Nodes.Add(new Year(2017));
            trw_Orders.ItemsSource = Nodes;
            dgrd_Orders.ItemsSource = Nodes[0].Orders;
        }
    }
}
