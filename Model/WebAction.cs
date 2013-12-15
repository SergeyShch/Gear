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

        public WebAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }

        protected override void Execute(object sender, EventArgs e)
        {
            Process.Start(this.Url);
        }

        public override ActionType Type
        {
            get
            {
                return ActionType.Website;
            }
        }
    }
}
