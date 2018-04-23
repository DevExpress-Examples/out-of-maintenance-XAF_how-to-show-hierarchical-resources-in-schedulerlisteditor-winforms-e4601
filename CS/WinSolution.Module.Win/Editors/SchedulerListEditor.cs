using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Scheduler;
using DevExpress.ExpressApp.Scheduler.Win;
using DevExpress.Persistent.Base.General;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.UI;

namespace WinSolution.Module.Win.Editors {
    [ListEditor(typeof(IEvent), true)]
    public class SchedulerListEditorEx : SchedulerListEditor {

        private ResourcesTree resourcesTree;
        private SplitterControl splitter;

        public SchedulerListEditorEx(IModelListView model) : base(model) { }
        protected override object CreateControlsCore() {
            System.Windows.Forms.Control panel = base.CreateControlsCore() as System.Windows.Forms.Control;
            IModelListViewScheduler model = this.Model as IModelListViewScheduler;
            if (model.ResourceClass != null && model.ResourceClass.TypeInfo.Implements<ITreeResource>()) {
                this.ResourcesMappings.ParentId = "Parent.Id";
                //this.SchedulerControl.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource;
                this.SchedulerControl.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Timeline;
                SchedulerControl.ActiveViewChanged += new System.EventHandler(SchedulerControl_ActiveViewChanged);
                resourcesTree = new ResourcesTree();
                resourcesTree.Columns.AddField("Name").Visible = true;
                resourcesTree.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Never;
                resourcesTree.Dock = System.Windows.Forms.DockStyle.Left;
                resourcesTree.Width = 150;
                resourcesTree.OptionsBehavior.Editable = false;
                resourcesTree.KeyFieldName = this.ResourcesMappings.Id;
                resourcesTree.ParentFieldName = this.ResourcesMappings.ParentId;
                resourcesTree.SchedulerControl = this.SchedulerControl;
                splitter = new SplitterControl();
                splitter.Dock = System.Windows.Forms.DockStyle.Left;
                panel.Controls.Add(splitter);
                panel.Controls.Add(resourcesTree);
            }
            return panel;
        }
        void SchedulerControl_ActiveViewChanged(object sender, System.EventArgs e) {
            SchedulerControl scheduler = (SchedulerControl)sender;
            if (splitter == null || resourcesTree == null) return;
            bool show = scheduler.ActiveViewType == SchedulerViewType.Timeline || scheduler.ActiveViewType == SchedulerViewType.Gantt;
            splitter.Visible = show;
            resourcesTree.Visible = show;
        }
    }
}
