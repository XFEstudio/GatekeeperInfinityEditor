using System.Diagnostics;
using XFEExtension.NetCore.CodeExtension;

namespace GatekeeperInfinityEditor;

public partial class MainForm : Form
{
    public static MainForm? Current { get; set; }
    public MainForm()
    {
        InitializeComponent();
        Current = this;
        Program.Manager.ValueChanged += Manger_ValueChanged;
    }

    private void Manger_ValueChanged(XFEExtension.NetCore.MemoryEditor.Manager.MemoryItem sender, XFEExtension.NetCore.MemoryEditor.MemoryValue e)
    {
        Trace.WriteLine($"标识名：{e.CustomName} 上一次是否读到值：{e.PreviousValueGetSuccessful} 上一次的值：{e.PreviousValue} 这次是否读到值：{e.CurrentValueGetSuccessful} 这次的值：{e.CurrentValue}");
        switch (e.CustomName)
        {
            case "MaxHealth":
                if (e.CurrentValueGetSuccessful && checkBox1.Checked)
                {
                    if (!Program.Manager["MaxHealth"].Write(float.Parse(textBox1.Text)))
                        Trace.WriteLine($"{e.CustomName} 写入失败");
                }
                break;
            case "AttackDamage":
                if (e.CurrentValueGetSuccessful && checkBox2.Checked)
                {
                    if (!Program.Manager["AttackDamage"].Write(float.Parse(textBox2.Text)))
                        Trace.WriteLine($"{e.CustomName} 写入失败");
                }
                break;
            case "YellowStone":
                if (e.CurrentValueGetSuccessful && checkBox3.Checked)
                {
                    if (!Program.Manager["YellowStone"].Write(int.Parse(textBox3.Text)))
                        Trace.WriteLine($"{e.CustomName} 写入失败");
                }
                break;
            case "BlueStone":
                if (e.CurrentValueGetSuccessful && checkBox4.Checked)
                {
                    if (!Program.Manager["BlueStone"].Write(int.Parse(textBox4.Text)))
                        Trace.WriteLine($"{e.CustomName} 写入失败");
                }
                break;
            default:
                break;
        }
    }

    private async void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is CheckBox checkBox)
        {
            if (checkBox.Checked)
            {
                if (CheckGameStart())
                {
                    textBox1.Enabled = false;
                    await Task.Delay(5);
                    Program.Manager["MaxHealth"].Write(float.Parse(textBox1.Text));
                }
                else
                {
                    checkBox.Checked = false;
                }
            }
            else
            {
                textBox1.Enabled = true;
            }
        }
    }

    private async void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is CheckBox checkBox)
        {
            if (checkBox.Checked)
            {
                if (CheckGameStart())
                {
                    textBox2.Enabled = false;
                    await Task.Delay(5);
                    Program.Manager["AttackDamage"].Write(float.Parse(textBox2.Text));
                }
                else
                {
                    checkBox.Checked = false;
                }
            }
            else
            {
                textBox2.Enabled = true;
            }
        }
    }

    private async void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is CheckBox checkBox)
        {
            if (checkBox.Checked)
            {
                if (CheckGameStart())
                {
                    textBox3.Enabled = false;
                    await Task.Delay(5);
                    Program.Manager["YellowStone"].Write(int.Parse(textBox3.Text));
                }
                else
                {
                    checkBox.Checked = false;
                }
            }
            else
            {
                textBox3.Enabled = true;
            }
        }
    }

    private async void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is CheckBox checkBox)
        {
            if (checkBox.Checked)
            {
                if (CheckGameStart())
                {
                    textBox4.Enabled = false;
                    await Task.Delay(5);
                    Program.Manager["BlueStone"].Write(int.Parse(textBox4.Text));
                }
                else
                {
                    checkBox.Checked = false;
                }
            }
            else
            {
                textBox4.Enabled = true;
            }
        }
    }

    private static bool CheckGameStart()
    {
        if (!SystemProfile.IsGameRunning)
        {
            MessageBox.Show("请启动游戏");
            return false;
        }
        return true;
    }

    public void RemoveAllCheck()
    {
        Invoke(() => checkBox1.Checked = false);
        Invoke(() => checkBox2.Checked = false);
        Invoke(() => checkBox3.Checked = false);
        Invoke(() => checkBox4.Checked = false);
    }
}
