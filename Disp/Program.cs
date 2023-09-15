using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace TaskManagerApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Загружаем информацию о процессах при запуске приложения
            RefreshProcessList();
        }

        private void RefreshProcessList()
        {
            Process[] processes = Process.GetProcesses();

            dataGridViewProcesses.Rows.Clear();

            foreach (Process process in processes)
            {
                // Добавляем информацию о процессе в DataGridView
                dataGridViewProcesses.Rows.Add(
                    process.ProcessName,
                    process.Id,
                    process.MachineName,
                    process.PrivateMemorySize64 / (1024 * 1024), // Размер памяти в МБ
                    process.BasePriority,
                    process.Threads.Count
                );
            }
        }

        private void dataGridViewProcesses_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewProcesses.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridViewProcesses.SelectedRows[0].Index;
                int processId = (int)dataGridViewProcesses.Rows[selectedRowIndex].Cells["ProcessId"].Value;

                Process process = Process.GetProcessById(processId);

                dataGridViewThreads.Rows.Clear();

                foreach (ProcessThread thread in process.Threads)
                {
                    // Добавляем информацию о потоке в DataGridView
                    dataGridViewThreads.Rows.Add(
                        thread.Id,
                        thread.BasePriority
                    );
                }
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshProcessList();
        }
    }
}
