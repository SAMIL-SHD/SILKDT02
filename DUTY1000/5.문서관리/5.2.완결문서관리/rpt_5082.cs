using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraPrinting;

namespace DUTY1000
{
    public partial class rpt_5082 : DevExpress.XtraReports.UI.XtraReport
    {
        SilkRoad.Common.CustomFormatter cf = new SilkRoad.Common.CustomFormatter();
        SilkRoad.Common.DrawRptSignBox dsb = new SilkRoad.Common.DrawRptSignBox();
        DataTable _signdt;

        public rpt_5082(DataTable signdt, DataSet ds)
        {
            InitializeComponent();
            _signdt = signdt;

            xrCheckBox1.Checked = ds.Tables["5080_REPORT"].Rows[0]["CHK1"].ToString() == "1" ? true : false;
            xrCheckBox2.Checked = ds.Tables["5080_REPORT"].Rows[0]["CHK2"].ToString() == "1" ? true : false;
            xrCheckBox3.Checked = ds.Tables["5080_REPORT"].Rows[0]["CHK3"].ToString() == "1" ? true : false;
        }

        private void pan_sign_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (_signdt != null && _signdt.Rows.Count > 0)
                this.pan_sign.Controls.Add(DT_SignBox(pan_sign, _signdt, "R"));
        }

        public DevExpress.XtraReports.UI.XRTable DT_SignBox(XRPanel panel, DataTable dt, string Location)
        {
            float X = 0;            //x 위치
            float totalWidth = 0f; //결재박스 총 너비
            DataTable signdt;

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            //결재정보dt
            signdt = dt;
            XRTable table = new XRTable();

            try
            {
                //처음 '결재' 부분이있어야하니까.. +1
                int cellsInRow = signdt.Rows.Count + 1;

                //테이블 기본설정
                table.Borders = DevExpress.XtraPrinting.BorderSide.All;
                table.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                table.BeginInit();

                for (int i = 0; i < 3; i++) //행
                {
                    XRTableRow row = new XRTableRow();
                    //row.HeightF = i == 0 ? 25 : 50; //행 높이설정
                    row.HeightF = i == 1 ? 35 : 20; //행 높이설정

                    for (int j = 0; j < cellsInRow; j++) //각 행의 열
                    {
                        XRTableCell cell = new XRTableCell();
                        cell.CanGrow = cell.CanShrink = false;
                        //셀 한칸의 width
                        cell.WidthF = (j == 0 ? 20 : 80); //셀 너비 설정
                        //((float)(80 * Convert.ToDecimal(signdt.Rows[j - 1]["SIGN_SIZE"].ToString() == "" ? "1" : signdt.Rows[j - 1]["SIGN_SIZE"].ToString())))

                        //'결재'글씨들어갈 칸은 보더 없어야 하므로 조정
                        cell.Borders = (j == 0 && i == 0) ? (BorderSide.All) : i == 1 ? (BorderSide.Left | BorderSide.Right)
                            : i == 2 ? (BorderSide.Bottom | BorderSide.Left | BorderSide.Right) : BorderSide.All;
                        //cell.Borders = (j == 0 && i == 0) ? (DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)
                        //    : ((j == 0 && i == 1) ? (DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) : DevExpress.XtraPrinting.BorderSide.All);

                        //i==0일경우..담당, 팀장등/ 1이면 '전결' 등..
                        cell.Text = j == 0 ? (i == 0 ? "결\r\n\r\n재" : "") : signdt.Rows[j - 1][i == 0 ? "LINE_JIWK" : "LINE_STAT"].ToString();
                        cell.BackColor = j == 0 || i == 0 ? Color.Gainsboro : Color.Transparent;
                        cell.RowSpan = j == 0 && i == 0 ? 3 : 1;

                        //첫번째 '결재'글씨들어가는 칸
                        if (j == 0)
                        {
                            cell.Multiline = true;
                            cell.StylePriority.UseTextAlignment = false;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        }
                        //if (j == 0 && i == 1)
                        //{
                        //    //cell.Multiline = true;
                        //    //cell.StylePriority.UseTextAlignment = false;
                        //    //cell.Text = "결\r\n\r\n재";
                        //    cell.HeightF = 50;
                        //    //cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                        //}
                        else if (j != 0 && i == 2)
                        {
                            cell.Text = dt.Rows[j - 1]["AP_DT"].ToString();
                            cell.Font = new Font("Times New Roman", 8F);
                            row.Cells.Add(cell);
                        }

                        row.Cells.Add(cell);
                        totalWidth += (i == 0 ? cell.WidthF : 0);
                    }
                    table.Rows.Add(row);
                }

                //패널이면 
                X = Location.Equals("R") ? (panel.WidthF - (totalWidth * (2.54f))) : (2.54f); //오른쪽인지 왼쪽인지확인.후 위치결정

                table.AdjustSize();
                table.EndInit();
            }
            catch (Exception ec)
            {
                MessageBox.Show("결재라인생성중 에러가 발생했습니다." + ec.Message, "DrawRptSignBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            table.LocationF = new DevExpress.Utils.PointFloat((X / 2.54f), 0F);

            return table;
        }
    }
}
