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
        /// <summary>
        /// Constructor that only call a base constructor
        /// </summary>
        /// <param name="param"></param>
        public ExeAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }
        /// <summary>
        /// Overrided execute method, that starts a process from provided path and optional comand-line args. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Type of action from enum - Exe. 
        /// </summary>
        public override ActionType Type
        {
            get { return ActionType.Exe; }
        }
    }
}
