﻿using paracobNET;

namespace prcEditor
{
    public class ParamListEntry
    {
        private int Index;
        private ParamValue Param;

        public int Key
        {
            get { return Index; }
        }
        public ParamType Type
        {
            get { return Param.TypeKey; }
        }
        public string Value
        {
            get { return Param.ToString(MainWindow.HashToStringLabels); }
            set { Param.SetValue(value, MainWindow.StringToHashLabels); }
        }

        public ParamListEntry(int key, ParamValue param)
        {
            Index = key;
            Param = param;
        }
    }
}
