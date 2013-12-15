using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTeam.Gear.Model
{
    using System.Windows.Forms;
    using System.Xml;

    public class GearDefinition
    {
        private int startX= 150;

        private int startY = 150;

        private int selectedIndex = 0;

        public GearDefinition()
        {
            this.MenuItems = new List<ToolStripItem>();
        }

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

        public void SetSelectedIndex(string indexS)
        {
            int i;
            if (int.TryParse(indexS, out i))
            {
                this.SelectedIndex = i;
            }
        }

        public List<ToolStripItem> MenuItems { get; set; }

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
