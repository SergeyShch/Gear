using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TopTeam.Gear.Utility;

namespace TopTeam.Gear.Model
{
    using System;
    using System.Diagnostics;

    public class ConsoleAction : Action
    {
        string Command
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Command, out val) ? val : string.Empty;
            }
        }

        bool ShouldCloseWindow
        {
            get
            {
                string val;
                if (this.Params.TryGetValue(ActionParam.Close, out val))
                {
                    val = val.ToLowerInvariant().Trim();
                    if (val == "yes" || val == "1" || val == "true")
                        return true;

                }
                return false;
            }
        }

        public ConsoleAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }

        protected override void Execute(object sender, EventArgs e)
        {
            Process.Start("cmd.exe", string.Format("{0} {1}", this.ShouldCloseWindow ? "/C" : "/K", this.Command));
        }

        public override ActionType Type
        {
            get
            {
                return ActionType.Console;
            }
        }
    }
}
