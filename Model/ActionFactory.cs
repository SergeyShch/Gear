namespace TopTeam.Gear.Model
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Xml;
    using TopTeam.Gear.Utility;
    /// <summary>
    /// Contain static method GetAction(), that returns an Action
    /// </summary>
    public static class ActionFactory
    {
        
        /// <summary>
        /// Static method, that returns an Action
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Action GetAction(XmlNode node)
        {
            if (node == null)
                return null;
            
            ActionType actionType;
            if (!Enum.TryParse(node.Name, true, out actionType))
                return null;
            
            var actionParams = new Dictionary<ActionParam, string>(); 
            if (node.Attributes != null)
            {
                foreach (var attr in node.Attributes)
                {
                    var xmlAttr = attr as XmlAttribute;
                    if (xmlAttr != null)
                    {
                        ActionParam param;
                        if (Enum.TryParse(xmlAttr.Name, true, out param))
                        {
                            actionParams.Add(param, xmlAttr.Value);
                        }
                    }
                }
            }

            Action action;
            
            switch (actionType)
            {
                case ActionType.Website:
                    action = new WebAction(actionParams);
                    break;

                case ActionType.RDC:
                    action = new RemoteDesktopAction(actionParams);
                    break;

                case ActionType.Dir:
                    action = new DirAction(actionParams);
                    break;

                case ActionType.Console:
                    action = new ConsoleAction(actionParams);
                    break;

                case ActionType.Exe:
                    action = new ExeAction(actionParams);
                    break;

                case ActionType.Email:
                    action = new EmailAction(actionParams);
                    break;

                case ActionType.Copy:
                    action = new CopyToClipboardAction(actionParams);
                    break;

                default:
                    action = new RootAction(actionParams);
                    break;
            }

            return action;
        }
    }
}
