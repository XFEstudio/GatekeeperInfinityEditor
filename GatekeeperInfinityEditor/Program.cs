using System.Diagnostics;

namespace GatekeeperInfinityEditor;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        var mainForm = new MainForm();
        Task.Run(async () =>
        {
            while (true)
            {
                SystemProfile.IsGameRunning = Process.GetProcessesByName("Gatekeeper Infinity").Length > 0;
                if (SystemProfile.IsGameRunning)
                    mainForm.Text = "Gatekeeper: Infinity 4���޸���[��Ϸ������]";
                else
                    mainForm.Text = "Gatekeeper: Infinity 4���޸���[δ������Ϸ]";
                await Task.Delay(500);
            }
        });
        Application.Run(mainForm);
    }
}