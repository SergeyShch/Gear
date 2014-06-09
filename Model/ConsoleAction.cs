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
        private static int quickNumber = 1;
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
        /// <summary>
        /// Constructor that only call a base constructor
        /// </summary>
        /// <param name="param"></param>
        public ConsoleAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }
        /// <summary>
        /// Overrided execute method, that starts a process cmd.exe with comand-line args. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Execute(object sender, EventArgs e)
        {
            Process.Start("cmd.exe", string.Format("{0} {1}", this.ShouldCloseWindow ? "/C" : "/K", this.Command));
        }
        /// <summary>
        /// Type of action from enum - Console. 
        /// </summary>
        public override ActionType Type
        {
            get
            {
                return ActionType.Console;
            }
        }

        public override int QuickNumber
        {
            get
            {
                if (ConsoleAction.quickNumber < 10) return ConsoleAction.quickNumber++;
                else return 0;
            }
        }
    }
}
