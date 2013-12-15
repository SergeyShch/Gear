using TopTeam.Gear.Utility;

namespace TopTeam.Gear.Model
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using TopTeam.Gear.Model;

    public abstract class Action
    {
        public abstract ActionType Type { get; }

        public string Name
        {
            get
            {
                return this.Params.ContainsKey(ActionParam.Name) ? this.Params[ActionParam.Name] : string.Empty;
            }
        }

        public Dictionary<ActionParam, string> Params { get; set; }

        public Action(Dictionary<ActionParam, string> param)
        {
            this.Params = param;
        }

        protected abstract void Execute(object sender, EventArgs e);

        private void ExecuteSafe(object sender, EventArgs e)
        {
            try
            {
                Program.HandledTurnOFf = true;
                this.Execute(sender, e);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    string.Format(
                        "Error while executing action [{0}] of type [{1}]{2}{3}",
                        this.Name,
                        this.Type,
                        Environment.NewLine,
                        exception.Message));
            }
            finally
            {
                Program.TurnOff();
            }
        }

        public virtual ToolStripMenuItem ToMenuItem()
        {
            var item = new ToolStripMenuItem(this.Name);
            item.Click += this.ExecuteSafe;
            item.Image = this.Icon;

            return item;
        }

        protected virtual Image Icon 
        {
            get
            {
                return AttachIcon.GetStandardIcon(this.Type);
            }
        }
    }
}
