namespace TopTeam.Gear.Utility
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using TopTeam.Gear.Model;

    public static class AttachIcon
    {
        private static Dictionary<ActionType, Image> standardIcons = new Dictionary<ActionType, Image>();

        public static Image GetStandardIcon(ActionType type)
        {
            if (standardIcons.ContainsKey(type))
                return standardIcons[type];

            Assembly thisExe = Assembly.GetExecutingAssembly();
            Stream file = thisExe.GetManifestResourceStream(
                string.Format("TopTeam.Gear.Icons.{0}.ico", type.ToString()));
            Image image = null;
            if (file != null)
                image = Image.FromStream(file);

            standardIcons.Add(type, image);
            return image;
        }
    }
}