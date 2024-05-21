using Embedded_Systems_Project.Addons;
using MySql.Data.MySqlClient;
using System.IO.Ports;


namespace Embedded_Systems_Project
{
    public partial class BoardControlForm : Form
    {
        Database myDatabase = new(); //Responsible for controlling the database
        SerialController serialController = new(); //Responsible for controlling the 

        private static LiveGraphUpdater? graphUpdater;

        private bool COM_Selected = false;
        private bool Baud_Selected = false;
        private readonly bool SerialConnected = false;


        private int PORTC = 0x00;
        private readonly char[] SevenSegDisplayChars = new char[2];

        private readonly LedBulb[] PORTA_lights;
        public const ushort PWM_MAX = 0x0190; //This may need to be 399

        /// <summary>
        /// Constructor for BoardControlForm Class
        /// </summary>
        public BoardControlForm()
        {
            InitializeComponent();

            _ = ServerNameSelection.Items.Add(myDatabase.SERVER_NAME);
            ServerNameSelection.Text = myDatabase.SERVER_NAME;
            UsernameTextBox.Text = myDatabase.USER_NAME;
            PasswordTextBox.Text = myDatabase.PASSWORD_NAME;
            DatabaseTextBox.Text = myDatabase.DATABASE_NAME;

            TempPlot.Interaction.Disable(); //Prevent interaction with the plot chart

            PORTA_lights = new LedBulb[8] { PA0, PA1, PA2, PA3, PA4, PA5, PA6, PA7 };

            int[] baudrates = { 4800, 9600, 19200, 38400, 57600, 115200, 230400, 460800, 921600 };

            //Grab available ports
            foreach (string portName in serialController.portNames)
            {
                _ = COM_Port_Dropdown.Items.Add(portName); //Add ports to list
            }

            foreach (int baudrate in baudrates)
            {
                _ = BaudrateSelection.Items.Add(baudrate); //Add Baud rates to list
            }

            BaudrateSelection.Text = baudrates[3].ToString();

            graphUpdater = new LiveGraphUpdater(TempPlot);
            graphUpdater.StartUpdating(new double[0]);

        }

        /// <summary>
        /// Determines what page the user is on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabController_Selecting(object sender, EventArgs e)
        {
            TabPage currentTab = (sender as TabControl).SelectedTab;

            if (currentTab == DigitalPage)
            {
                EnableGaugeTimers(false);
                PORTC_LIGHTS_TIMER.Enabled = true;
                DATABASE_TIMER.Enabled = false;
            }
            else if (currentTab == PortLightsPage)
            {
                EnableGaugeTimers(true);
                DATABASE_TIMER.Enabled = false;
            }

            else if (currentTab == TempPage)
            {
                DATABASE_TIMER.Enabled = true;
            }
            else
            {
                EnableGaugeTimers(false);
                PORTC_LIGHTS_TIMER.Enabled = false;
                DATABASE_TIMER.Enabled = false;

            }
        }

        //Serial port Connection buttons
        /// <summary>
        /// Button event for connected to serial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            SerialConnectionErrorLabel.Hide(); //Hide any error messages when the user trys to connect

            //Check that a port and baud has been selected
            if (COM_Selected && Baud_Selected)
            {
                try
                {
                    //Set a new serial port
                    serialController.serialPort = new SerialPort(COM_Port_Dropdown.SelectedItem.ToString(),
                                            int.Parse(BaudrateSelection.SelectedItem.ToString()));

                    serialController.serialPort.Open(); //Open the serial port

                    DisconnectButton.Enabled = true; //Enable the disconnect button
                    ConnectButton.Enabled = false; //Disable the connect button

                    RefreshButton.Enabled = true; //Enable the refresh button

                    if (serialController.serialPort.IsOpen)
                    {
                        SerialPortStatusBulb.On = true; //Turn connection status bulb on
                    }
                    else
                    {
                        SerialPortStatusBulb.Blink(30); //Blink if there is an issue
                    }
                }
                catch (Exception ex)
                {
                    SerialConnectionErrorLabel.Text = "Connnection Error: " + ex.Message; //Show error message if conenction failed
                    SerialConnectionErrorLabel.Show(); //Show the error message
                }

            }

        }
        /// <summary>
        /// Disconnect from serial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            serialController.serialPort.Close();
            SerialPortStatusBulb.On = false;

