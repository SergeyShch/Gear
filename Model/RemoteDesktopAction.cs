using System.Windows.Forms;
using TopTeam.Gear.Utility;

namespace TopTeam.Gear.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class RemoteDesktopAction : Action
    {
        private int quickNumber = 1;
        /// <summary>
        /// Constructor that only call a base constructor
        /// </summary>
        /// <param name="param"></param>
        public RemoteDesktopAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }
        /// <summary>
        /// Readonly property that returns provided adress from definition or empty string
        /// </summary>
        public string Address
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Address, out val) ? val : string.Empty;
            }
        }
        /// <summary>
        /// Readonly property that returns provided user name from definition or empty string
        /// </summary>
        public string UserName
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Username, out val) ? val : string.Empty;
            }
        }
        /// <summary>
        /// Readonly property that returns provided password from definition or empty string
        /// </summary>
        public string Password
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Password, out val) ? val : string.Empty;
            }
        }
        /// <summary>
        /// Overrided execute method, that starts the process cmdkey.exe first with arguments (adress, user name and password) and then starts mstsc.exe with adress. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Type of action from enum - RDC. 
        /// </summary>
        public override ActionType Type
        {
            get
            {
                return ActionType.RDC;
            }
        }

        public override int QuickNumber
        {
            get
            {
                return this.quickNumber++;
            }
           
        }
    }
}
