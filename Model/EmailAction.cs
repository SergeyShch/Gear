using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTeam.Gear.Model
{
    using System.Diagnostics;

    internal class EmailAction : Action
    {
        private string Subject
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Subject, out val) ? val : string.Empty;
            }
        }
        private string To
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.To, out val) ? val : string.Empty;
            }
        }

        private string Cc
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Cc, out val) ? val : string.Empty;
            }
        }

        private string Bcc
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Bcc, out val) ? val : string.Empty;
            }
        }

        private string Body
        {
            get
            {
                string val;
                return this.Params.TryGetValue(ActionParam.Body, out val) ? val : string.Empty;
            }
        }

        public EmailAction(Dictionary<ActionParam, string> param)
            : base(param)
        {
        }

        protected override void Execute(object sender, EventArgs e)
        {
            string mail = "mailto:" + this.To;

            if (!string.IsNullOrWhiteSpace(this.Subject) || !string.IsNullOrWhiteSpace(this.Cc)
                || !string.IsNullOrWhiteSpace(this.Bcc))
            {
            }

            bool first = true;

            if (!string.IsNullOrWhiteSpace(this.Subject))
            {
                mail += (first ? "?" : "&") + "Subject=" + this.Subject;
                first = false;
            }

            if (!string.IsNullOrWhiteSpace(this.Cc))
            {
                mail += (first ? "?" : "&") + "CC=" + this.Cc;
                first = false;
            }

            if (!string.IsNullOrWhiteSpace(this.Bcc))
            {
                mail += (first ? "?" : "&") + "BCC=" + this.Bcc;
                first = false;
            }

            if (!string.IsNullOrWhiteSpace(this.Body))
            {
                mail += (first ? "?" : "&") + "BODY=" + this.Body;
                first = false;
            }

            Process.Start(mail);
        }

        public override ActionType Type
        {
            get { return ActionType.Email; }
        }
    }
}
