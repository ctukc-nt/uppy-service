using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Domain.Model;
using Mongo.Common;
using UPPY.Services.DataManagers;
using UPPY.Services.DataManagerService;

namespace UPPY.Tools
{
    public partial class MainForm : Form
    {
        private readonly MaintenanceTools _maintenanceTools = new MaintenanceTools();

        delegate void SetTextCallback(string text, int all, int processed);

        public MainForm()
        {
            InitializeComponent();
            _maintenanceTools.ProgressChanged = GetValue;
        }

        private void GetValue(string name, int all, int processed)
        {
            if (rtbLog.InvokeRequired)
            {
                SetTextCallback d = GetValue;
                rtbLog.Invoke(d, name, all, processed);
            }
            else
            {
                Application.DoEvents();
                rtbLog.AppendText($"{name} {all} {processed} \r\n");
                rtbLog.SelectionStart =
                    rtbLog.TextLength;
                rtbLog.ScrollToCaret();
            }
        }

        private void cbCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < clbValues.Items.Count; i++)
            {
                clbValues.SetItemChecked(i, cbCheckAll.Checked);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private async void btnCheckTechRoutesName_Click(object sender, EventArgs e)
        {
            ShowProgressForm();

            var task = new Task<List<TechRoute>>(() => _maintenanceTools.CheckNameTechRoutes());
            task.Start();
            await task;

            clbValues.DataSource = task.Result;
            CloseProgressForm();

            btnRepairTechRoutesName.Enabled = true;
        }

        private async void btnCheckUnusableTechRoutes_Click(object sender, EventArgs e)
        {
            ShowProgressForm();

            var task = new Task<List<TechRoute>>(() => _maintenanceTools.CheckUnusableTechRoutes());
            task.Start();
            await task;

            clbValues.DataSource = task.Result.OrderBy(x => x.Name).ToList();
            CloseProgressForm();

            btnDelUnusableTechRoutes.Enabled = true;
        }

        private async void btnCheckDoubles_Click(object sender, EventArgs e)
        {
            ShowProgressForm();

            var task = new Task<List<GrouppedTechRoutes>>(() => _maintenanceTools.CheckDoublesTechRoutes());
            task.Start();
            await task;

            clbValues.DataSource = task.Result.OrderBy(x => x.Name).ToList();
            rtbLog.Clear();
            foreach (var grouppedTechRoutese in task.Result)
            {
                rtbLog.AppendText($"{grouppedTechRoutese.Name}\r\n");
            }

            CloseProgressForm();

            rtbLog.AppendText($"{task.Result.Sum(x=>x.Count)} \r\n");
            btnRepairDoubles.Enabled = true;
        }

        private async void btnCheckUnusableCollection_Click(object sender, EventArgs e)
        {
            ShowProgressForm();

            var task = new Task<List<GrouppedTechRoutes>>(() => _maintenanceTools.CheckUnusableCollections());
            task.Start();
            await task;

            clbValues.DataSource = task.Result.OrderBy(x => x.Name).ToList();
            CloseProgressForm();

            btnDelUnusableCollection.Enabled = true;
        }

        private async void btnCheckHier_Click(object sender, EventArgs e)
        {
            ShowProgressForm();

            var task = new Task<List<BadHierarchicalData>>(() => _maintenanceTools.CheckHierarchicalData());
            task.Start();
            await task;

            clbValues.DataSource = task.Result.OrderBy(x => x.Name).ToList();
            CloseProgressForm();

            btnRemoveHier.Enabled = true;
        }

        private async void btnRepairTechRoutesName_Click(object sender, EventArgs e)
        {
            ShowProgressForm();

            var toRepair = clbValues.CheckedItems.Cast<TechRoute>().ToList();
            var task = new Task(() => { _maintenanceTools.RepairNameTechRoutes(toRepair); });
            task.Start();
            await task;

            CloseProgressForm();
        }

        private async void btnRepairDoubles_Click(object sender, EventArgs e)
        {
            ShowProgressForm();

            var toRepair = clbValues.CheckedItems.Cast<GrouppedTechRoutes>().ToList();
            var task = new Task(() => { _maintenanceTools.RepairDoublesTechRoutes(toRepair); });
            task.Start();
            await task;

            CloseProgressForm();
        }

        private async void btnDelUnusableTechRoutes_Click(object sender, EventArgs e)
        {
            ShowProgressForm();

            var toRepair = clbValues.CheckedItems.Cast<TechRoute>().ToList();
            var task = new Task(() => { _maintenanceTools.DelUnusableTechRoutes(toRepair); });
            task.Start();
            await task;

            CloseProgressForm();
        }

        private async void btnDelUnusableCollection_Click(object sender, EventArgs e)
        {
            ShowProgressForm();

            var toRepair = clbValues.CheckedItems.Cast<GrouppedTechRoutes>().ToList();
            var task = new Task(() => { _maintenanceTools.DelUnusableCollections(toRepair); });
            task.Start();
            await task;

            CloseProgressForm();
        }

        private void ShowProgressForm()
        {
            Enabled = false;
            progressPanel1.Visible = true;

            btnRepairTechRoutesName.Enabled = false;
            btnRepairDoubles.Enabled = false;
            btnDelUnusableTechRoutes.Enabled = false;
            btnDelUnusableCollection.Enabled = false;
            btnRemoveHier.Enabled = false;
        }

        private void CloseProgressForm()
        {
            Enabled = true;
            progressPanel1.Visible = false;

            btnRepairTechRoutesName.Enabled = false;
            btnRepairDoubles.Enabled = false;
            btnDelUnusableTechRoutes.Enabled = false;
            btnDelUnusableCollection.Enabled = false;
            btnRemoveHier.Enabled = false;
        }

        private void btnRebuildIndexes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not realized yet!");
            return;
            _maintenanceTools.RebuildIndexes();
        }

        private async void btnRemoveHier_Click(object sender, EventArgs e)
        {
            ShowProgressForm();

            var toRepair = clbValues.CheckedItems.Cast<BadHierarchicalData>().ToList();
            var task = new Task(() => { _maintenanceTools.RemoveHierarchicalWithoutParents(toRepair); });
            task.Start();
            await task;

            CloseProgressForm();
        }

        private async void btnCheckWorkHourDrawing_Click(object sender, EventArgs e)
        {
            ShowProgressForm();

            var task = new Task<List<GrouppedTechRoutes>>(() => _maintenanceTools.CheckWorkHourDrawing());
            task.Start();
            await task;

            clbValues.DataSource = task.Result.OrderBy(x => x.Name).ToList();
            CloseProgressForm();

            btnRemoveHier.Enabled = true;
        }

        private async void btnDelWorkHourDrawing_Click(object sender, EventArgs e)
        {
            ShowProgressForm();

            var toRepair = clbValues.CheckedItems.Cast<GrouppedTechRoutes>().ToList();
            var task = new Task(() => { _maintenanceTools.RemoveDoublesWorkHourDrawing(toRepair); });
            task.Start();
            await task;

            CloseProgressForm();
        }

        private async void btnCheckDeletedTO_Click(object sender, EventArgs e)
        {
            ShowProgressForm();

            var task = new Task<List<GrouppedTechRoutes>>(() => _maintenanceTools.CheckDeletedTechOpers());
            task.Start();
            await task;

            clbValues.DataSource = task.Result;

            CloseProgressForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MaintenanceTools tools = new MaintenanceTools();
            tools.RestoreDrawings();
        }
    }
}