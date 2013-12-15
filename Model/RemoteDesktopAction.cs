using System.Windows.Forms;
using TopTeam.Gear.Utility;

namespace TopTeam.Gear.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class RemoteDesktopAction : Action
    {
        public RemoteDesktopAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }

        public string Address
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Address, out val) ? val : string.Empty;
            }
        }

        public string UserName
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Username, out val) ? val : string.Empty;
            }
        }

        public string Password
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Password, out val) ? val : string.Empty;
            }
        }

        protected override void Execute(object sender, EventArgs e)
        {
            var rdcProcess = new Process();
            rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\cmdkey.exe");
            rdcProcess.StartInfo.Arguments = "/generic:TERMSRV/" + this.Address + " /user:" + this.UserName + " /pass:" + this.Password;
            rdcProcess.Start();

            rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
            rdcProcess.StartInfo.Arguments = "/v " + this.Address; // ip or name of computer to connect
            rdcProcess.Start();
        }

        public override ActionType Type
        {
            get
            {
                return ActionType.RDC;
            }
        }
    }
}
