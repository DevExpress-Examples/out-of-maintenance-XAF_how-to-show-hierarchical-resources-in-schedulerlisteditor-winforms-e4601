using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace WinSolution.Module {

    [NavigationItem]
    public class HierarchicalResource : BaseObject, ITreeNode, IResource, ITreeResource {

        private string _Name;
        private HierarchicalResource _Parent;
        private int _Color;

        public HierarchicalResource(Session s) : base(s) { }

        public string Name { get { return _Name; } set { SetPropertyValue("Name", ref _Name, value); } }
        [Association]
        public HierarchicalResource Parent { get { return _Parent; } set { SetPropertyValue("Parent", ref _Parent, value); } }
        [Association]
        public XPCollection<HierarchicalResource> Children { get { return GetCollection<HierarchicalResource>("Children"); } }
        public int Color { get { return _Color; } set { SetPropertyValue("Color", ref _Color, value); } }


        System.ComponentModel.IBindingList ITreeNode.Children { get { return Children; } }
        string ITreeNode.Name { get { return Name; } }
        ITreeNode ITreeNode.Parent { get { return Parent; } }

        [Browsable(false), NonPersistent]
        public string Caption { get { return Name; } set { Name = value; } }
        [Browsable(false)]
        public object Id { get { return Oid; } }
        [Browsable(false)]
        public int OleColor { get { return System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(Color)); } }
    }
}
