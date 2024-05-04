using XFEExtension.NetCore.MemoryEditor.Manager;

namespace GatekeeperInfinityEditor;

internal static class Program
{
    internal static DynamicMemoryManager Manager { get; } = MemoryManager.CreateBuilder()
        .WithAutoReacquireProcess("Gatekeeper Infinity")
        .BuildDynamicManager(
        MemoryItemBuilder.Create<float>("MaxHealth")
                         .WithResolvePointer("GameAssembly.dll", 0x03F5E2B0, 0xB8, 0x8, 0x20, 0x118, 0x120)
                         .WithListener(10, true),

        MemoryItemBuilder.Create<float>("AttackDamage")
                         .WithResolvePointer("GameAssembly.dll", 0x03F5E2B0, 0xB8, 0x8, 0x20, 0x120, 0x120)
                         .WithListener(10, true),

        MemoryItemBuilder.Create<int>("YellowStone")
                         .WithResolvePointer("GameAssembly.dll", 0x03FE0750, 0xB8, 0x8, 0x18)
                         .WithListener(10, true),

        MemoryItemBuilder.Create<int>("BlueStone")
                         .WithResolvePointer("GameAssembly.dll", 0x03FEE2B0, 0x50, 0xB8, 0x8, 0x148, 0x18)
                         .WithListener(10, true)
        );
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
        Manager.CurrentProcessEntered += Manger_CurrentProcessEntered;
        Manager.CurrentProcessExit += Manger_CurrentProcessExit;
        Task.Run(async () => await Manager.WaitProcessEnter());
        Application.Run(mainForm);
    }

    private static void Manger_CurrentProcessExit(object? sender, EventArgs e)
    {
        SystemProfile.IsGameRunning = false;
        MainForm.Current?.Invoke(() => MainForm.Current.Text = "Gatekeeper: Infinity 4项修改器[未启动游戏]");
        MainForm.Current?.RemoveAllCheck();
    }

    private static void Manger_CurrentProcessEntered(object? sender, EventArgs e)
    {
        SystemProfile.IsGameRunning = true;
        MainForm.Current?.Invoke(() => MainForm.Current.Text = "Gatekeeper: Infinity 4项修改器[游戏进行中]");
    }
}