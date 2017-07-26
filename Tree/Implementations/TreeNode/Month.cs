using System;
using System.Collections.Generic;
using Tree.BaseEnums;
using Tree.Interfaces;

namespace Tree.Implementations.TreeNode
{
    public class Month : TreeNodeBase
    {
       
        #region Properties
        public MonthEn Value { get; set; }

        public override string NodeName
        {
            get
            {
                return Value.ToString();
            }
        }

        #endregion Properties

        #region Methods

        public Month(string month)
        {
            MonthEn val;
            Enum.TryParse(month, out val);
            Value = val;
        }

        public Month(int month)
        {
            Value = (MonthEn) month;
            AddNewChild();
            AddNewChild();
            AddNewChild();
        }

        public override ITreeNode CreateNewChild()
        {
            return  new Order(true) {
                    Year = 2017, 
                    Month = (MonthEn)1,
                    Day= 25, 
                    DeviceName ="DeviceName",
                    Address ="Address",
                    Phone ="Phone",
                    JobType ="JobType",
                    InstalledDetails ="InstalledDetails",
                    Income =2000,
                    Outgo =500
                };
        }

        #endregion Methods
    }
}
