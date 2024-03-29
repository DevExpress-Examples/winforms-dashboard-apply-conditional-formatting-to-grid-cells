﻿Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.DashboardCommon
Imports DevExpress.XtraEditors

Namespace Grid_FormatRules
	Partial Public Class DateOccurringForm
		Inherits XtraForm
		Public Sub New()
			InitializeComponent()
			AddHandler dashboardViewer1.AsyncDataLoading, AddressOf OnDashboardViewerAsyncDataLoading
			dashboardViewer1.DataSourceOptions.ObjectDataSourceLoadingBehavior = DevExpress.DataAccess.DocumentLoadingBehavior.LoadAsIs

			Dim dashboard As New Dashboard()
			dashboard.LoadFromXml("..\..\Data\DashboardDate.xml")
			dashboardViewer1.Dashboard = dashboard
			Dim grid As GridDashboardItem = CType(dashboard.Items("gridDashboardItem1"), GridDashboardItem)
			Dim [date] As GridDimensionColumn = CType(grid.Columns(0), GridDimensionColumn)

			Dim dateOccurringRule As New GridItemFormatRule([date])
			Dim dateOccurringCondition As New FormatConditionDateOccurring()
			dateOccurringCondition.DateType = FilterDateType.MonthAgo1 Or FilterDateType.MonthAgo2
			dateOccurringCondition.StyleSettings = New AppearanceSettings(FormatConditionAppearanceType.PaleOrange)
			dateOccurringRule.Condition = dateOccurringCondition
			dateOccurringRule.ApplyToRow = True

			grid.FormatRules.Add(dateOccurringRule)
		End Sub
		Private Sub OnDashboardViewerAsyncDataLoading(ByVal sender As Object, ByVal e As DataLoadingEventArgs)
			e.Data = DataGenerator.CreateData()
		End Sub
	End Class
End Namespace
