namespace TopTeam.Gear.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;


    public class WebAction : Action
    {
        private string Url
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Url, out val) ? val : string.Empty;
            }
        }
        /// <summary>
        /// Constructor that only call a base constructor
        /// </summary>
        /// <param name="param"></param>
        public WebAction(Dictionary<ActionParam, string> param)
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
            Process.Start(this.Url);
        }
        /// <summary>
        /// Type of action from enum - Website. 
        /// </summary>
        public override ActionType Type
        {
            get
            {
                return ActionType.Website;
            }
        }
    }
}
