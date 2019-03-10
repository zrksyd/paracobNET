﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using paracobNET;

namespace prcEditor
{
    class ParamTreeItem : TreeViewItem
    {
        public ParamBase Param { get; set; }
        public new ParamTreeItem Parent { get; set; }

        private string _ParentAccessor;
        public string ParentAccessor
        {
            get { return _ParentAccessor; }
            set
            {
                _ParentAccessor = value;
                if (!string.IsNullOrEmpty(value))
                    Header = Param.TypeKey.ToString() + $" ({value})";
            }
        }

        public ParamTreeItem(ParamBase param, ParamTreeItem parent)
        {
            Param = param;
            Parent = parent;

            switch (param.TypeKey)
            {
                case ParamType.array:
                    {
                        var nodes = (param as ParamArray).Nodes;
                        string format = $"d{(nodes.Length - 1).ToString().Length}";
                        for (int i = 0; i < nodes.Length; i++)
                        {
                            var child = new ParamTreeItem(nodes[i], this);
                            child.ParentAccessor = i.ToString(format);
                            Items.Add(child);
                        }
                    }
                    break;
                case ParamType.@struct:
                    foreach (var childParam in (param as ParamStruct).Nodes)
                    {
                        var childNode = new ParamTreeItem(childParam.Value, this);
                        if (MainWindow.HashToStringLabels.TryGetValue(childParam.Key, out string label))
                            childNode.ParentAccessor = label;
                        else
                            childNode.ParentAccessor = "0x" + childParam.Key.ToString("x10");
                        Items.Add(childNode);
                    }
                    break;
            }

            Header = Param.TypeKey.ToString();
        }
    }
}