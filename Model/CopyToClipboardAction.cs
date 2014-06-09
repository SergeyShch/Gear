using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace TopTeam.Gear.Model
{
    public class CopyToClipboardAction : Action
    {
        private static int quickNumber = 1;
        private string Text
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Copy, out val) ? val : string.Empty;

            }
        }

        /// <summary>
        /// Constructor that only call a base constructor
        /// </summary>
        /// <param name="param"></param>
        public CopyToClipboardAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }
        /// <summary>
        /// Overrided execute method, that starts the process - opening provided url adress in standart browser. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Execute(object sender, EventArgs e)
        {
            Clipboard.SetText(this.Text);
        }

        /// <summary>
        /// Type of action from enum - Clipboard. 
        /// </summary>
        public override ActionType Type
        {
            get
            {
                return ActionType.Copy;
            }
        }

        public override int QuickNumber
        {
            get
            { return CopyToClipboardAction.quickNumber++; }
            
        }
    }
}
