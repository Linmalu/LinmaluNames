using LinmaluNames.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LinmaluNames
{
    public partial class Names : Form
    {
        private const float version = 0.1F;

        public Names()
        {
            InitializeComponent();
            Text += "_v" + version;
            FileImage.Images.Add(Resources.folder);
            checkFile();
        }

        private void Names_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.A:
                    if(e.Control)
                    {
                        foreach(ListViewItem item in FileList.Items)
                        {
                            item.Selected = true;
                        }
                    }
                    break;
                case Keys.Delete:
                    foreach(ListViewItem item in FileList.SelectedItems)
                    {
                        FileList.Items.Remove(item);
                    }
                    checkFile();
                    break;
            }
        }

        private void Names_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Array.Sort(files);
            foreach (string s in files)
            {
                bool exists = false;
                foreach (ListViewItem items in FileList.Items)
                {
                    if (s == (items.SubItems[3].Text + "\\" + items.SubItems[1].Text).Replace("\\\\", "\\"))
                    {
                        exists = true;
                    }
                }
                if (exists)
                {
                    continue;
                }
                FileInfo file = new FileInfo(s);
                ListViewItem item = new ListViewItem();
                if (file.Exists)
                {
                    item.SubItems.Add(file.Name);
                    item.SubItems.Add(file.Name);
                    item.SubItems.Add(file.DirectoryName);
                    item.ImageIndex = FileImage.Images.Count;
                    FileImage.Images.Add(Icon.ExtractAssociatedIcon(s));
                }
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(s);
                    item.SubItems.Add(dir.Name);
                    item.SubItems.Add(dir.Name);
                    item.SubItems.Add(dir.Parent.FullName);
                    item.ImageIndex = 0;
                }
                FileList.Items.Add(item);
            }
            checkFile();
        }

        private void Names_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in FileList.Items)
            {
                string name = (item.SubItems[3].Text + "\\" + item.SubItems[1].Text).Replace("\\\\", "\\");
                string subname = (item.SubItems[3].Text + "\\" + item.SubItems[2].Text).Replace("\\\\", "\\");
                if (name != subname)
                {
                    if ((item.ImageIndex != 0 && !File.Exists(name)) || (item.ImageIndex == 0 && !Directory.Exists(name)))
                    {
                        MessageBox.Show(name + "파일이 존재하지 않습니다.");
                        return;
                    }
                    if ((item.ImageIndex != 0 && name != subname && File.Exists(subname)) || (item.ImageIndex == 0 && Directory.Exists(subname)))
                    {
                        MessageBox.Show(subname + "파일은 이미 존재합니다.");
                        return;
                    }
                }
            }
            List<ListViewItem> items = new List<ListViewItem>();
            try
            {
                foreach (ListViewItem item in FileList.Items)
                {
                    string name = (item.SubItems[3].Text + "\\" + item.SubItems[1].Text).Replace("\\\\", "\\");
                    string subname = (item.SubItems[3].Text + "\\" + item.SubItems[2].Text).Replace("\\\\", "\\");
                    if (name != subname)
                    {
                        if (item.ImageIndex != 0)
                        {
                            File.Move(name, subname);
                        }
                        else
                        {
                            Directory.Move(name, subname);
                        }
                        items.Add(item);
                        string name1 = item.SubItems[1].Text;
                        string name2 = item.SubItems[2].Text;
                        item.SubItems[1].Text = name2;
                        item.SubItems[2].Text = name1;
                    }
                }
            }
            catch
            {
                foreach(ListViewItem item in items)
                {
                    string name = (item.SubItems[3].Text + "\\" + item.SubItems[1].Text).Replace("\\\\", "\\");
                    string subname = (item.SubItems[3].Text + "\\" + item.SubItems[2].Text).Replace("\\\\", "\\");
                    if (name != subname)
                    {
                        if (item.ImageIndex != 0)
                        {
                            File.Move(subname, name);
                        }
                        else
                        {
                            Directory.Move(subname, name);
                        }
                        string name1 = item.SubItems[1].Text;
                        string name2 = item.SubItems[2].Text;
                        item.SubItems[1].Text = name2;
                        item.SubItems[2].Text = name1;
                    }
                }
                MessageBox.Show("이름을 변경할 수 없습니다.");
            }
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in FileList.Items)
            {
                item.SubItems[2].Text = item.SubItems[1].Text;
            }
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in FileList.Items)
            {
                string name = item.SubItems[1].Text;
                int size = name.LastIndexOf(".");
                item.SubItems[2].Text = size == -1 ? "" : name.Substring(size);
            }
        }

        private void Btn4_Click(object sender, EventArgs e)
        {
            new SubNames(this, 4).ShowDialog();
        }

        private void Btn5_Click(object sender, EventArgs e)
        {
            new SubNames(this, 5).ShowDialog();
        }

        private void Btn6_Click(object sender, EventArgs e)
        {
            new SubNames(this, 6).ShowDialog();
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            FileList.Items.Clear();
            FileImage.Images.Clear();
            FileImage.Images.Add(Resources.folder);
            checkFile();
        }

        private void Btn8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("준비중입니다.");
        }

        private void Menu_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkFile()
        {
            bool enabled = FileList.Items.Count == 0 ? false : true;
            foreach (Control item in Controls)
            {
                if (item.GetType() == typeof(Button))
                {
                    item.Enabled = enabled;
                }
            }
        }
        public void addPrefixName(string name)
        {
            foreach (ListViewItem item in FileList.Items)
            {
                item.SubItems[2].Text = name + item.SubItems[2].Text;
            }
        }
        public void addSuffixName(string name)
        {
            foreach (ListViewItem item in FileList.Items)
            {
                string subname = item.SubItems[2].Text;
                int size = subname.LastIndexOf(".");
                item.SubItems[2].Text = size == -1 ? subname + name : subname.Insert(size, name);
            }
        }
        public void addNumberName(int number1, int number2)
        {
            string s = "{0:D" + number1 + "}";
            foreach (ListViewItem item in FileList.Items)
            {
                string subname = item.SubItems[2].Text;
                int size = subname.LastIndexOf(".");
                item.SubItems[2].Text = size == -1 ? subname + string.Format(s, number2++) : subname.Insert(size, string.Format(s, number2++));
            }
        }
    }
}
