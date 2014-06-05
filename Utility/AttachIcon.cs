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
        /// <summary>
        /// Try to find icon in Dictionary, that attached to the action. If find return it. 
        /// Else try to find icon in Gear working directory -> /Icons/, if find add it to Dictionary and return it. 
        /// Else return null. 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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