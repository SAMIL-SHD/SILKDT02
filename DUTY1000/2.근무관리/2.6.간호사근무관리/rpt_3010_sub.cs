using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;
using System.IO;
using SilkRoad.Common;

namespace DUTY1000
{
    public partial class rpt_3010_sub : DevExpress.XtraReports.UI.XtraReport
    {
        SilkRoad.Common.CustomFormatter cf = new SilkRoad.Common.CustomFormatter();
        SilkRoad.Common.DrawRptSignBox dsb = new SilkRoad.Common.DrawRptSignBox();
        CommonLibrary clib = new CommonLibrary();
        DataProcFunc df = new DataProcFunc();

        public rpt_3010_sub()
        {
            InitializeComponent();
            
			c_01.DataBindings.Add("Text", DataSource, "D01", "");
            c_02.DataBindings.Add("Text", DataSource, "D02", "");
            c_03.DataBindings.Add("Text", DataSource, "D03", "");
            c_04.DataBindings.Add("Text", DataSource, "D04", "");
            c_05.DataBindings.Add("Text", DataSource, "D05", "");
            c_06.DataBindings.Add("Text", DataSource, "D06", "");
            c_07.DataBindings.Add("Text", DataSource, "D07", "");
            c_08.DataBindings.Add("Text", DataSource, "D08", "");
            c_09.DataBindings.Add("Text", DataSource, "D09", "");
            c_10.DataBindings.Add("Text", DataSource, "D10", "");
            c_11.DataBindings.Add("Text", DataSource, "D11", "");
            c_12.DataBindings.Add("Text", DataSource, "D12", "");
            c_13.DataBindings.Add("Text", DataSource, "D13", "");
            c_14.DataBindings.Add("Text", DataSource, "D14", "");
            c_15.DataBindings.Add("Text", DataSource, "D15", "");
            c_16.DataBindings.Add("Text", DataSource, "D16", "");
            c_17.DataBindings.Add("Text", DataSource, "D17", "");
            c_18.DataBindings.Add("Text", DataSource, "D18", "");
            c_19.DataBindings.Add("Text", DataSource, "D19", "");
            c_20.DataBindings.Add("Text", DataSource, "D20", "");
            c_21.DataBindings.Add("Text", DataSource, "D21", "");
            c_22.DataBindings.Add("Text", DataSource, "D22", "");
            c_23.DataBindings.Add("Text", DataSource, "D23", "");
            c_24.DataBindings.Add("Text", DataSource, "D24", "");
            c_25.DataBindings.Add("Text", DataSource, "D25", "");
            c_26.DataBindings.Add("Text", DataSource, "D26", "");
            c_27.DataBindings.Add("Text", DataSource, "D27", "");
            c_28.DataBindings.Add("Text", DataSource, "D28", "");
            c_29.DataBindings.Add("Text", DataSource, "D29", "");
            c_30.DataBindings.Add("Text", DataSource, "D30", "");
            c_31.DataBindings.Add("Text", DataSource, "D31", "");
        }
    }
}
