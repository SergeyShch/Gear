namespace TopTeam.Gear
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using TopTeam.Gear.Parsers;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            

            var menuStrip = new ContextMenuStrip();
            
            var gear = Parser.ReadConfigs();
            if (gear == null || gear.MenuItems == null || gear.MenuItems.Count == 0)
            {
                TurnOffAsync();
                return;                                                                       
            }

            menuStrip.Items.AddRange(gear.MenuItems.ToArray());
            menuStrip.Closed += MenuStripClosed;
            
            menuStrip.Show(gear.StartX, gear.StartY);
            menuStrip.Focus();
            menuStrip.Items[gear.SelectedIndex].Select();

            Application.Run();
        }

        static void MenuStripClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            TurnOffAsync();
        }
        /// <summary>
        /// Turn off the application
        /// </summary>
        public static void TurnOff()
        {
            Application.Exit();
        }

        private static void TurnOffAsync()
        {
            Task.Run(
                async () =>
                {
                    await Task.Delay(150);
                    if (!HandledTurnOFf)
                    {
                        TurnOff();
                    }
                });
        }
        /// <summary>
        /// Used to turn off application if there is no any action is active (for example if pressed alt+tab)
        /// </summary>
        public static bool HandledTurnOFf = false;
    }
}
