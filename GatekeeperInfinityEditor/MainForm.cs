using System.Diagnostics;
using XFEExtension.NetCore.MemoryEditor;

namespace GatekeeperInfinityEditor;

public partial class MainForm : Form
{
    nint healthBaseAddress = 288;
    nint attackBaseAddress = 288;
    nint yellowStoneBaseAddress = 24;
    nint blueStoneBaseAddress = 24;
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
                Task.Run(() =>
                {
                    while (healthBaseAddress == 288)
                    {
                        healthBaseAddress = CurrentEditor!.ResolvePointerAddress("GameAssembly.dll", 0x03F5E2B0, 0xB8, 0x8, 0x20, 0x118, 0x120);
                    }
                    CurrentEditor!.AddListener<float>(healthBaseAddress, "HP");
                    CurrentEditor!.WriteMemory(healthBaseAddress, float.Parse(textBox1.Text));
                });
            }
            else
            {
                textBox1.Enabled = true;
                CurrentEditor!.RemoveListener(healthBaseAddress);
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
                Task.Run(() =>
                {
                    while (attackBaseAddress == 288)
                    {
                        attackBaseAddress = CurrentEditor!.ResolvePointerAddress("GameAssembly.dll", 0x03F5E2B0, 0xB8, 0x8, 0x20, 0x120, 0x120);
                    }
                    CurrentEditor!.AddListener<float>(attackBaseAddress, "ATK");
                    CurrentEditor!.WriteMemory(attackBaseAddress, float.Parse(textBox2.Text));
                });
            }
            else
            {
                textBox2.Enabled = true;
                CurrentEditor!.RemoveListener(attackBaseAddress);
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
                Task.Run(() =>
                {
                    while (yellowStoneBaseAddress == 24)
                    {
                        yellowStoneBaseAddress = CurrentEditor!.ResolvePointerAddress("GameAssembly.dll", 0x03FE0750, 0xB8, 0x8, 0x18);
                    }
                    CurrentEditor!.AddListener<int>(yellowStoneBaseAddress, "YellowStone");
                    CurrentEditor!.WriteMemory(yellowStoneBaseAddress, int.Parse(textBox3.Text));
                });
            }
            else
            {
                textBox3.Enabled = true;
                CurrentEditor!.RemoveListener(yellowStoneBaseAddress);
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
                    while (blueStoneBaseAddress == 24)
                    {
                        blueStoneBaseAddress = CurrentEditor!.ResolvePointerAddress("GameAssembly.dll", 0x03FEE2B0, 0x50, 0xB8, 0x8, 0x148, 0x18);
                    }
                    CurrentEditor!.AddListener<int>(blueStoneBaseAddress, "BlueStone");
                    CurrentEditor!.WriteMemory(blueStoneBaseAddress, int.Parse(textBox4.Text));
                });
            }
            else
            {
                textBox4.Enabled = true;
                CurrentEditor!.RemoveListener(blueStoneBaseAddress);
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
            blueStoneBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03FEE2B0, 0x50, 0xB8, 0x8, 0x148, 0x18);
            yellowStoneBaseAddress = CurrentEditor.ResolvePointerAddress("GameAssembly.dll", 0x03FE0750, 0xB8, 0x8, 0x18);
            CurrentEditor.ValueChanged += (sender, e) =>
            {
                Trace.WriteLine($"名称：{e.CustomName} 地址：{sender:X}\t值从：{e.PreviousValue}  变更为：{e.CurrentValue}");
                switch (e.CustomName)
                {
                    case "HP":
                        CurrentEditor.WriteMemory(healthBaseAddress, float.Parse(textBox1.Text));
                        break;
                    case "ATK":
                        CurrentEditor.WriteMemory(attackBaseAddress, float.Parse(textBox2.Text));
                        break;
                    case "YellowStone":
                        CurrentEditor.WriteMemory(yellowStoneBaseAddress, int.Parse(textBox3.Text));
                        break;
                    case "BlueStone":
                        CurrentEditor.WriteMemory(blueStoneBaseAddress, int.Parse(textBox4.Text));
                        break;
                    default:
                        break;
                }
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
