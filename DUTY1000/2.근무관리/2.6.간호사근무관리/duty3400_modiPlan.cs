using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DUTY1200
{
    public partial class duty3400_modiPlan : Form
    {
        public string _returnVal = "";

        public duty3400_modiPlan()
        {
            InitializeComponent();
        }

        //datarow 전달받아서 화면에 설정 
        public duty3400_modiPlan(string sawon, string date, string req, string planval)
        {
            InitializeComponent();
            lb_sawon.Text = sawon;
            lb_date.Text = date;
            //lb_memo.Text = ;

            //cmb_gtype_fr.EditValue = req;   //신청값 (변경될지 모르니 일단 지우지 않고 숨기기만)
            txt_gtype_fr.Text = req;        //신청값 text로. 신청한 것 수정할 것이 아니니까. 
            cmb_gtype_to.EditValue = planval; //변경(확정)값

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            _returnVal = cmb_gtype_to.EditValue.ToString();
        }
    }
}
