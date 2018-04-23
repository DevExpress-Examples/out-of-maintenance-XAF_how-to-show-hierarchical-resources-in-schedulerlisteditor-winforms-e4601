Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Scheduler
Imports DevExpress.ExpressApp.Scheduler.Win
Imports DevExpress.Persistent.Base.General
Imports DevExpress.XtraEditors
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.UI

Namespace WinSolution.Module.Win.Editors
	<ListEditor(GetType(IEvent), True)> _
	Public Class SchedulerListEditorEx
		Inherits SchedulerListEditor

		Private resourcesTree As ResourcesTree
		Private splitter As SplitterControl

		Public Sub New(ByVal model As IModelListView)
			MyBase.New(model)
		End Sub
		Protected Overrides Function CreateControlsCore() As Object
			Dim panel As System.Windows.Forms.Control = TryCast(MyBase.CreateControlsCore(), System.Windows.Forms.Control)
			Dim model As IModelListViewScheduler = TryCast(Me.Model, IModelListViewScheduler)
			If model.ResourceClass IsNot Nothing AndAlso model.ResourceClass.TypeInfo.Implements(Of ITreeResource)() Then
				Me.ResourcesMappings.ParentId = "Parent.Id"
				'this.SchedulerControl.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource;
				Me.SchedulerControl.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Timeline
				AddHandler SchedulerControl.ActiveViewChanged, AddressOf SchedulerControl_ActiveViewChanged
				resourcesTree = New ResourcesTree()
				resourcesTree.Columns.AddField("Name").Visible = True
				resourcesTree.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Never
				resourcesTree.Dock = System.Windows.Forms.DockStyle.Left
				resourcesTree.Width = 150
				resourcesTree.OptionsBehavior.Editable = False
				resourcesTree.KeyFieldName = Me.ResourcesMappings.Id
				resourcesTree.ParentFieldName = Me.ResourcesMappings.ParentId
				resourcesTree.SchedulerControl = Me.SchedulerControl
				splitter = New SplitterControl()
				splitter.Dock = System.Windows.Forms.DockStyle.Left
				panel.Controls.Add(splitter)
				panel.Controls.Add(resourcesTree)
			End If
			Return panel
		End Function
		Private Sub SchedulerControl_ActiveViewChanged(ByVal sender As Object, ByVal e As System.EventArgs)
			Dim scheduler As SchedulerControl = CType(sender, SchedulerControl)
			If splitter Is Nothing OrElse resourcesTree Is Nothing Then Return
			Dim show As Boolean = scheduler.ActiveViewType = SchedulerViewType.Timeline OrElse scheduler.ActiveViewType = SchedulerViewType.Gantt
			splitter.Visible = show
			resourcesTree.Visible = show
		End Sub
	End Class
End Namespace
