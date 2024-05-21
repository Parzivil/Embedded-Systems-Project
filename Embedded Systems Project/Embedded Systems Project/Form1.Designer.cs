using AquaControls;
using System.Windows.Forms;
using System.Linq.Expressions;
using Embedded_Systems_Project.Addons;

namespace Embedded_Systems_Project
{
    partial class BoardControlForm
    {

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoardControlForm));
            SerialPortStatusBulb = new LedBulb();
            SerialPortBox = new GroupBox();
            SerialConnectionErrorLabel = new Label();
            BaudrateSelection = new ComboBox();
            BaudRateLabel = new Label();
            PortStatusLabel = new Label();
            DisconnectButton = new Button();
            ConnectButton = new Button();
            COMPORTLable = new Label();
            COM_Port_Dropdown = new ComboBox();
            DatabaseGroup = new GroupBox();
            label7 = new Label();
            ServerConnectionLED = new LedBulb();
            DatabaseDisconnectButton = new Button();
            DatabaseTextBox = new TextBox();
            DatabaseConnectButton = new Button();
            PasswordTextBox = new TextBox();
            UsernameTextBox = new TextBox();
            ServerNameSelection = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            TabController = new TabControl();
            SetupTabPage = new TabPage();
            DigitalPage = new TabPage();
            sevenSeg_2 = new SevenSegment();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            PA7 = new LedBulb();
            PA6 = new LedBulb();
            PA5 = new LedBulb();
            PA4 = new LedBulb();
            PA3 = new LedBulb();
            PA2 = new LedBulb();
            PA1 = new LedBulb();
            PA0 = new LedBulb();
            label6 = new Label();
            RefreshButton = new Button();
            label5 = new Label();
            PC7_CheckBox = new CheckBox();
            PC6_CheckBox = new CheckBox();
            PC5_CheckBox = new CheckBox();
            PC4_CheckBox = new CheckBox();
            PC3_CheckBox = new CheckBox();
            PC2_CheckBox = new CheckBox();
            PC1_CheckBox = new CheckBox();
            PC0_CheckBox = new CheckBox();
            sevenSeg_1 = new SevenSegment();
            PortLightsPage = new TabPage();
            groupBox1 = new GroupBox();
            LightPercentageLabel = new Label();
            LightScrollBar = new VScrollBar();
            LIGHT_LABEL = new Label();
            LightGauge = new AquaGauge();
            PotsGroup = new GroupBox();
            label17 = new Label();
            label16 = new Label();
            PotGauge2 = new AquaGauge();
            PotGauge1 = new AquaGauge();
            TempPage = new TabPage();
            label25 = new Label();
            TempLabel = new Label();
            label23 = new Label();
            groupBox2 = new GroupBox();
            groupBox4 = new GroupBox();
            dataLoggingStateLabel = new Label();
            disableLoggingButton = new Button();
            EnableLoggingButton = new Button();
            groupBox3 = new GroupBox();
            numericUpDown4 = new NumericUpDown();
            InsertIntoTableButton = new Button();
            label22 = new Label();
            TempPlot = new System.Windows.Forms.DataVisualization.Charting.Chart();
            label21 = new Label();
            label20 = new Label();
            label19 = new Label();
            label18 = new Label();
            KiSet = new NumericUpDown();
            KpSet = new NumericUpDown();
            SetPointTemp = new NumericUpDown();
            PORTC_LIGHTS_TIMER = new System.Windows.Forms.Timer(components);
            POT1_TIMER = new System.Windows.Forms.Timer(components);
            POT2_TIMER = new System.Windows.Forms.Timer(components);
            LIGHT_TIMER = new System.Windows.Forms.Timer(components);
            DATABASE_TIMER = new System.Windows.Forms.Timer(components);
            TEMP_TIMER = new System.Windows.Forms.Timer(components);
            SerialPortBox.SuspendLayout();
            DatabaseGroup.SuspendLayout();
            TabController.SuspendLayout();
            SetupTabPage.SuspendLayout();
            DigitalPage.SuspendLayout();
            PortLightsPage.SuspendLayout();
            groupBox1.SuspendLayout();
            PotsGroup.SuspendLayout();
            TempPage.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TempPlot).BeginInit();
            ((System.ComponentModel.ISupportInitialize)KiSet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)KpSet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SetPointTemp).BeginInit();
            SuspendLayout();
            // 
            // SerialPortStatusBulb
            // 
            SerialPortStatusBulb.Location = new Point(144, 174);
            SerialPortStatusBulb.Name = "SerialPortStatusBulb";
            SerialPortStatusBulb.On = false;
            SerialPortStatusBulb.Size = new Size(20, 20);
            SerialPortStatusBulb.TabIndex = 0;
            // 
            // SerialPortBox
            // 
            SerialPortBox.Controls.Add(SerialConnectionErrorLabel);
            SerialPortBox.Controls.Add(BaudrateSelection);
            SerialPortBox.Controls.Add(BaudRateLabel);
            SerialPortBox.Controls.Add(PortStatusLabel);
            SerialPortBox.Controls.Add(DisconnectButton);
            SerialPortBox.Controls.Add(ConnectButton);
            SerialPortBox.Controls.Add(COMPORTLable);
            SerialPortBox.Controls.Add(COM_Port_Dropdown);
            SerialPortBox.Controls.Add(SerialPortStatusBulb);
            SerialPortBox.Location = new Point(40, 6);
            SerialPortBox.Name = "SerialPortBox";
            SerialPortBox.Size = new Size(437, 254);
            SerialPortBox.TabIndex = 0;
            SerialPortBox.TabStop = false;
            SerialPortBox.Text = "Serial Port Connection";
            // 
            // SerialConnectionErrorLabel
            // 
            SerialConnectionErrorLabel.AutoSize = true;
            SerialConnectionErrorLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            SerialConnectionErrorLabel.ForeColor = Color.Red;
            SerialConnectionErrorLabel.Location = new Point(28, 213);
            SerialConnectionErrorLabel.Name = "SerialConnectionErrorLabel";
            SerialConnectionErrorLabel.Size = new Size(130, 21);
            SerialConnectionErrorLabel.TabIndex = 7;
            SerialConnectionErrorLabel.Text = "Connection Error";
            SerialConnectionErrorLabel.Visible = false;
            // 
            // BaudrateSelection
            // 
            BaudrateSelection.FormattingEnabled = true;
            BaudrateSelection.Location = new Point(111, 70);
            BaudrateSelection.Name = "BaudrateSelection";
            BaudrateSelection.Size = new Size(167, 23);
            BaudrateSelection.TabIndex = 6;
            BaudrateSelection.SelectedIndexChanged += BaudrateSelection_SelectedIndexChanged;
            // 
            // BaudRateLabel
            // 
            BaudRateLabel.AutoSize = true;
            BaudRateLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            BaudRateLabel.Location = new Point(28, 73);
            BaudRateLabel.Name = "BaudRateLabel";
            BaudRateLabel.Size = new Size(73, 20);
            BaudRateLabel.TabIndex = 5;
            BaudRateLabel.Text = "Baudrate";
            // 
            // PortStatusLabel
            // 
            PortStatusLabel.AutoSize = true;
            PortStatusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            PortStatusLabel.Location = new Point(28, 179);
            PortStatusLabel.Name = "PortStatusLabel";
            PortStatusLabel.Size = new Size(103, 15);
            PortStatusLabel.TabIndex = 4;
            PortStatusLabel.Text = "Serial Port Status";
            // 
            // DisconnectButton
            // 
            DisconnectButton.Enabled = false;
            DisconnectButton.Location = new Point(260, 123);
            DisconnectButton.Name = "DisconnectButton";
            DisconnectButton.Size = new Size(145, 23);
            DisconnectButton.TabIndex = 3;
            DisconnectButton.Text = "Disconnect";
            DisconnectButton.UseVisualStyleBackColor = true;
            DisconnectButton.Click += DisconnectButton_Click;
            // 
            // ConnectButton
            // 
            ConnectButton.Location = new Point(28, 123);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(136, 23);
            ConnectButton.TabIndex = 2;
            ConnectButton.Text = "Connect";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // COMPORTLable
            // 
            COMPORTLable.AutoSize = true;
            COMPORTLable.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            COMPORTLable.Location = new Point(28, 38);
            COMPORTLable.Name = "COMPORTLable";
            COMPORTLable.Size = new Size(77, 20);
            COMPORTLable.TabIndex = 1;
            COMPORTLable.Text = "COM Port";
            // 
            // COM_Port_Dropdown
            // 
            COM_Port_Dropdown.FormattingEnabled = true;
            COM_Port_Dropdown.Location = new Point(111, 35);
            COM_Port_Dropdown.Name = "COM_Port_Dropdown";
            COM_Port_Dropdown.Size = new Size(167, 23);
            COM_Port_Dropdown.TabIndex = 0;
            COM_Port_Dropdown.SelectedIndexChanged += COM_Port_Dropdown_SelectedIndexChanged;
            // 
            // DatabaseGroup
            // 
            DatabaseGroup.Controls.Add(label7);
            DatabaseGroup.Controls.Add(ServerConnectionLED);
            DatabaseGroup.Controls.Add(DatabaseDisconnectButton);
            DatabaseGroup.Controls.Add(DatabaseTextBox);
            DatabaseGroup.Controls.Add(DatabaseConnectButton);
            DatabaseGroup.Controls.Add(PasswordTextBox);
            DatabaseGroup.Controls.Add(UsernameTextBox);
            DatabaseGroup.Controls.Add(ServerNameSelection);
            DatabaseGroup.Controls.Add(label4);
            DatabaseGroup.Controls.Add(label3);
            DatabaseGroup.Controls.Add(label2);
            DatabaseGroup.Controls.Add(label1);
            DatabaseGroup.Location = new Point(40, 266);
            DatabaseGroup.Name = "DatabaseGroup";
            DatabaseGroup.Size = new Size(437, 300);
            DatabaseGroup.TabIndex = 1;
            DatabaseGroup.TabStop = false;
            DatabaseGroup.Text = "Database Server Connection";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(28, 262);
            label7.Name = "label7";
            label7.Size = new Size(123, 15);
            label7.TabIndex = 8;
            label7.Text = "Database Port Status";
            // 
            // ServerConnectionLED
            // 
            ServerConnectionLED.Location = new Point(187, 257);
            ServerConnectionLED.Name = "ServerConnectionLED";
            ServerConnectionLED.On = false;
            ServerConnectionLED.Size = new Size(20, 20);
            ServerConnectionLED.TabIndex = 7;
            // 
            // DatabaseDisconnectButton
            // 
            DatabaseDisconnectButton.Location = new Point(187, 214);
            DatabaseDisconnectButton.Name = "DatabaseDisconnectButton";
            DatabaseDisconnectButton.Size = new Size(145, 23);
            DatabaseDisconnectButton.TabIndex = 8;
            DatabaseDisconnectButton.Text = "Database-Disconnect";
            DatabaseDisconnectButton.UseVisualStyleBackColor = true;
            DatabaseDisconnectButton.Click += DatabaseDisconnectButton_Click;
            // 
            // DatabaseTextBox
            // 
            DatabaseTextBox.Location = new Point(134, 161);
            DatabaseTextBox.Name = "DatabaseTextBox";
            DatabaseTextBox.Size = new Size(167, 23);
            DatabaseTextBox.TabIndex = 13;
            // 
            // DatabaseConnectButton
            // 
            DatabaseConnectButton.Location = new Point(28, 214);
            DatabaseConnectButton.Name = "DatabaseConnectButton";
            DatabaseConnectButton.Size = new Size(136, 23);
            DatabaseConnectButton.TabIndex = 7;
            DatabaseConnectButton.Text = "Database-Connect";
            DatabaseConnectButton.UseVisualStyleBackColor = true;
            DatabaseConnectButton.Click += DatabaseConnectButton_Click;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(134, 121);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.Size = new Size(167, 23);
            PasswordTextBox.TabIndex = 12;
            PasswordTextBox.UseSystemPasswordChar = true;
            // 
            // UsernameTextBox
            // 
            UsernameTextBox.Location = new Point(134, 77);
            UsernameTextBox.Name = "UsernameTextBox";
            UsernameTextBox.Size = new Size(167, 23);
            UsernameTextBox.TabIndex = 11;
            // 
            // ServerNameSelection
            // 
            ServerNameSelection.FormattingEnabled = true;
            ServerNameSelection.Location = new Point(134, 40);
            ServerNameSelection.Name = "ServerNameSelection";
            ServerNameSelection.Size = new Size(167, 23);
            ServerNameSelection.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(28, 40);
            label4.Name = "label4";
            label4.Size = new Size(100, 20);
            label4.TabIndex = 9;
            label4.Text = "Server Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(28, 80);
            label3.Name = "label3";
            label3.Size = new Size(80, 20);
            label3.TabIndex = 8;
            label3.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(28, 124);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 7;
            label2.Text = "Password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(28, 164);
            label1.Name = "label1";
            label1.Size = new Size(74, 20);
            label1.TabIndex = 6;
            label1.Text = "Database";
            // 
            // TabController
            // 
            TabController.Controls.Add(SetupTabPage);
            TabController.Controls.Add(DigitalPage);
            TabController.Controls.Add(PortLightsPage);
            TabController.Controls.Add(TempPage);
            TabController.Location = new Point(12, 12);
            TabController.Name = "TabController";
            TabController.SelectedIndex = 0;
            TabController.Size = new Size(541, 601);
            TabController.TabIndex = 2;
            TabController.Selecting += TabController_Selecting;
            // 
            // SetupTabPage
            // 
            SetupTabPage.Controls.Add(SerialPortBox);
            SetupTabPage.Controls.Add(DatabaseGroup);
            SetupTabPage.Location = new Point(4, 24);
            SetupTabPage.Name = "SetupTabPage";
            SetupTabPage.Padding = new Padding(3);
            SetupTabPage.Size = new Size(533, 573);
            SetupTabPage.TabIndex = 0;
            SetupTabPage.Text = "Setup";
            SetupTabPage.UseVisualStyleBackColor = true;
            // 
            // DigitalPage
            // 
            DigitalPage.Controls.Add(sevenSeg_2);
            DigitalPage.Controls.Add(label12);
            DigitalPage.Controls.Add(label13);
            DigitalPage.Controls.Add(label14);
            DigitalPage.Controls.Add(label15);
            DigitalPage.Controls.Add(label11);
            DigitalPage.Controls.Add(label10);
            DigitalPage.Controls.Add(label9);
            DigitalPage.Controls.Add(label8);
            DigitalPage.Controls.Add(PA7);
            DigitalPage.Controls.Add(PA6);
            DigitalPage.Controls.Add(PA5);
            DigitalPage.Controls.Add(PA4);
            DigitalPage.Controls.Add(PA3);
            DigitalPage.Controls.Add(PA2);
            DigitalPage.Controls.Add(PA1);
            DigitalPage.Controls.Add(PA0);
            DigitalPage.Controls.Add(label6);
            DigitalPage.Controls.Add(RefreshButton);
            DigitalPage.Controls.Add(label5);
            DigitalPage.Controls.Add(PC7_CheckBox);
            DigitalPage.Controls.Add(PC6_CheckBox);
            DigitalPage.Controls.Add(PC5_CheckBox);
            DigitalPage.Controls.Add(PC4_CheckBox);
            DigitalPage.Controls.Add(PC3_CheckBox);
            DigitalPage.Controls.Add(PC2_CheckBox);
            DigitalPage.Controls.Add(PC1_CheckBox);
            DigitalPage.Controls.Add(PC0_CheckBox);
            DigitalPage.Controls.Add(sevenSeg_1);
            DigitalPage.Location = new Point(4, 24);
            DigitalPage.Name = "DigitalPage";
            DigitalPage.Padding = new Padding(3);
            DigitalPage.Size = new Size(533, 573);
            DigitalPage.TabIndex = 1;
            DigitalPage.Text = "Digital I/O";
            DigitalPage.UseVisualStyleBackColor = true;
            // 
            // sevenSeg_2
            // 
            sevenSeg_2.ColorBackground = Color.DarkGray;
            sevenSeg_2.ColorDark = Color.DimGray;
            sevenSeg_2.ColorLight = Color.Red;
            sevenSeg_2.CustomPattern = 0;
            sevenSeg_2.DecimalOn = false;
            sevenSeg_2.DecimalShow = false;
            sevenSeg_2.ElementWidth = 10;
            sevenSeg_2.ItalicFactor = 0F;
            sevenSeg_2.Location = new Point(411, 164);
            sevenSeg_2.Name = "sevenSeg_2";
            sevenSeg_2.Padding = new Padding(4);
            sevenSeg_2.Size = new Size(50, 70);
            sevenSeg_2.TabIndex = 27;
            sevenSeg_2.TabStop = false;
            sevenSeg_2.Value = null;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label12.Location = new Point(107, 353);
            label12.Name = "label12";
            label12.Size = new Size(29, 17);
            label12.TabIndex = 26;
            label12.Text = "PA7";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label13.Location = new Point(107, 327);
            label13.Name = "label13";
            label13.Size = new Size(29, 17);
            label13.TabIndex = 25;
            label13.Text = "PA6";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label14.Location = new Point(107, 301);
            label14.Name = "label14";
            label14.Size = new Size(29, 17);
            label14.TabIndex = 24;
            label14.Text = "PA5";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label15.Location = new Point(107, 275);
            label15.Name = "label15";
            label15.Size = new Size(29, 17);
            label15.TabIndex = 23;
            label15.Text = "PA4";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(107, 249);
            label11.Name = "label11";
            label11.Size = new Size(29, 17);
            label11.TabIndex = 22;
            label11.Text = "PA3";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(107, 223);
            label10.Name = "label10";
            label10.Size = new Size(29, 17);
            label10.TabIndex = 21;
            label10.Text = "PA2";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(107, 197);
            label9.Name = "label9";
            label9.Size = new Size(29, 17);
            label9.TabIndex = 20;
            label9.Text = "PA1";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(107, 171);
            label8.Name = "label8";
            label8.Size = new Size(29, 17);
            label8.TabIndex = 19;
            label8.Text = "PA0";
            // 
            // PA7
            // 
            PA7.Location = new Point(81, 350);
            PA7.Name = "PA7";
            PA7.On = false;
            PA7.Size = new Size(20, 20);
            PA7.TabIndex = 18;
            // 
            // PA6
            // 
            PA6.Location = new Point(81, 324);
            PA6.Name = "PA6";
            PA6.On = false;
            PA6.Size = new Size(20, 20);
            PA6.TabIndex = 17;
            // 
            // PA5
            // 
            PA5.Location = new Point(81, 298);
            PA5.Name = "PA5";
            PA5.On = false;
            PA5.Size = new Size(20, 20);
            PA5.TabIndex = 16;
            // 
            // PA4
            // 
            PA4.Location = new Point(81, 272);
            PA4.Name = "PA4";
            PA4.On = false;
            PA4.Size = new Size(20, 20);
            PA4.TabIndex = 15;
            // 
            // PA3
            // 
            PA3.Location = new Point(81, 246);
            PA3.Name = "PA3";
            PA3.On = false;
            PA3.Size = new Size(20, 20);
            PA3.TabIndex = 14;
            // 
            // PA2
            // 
            PA2.Location = new Point(81, 220);
            PA2.Name = "PA2";
            PA2.On = false;
            PA2.Size = new Size(20, 20);
            PA2.TabIndex = 13;
            // 
            // PA1
            // 
            PA1.Location = new Point(81, 194);
            PA1.Name = "PA1";
            PA1.On = false;
            PA1.Size = new Size(20, 20);
            PA1.TabIndex = 12;
            // 
            // PA0
            // 
            PA0.Location = new Point(81, 168);
            PA0.Name = "PA0";
            PA0.On = false;
            PA0.Size = new Size(20, 20);
            PA0.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(81, 132);
            label6.Name = "label6";
            label6.Size = new Size(58, 25);
            label6.TabIndex = 10;
            label6.Text = "PINA";
            // 
            // RefreshButton
            // 
            RefreshButton.Enabled = false;
            RefreshButton.Location = new Point(333, 375);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(75, 23);
            RefreshButton.TabIndex = 9;
            RefreshButton.Text = "Refresh";
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(333, 132);
            label5.Name = "label5";
            label5.Size = new Size(72, 25);
            label5.TabIndex = 8;
            label5.Text = "PORTC";
            // 
            // PC7_CheckBox
            // 
            PC7_CheckBox.AutoSize = true;
            PC7_CheckBox.Location = new Point(274, 344);
            PC7_CheckBox.Name = "PC7_CheckBox";
            PC7_CheckBox.Size = new Size(47, 19);
            PC7_CheckBox.TabIndex = 7;
            PC7_CheckBox.Text = "PC7";
            PC7_CheckBox.UseVisualStyleBackColor = true;
            PC7_CheckBox.CheckedChanged += PC7_CheckBox_CheckedChanged;
            // 
            // PC6_CheckBox
            // 
            PC6_CheckBox.AutoSize = true;
            PC6_CheckBox.Location = new Point(274, 319);
            PC6_CheckBox.Name = "PC6_CheckBox";
            PC6_CheckBox.Size = new Size(47, 19);
            PC6_CheckBox.TabIndex = 6;
            PC6_CheckBox.Text = "PC6";
            PC6_CheckBox.UseVisualStyleBackColor = true;
            PC6_CheckBox.CheckedChanged += PC6_CheckBox_CheckedChanged;
            // 
            // PC5_CheckBox
            // 
            PC5_CheckBox.AutoSize = true;
            PC5_CheckBox.Location = new Point(274, 294);
            PC5_CheckBox.Name = "PC5_CheckBox";
            PC5_CheckBox.Size = new Size(47, 19);
            PC5_CheckBox.TabIndex = 5;
            PC5_CheckBox.Text = "PC5";
            PC5_CheckBox.UseVisualStyleBackColor = true;
            PC5_CheckBox.CheckedChanged += PC5_CheckBox_CheckedChanged;
            // 
            // PC4_CheckBox
            // 
            PC4_CheckBox.AutoSize = true;
            PC4_CheckBox.Location = new Point(274, 269);
            PC4_CheckBox.Name = "PC4_CheckBox";
            PC4_CheckBox.Size = new Size(47, 19);
            PC4_CheckBox.TabIndex = 4;
            PC4_CheckBox.Text = "PC4";
            PC4_CheckBox.UseVisualStyleBackColor = true;
            PC4_CheckBox.CheckedChanged += PC4_CheckBox_CheckedChanged;
            // 
            // PC3_CheckBox
            // 
            PC3_CheckBox.AutoSize = true;
            PC3_CheckBox.Location = new Point(274, 244);
            PC3_CheckBox.Name = "PC3_CheckBox";
            PC3_CheckBox.Size = new Size(47, 19);
            PC3_CheckBox.TabIndex = 3;
            PC3_CheckBox.Text = "PC3";
            PC3_CheckBox.UseVisualStyleBackColor = true;
            PC3_CheckBox.CheckedChanged += PC3_CheckBox_CheckedChanged;
            // 
            // PC2_CheckBox
            // 
            PC2_CheckBox.AutoSize = true;
            PC2_CheckBox.Location = new Point(274, 219);
            PC2_CheckBox.Name = "PC2_CheckBox";
            PC2_CheckBox.Size = new Size(47, 19);
            PC2_CheckBox.TabIndex = 2;
            PC2_CheckBox.Text = "PC2";
            PC2_CheckBox.UseVisualStyleBackColor = true;
            PC2_CheckBox.CheckedChanged += PC2_CheckBox_CheckedChanged;
            // 
            // PC1_CheckBox
            // 
            PC1_CheckBox.AutoSize = true;
            PC1_CheckBox.Location = new Point(274, 194);
            PC1_CheckBox.Name = "PC1_CheckBox";
            PC1_CheckBox.Size = new Size(47, 19);
            PC1_CheckBox.TabIndex = 1;
            PC1_CheckBox.Text = "PC1";
            PC1_CheckBox.UseVisualStyleBackColor = true;
            PC1_CheckBox.CheckedChanged += PC1_CheckBox_CheckedChanged;
            // 
            // PC0_CheckBox
            // 
            PC0_CheckBox.AutoSize = true;
            PC0_CheckBox.Location = new Point(274, 169);
            PC0_CheckBox.Name = "PC0_CheckBox";
            PC0_CheckBox.Size = new Size(47, 19);
            PC0_CheckBox.TabIndex = 0;
            PC0_CheckBox.Text = "PC0";
            PC0_CheckBox.UseVisualStyleBackColor = true;
            PC0_CheckBox.CheckedChanged += PC0_CheckBox_CheckedChanged;
            // 
            // sevenSeg_1
            // 
            sevenSeg_1.ColorBackground = Color.DarkGray;
            sevenSeg_1.ColorDark = Color.DimGray;
            sevenSeg_1.ColorLight = Color.Red;
            sevenSeg_1.CustomPattern = 119;
            sevenSeg_1.DecimalOn = false;
            sevenSeg_1.DecimalShow = false;
            sevenSeg_1.ElementWidth = 10;
            sevenSeg_1.ItalicFactor = 0F;
            sevenSeg_1.Location = new Point(355, 164);
            sevenSeg_1.Name = "sevenSeg_1";
            sevenSeg_1.Padding = new Padding(4);
            sevenSeg_1.Size = new Size(50, 70);
            sevenSeg_1.TabIndex = 1;
            sevenSeg_1.TabStop = false;
            sevenSeg_1.Value = "0";
            // 
            // PortLightsPage
            // 
            PortLightsPage.Controls.Add(groupBox1);
            PortLightsPage.Controls.Add(PotsGroup);
            PortLightsPage.Location = new Point(4, 24);
            PortLightsPage.Name = "PortLightsPage";
            PortLightsPage.Padding = new Padding(3);
            PortLightsPage.Size = new Size(533, 573);
            PortLightsPage.TabIndex = 2;
            PortLightsPage.Text = "Ports-Lights";
            PortLightsPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(LightPercentageLabel);
            groupBox1.Controls.Add(LightScrollBar);
            groupBox1.Controls.Add(LIGHT_LABEL);
            groupBox1.Controls.Add(LightGauge);
            groupBox1.Location = new Point(6, 288);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(373, 276);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Pots";
            // 
            // LightPercentageLabel
            // 
            LightPercentageLabel.AutoSize = true;
            LightPercentageLabel.Location = new Point(78, 233);
            LightPercentageLabel.Name = "LightPercentageLabel";
            LightPercentageLabel.Size = new Size(23, 15);
            LightPercentageLabel.TabIndex = 5;
            LightPercentageLabel.Text = "0%";
            // 
            // LightScrollBar
            // 
            LightScrollBar.Location = new Point(27, 51);
            LightScrollBar.Maximum = 110;
            LightScrollBar.Name = "LightScrollBar";
            LightScrollBar.Size = new Size(39, 201);
            LightScrollBar.TabIndex = 4;
            LightScrollBar.Value = 100;
            LightScrollBar.Scroll += LightScrollBar_Scroll;
            // 
            // LIGHT_LABEL
            // 
            LIGHT_LABEL.AutoSize = true;
            LIGHT_LABEL.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            LIGHT_LABEL.Location = new Point(208, 51);
            LIGHT_LABEL.Name = "LIGHT_LABEL";
            LIGHT_LABEL.Size = new Size(126, 25);
            LIGHT_LABEL.TabIndex = 3;
            LIGHT_LABEL.Text = "Light Output";
            // 
            // LightGauge
            // 
            LightGauge.BackColor = Color.Transparent;
            LightGauge.DialColor = Color.Lavender;
            LightGauge.DialText = "Light Meter";
            LightGauge.Glossiness = 11.363636F;
            LightGauge.Location = new Point(191, 79);
            LightGauge.Margin = new Padding(4, 3, 4, 3);
            LightGauge.MaxValue = 255F;
            LightGauge.MinValue = 0F;
            LightGauge.Name = "LightGauge";
            LightGauge.RecommendedValue = 0F;
            LightGauge.Size = new Size(175, 173);
            LightGauge.TabIndex = 1;
            LightGauge.ThresholdPercent = 0F;
            LightGauge.Value = 0F;
            // 
            // PotsGroup
            // 
            PotsGroup.Controls.Add(label17);
            PotsGroup.Controls.Add(label16);
            PotsGroup.Controls.Add(PotGauge2);
            PotsGroup.Controls.Add(PotGauge1);
            PotsGroup.Location = new Point(6, 6);
            PotsGroup.Name = "PotsGroup";
            PotsGroup.Size = new Size(373, 276);
            PotsGroup.TabIndex = 1;
            PotsGroup.TabStop = false;
            PotsGroup.Text = "Pots";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label17.Location = new Point(208, 51);
            label17.Name = "label17";
            label17.Size = new Size(131, 25);
            label17.TabIndex = 3;
            label17.Text = "Pot 2 Voltage";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label16.Location = new Point(27, 51);
            label16.Name = "label16";
            label16.Size = new Size(131, 25);
            label16.TabIndex = 2;
            label16.Text = "Pot 1 Voltage";
            // 
            // PotGauge2
            // 
            PotGauge2.BackColor = Color.Transparent;
            PotGauge2.DialColor = Color.Lavender;
            PotGauge2.DialText = "Potential Meter 2";
            PotGauge2.Glossiness = 11.363636F;
            PotGauge2.Location = new Point(191, 97);
            PotGauge2.Margin = new Padding(4, 3, 4, 3);
            PotGauge2.MaxValue = 5F;
            PotGauge2.MinValue = 0F;
            PotGauge2.Name = "PotGauge2";
            PotGauge2.RecommendedValue = 0F;
            PotGauge2.Size = new Size(175, 173);
            PotGauge2.TabIndex = 1;
            PotGauge2.ThresholdPercent = 0F;
            PotGauge2.Value = 0F;
            // 
            // PotGauge1
            // 
            PotGauge1.BackColor = Color.Transparent;
            PotGauge1.DialColor = Color.Lavender;
            PotGauge1.DialText = "Potential Meter 1";
            PotGauge1.Glossiness = 11.363636F;
            PotGauge1.Location = new Point(7, 97);
            PotGauge1.Margin = new Padding(4, 3, 4, 3);
            PotGauge1.MaxValue = 5F;
            PotGauge1.MinValue = 0F;
            PotGauge1.Name = "PotGauge1";
            PotGauge1.RecommendedValue = 0F;
            PotGauge1.Size = new Size(175, 173);
            PotGauge1.TabIndex = 0;
            PotGauge1.ThresholdPercent = 0F;
            PotGauge1.Value = 0F;
            // 
            // TempPage
            // 
            TempPage.Controls.Add(label25);
            TempPage.Controls.Add(TempLabel);
            TempPage.Controls.Add(label23);
            TempPage.Controls.Add(groupBox2);
            TempPage.Controls.Add(label22);
            TempPage.Controls.Add(TempPlot);
            TempPage.Controls.Add(label21);
            TempPage.Controls.Add(label20);
            TempPage.Controls.Add(label19);
            TempPage.Controls.Add(label18);
            TempPage.Controls.Add(KiSet);
            TempPage.Controls.Add(KpSet);
            TempPage.Controls.Add(SetPointTemp);
            TempPage.Location = new Point(4, 24);
            TempPage.Name = "TempPage";
            TempPage.Padding = new Padding(3);
            TempPage.Size = new Size(533, 573);
            TempPage.TabIndex = 3;
            TempPage.Text = "Temp Control";
            TempPage.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.BorderStyle = BorderStyle.Fixed3D;
            label25.ImageAlign = ContentAlignment.MiddleLeft;
            label25.Location = new Point(23, 296);
            label25.Name = "label25";
            label25.Size = new Size(15, 17);
            label25.TabIndex = 17;
            label25.Text = "0";
            // 
            // TempLabel
            // 
            TempLabel.AutoSize = true;
            TempLabel.BorderStyle = BorderStyle.Fixed3D;
            TempLabel.ImageAlign = ContentAlignment.MiddleLeft;
            TempLabel.Location = new Point(23, 245);
            TempLabel.Name = "TempLabel";
            TempLabel.Size = new Size(21, 17);
            TempLabel.TabIndex = 16;
            TempLabel.Text = "20";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(20, 270);
            label23.Name = "label23";
            label23.Size = new Size(105, 15);
            label23.TabIndex = 15;
            label23.Text = "Motor Speed [ % ]:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Location = new Point(9, 403);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(521, 164);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "Cloud Data Logging";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(dataLoggingStateLabel);
            groupBox4.Controls.Add(disableLoggingButton);
            groupBox4.Controls.Add(EnableLoggingButton);
            groupBox4.Location = new Point(242, 22);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(273, 135);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "Auto Data Logging";
            // 
            // dataLoggingStateLabel
            // 
            dataLoggingStateLabel.AutoSize = true;
            dataLoggingStateLabel.Location = new Point(88, 64);
            dataLoggingStateLabel.Name = "dataLoggingStateLabel";
            dataLoggingStateLabel.Size = new Size(97, 15);
            dataLoggingStateLabel.TabIndex = 2;
            dataLoggingStateLabel.Text = "Logging stopped";
            dataLoggingStateLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // disableLoggingButton
            // 
            disableLoggingButton.Enabled = false;
            disableLoggingButton.Location = new Point(6, 82);
            disableLoggingButton.Name = "disableLoggingButton";
            disableLoggingButton.Size = new Size(261, 23);
            disableLoggingButton.TabIndex = 1;
            disableLoggingButton.Text = "Stop Data Logging";
            disableLoggingButton.UseVisualStyleBackColor = true;
            // 
            // EnableLoggingButton
            // 
            EnableLoggingButton.Location = new Point(6, 38);
            EnableLoggingButton.Name = "EnableLoggingButton";
            EnableLoggingButton.Size = new Size(261, 23);
            EnableLoggingButton.TabIndex = 0;
            EnableLoggingButton.Text = "Enable Data Logging";
            EnableLoggingButton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(numericUpDown4);
            groupBox3.Controls.Add(InsertIntoTableButton);
            groupBox3.Location = new Point(14, 22);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(222, 135);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Manual Data Logging";
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(6, 40);
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(210, 23);
            numericUpDown4.TabIndex = 1;
            // 
            // InsertIntoTableButton
            // 
            InsertIntoTableButton.Location = new Point(6, 81);
            InsertIntoTableButton.Name = "InsertIntoTableButton";
            InsertIntoTableButton.Size = new Size(210, 23);
            InsertIntoTableButton.TabIndex = 0;
            InsertIntoTableButton.Text = "Insert Data into Table";
            InsertIntoTableButton.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(20, 221);
            label22.Name = "label22";
            label22.Size = new Size(106, 15);
            label22.TabIndex = 12;
            label22.Text = "Actual Temp [ °C ]:";
            // 
            // TempPlot
            // 
            TempPlot.BorderlineColor = SystemColors.Window;
            chartArea1.Name = "ChartArea1";
            TempPlot.ChartAreas.Add(chartArea1);
            TempPlot.Location = new Point(149, 41);
            TempPlot.Name = "TempPlot";
            series1.BorderColor = Color.FromArgb(64, 64, 64);
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Color = Color.FromArgb(192, 0, 0);
            series1.Name = "Temp";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Name = "Target";
            TempPlot.Series.Add(series1);
            TempPlot.Series.Add(series2);
            TempPlot.Size = new Size(372, 338);
            TempPlot.TabIndex = 11;
            TempPlot.Text = "chart1";
            title1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            title1.DockingOffset = 3;
            title1.Name = "TempTitle";
            title1.Text = "Temp (C°)";
            title2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title2.DockingOffset = -3;
            title2.Name = "SamplesTitle";
            title2.Text = "Samples";
            TempPlot.Titles.Add(title1);
            TempPlot.Titles.Add(title2);
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(47, 92);
            label21.Name = "label21";
            label21.Size = new Size(57, 15);
            label21.TabIndex = 6;
            label21.Text = "PI Tuning";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(33, 23);
            label20.Name = "label20";
            label20.Size = new Size(110, 15);
            label20.TabIndex = 5;
            label20.Text = "Setpoint Temp [ °C]";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(20, 141);
            label19.Name = "label19";
            label19.Size = new Size(17, 15);
            label19.TabIndex = 4;
            label19.Text = "Ki";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(20, 112);
            label18.Name = "label18";
            label18.Size = new Size(21, 15);
            label18.TabIndex = 3;
            label18.Text = "Kp";
            // 
            // KiSet
            // 
            KiSet.Location = new Point(47, 139);
            KiSet.Name = "KiSet";
            KiSet.Size = new Size(71, 23);
            KiSet.TabIndex = 2;
            KiSet.TextAlign = HorizontalAlignment.Center;
            // 
            // KpSet
            // 
            KpSet.Location = new Point(47, 110);
            KpSet.Name = "KpSet";
            KpSet.Size = new Size(71, 23);
            KpSet.TabIndex = 1;
            KpSet.TextAlign = HorizontalAlignment.Center;
            // 
            // SetPointTemp
            // 
            SetPointTemp.Location = new Point(47, 41);
            SetPointTemp.Name = "SetPointTemp";
            SetPointTemp.Size = new Size(71, 23);
            SetPointTemp.TabIndex = 0;
            SetPointTemp.TextAlign = HorizontalAlignment.Center;
            // 
            // PORTC_LIGHTS_TIMER
            // 
            PORTC_LIGHTS_TIMER.Interval = 10;
            PORTC_LIGHTS_TIMER.Tag = "DigitalPage";
            PORTC_LIGHTS_TIMER.Tick += PORTC_LIGHTS_TIMER_Tick;
            // 
            // POT1_TIMER
            // 
            POT1_TIMER.Interval = 12;
            POT1_TIMER.Tag = "LightsPage";
            POT1_TIMER.Tick += POT1_TIMER_Tick;
            // 
            // POT2_TIMER
            // 
            POT2_TIMER.Interval = 11;
            POT2_TIMER.Tag = "LightsPage";
            POT2_TIMER.Tick += POT2_TIMER_Tick;
            // 
            // LIGHT_TIMER
            // 
            LIGHT_TIMER.Interval = 10;
            LIGHT_TIMER.Tag = "LightsPage";
            LIGHT_TIMER.Tick += LIGHT_TIMER_Tick;
            // 
            // DATABASE_TIMER
            // 
            DATABASE_TIMER.Interval = 50;
            DATABASE_TIMER.Tag = "TempPage";
            DATABASE_TIMER.Tick += DATABASE_TIMER_Tick;
            // 
            // TEMP_TIMER
            // 
            TEMP_TIMER.Interval = 10;
            TEMP_TIMER.Tick += TEMP_TIMER_Tick;
            // 
            // BoardControlForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(558, 618);
            Controls.Add(TabController);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BoardControlForm";
            Text = "AUT Application Board Control - Robin Nowlan";
            SerialPortBox.ResumeLayout(false);
            SerialPortBox.PerformLayout();
            DatabaseGroup.ResumeLayout(false);
            DatabaseGroup.PerformLayout();
            TabController.ResumeLayout(false);
            SetupTabPage.ResumeLayout(false);
            DigitalPage.ResumeLayout(false);
            DigitalPage.PerformLayout();
            PortLightsPage.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            PotsGroup.ResumeLayout(false);
            PotsGroup.PerformLayout();
            TempPage.ResumeLayout(false);
            TempPage.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)TempPlot).EndInit();
            ((System.ComponentModel.ISupportInitialize)KiSet).EndInit();
            ((System.ComponentModel.ISupportInitialize)KpSet).EndInit();
            ((System.ComponentModel.ISupportInitialize)SetPointTemp).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox SerialPortBox;
        private ComboBox COM_Port_Dropdown;
        private Label COMPORTLable;
        private Button DisconnectButton;
        private Button ConnectButton;
        private Label PortStatusLabel;
        private GroupBox DatabaseGroup;
        private TabControl TabController;
        private TabPage SetupTabPage;
        private TabPage DigitalPage;
        private TabPage PortLightsPage;
        private TabPage TempPage;
        private ComboBox BaudrateSelection;
        private Label BaudRateLabel;
        private Button DatabaseDisconnectButton;
        private TextBox DatabaseTextBox;
        private Button DatabaseConnectButton;
        private TextBox PasswordTextBox;
        private TextBox UsernameTextBox;
        private ComboBox ServerNameSelection;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label6;
        private Button RefreshButton;
        private Label label5;
        private CheckBox PC7_CheckBox;
        private CheckBox PC6_CheckBox;
        private CheckBox PC5_CheckBox;
        private CheckBox PC4_CheckBox;
        private CheckBox PC3_CheckBox;
        private CheckBox PC2_CheckBox;
        private CheckBox PC1_CheckBox;
        private CheckBox PC0_CheckBox;
        private LedBulb SerialPortStatusBulb;
        private LedBulb ServerConnectionLED;
        private Label SerialConnectionErrorLabel;
        private Label label7;
        private LedBulb ledBulb4;
        private LedBulb ledBulb5;
        private LedBulb ledBulb3;
        private LedBulb ledBulb2;
        private LedBulb ledBulb6;
        private LedBulb ledBulb7;
        private LedBulb ledBulb8;
        private LedBulb ledBulb9;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private SevenSegment sevenSeg_1;
        private SevenSegment sevenSeg_2;
        private GroupBox PotsGroup;
        private Label label17;
        private Label label16;
        private AquaGauge PotGauge2;
        private AquaGauge PotGauge1;
        private System.Windows.Forms.Timer timer1;
        private LedBulb PA0;
        private LedBulb PA7;
        private LedBulb PA6;
        private LedBulb PA5;
        private LedBulb PA4;
        private LedBulb PA3;
        private LedBulb PA2;
        private LedBulb PA1;
        private System.Windows.Forms.Timer PORTC_LIGHTS_TIMER;
        private System.Windows.Forms.Timer POT1_TIMER;
        private System.Windows.Forms.Timer POT2_TIMER;
        private GroupBox groupBox1;
        private Label LIGHT_LABEL;
        private AquaGauge LightGauge;
        private System.Windows.Forms.Timer LIGHT_TIMER;
        private Label LightPercentageLabel;
        private VScrollBar LightScrollBar;
        private Label label21;
        private Label label20;
        private Label label19;
        private Label label18;
        private NumericUpDown KiSet;
        private NumericUpDown KpSet;
        private NumericUpDown SetPointTemp;
        private System.Windows.Forms.Timer DATABASE_TIMER;
        private System.Windows.Forms.DataVisualization.Charting.Chart TempPlot;
        private Label label22;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
        private NumericUpDown numericUpDown4;
        private Button InsertIntoTableButton;
        private Label dataLoggingStateLabel;
        private Button disableLoggingButton;
        private Button EnableLoggingButton;
        private Label label23;
        private Label label25;
        private Label TempLabel;
        private System.Windows.Forms.Timer TEMP_TIMER;
    }
}