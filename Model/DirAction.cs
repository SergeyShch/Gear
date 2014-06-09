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
        private static int quickNumber = 1;
        string Path
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Path, out val) ? val : string.Empty;
            }
        }
        /// <summary>
        /// Constructor that only call a base constructor
        /// </summary>
        /// <param name="param"></param>
        public DirAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }
        /// <summary>
        /// Overrided execute method, that starts the process that open directory from Path, or Gear working directory if Path was not provided. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Type of action from enum - Dir. 
        /// </summary>
        public override ActionType Type
        {
            get { return ActionType.Dir; }
        }

        public override int QuickNumber
        {
            get
            {
                if (DirAction.quickNumber < 10) return DirAction.quickNumber++;
                else return 0;
            }
        }
    }
}
