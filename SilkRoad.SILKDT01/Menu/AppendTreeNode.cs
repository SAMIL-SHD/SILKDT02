using System.Windows.Forms;
using SilkRoad.Config;
using DevExpress.XtraTreeList.Nodes;

namespace SilkRoad.SILKDT01
{
    public class AppendTreeNode
    {
        #region Node Append or Delete
        public string ViewNode(int toolbarType, Control cont)
        {
            string menuCap = string.Empty;
            try
            {
                if (cont is DevExpress.XtraTreeList.TreeList)
                {
                    DeleteNode2TreeList(cont);
                    menuCap = AddNode2TreeList(toolbarType, cont);
                    DevExpress.XtraTreeList.TreeList tl = cont as DevExpress.XtraTreeList.TreeList;
                    tl.ExpandAll();
                }
            }
            catch
            {
                menuCap = string.Empty;
            }
            return menuCap;
        }

        private void DeleteNode2TreeList(Control cont)
        {
            DevExpress.XtraTreeList.TreeList tl = cont as DevExpress.XtraTreeList.TreeList;
            int nodeCnt = tl.Nodes.Count;
            for (int i = nodeCnt - 1; i >= 0; i--)
            {
                tl.DeleteNode(tl.Nodes[i]);
            }
        }

        /// <summary>
        /// 트리메뉴생성
        /// </summary>
        /// <param name="toolbarType"></param>
        /// <param name="cont"></param>
        /// <returns></returns>
        private string AddNode2TreeList(int toolbarType, Control cont)
        {
            DevExpress.XtraTreeList.TreeList tl = cont as DevExpress.XtraTreeList.TreeList;
            string menuName = string.Empty;
            tl.BeginUnboundLoad();
            switch (toolbarType)
            {
                case 0:
                    menuName = "코드관리 ";
                    #region 코드관리
                    TreeListNode parentForRootNodes = null;
					
					TreeListNode rootNode = tl.AppendNode(new object[] { "기초코드", "", "", "" }, parentForRootNodes);
					//if (SRConfig.US_GUBN == "1")
					//{
					//	//tl.AppendNode(new object[] { "사용자부서연결", "DUTY1000.duty1005", "", "1" }, rootNode);
					//	tl.AppendNode(new object[] { "인사기본관리", "DUTY1000.duty1006", "", "1" }, rootNode);
					//}
					tl.AppendNode(new object[] { "인사기본관리", "WAGE1000.duty1006", "", "1" }, rootNode);
					tl.AppendNode(new object[] { "간호사정보관리", "DUTY1000.duty1050", "", "" }, rootNode);
					tl.AppendNode(new object[] { "부서-직원설정", "DUTY1000.duty1011", "", "" }, rootNode);
					tl.AppendNode(new object[] { "근무유형정의", "DUTY1000.duty1030", "", "" }, rootNode);
					tl.AppendNode(new object[] { "휴일설정", "DUTY1000.duty1040", "", "" }, rootNode);

					TreeListNode rootNode2 = tl.AppendNode(new object[] { "급여관리", "", "", "" }, parentForRootNodes);
					tl.AppendNode(new object[] { "고정항목관리", "WAGE1000.wage3220", "", "" }, rootNode2);
					tl.AppendNode(new object[] { "변동항목관리", "WAGE1000.wage3230", "", "" }, rootNode2);
					#endregion
					break;
                case 1:
                    menuName = "Duty관리";
                    #region Duty관리
                    parentForRootNodes = null;
                    rootNode = tl.AppendNode(new object[] { "공지사항관리", "", "" }, parentForRootNodes);
					tl.AppendNode(new object[] { "공지사항관리", "DUTY1000.duty4010", "" }, rootNode);

					rootNode2 = tl.AppendNode(new object[] { "근무관리", "", "", "" }, parentForRootNodes);
					tl.AppendNode(new object[] { "CALL/OT관리", "DUTY1000.duty2020", "", "" }, rootNode2); //OT조회및승인
					//tl.AppendNode(new object[] { "SAVE&OT관리", "DUTY1000.duty2040", "", "" }, rootNode2);
					tl.AppendNode(new object[] { "근무및당직관리", "DUTY1000.duty2060", "", "" }, rootNode2);
					tl.AppendNode(new object[] { "OFF신청조회", "DUTY1000.duty2010", "", "" }, rootNode2);
                    tl.AppendNode(new object[] { "간호사근무관리", "DUTY1000.duty3010", "", "" }, rootNode2);
					tl.AppendNode(new object[] { "간호사근무마감설정", "DUTY1000.duty3020", "", "" }, rootNode2);

					TreeListNode rootNode3 = tl.AppendNode(new object[] { "연차및휴가관리", "", "", "" }, parentForRootNodes);
					tl.AppendNode(new object[] { "연차신청및조회", "DUTY1000.duty8030", "", "" }, rootNode3);
					tl.AppendNode(new object[] { "휴가신청및조회", "DUTY1000.duty8050", "", "" }, rootNode3);
					tl.AppendNode(new object[] { "연차및휴가현황", "DUTY1000.duty8090", "", "" }, rootNode3);
					tl.AppendNode(new object[] { "사원별연차관리", "DUTY1000.duty8020", "", "" }, rootNode3);
					tl.AppendNode(new object[] { "연차휴가사용촉구현황", "DUTY1000.duty8010", "", "" }, rootNode3);
					
					TreeListNode rootNode4 = tl.AppendNode(new object[] { "문서관리", "", "", "" }, parentForRootNodes);
					tl.AppendNode(new object[] { "결재문서관리", "DUTY1000.duty5060", "", "" }, rootNode4);
					//tl.AppendNode(new object[] { "진행문서관리", "DUTY1000.duty5070", "", "" }, rootNode4);
					tl.AppendNode(new object[] { "완결문서관리", "DUTY1000.duty5080", "", "" }, rootNode4);

					#endregion
					break;
                case 2:
                    menuName = "마감관리";
                    #region 마감관리
                    parentForRootNodes = null;
                    rootNode = tl.AppendNode(new object[] { "마감관리", "", "" }, parentForRootNodes);
					tl.AppendNode(new object[] { "근무집계표", "DUTY1000.duty3080", "", "" }, rootNode);
					tl.AppendNode(new object[] { "최종마감관리", "DUTY1000.duty3090", "", "" }, rootNode);
					tl.AppendNode(new object[] { "KT근태연동조회", "DUTY1000.duty5010", "", "" }, rootNode);
					tl.AppendNode(new object[] { "연차정산관리", "DUTY1000.duty3030", "", "" }, rootNode);
					
					rootNode2 = tl.AppendNode(new object[] { "환경설정", "", "", "" }, parentForRootNodes);
					tl.AppendNode(new object[] { "시급관리", "DUTY1000.duty9010", "", "" }, rootNode2);
					tl.AppendNode(new object[] { "당직시간관리", "DUTY1000.duty9030", "", "" }, rootNode2);
					tl.AppendNode(new object[] { "N수당관리", "DUTY1000.duty9040", "", "" }, rootNode2);
					tl.AppendNode(new object[] { "고정OT부서설정", "DUTY1000.duty9050", "", "" }, rootNode2);
					tl.AppendNode(new object[] { "수당산식설정", "DUTY1000.duty9020", "", "" }, rootNode2);

					#endregion
					break;
            }
            tl.EndUnboundLoad();
            string menuCaption = menuName + "항목";
            return menuCaption;
        }
        #endregion
    }
}
