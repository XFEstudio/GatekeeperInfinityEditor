using System.Diagnostics;
using XFEExtension.NetCore.MemoryEditor;

namespace GatekeeperInfinityEditor;

public partial class MainForm : Form
{
    nint healthBaseAddress = 0;
    nint attackBaseAddress = 0;
    nint yellowStoneBaseAddress = 0;
    nint blueStoneBaseAddress = 0;
    public static MemoryEditor? CurrentEditor { get; set; }
    public MainForm()
    {
        InitializeComponent();
        CheckEditorInitialize();
    }

    private void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        CheckEditorInitialize();
        if (sender is CheckBox checkBox)
        {
            if (checkBox.Checked)
            {
                if (!SystemProfile.IsGameRunning)
                {
                    MessageBox.Show("请启动游戏");
                    checkBox.Checked = false;
                    return;
                }
                textBox1.Enabled = false;
                CurrentEditor!.AddListener<float>(() => CurrentEditor?.ResolvePointerAddress("GameAssembly.dll", 0x03F5E2B0, 0xB8, 0x8, 0x20, 0x118, 0x120), 1, "HP");
                healthBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03F5E2B0, 0xB8, 0x8, 0x20, 0x118, 0x120);
                CurrentEditor.WriteMemory(healthBaseAddress, float.Parse(textBox1.Text));
            }
            else
            {
                textBox1.Enabled = true;
                CurrentEditor?.RemoveListener("HP");
            }
        }
    }

    private void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        CheckEditorInitialize();
        if (sender is CheckBox checkBox)
        {
            if (checkBox.Checked)
            {
                if (!SystemProfile.IsGameRunning)
                {
                    MessageBox.Show("请启动游戏");
                    checkBox.Checked = false;
                    return;
                }
                textBox2.Enabled = false;
                CurrentEditor!.AddListener<float>(() => CurrentEditor?.ResolvePointerAddress("GameAssembly.dll", 0x03F5E2B0, 0xB8, 0x8, 0x20, 0x120, 0x120), 1, "ATK");
                attackBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03F5E2B0, 0xB8, 0x8, 0x20, 0x120, 0x120);
                CurrentEditor.WriteMemory(attackBaseAddress, float.Parse(textBox2.Text));
            }
            else
            {
                textBox2.Enabled = true;
                CurrentEditor?.RemoveListener("ATK");
            }
        }
    }

    private void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        CheckEditorInitialize();
        if (sender is CheckBox checkBox)
        {
            if (checkBox.Checked)
            {
                if (!SystemProfile.IsGameRunning)
                {
                    MessageBox.Show("请启动游戏");
                    checkBox.Checked = false;
                    return;
                }
                textBox3.Enabled = false;
                CurrentEditor!.AddListener<int>(() => CurrentEditor?.ResolvePointerAddress("GameAssembly.dll", 0x03FE0750, 0xB8, 0x8, 0x18), 1, "YellowStone");
                yellowStoneBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03FE0750, 0xB8, 0x8, 0x18);
                CurrentEditor.WriteMemory(yellowStoneBaseAddress, int.Parse(textBox3.Text));
            }
            else
            {
                textBox3.Enabled = true;
                CurrentEditor?.RemoveListener("YellowStone");
            }
        }
    }

    private void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        CheckEditorInitialize();
        if (sender is CheckBox checkBox)
        {
            if (checkBox.Checked)
            {
                if (!SystemProfile.IsGameRunning)
                {
                    MessageBox.Show("请启动游戏");
                    checkBox.Checked = false;
                    return;
                }
                textBox4.Enabled = false;
                Task.Run(() =>
                {
                    CurrentEditor!.AddListener<int>(() => CurrentEditor?.ResolvePointerAddress("GameAssembly.dll", 0x03FEE2B0, 0x50, 0xB8, 0x8, 0x148, 0x18), 1, "BlueStone");
                    blueStoneBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03FEE2B0, 0x50, 0xB8, 0x8, 0x148, 0x18);
                    CurrentEditor.WriteMemory(blueStoneBaseAddress, int.Parse(textBox4.Text));
                });
            }
            else
            {
                textBox4.Enabled = true;
                CurrentEditor?.RemoveListener("BlueStone");
            }
        }
    }

    private void CheckEditorInitialize()
    {
        if (CurrentEditor is null && SystemProfile.IsGameRunning)
        {
            CurrentEditor = new("Gatekeeper Infinity");
            healthBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03F5E2B0, 0xB8, 0x8, 0x20, 0x118, 0x120);
            attackBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03F5E2B0, 0xB8, 0x8, 0x20, 0x120, 0x120);
            yellowStoneBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03FE0750, 0xB8, 0x8, 0x18);
            blueStoneBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03FEE2B0, 0x50, 0xB8, 0x8, 0x148, 0x18);
            CurrentEditor.ValueChanged += (sender, e) =>
            {
                Trace.WriteLine($"名称：{e.CustomName} 地址：{sender:X}\t是否读取到值  上次：{e.PreviousValueGetSuccessful}  这次：{e.CurrentValueGetSuccessful}  值从：{e.PreviousValue}  变更为：{e.CurrentValue}");
                switch (e.CustomName)
                {
                    case "HP":
                        if (e.CurrentValueGetSuccessful)
                        {
                            healthBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03F5E2B0, 0xB8, 0x8, 0x20, 0x118, 0x120);
                            CurrentEditor.WriteMemory(healthBaseAddress, float.Parse(textBox1.Text));
                        }
                        break;
                    case "ATK":
                        if (e.CurrentValueGetSuccessful)
                        {
                            attackBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03F5E2B0, 0xB8, 0x8, 0x20, 0x120, 0x120);
                            CurrentEditor.WriteMemory(attackBaseAddress, float.Parse(textBox2.Text));
                        }
                        break;
                    case "YellowStone":
                        if (e.CurrentValueGetSuccessful)
                        {
                            yellowStoneBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03FE0750, 0xB8, 0x8, 0x18);
                            CurrentEditor.WriteMemory(yellowStoneBaseAddress, int.Parse(textBox3.Text));
                        }
                        break;
                    case "BlueStone":
                        if (e.CurrentValueGetSuccessful)
                        {
                            blueStoneBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03FEE2B0, 0x50, 0xB8, 0x8, 0x148, 0x18);
                            CurrentEditor.WriteMemory(blueStoneBaseAddress, int.Parse(textBox4.Text));
                        }
                        break;
                    default:
                        break;
                }
                Trace.WriteLine(healthBaseAddress.ToString("X"));
            };
        }
    }

    public void RemoveAllCheck()
    {
        Invoke(() => checkBox1.Checked = false);
        Invoke(() => checkBox2.Checked = false);
        Invoke(() => checkBox3.Checked = false);
        Invoke(() => checkBox4.Checked = false);
    }
}
