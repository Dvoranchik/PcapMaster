using System;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Collections.Generic;
using System.Configuration;
using PcapParser;
using System.Reflection;
using System.Threading;

namespace PcapMaster
{
    public partial class PcapMasterApp : MetroForm
    {
        public static int cols = Settings.Default.NumberCols;
        public static int rows = Settings.Default.NumberRows;
        public static int counter = Settings.Default.NumberRows;
        public static int number = 1;

        public Action addRows;
        public System.Threading.Tasks.Task Add;
        public System.Threading.CancellationTokenSource cts;
        public System.Threading.CancellationToken token;

        public PcapMasterApp()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Table.Visible = false;
            LayoutPanel.Visible = false;
        }

        private async void OpenMenuButtom_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файлы pcap|*.pcap";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in OPF.FileNames)
                {
                    
                    Controller controller = Controller.getInstance();
                    controller.Close();
                    controller.Start(file, Settings.Default.NumberRows);
                    Controller control = Controller.getInstance();
                    control.Load();
                    List<PPacket> protocols = control.GetList();
                    string[,] ar = new string[cols, rows];
                    if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
                    {
                        Type dgvType = Table.GetType();
                        System.Reflection.PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                        pi.SetValue(Table, true, null);
                    }

                    for (int i = 0; i < cols; i++)
                    {
                        Table.Columns.Add("", ConfigurationManager.AppSettings[Convert.ToString(i)]);
                    }
                    rows = protocols.Count;

                    for (int i = 0; i < rows; i++)
                    {
                        Table.Rows.Add("");
                        Table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }

                    for (int j = 0; j < rows; j++)
                    {
                       Table.Rows[j].Cells[0].Value = number++;
                    }

                    for (int i = 1; i < cols; i++)
                    {
                        for (int j = 0; j < rows; j++)
                        {
                            Table.Rows[j].Cells[i].Value = protocols[j % rows].GetPacket(i-1);
                        }
                    }

                    control.Clear();
                    Table.Visible = true;
                }
                OpenMenuButtom.Enabled = true;
                //запуск задачи подгрузки таблицы
                cts = new CancellationTokenSource();
                token = cts.Token;

                addRows = () => DataGridView1_Scroll(token);
                Add =  new System.Threading.Tasks.Task(addRows);
                Add.Start();
            }
        }

        private void ExitMenuButton_Click(object sender, EventArgs e)
        {
            if (Table.Rows.Count > 0)
            {
                cts?.Cancel();
                Add.Wait();
                Add.Dispose();
            }
            Application.Exit();
        }

        private void DataGridView1_Scroll(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (Table.Rows.Count > 0)
                {
                    if (Table.Rows[Table.Rows.Count - 1].Displayed)
                    {
                        Controller control = Controller.getInstance();
                        control.Load();
                        List<PPacket> protocols = control.GetList();
                        {
                            foreach (var item in protocols)
                            {
                                Action action = () => Table.Rows.Add(number++,item.GetPacket(0), item.GetPacket(1),
                                   item.GetPacket(2), item.GetPacket(3), item.GetPacket(4),
                                   item.GetPacket(5), item.GetPacket(6), item.GetPacket(7));
                                if (InvokeRequired)
                                    Invoke(action);
                            }
                        }
                        control.Clear();
                    }
                }
            }
        }

        private void CloseFileMenuButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Идея: Федосов \n Программисты: \n - Бородин \n - Дирин \n - Митрошин \n О программе: PcapMaster приложение, созданное для работы с файлами, имеющие расширение .pcap ");
        }

    }
}
