using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTeam.Gear.Model
{
    using System.Windows.Forms;
    using System.Xml;
    /// <summary>
    /// Define properties of app
    /// </summary>
    public class GearDefinition
    {
        private int startX= 150;

        private int startY = 150;

        private int selectedIndex = 0;
        /// <summary>
        /// Default constructor
        /// </summary>
        public GearDefinition()
        {
            this.MenuItems = new List<ToolStripItem>();
        }
        /// <summary>
        /// Get/set x-coordinate of ToolStripItem
        /// </summary>
        public int StartX
        {
            get
            {
                return startX;
            }
            set
            {
                startX = value;
            }
        }
        /// <summary>
        /// Get/set y-coordinate of ToolStripItem
        /// </summary>
        public int StartY
        {
            get
            {
                return startY;
            }
            set
            {
                startY = value;
            }
        }
        /// <summary>
        /// Get/set selected item in ToolStripItem. Get 0 if it more than count. 
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (selectedIndex < 0 || selectedIndex >= this.MenuItems.Count)
                    return 0;
                return this.selectedIndex;
            }
            set
            {
                this.selectedIndex = value;
            }
        }
        /// <summary>
        /// Sets startX from string
        /// </summary>
        /// <param name="xS"></param>
        public void SetStartX(string xS)
        {
            int x;
            if (xS.ToLowerInvariant().Trim() == "mouse")
            {
                this.StartX = Cursor.Position.X;
            }
            else if(int.TryParse(xS, out x))
            {
                this.startX = x;
            }
        }
        /// <summary>
        /// Sets startY from string
        /// </summary>
        /// <param name="yS"></param>
        public void SetStartY(string yS)
        {
            int y;
            if (yS.ToLowerInvariant().Trim() == "mouse")
            {
                this.StartY = Cursor.Position.Y;
            }
            else if (int.TryParse(yS, out y))
            {
                this.startY = y;
            }
        }
        /// <summary>
        /// Sets selectedIndex from string
        /// </summary>
        /// <param name="indexS"></param>
        public void SetSelectedIndex(string indexS)
        {
            int i;
            if (int.TryParse(indexS, out i))
            {
                this.SelectedIndex = i;
            }
        }

        public List<ToolStripItem> MenuItems { get; set; }
        
        /// <summary>
        /// Sets startX, startY and selectedIndex from RootNode
        /// </summary>
        /// <param name="root"></param>
        public void InitializeFromRootNode(XmlNode root)
        {
            var action = ActionFactory.GetAction(root) as RootAction;
            if (action != null && action.Type == ActionType.Root)
            {
                this.SetStartX(action.StartX);
                this.SetStartY(action.StartY);
                this.SetSelectedIndex(action.Selected);
            }
        }
    }
}
