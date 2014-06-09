using System.Windows.Forms;

namespace TopTeam.Gear.Model
{
    using System;
    using System.Collections.Generic;

    public class RootAction : Action
    {
        private static int quickNumber = 1;
        public string StartX
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.StartX, out val) ? val : string.Empty;
            }
        }

        public string StartY
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.StartY, out val) ? val : string.Empty;
            }
        }

        public string Selected
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Selected, out val) ? val : string.Empty;
            }
        }
        /// <summary>
        /// Constructor that only call a base constructor
        /// </summary>
        /// <param name="param"></param>
        public RootAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }

        protected override void Execute(object sender, EventArgs e)
        {
        }

        public override ToolStripMenuItem ToMenuItem()
        {
            ToolStripMenuItem item = new ToolStripMenuItem(this.Name);
            return item;
        }
        /// <summary>
        /// Type of action from enum - Root. 
        /// </summary>
        public override ActionType Type
        {
            get
            {
                return ActionType.Root;
            }
        }

        public override int QuickNumber
        {
            get
            { return RootAction.quickNumber++; }
        }
    }
}
