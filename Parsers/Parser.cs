using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace TopTeam.Gear.Parsers
{
    using TopTeam.Gear.Model;

    public static class Parser
    {
        /// <summary>
        /// Trying to read configs from working directory, then from comand line arguments. 
        /// </summary>
        /// <returns></returns>
        public static GearDefinition ReadConfigs()
        {
            var gear = new GearDefinition();

            // Trying to get configs from working directory (*.gear) first.

            var standardConfigPaths = new string[0];
            try
            {
                standardConfigPaths = Directory.GetFiles(
                    Directory.GetCurrentDirectory(), "*.gear", SearchOption.TopDirectoryOnly);
            }
            catch
            {
            }

            foreach (var path in standardConfigPaths)
            {
                ReadConfigFromFile(path, gear);
            }

            // Getting configs from command line arguments.
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                gear = new GearDefinition();
                for (int i = 1; i < args.Length; i++)
                {
                    ReadConfigFromFile(args[i], gear);
                }
            }

            if (gear.MenuItems.Count == 0)
            {
                MessageBox.Show(
                    "No Gear config was found. Please place config (with 'gear' extension) in target folder or pass path(s) to configs via command line args.");
            }

            return gear;
        }

        private static void ReadConfigFromFile(string path, GearDefinition gear)
        {
            var items = new List<ToolStripItem>();
            try
            {
                string xml = File.ReadAllText(path);
                var doc = new XmlDocument();
                doc.LoadXml(xml);

                gear.InitializeFromRootNode(doc.FirstChild);

                foreach (var child in doc.FirstChild.ChildNodes)
                {
                    XmlNode node = child as XmlNode;
                    if (node != null)
                    {
                        ToolStripMenuItem item = Parse(node);
                        if (item != null)
                            gear.MenuItems.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Unable to parse config [{0}]{1}Error: {2}", path, Environment.NewLine, ex.Message));
            }
        }

        private static ToolStripMenuItem Parse(XmlNode node)
        {
            ToolStripMenuItem item = ActionFactory.GetAction(node).ToMenuItem();

            if (item == null)
                return null;

            // Add folder items.
            if (node.Name.ToLowerInvariant() == "root")
            {
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    var subMenuItem = Parse(subNode);

                    if (subMenuItem != null)
                        item.DropDown.Items.Add(subMenuItem);
                }

                return item;
            }

            return item;
        }
    }
}
