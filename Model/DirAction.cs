using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TopTeam.Gear.Utility;

namespace TopTeam.Gear.Model
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public class  DirAction : Action
    {
        string Path
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Path, out val) ? val : string.Empty;
            }
        }

        public DirAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }

        protected override void Execute(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.Path))
            {
                // If path was not provided - open Gear working directory.
                Process.Start(Directory.GetCurrentDirectory());
            }
            else
            {
                Process.Start(this.Path);
            }
        }

        public override ActionType Type
        {
            get { return ActionType.Dir; }
        }
    }
}
