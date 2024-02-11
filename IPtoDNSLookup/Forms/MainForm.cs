using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LookupsIPsToDNS.Forms
{
    public partial class MainForm : Form
    {
        private DataTable _table = new DataTable();
        private BackgroundWorker _lookupWorker = new BackgroundWorker();
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        // Declare processedIPs as a member variable of the class
        private readonly HashSet<string> _processedIPs = new HashSet<string>();

        #region MyRegion

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public MainForm()
        {
            InitializeComponent();

            // Set up the DataTable columns
            _table.Columns.Add("IP Address", typeof(string));
            _table.Columns.Add("Domain Name", typeof(string));

            // Bind the DataTable to the DataGridView
            resultDataGridView.AutoGenerateColumns = true;
            resultDataGridView.DataSource = _table;

            // Set up the BackgroundWorker
            _lookupWorker.WorkerSupportsCancellation = true;
            _lookupWorker.DoWork += LookupWorker_DoWork;
            _lookupWorker.RunWorkerCompleted += LookupWorker_RunWorkerCompleted;

            // Set status in form status bar
            toolStripStatusLabel.Text = @"Ready.";

            // Set title
            Text = Text + @" v. " + AssemblyVersion;
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            // Set the default button to the Lookup button
            FormatOutputGrid();
        }

        public void FormatOutputGrid()
        {
            foreach (DataGridViewColumn col in resultDataGridView.Columns)
            {
                // Skip the first column
                if (col.Index != 0)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    col.FillWeight = 1;
                }
            }
        }

        private void UpdateClearButton()
        {
            // Enable the Clear button if there are rows in the DataGridView
            buttonCleanOutput.Enabled = resultDataGridView.Rows.Count > 1;
        }

        private void UpdateExportButton()
        {
            // Enable the Export button if there are rows in the DataGridView
            buttonExporttoCSV.Enabled = resultDataGridView.Rows.Count > 1;
        }

        private void DoIpLookupWork()
        {
            var ips = ipAddressTextBox.Text.Split('\n').Select(ip => ip.Trim()).Where(ip => !string.IsNullOrEmpty(ip));

            var enumerable = ips as string[] ?? ips.ToArray();
            var invalidIPs = enumerable.Where(ip => !IPAddress.TryParse(ip, out _));

            var iPs = invalidIPs as string[] ?? invalidIPs.ToArray();
            if (iPs.Any())
            {
                var invalidIpList = string.Join("\n", iPs);
                var result = MessageBox.Show($@"The following IP addresses are invalid:
{invalidIpList}

Do you want to remove them?", @"Invalid IP Address", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ipAddressTextBox.Lines = enumerable.Where(ip => IPAddress.TryParse(ip, out _)).ToArray();

                    // all IP addresses are valid
                    toolStripStatusLabel.Text = @"Invalid IP address(es) removed. Please try again.";
                }
                else if (result == DialogResult.No)
                {
                    // Set status text in status bar
                    toolStripStatusLabel.Text = @"Invalid IP address(es) found. Please correct the IP address(es) and try again.";
                }
            }
            else if (!enumerable.Any(ip => IPAddress.TryParse(ip, out _)))
            {
                // No valid IP addresses found
                MessageBox.Show(@"No valid IP addresses found to lookup. Please enter valid IP addresses and try again.", @"Invalid IP Address to lookup", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Set status text in status bar
                toolStripStatusLabel.Text = @"No valid IP addresses found. Please enter valid IP addresses and try again.";

                // Set focus to the IP address TextBox
                ipAddressTextBox.Focus();
            }
            else
            {
                // all IP addresses are valid
                // Disable the input controls
                ipAddressTextBox.Enabled = false;
                lookupButton.Enabled = false;
                cancelButton.Enabled = true;
                buttonCleanOutput.Enabled = false;
                cleanupIPListButton.Enabled = false;
                buttonExporttoCSV.Enabled = false;
                loadIPsFromcsvFileToolStripMenuItem.Enabled = false;

                if (!_lookupWorker.IsBusy)
                {
                    _table.Clear();
                    _lookupWorker.RunWorkerAsync();
                }
            }
        }
        
        private void LookupWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] ipAddresses = ipAddressTextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                toolStripStatusLabel.Text = @"Running... Please wait for the task to complete...";

                // Create a HashSet to store processed IP addresses
                HashSet<string> processedIPs = new HashSet<string>();

                // Create a lock object for thread safety
                object lockObj = new object();

                Parallel.ForEach(ipAddresses, (ipAddress, state) =>
                {
                    if (_lookupWorker.CancellationPending)
                    {
                        state.Break();
                        return;
                    }

                    // Check if the IP address has already been processed
                    bool isDuplicate;
                    lock (lockObj)
                    {
                        isDuplicate = !processedIPs.Add(ipAddress);
                    }

                    if (isDuplicate)
                    {
                        // Skip adding the row
                        return;
                    }

                    try
                    {
                        IPHostEntry hostEntry = Dns.GetHostEntry(IPAddress.Parse(ipAddress));

                        DataRow row = _table.NewRow();
                        row["IP Address"] = ipAddress;
                        row["Domain Name"] = hostEntry.HostName;

                        // update DataTable on UI thread
                        Invoke(new Action(() =>
                        {
                            _table.Rows.Add(row);
                            int lastIndex = resultDataGridView.Rows.Count - 1;
                            // Scroll to the end of the DataGridView control
                            resultDataGridView.FirstDisplayedScrollingRowIndex = lastIndex;
                        }));

#if DEBUG
            MessageBox.Show($@"IP Address: {ipAddress} " + Environment.NewLine + $@"Domain Name: {hostEntry.HostName}");
#endif
                    }
                    catch (SocketException ex)
                    {
                        // The host does not exist
                        if (ex.ErrorCode == 11001)
                        {
                            DataRow row = _table.NewRow();
                            row["IP Address"] = ipAddress;
                            row["Domain Name"] = "The host does not exist (Non-existent domain)";
                            Invoke(new Action(() =>
                            {
                                _table.Rows.Add(row);
                                int lastIndex = resultDataGridView.Rows.Count - 1;
                                // Scroll to the end of the DataGridView control
                                resultDataGridView.FirstDisplayedScrollingRowIndex = lastIndex;
                            }));
                        }
                        else
                        {
#if DEBUG
                MessageBox.Show($@"Error looking up IP address {ipAddress}: {ex.Message}");
                // handle any other errors
#endif
                        }
                    }
                });
            }
            finally
            {
                // Clear the processed IP addresses after completion
                _processedIPs.Clear();

                // Set status in form status bar
                toolStripStatusLabel.Text = @"Task completed.";

                // Perform any other cleanup or post-processing here
            }

            /*string[] ipAddresses = ipAddressTextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                toolStripStatusLabel.Text = @"Running... Please wait for task to complete...";

                HashSet<string> processedIPs = new HashSet<string>(); // Create a HashSet to store processed IP addresses
                if (processedIPs == null) throw new ArgumentNullException(nameof(processedIPs));

                Parallel.ForEach(ipAddresses, (ipAddress, state) =>
                {
                    if (_lookupWorker.CancellationPending)
                    {
                        state.Break();
                        return;
                    }

                    try
                    {
                        IPHostEntry hostEntry = Dns.GetHostEntry(IPAddress.Parse(ipAddress));

                        // Check if IP address has already been processed
                        lock (processedIPs)
                        {
                            if (processedIPs.Contains(ipAddress))
                            {
                                // Skip adding the row
                                return;
                            }

                            // Add the IP address to the processed set
                            processedIPs.Add(ipAddress);
                        }

                        DataRow row = _table.NewRow();
                        row["IP Address"] = ipAddress;
                        row["Domain Name"] = hostEntry.HostName;

                        // update DataTable on UI thread
                        Invoke(new Action(() =>
                        {
                            _table.Rows.Add(row);
                            int lastIndex = resultDataGridView.Rows.Count - 1;
                            // Scroll to the end of the DataGridView control
                            resultDataGridView.FirstDisplayedScrollingRowIndex = lastIndex;
                        }));
#if DEBUG
        MessageBox.Show($"IP Address: {ipAddress}, Domain Name: {hostEntry.HostName}");
#endif
                    }
                    catch (SocketException ex)
                    {
                        // The host does not exist
                        if (ex.ErrorCode == 11001)
                        {
                            DataRow row = _table.NewRow();
                            row["IP Address"] = ipAddress;
                            row["Domain Name"] = "The host does not exist (Non-existent domain)";
                            Invoke(new Action(() =>
                            {
                                _table.Rows.Add(row);
                                int lastIndex = resultDataGridView.Rows.Count - 1;
                                // Scroll to the end of the DataGridView control
                                resultDataGridView.FirstDisplayedScrollingRowIndex = lastIndex;
                            }));
                        }
                        else
                        {
#if DEBUG
            MessageBox.Show($"Error looking up IP address {ipAddress}: {ex.Message}");
            // handle any other errors
#endif
                        }
                    }
                });


                /*Parallel.ForEach(ipAddresses, (ipAddress, state) =>
                {
                    if (lookupWorker.CancellationPending)
                    {
                        state.Break();
                        return;
                    }

                    try
                    {
                        IPHostEntry hostEntry = Dns.GetHostEntry(IPAddress.Parse(ipAddress));
                        DataRow row = table.NewRow();
                        row["IP Address"] = ipAddress;
                        row["Domain Name"] = hostEntry.HostName;
                        
                        // update DataTable on UI thread
                        Invoke(new Action(() =>
                        {
                            table.Rows.Add(row);
                            int lastIndex = resultDataGridView.Rows.Count - 1;
                            resultDataGridView.FirstDisplayedScrollingRowIndex = lastIndex; // Scroll to the end of the DataGridView control
                        }));
#if DEBUG
                        MessageBox.Show($"IP Address: {ipAddress}, Domain Name: {hostEntry.HostName}");
#endif
                    }
                    catch (SocketException ex)
                    {
                        if (ex.ErrorCode == 11001) // The host does not exist
                        {
                            DataRow row = table.NewRow();
                            row["IP Address"] = ipAddress;
                            row["Domain Name"] = "The host does not exist (Non-existent domain)";
                            Invoke(new Action(() =>
                            {
                                table.Rows.Add(row);
                                int lastIndex = resultDataGridView.Rows.Count - 1;
                                resultDataGridView.FirstDisplayedScrollingRowIndex = lastIndex; // Scroll to the end of the DataGridView control
                            }));
                        }
                        else
                        {
#if DEBUG
                            MessageBox.Show($"Error looking up IP address {ipAddress}: {ex.Message}");
                            // handle any other errors
#endif
                        }
                    }
                });#1#
            }
            catch (OperationCanceledException)
            {
                // ignore cancellation exception
            }*/
        }

        private void LookupWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show($@"An error occurred while performing the DNS lookup: {e.Error.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                toolStripStatusLabel.Text = @"Finished with error(s).";
            }
            else if (e.Cancelled)
            {
                MessageBox.Show(@"DNS lookup cancelled by user.", @"Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);

                toolStripStatusLabel.Text = @"Finished - cancelled by user.";
            }
            else
            {
                MessageBox.Show(@"DNS lookup completed successfully.", @"Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                toolStripStatusLabel.Text = @"Finished.";
            }

            // Enable the input controls
            ipAddressTextBox.Enabled = true;
            lookupButton.Enabled = true;
            cancelButton.Enabled = false;
            buttonCleanOutput.Enabled = true;
            cleanupIPListButton.Enabled = true;
            buttonExporttoCSV.Enabled = true;
            loadIPsFromcsvFileToolStripMenuItem.Enabled = true;

            // Check if DataGridView has any rows and update Clear button accordingly
            UpdateClearButton();
            UpdateExportButton();
        }

        private static void AddIpAddressesToTextBox(List<string> ipAddresses, TextBox textBox)
        {
            // Add each IP address to the list
            foreach (string ipAddress in ipAddresses)
            {
                textBox.AppendText($"{ipAddress}{Environment.NewLine}");
            }
        }

        private List<string> ReadIpAddressesFromCsv(string filePath)
        {
            List<string> ipAddresses = new List<string>();

            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(filePath);

            // Check if the file has at least one line (header or data)
            if (lines.Length >= 1)
            {
                // Split the header line to get column names
                string[] headers = lines[0].Split(',');

                // Find the column index for "IP Address"
                int ipAddressIndex = Array.IndexOf(headers, "IP Address");

                // Iterate over data lines starting from the second line
                for (int i = 1; i < lines.Length; i++)
                {
                    // Split the current line into values
                    string[] values = lines[i].Split(',');

                    // Ensure the line has enough columns and contains a valid IP address
                    if (values.Length > ipAddressIndex)
                    {
                        string ipAddress = values[ipAddressIndex];

                        // Validate the IP address (you may want to add more robust validation)
                        if (IPAddress.TryParse(ipAddress, out _))
                        {
                            // Add the valid IP address to the list
                            ipAddresses.Add(ipAddress);
                        }
                        else
                        {
                            // Handle invalid IP addresses (you may want to display a message or log it)
                            MessageBox.Show($@"Invalid IP address: {ipAddress}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            return ipAddresses;
        }

        private void buttonLookup_Click(object sender, EventArgs e)
        {
            // Get the IP addresses from the text box and start the lookup
            DoIpLookupWork();
        }

        #endregion Main shared code

        #region Menu code

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open form
            try
            {
                using (var frm = new AboutBox())
                {
                    //LogClass.LogWriteProgram(LogFileProgramProgramMessageTxt + "Opened Convert certificate form by user.");

                    // Create an instance of the form to open
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                // Show error message
                MessageBox.Show(@"Error: " + ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void loadIPsFromcsvFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = @"Select CSV File",
                Filter = @"CSV Files|*.csv",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // Read the IP addresses from the selected CSV file
                List<string> ipAddresses = ReadIpAddressesFromCsv(filePath);

                // Clear the current IP addresses in the TextBox
                ipAddressTextBox.Clear();

                // Add IP addresses to the multiline TextBox
                AddIpAddressesToTextBox(ipAddresses, ipAddressTextBox);
            }
        }

        #endregion Menu code

        #region Form code

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            resultDataGridView.DataSource = _table;
            if (_lookupWorker.IsBusy)
            {
                _tokenSource.Cancel();
            }
        }
        
        private void buttonCleanOutput_Click(object sender, EventArgs e)
        {
            // Ask the user for confirmation before clearing the TextBox
            DialogResult result = MessageBox.Show(@"Are you sure you want to clear the looked up IP address list?", @"Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Clear the current data source
                resultDataGridView.DataSource = null;

                // Clear the rows in the DataTable
                _table.Clear();

                // Re-bind the DataTable to the DataGridView
                resultDataGridView.DataSource = _table;

                FormatOutputGrid();

                // Set status text in status bar
                toolStripStatusLabel.Text = @"Ready.";

                // Disable Clear button since there's no data to clear
                UpdateClearButton();
                UpdateExportButton();
            }
        }

        private void buttonExporttoCSV_Click(object sender, EventArgs e)
        {
            // Show a save file dialog to get the file path for the CSV file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = @"CSV files (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                // The user cancelled the dialog - return
                return;
            }

            try
            {
                // Open the CSV file for writing
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    // Write the header row
                    writer.WriteLine(string.Join(",", resultDataGridView.Columns.Cast<DataGridViewColumn>().Select(col => col.HeaderText)));

                    // Write the data rows
                    foreach (DataGridViewRow row in resultDataGridView.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            writer.WriteLine(string.Join(",", row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value)));
                        }
                    }
                }

                // Show success
                MessageBox.Show($@"Data exported to {saveFileDialog.FileName}", @"Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                // Show error
                MessageBox.Show($@"Data not exported to {saveFileDialog.FileName}" + Environment.NewLine + Environment.NewLine + @"Error: " + exception, @"Export error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }
        }

        private void cleanupIPListButton_Click(object sender, EventArgs e)
        {
            // Ask the user for confirmation before clearing the TextBox
            DialogResult result = MessageBox.Show(@"Are you sure you want to clear the IP address list?", @"Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Remove empty lines and duplicates from the IP address list
                ipAddressTextBox.Text = null;

                // Set status text in the status bar
                toolStripStatusLabel.Text = @"Ready.";

                // Disable the Clear button since there's no data to clear
                UpdateClearButton();
                UpdateExportButton();
            }
        }

        #endregion Form code
    }
}