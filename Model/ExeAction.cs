using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TopTeam.Gear.Utility;

namespace TopTeam.Gear.Model
{
    using System;
    using System.Diagnostics;

    public class ExeAction : Action
    {
        private string Path
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Path, out val) ? val : string.Empty;
            }
        }

        private string Args
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Args, out val) ? val : string.Empty;
            }
        }

        public ExeAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }

        protected override void Execute(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Args))
            {
                Process.Start(this.Path, this.Args);
            }
            else
            {
                Process.Start(this.Path);
            }
        }

        public override ActionType Type
        {
            get { return ActionType.Exe; }
        }
    }
}