            DisconnectButton.Enabled = false;
            ConnectButton.Enabled = true;

            RefreshButton.Enabled = false;

            EnableGaugeTimers(false);
            LIGHT_TIMER.Enabled = false;
        }

        //Database Conection buttons
        /// <summary>
        /// Runs when the database connect button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseConnectButton_Click(object sender, EventArgs e)
        {
            try
            {

                myDatabase.mySqlConnection = new MySqlConnection(myDatabase.connectionString);


                ServerConnectionLED.On = true;

                if (serialController.serialPort.IsOpen)
                {
                    DATABASE_TIMER.Enabled = true;
                }
            }
            catch (Exception)
            {
                ServerConnectionLED.On = false;
                DATABASE_TIMER.Enabled = false;
            }
        }
        /// <summary>
        /// Runs when the database disconnect button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseDisconnectButton_Click(object sender, EventArgs e)
        {
            myDatabase.mySqlConnection.Close();
            DATABASE_TIMER.Enabled = false;

            ServerConnectionLED.On = false;
        }


        /// <summary>
        /// Takes in the current binary value of the switches, 
        /// converts it to a hex value and displays it on the seven segment displays
        /// </summary>
        private void refreshSevenSeg()
        {
            string portC_hex = PORTC.ToString("X4"); //Convert to hex 
            sevenSeg_1.Value = portC_hex[^2].ToString(); //Set the first character
            sevenSeg_2.Value = portC_hex[^1].ToString(); //Set the second character

            serialController.writeSerial(serialController.SET_PORTC, (byte)PORTC, (byte)PORTC); //Write the values to the serial PORT
        }


        /// <summary>
        /// Sets all the values of the port to 0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            //Set all the check boxes to false
            foreach (CheckBox checkBox in new CheckBox[] { PC0_CheckBox, PC1_CheckBox, PC2_CheckBox, PC3_CheckBox, PC4_CheckBox, PC5_CheckBox, PC6_CheckBox, PC7_CheckBox })
            {
                checkBox.Checked = false;
            }

            PORTC = 0x00; //Clear PORTC values 

            refreshSevenSeg(); //Refresh the seven Segment display
        }

        private void Set_PORTA_Lights(char val)
        {
            int intValue = val; //Convert the char into an int
            //Loop through each light
            for (int i = 0; i < 8; i++)
            {
                PORTA_lights[i].On = ((intValue >> i) & 0x01) == 1; //Shift the value right and grab the last bit
            }
        }

        /// <summary>
        /// Enables the gauge timers 
        /// </summary>
        /// <param name="enable">Boolean whether the timers are enabled or not (true is enabled, false is not enabled)</param>
        private void EnableGaugeTimers(bool enable)
        {
            if (enable)
            {
                //Enable the ports 
                POT1_TIMER.Enabled = true;
                POT2_TIMER.Enabled = true;
                LIGHT_TIMER.Enabled = true;

                PORTC_LIGHTS_TIMER.Enabled = false;
            }

            else
            {
                POT1_TIMER.Enabled = false;
                POT2_TIMER.Enabled = false;
                LIGHT_TIMER.Enabled = false;
            }

        }

        //Timers

        private void PORTC_LIGHTS_TIMER_Tick(object sender, EventArgs e)
        {
            serialController.writeSerial(serialController.READ_PINA);
            serialController.ReadSerial();


            if (serialController.getInstruction() == serialController.READ_PINA)
            {
                Set_PORTA_Lights((char)serialController.getSecondByte());
            }

        }

        private void POT1_TIMER_Tick(object sender, EventArgs e)
        {
            serialController.writeSerial(serialController.READ_POT1);
            serialController.ReadSerial();

            if (serialController.getInstruction() == serialController.READ_POT1)
            {
                float val = (serialController.getSecondByte() << 8) | serialController.getSecondByte();
                PotGauge1.Value = val / (0xFFFF / PotGauge2.MaxValue);
            }
        }

        private void POT2_TIMER_Tick(object sender, EventArgs e)
        {
            serialController.writeSerial(serialController.READ_POT2);
            serialController.ReadSerial();

            if (serialController.getInstruction() == serialController.READ_POT2)
            {
                float val = (serialController.getSecondByte() << 8) | serialController.getSecondByte();
                PotGauge2.Value = val / (0xFFFF / PotGauge2.MaxValue);
            }
        }

        private void LightScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            int percentage = 100 - LightScrollBar.Value;
            if (percentage >= 0)
            {
                ushort value = (ushort)(percentage * PWM_MAX / 100);
                serialController.writeSerial(serialController.SET_LIGHT, value);
                serialController.ReadSerial(); //Clear the read buffer of the confirmation code

                LightPercentageLabel.Text = percentage.ToString() + "%" + " DEBUG: " + value.ToString("X") + "\n" + BitConverter.ToString(new byte[] { serialController.START_BYTE, serialController.getInstruction(), serialController.getFirstByte(), serialController.getSecondByte(), serialController.STOP_BYTE });
            }
            else
            {
                LightScrollBar.Value = 0;
            }
        }

        private void LIGHT_TIMER_Tick(object sender, EventArgs e)
        {
            serialController.writeSerial(serialController.READ_LIGHT);
            serialController.ReadSerial();

            if (serialController.getInstruction() == serialController.READ_LIGHT)
            {
                float val = serialController.getData();
                LightGauge.Value = val;
            }


        }

        private void DATABASE_TIMER_Tick(object sender, EventArgs e)
        {
            serialController.writeSerial(serialController.READ_TEMP);

            serialController.ReadSerial();

            if (serialController.getInstruction() == serialController.READ_TEMP)
            {
                serialController.TEMP_CONST = (float)TempConstantAdjuster.Value;
                char temp = serialController.getData();

                double tempF = temp * serialController.TEMP_CONST / 0xFFFF;


                graphUpdater.Update(tempF); //Plot the temp
                myDatabase.SendToDatabase((float)Math.Round(tempF, 2)); //Send the data to the database

                TempLabel.Text = tempF.ToString();

                Invalidate();
                Update();
            }
        }



        //Check box control
        private void PC7_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC7_CheckBox.Checked)
            {
                PORTC |= 1 << 7;
            }
            else
            {
                PORTC &= ~(1 << 7);
            }

            refreshSevenSeg();
        }
        private void PC6_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC6_CheckBox.Checked)
            {
                PORTC |= 1 << 6;
            }
            else
            {
                PORTC &= ~(1 << 6);
            }

            refreshSevenSeg();
        }
        private void PC5_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC5_CheckBox.Checked)
            {
                PORTC |= 1 << 5;
            }
            else
            {
                PORTC &= ~(1 << 5);
            }

            refreshSevenSeg();
        }
        private void PC4_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC4_CheckBox.Checked)
            {
                PORTC |= 1 << 4;
            }
            else
            {
                PORTC &= ~(1 << 4);
            }

            refreshSevenSeg();
        }
        private void PC3_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC3_CheckBox.Checked)
            {
                PORTC |= 1 << 3;
            }
            else
            {
                PORTC &= ~(1 << 3);
            }

            refreshSevenSeg();
        }
        private void PC2_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC2_CheckBox.Checked)
            {
                PORTC |= 1 << 2;
            }
            else
            {
                PORTC &= ~(1 << 2);
            }

            refreshSevenSeg();
        }
        private void PC1_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC1_CheckBox.Checked)
            {
                PORTC |= 1 << 1;
            }
            else
            {
                PORTC &= ~(1 << 1);
            }

            refreshSevenSeg();
        }
        private void PC0_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC0_CheckBox.Checked)
            {
                PORTC |= 1 << 0;
            }
            else
            {
                PORTC &= ~(1 << 0);
            }

            refreshSevenSeg();
        }

        //Dropdown validators
        private void COM_Port_Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            COM_Selected = true;
        }
        private void BaudrateSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            Baud_Selected = true;
        }
    }
}