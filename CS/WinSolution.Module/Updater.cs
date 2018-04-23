using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using System.Collections.Generic;

namespace WinSolution.Module.DatabaseUpdate {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            if (ObjectSpace.FindObject<HierarchicalResource>(null) == null) {
                List<HierarchicalResource> list = new List<HierarchicalResource>();
                int num = 12;
                Random rnd = new Random();
                for (int i = 0; i < num; i++) {
                    HierarchicalResource r = ObjectSpace.CreateObject<HierarchicalResource>();
                    r.Name = string.Format("Resource{0}", i);
                    r.Color = GetColor(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble());
                    if (list.Count > 0) {
                        r.Parent = list[rnd.Next(list.Count)];
                    }
                    list.Add(r);
                }
            }
        }
        int GetColor(double r, double g, double b) {
            double l = (r + g + b) / 3;
            int mag = 127, off = 128;
            return System.Drawing.Color.FromArgb(NormColor(r * l, mag, off), NormColor(g * l, mag, off), NormColor(b * l, mag, off)).ToArgb();
        }
        int NormColor(double i, int mag, int off) { return (int)(i * mag + off); }
    }
}
