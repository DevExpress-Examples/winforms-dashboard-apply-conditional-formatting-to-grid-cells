﻿using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraEditors;

namespace Grid_FormatRules {
    public partial class RangeColorBarConditionForm : XtraForm {
        public RangeColorBarConditionForm() {
            InitializeComponent();
            dashboardViewer1.CustomizeDashboardTitle += DashboardViewer1_CustomizeDashboardTitle;
            Dashboard dashboard = new Dashboard(); 
            dashboard.LoadFromXml(@"..\..\Data\Dashboard.xml");
            dashboardViewer1.Dashboard = dashboard;
            GridDashboardItem grid = (GridDashboardItem)dashboard.Items["gridDashboardItem1"];
            GridMeasureColumn extendedPrice = (GridMeasureColumn)grid.Columns[1];

            GridItemFormatRule rangeRule = new GridItemFormatRule(extendedPrice);
            FormatConditionColorRangeBar rangeBarCondition =
                new FormatConditionColorRangeBar(FormatConditionRangeSetPredefinedType.ColorsRedGreenBlue);
            rangeBarCondition.BarOptions.ShowBarOnly = true;
            rangeRule.Condition = rangeBarCondition;

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
            FormatConditionColorRangeBar rangeBarCondition = (FormatConditionColorRangeBar)rangeRule.Condition;
            RangeInfo range3 = rangeBarCondition.RangeSet[2];
            range3.Value = 50;
            range3.StyleSettings =
                new BarStyleSettings(FormatConditionAppearanceType.GradientGreen);

            RangeInfo range4 = new RangeInfo();
            range4.Value = 75;
            range4.StyleSettings =
                new BarStyleSettings(FormatConditionAppearanceType.GradientBlue);
            rangeBarCondition.RangeSet.Add(range4);

            rangeRule.Condition = rangeBarCondition;
        }
    }
}
