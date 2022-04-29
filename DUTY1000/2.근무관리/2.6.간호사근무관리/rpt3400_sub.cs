using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;
using System.IO;

namespace DUTY1200
{
    public partial class rpt3400_sub : DevExpress.XtraReports.UI.XtraReport
    {
        SilkRoad.Common.CustomFormatter cf = new SilkRoad.Common.CustomFormatter();
        SilkRoad.Common.DrawRptSignBox dsb = new SilkRoad.Common.DrawRptSignBox();
        DataSet sub_ds;
        DataFuns.df1000 df = new DataFuns.df1000();

        public rpt3400_sub( )
        {
            InitializeComponent();
            c2_cnt.DataBindings.Add("Text", DataSource, "CNT", "");
            c2_1.DataBindings.Add("Text", DataSource, "D1", "");
            c2_2.DataBindings.Add("Text", DataSource, "D2", "");
            c2_3.DataBindings.Add("Text", DataSource, "D3", "");
            c2_4.DataBindings.Add("Text", DataSource, "D4", "");
            c2_5.DataBindings.Add("Text", DataSource, "D5", "");
            c2_6.DataBindings.Add("Text", DataSource, "D6", "");
            c2_7.DataBindings.Add("Text", DataSource, "D7", "");
            c2_8.DataBindings.Add("Text", DataSource, "D8", "");
            c2_9.DataBindings.Add("Text", DataSource, "D9", "");
            c2_10.DataBindings.Add("Text", DataSource, "D10", "");
            c2_11.DataBindings.Add("Text", DataSource, "D11", "");
            c2_12.DataBindings.Add("Text", DataSource, "D12", "");
            c2_13.DataBindings.Add("Text", DataSource, "D13", "");
            c2_14.DataBindings.Add("Text", DataSource, "D14", "");
            c2_15.DataBindings.Add("Text", DataSource, "D15", "");
            c2_16.DataBindings.Add("Text", DataSource, "D16", "");
            c2_17.DataBindings.Add("Text", DataSource, "D17", "");
            c2_18.DataBindings.Add("Text", DataSource, "D18", "");
            c2_19.DataBindings.Add("Text", DataSource, "D19", "");
            c2_20.DataBindings.Add("Text", DataSource, "D20", "");
            c2_21.DataBindings.Add("Text", DataSource, "D21", "");
            c2_22.DataBindings.Add("Text", DataSource, "D22", "");
            c2_23.DataBindings.Add("Text", DataSource, "D23", "");
            c2_24.DataBindings.Add("Text", DataSource, "D24", "");
            c2_25.DataBindings.Add("Text", DataSource, "D25", "");
            c2_26.DataBindings.Add("Text", DataSource, "D26", "");
            c2_27.DataBindings.Add("Text", DataSource, "D27", "");
            c2_28.DataBindings.Add("Text", DataSource, "D28", "");
            c2_29.DataBindings.Add("Text", DataSource, "D29", "");
            c2_30.DataBindings.Add("Text", DataSource, "D30", "");
            c2_31.DataBindings.Add("Text", DataSource, "D31", "");
        }

    }
}
