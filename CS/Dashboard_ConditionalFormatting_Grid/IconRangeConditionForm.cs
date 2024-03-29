﻿using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraEditors;

namespace Grid_FormatRules {
    public partial class IconRangeConditionForm : XtraForm {
        public IconRangeConditionForm() {
            InitializeComponent();
            dashboardViewer1.CustomizeDashboardTitle += DashboardViewer1_CustomizeDashboardTitle;
            Dashboard dashboard = new Dashboard(); dashboard.LoadFromXml(@"..\..\Data\Dashboard.xml");
            dashboardViewer1.Dashboard = dashboard;
            GridDashboardItem grid = (GridDashboardItem)dashboard.Items["gridDashboardItem1"];
            GridMeasureColumn extendedPrice = (GridMeasureColumn)grid.Columns[1];

            GridItemFormatRule rangeRule = new GridItemFormatRule(extendedPrice);
            FormatConditionRangeSet rangeCondition = 
                new FormatConditionRangeSet(FormatConditionRangeSetPredefinedType.PositiveNegative3);
            rangeRule.Condition = rangeCondition;

            grid.FormatRules.AddRange(rangeRule);
        }

        private void DashboardViewer1_CustomizeDashboardTitle(object sender, CustomizeDashboardTitleEventArgs e)
        {
            DashboardToolbarItem itemUpdate = new DashboardToolbarItem((args) => UpdateFormatting()) {
                Caption = "Update Format Rule",
            };
            e.Items.Add(itemUpdate);
        }

        private void UpdateFormatting() {
            GridDashboardItem grid = 
                (GridDashboardItem)dashboardViewer1.Dashboard.Items["gridDashboardItem1"];
            GridItemFormatRule rangeRule = grid.FormatRules[0];
            FormatConditionRangeSet rangeCondition = (FormatConditionRangeSet)rangeRule.Condition;
            RangeInfo range3 = rangeCondition.RangeSet[2];
            range3.Value = 50;
            range3.StyleSettings = 
                new IconSettings(FormatConditionIconType.DirectionalYellowUpInclineArrow);

            RangeInfo range4 = new RangeInfo();
            range4.Value = 75;
            range4.StyleSettings = 
                new IconSettings(FormatConditionIconType.IndicatorCircledGreenCheck);
            rangeCondition.RangeSet.Add(range4);

            rangeRule.Condition = rangeCondition;
        }
    }
}
