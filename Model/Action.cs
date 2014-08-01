using TopTeam.Gear.Utility;

namespace TopTeam.Gear.Model
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using TopTeam.Gear.Model;
    /// <summary>
    /// Base abstract class for all actions
    /// </summary>
    public abstract class Action
    {
        public abstract int QuickNumber { get; set; }
        /// <summary>
        /// Abstract readonly field from enum ActionType
        /// </summary>
        public abstract ActionType Type { get; }
        /// <summary>
        /// Readonly prop, that return name from Dictionary Params or return empty string
        /// </summary>
        
        public string Name
        {
            get
            {
                if (Settings.NumControlEnable)
                {
                    if (this.QuickNumber == 0)
                    {
                        return this.Params.ContainsKey(ActionParam.Name) ? this.Params[ActionParam.Name] : string.Empty;
                    }
                    return string.Format("{0}. {1}", this.QuickNumber++, this.Params.ContainsKey(ActionParam.Name) ? this.Params[ActionParam.Name] : string.Empty);
                }
                return this.Params.ContainsKey(ActionParam.Name) ? this.Params[ActionParam.Name] : string.Empty;
            }
        }
        /// <summary>
        /// Public Dictionary(enum, string) prop. 
        /// </summary>
        public Dictionary<ActionParam, string> Params { get; set; }

        //ctor
        public Action(Dictionary<ActionParam, string> param)
        {
            this.Params = param;
        }
        
        /// <summary>
        /// Main abstract method of actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void Execute(object sender, EventArgs e);
        /// <summary>
        /// Try to call Execute(). Finally call TurnOff() to close the app.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Initialize a new ToolStripMenuItem, add Click EventHandler and attach an icon. Can be overridden.
        /// </summary>
        /// <returns></returns>
        public virtual ToolStripMenuItem ToMenuItem()
        {
            var item = new ToolStripMenuItem(this.Name);
            item.Click += this.ExecuteSafe;
            item.Image = this.Icon;

            return item;
        }
        /// <summary>
        /// Readonly prop, that return icon with the same name of a type. Can be overridden. 
        /// </summary>
        protected virtual Image Icon 
        {
            get
            {
                return AttachIcon.GetStandardIcon(this.Type);
            }
        }
    }
}