using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraGrid.Localization;
using System.IO;
using System.Text;

namespace SilkRoad.SILKDT02
{
    public partial class QRcode : SilkRoad.Form.Base.FormX
    {
        public frmMain frmMain;

        public QRcode()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        #endregion

        #region 1 Form

        private void QRcode_Load(object sender, EventArgs e)
        {
        }

		#endregion

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
					}
	}
}
