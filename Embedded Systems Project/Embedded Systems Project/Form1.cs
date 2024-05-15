using Embedded_Systems_Project.Addons;
using MySql.Data.MySqlClient;
using System.IO.Ports;

namespace Embedded_Systems_Project
{
    public partial class BoardControlForm : Form
    {
        /// <summary>
        /// Database Connection
        /// </summary>
        private MySqlConnection mySqlConnection;
        private MySqlDataReader mySqlDataReader;

        //Database Credentials
        private readonly string SERVER_NAME = "127.0.0.1";
        private readonly string USER_NAME = "ST123456";
        private readonly string DATABASE_NAME = "temperature_record";
        private readonly string PASSWORD_NAME = "ZJx(]8djn-3@.Q/u";
        private readonly string TABLE_NAME = "temperature";

        private static LiveGraphUpdater graphUpdater;
        //Read instruction bytes
        private const byte TXCHECK = 0x00;
        private const byte READ_PINA = 0x01;
        private const byte READ_POT1 = 0x02;
        private const byte READ_POT2 = 0x03;
        private const byte READ_TEMP = 0x04;
        private const byte READ_LIGHT = 0x05;

        //Write instruction bytes
        private const byte SET_PORTC = 0x0A;
        private const byte SET_HEATER = 0x0B;
        private const byte SET_LIGHT = 0x0C;
        private const byte SET_MOTOR = 0x0D;

        //Start and stop instruction bytes
        private const byte START_BYTE = 0x53;
        private const byte STOP_BYTE = 0xAA;

        private float TEMP_CONST = 26;

        //byte variables to store the incoming bytes
        private static byte instructionByte, firstByte, secondByte;
        private static SerialPort serialPort = new(); //Create a new serial port

        private bool COM_Selected = false;
        private bool Baud_Selected = false;
        private readonly bool SerialConnected = false;

        private int PORTC = 0x00;
        private readonly char[] SevenSegDisplayChars = new char[2];

        private readonly LedBulb[] PORTA_lights;

        /// <summary>
        /// Constructor for BoardControlForm Class
        /// </summary>
        public BoardControlForm()
        {
            InitializeComponent();

            _ = ServerNameSelection.Items.Add(SERVER_NAME);
            ServerNameSelection.Text = SERVER_NAME;
            UsernameTextBox.Text = USER_NAME;
            PasswordTextBox.Text = PASSWORD_NAME;
            DatabaseTextBox.Text = DATABASE_NAME;

            TempPlot.Interaction.Disable(); //Prevent interaction with the plot chart

            PORTA_lights = new LedBulb[8] { PA0, PA1, PA2, PA3, PA4, PA5, PA6, PA7 };

            string[] portNames = SerialPort.GetPortNames(); //Grab available ports

            int[] baudrates = { 4800, 9600, 19200, 38400, 57600, 115200, 230400, 460800, 921600 };

            foreach (string portName in portNames)
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
                    serialPort = new SerialPort(COM_Port_Dropdown.SelectedItem.ToString(),
                                            int.Parse(BaudrateSelection.SelectedItem.ToString()));

                    serialPort.Open(); //Open the serial port

                    DisconnectButton.Enabled = true; //Enable the disconnect button
                    ConnectButton.Enabled = false; //Disable the connect button

                    RefreshButton.Enabled = true; //Enable the refresh button

                    if (serialPort.IsOpen)
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
        /// Runs when the database connect button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "server=" + ServerNameSelection.SelectedItem + ";" +
                                            "user=" + UsernameTextBox.Text + ";" +
                                            "database=" + DatabaseTextBox.Text + ";" +
                                            "password=" + PasswordTextBox.Text + ";";
                mySqlConnection = new MySqlConnection(connectionString);


                ServerConnectionLED.On = true;

                if (serialPort.IsOpen)
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
            mySqlConnection.Close();
            DATABASE_TIMER.Enabled = false;
            ServerConnectionLED.On = false;
        }

        /// <summary>
        /// Reads the serialPort byte values and stores it in global variables 
        /// </summary>
        private void ReadSerial()
        {
            byte[] bytes = ReadSerialPackage();
            if (bytes != null)
            {
                if (bytes.Length > 4)
                {
                    instructionByte = bytes[1];
                    firstByte = bytes[2];
                    secondByte = bytes[3];
                }
                else if (bytes.Length >= 3)
                {
                    instructionByte = bytes[1];
                }
            }
        }

        /// <summary>
        /// Reads the serial port and outputs a byte array containing the data read
        /// </summary>
        /// <returns>returns a byte array containing the data from the serial port</returns>
        private byte[]? ReadSerialPackage()
        {
            List<byte> packageBytes = new(); //List to store bytes
            bool packageStartFound = false; //Boolen to store if the first byte is found

            //Consistantly reads the serial port for bytes
            for (int i = 0; i < 5; i++)
            {
                if (serialPort.BytesToRead > 0)
                {
                    byte currentByte = (byte)serialPort.ReadByte();

                    if (!packageStartFound && currentByte == START_BYTE)
                    {
                        packageStartFound = true;
                        packageBytes.Add(currentByte);
                    }

                    else if (packageStartFound)
                    {
                        packageBytes.Add(currentByte);
                        if (currentByte == STOP_BYTE)
                        {
                            return packageBytes.ToArray();
                        }
                    }
                }

                else
                {
                    break; //Break out of the loop if no bytes are found
                }
            }


            return null;
        }

        /// <summary>
        /// Sends just and instruction byte to the serial device
        /// </summary>
        /// <param name="instruction"></param>
        public static void writeSerial(byte instruction)
        {

            byte[] bytes = { START_BYTE, instruction, STOP_BYTE };
            string output = System.Text.Encoding.Default.GetString(bytes);

            serialPort.Write(output);
            serialPort.Write(output);

        }

        /// <summary>
        /// Sends instruction and data bytes to the serial device
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="firstByte"></param>
        /// <param name="secondByte"></param>
        public static void writeSerial(byte instruction, byte firstByte, byte secondByte)
        {
            byte[] bytes = {START_BYTE , //Send the start byte
                            instruction , //Send the instruction
                            firstByte , //Send the first byte
                            secondByte , //Send the second byte
                            STOP_BYTE };

            serialPort.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Sends a short with an instruction byte (auto converts short into bytes)
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="val"></param>
        public static void writeSerial(byte instruction, ushort val)
        {
            byte[] bytes = {START_BYTE , //Send the start byte
                            instruction , //Send the instruction
                            (byte)(val & 0xff), //Send the first byte
                            (byte)((val >> 8) & 0xff) , //Send the second byte
                            STOP_BYTE };


            //string output = System.Text.Encoding.Default.GetString(bytes);
            //serialPort.WriteLine(output);

            serialPort.Write(bytes, 0, bytes.Length);
        }

        //Disconnect from serial
        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            serialPort.Close();
            SerialPortStatusBulb.On = false;

            DisconnectButton.Enabled = false;
            ConnectButton.Enabled = true;

            RefreshButton.Enabled = false;

            EnableGaugeTimers(false);
            LIGHT_TIMER.Enabled = false;
        }

        private void COM_Port_Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            COM_Selected = true;

        }

        private void BaudrateSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            Baud_Selected = true;
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

            writeSerial(SET_PORTC, (byte)PORTC, (byte)PORTC); //Write the values to the serial PORT
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            //Set all the check boxes to false
            PC0_CheckBox.Checked = false;
            PC1_CheckBox.Checked = false;
            PC2_CheckBox.Checked = false;
            PC3_CheckBox.Checked = false;
            PC4_CheckBox.Checked = false;
            PC5_CheckBox.Checked = false;
            PC6_CheckBox.Checked = false;
            PC7_CheckBox.Checked = false;

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
            writeSerial(READ_PINA);
            ReadSerial();

            if (instructionByte == READ_PINA)
            {
                Set_PORTA_Lights((char)secondByte);
            }

        }

        private void POT1_TIMER_Tick(object sender, EventArgs e)
        {
            writeSerial(READ_POT1);
            ReadSerial();

            if (instructionByte == READ_POT1)
            {
                float val = (secondByte << 8) | firstByte;
                PotGauge1.Value = val / (0xFFFF / PotGauge2.MaxValue);
            }
        }

        private void POT2_TIMER_Tick(object sender, EventArgs e)
        {
            writeSerial(READ_POT2);
            ReadSerial();

            if (instructionByte == READ_POT2)
            {
                float val = (secondByte << 8) | firstByte;
                PotGauge2.Value = val / (0xFFFF / PotGauge2.MaxValue);
            }
        }

        private void LIGHT_TIMER_Tick(object sender, EventArgs e)
        {
            writeSerial(READ_LIGHT);
            ReadSerial();

            if (instructionByte == READ_LIGHT)
            {
                float val = ((secondByte << 8) | firstByte) / 0xFF;
                LightGauge.Value = val;
            }

            int percentage = 100 - LightScrollBar.Value;
            if (percentage >= 0)
            {
                ushort value = (ushort)(percentage * 0xFFFF / 100);
                LightPercentageLabel.Text = percentage.ToString() + "%" + " DEBUG: " + value;
                writeSerial(SET_LIGHT, value);
                ReadSerial(); //Clear the read buffer of the confirmation code
            }
            else
            {
                LightScrollBar.Value = 0;
            }
        }


        /// <summary>
        /// Sends data to the set database
        /// </summary>
        /// <param name="data"></param>
        private void SendToDatabase(float data)
        {

            mySqlConnection.Open();
            string Query = "insert into " + DATABASE_NAME + "." + TABLE_NAME + "(timeStamp,temperature,remark) values('"
                + DateTime.Now + "','" + data + "','" + USER_NAME + "');";
            MySqlCommand Command = new(Query, mySqlConnection);


            mySqlDataReader = Command.ExecuteReader(); //Exicute the command
            mySqlConnection.Close();

        }

        private void DATABASE_TIMER_Tick(object sender, EventArgs e)
        {
            writeSerial(READ_TEMP);
            ReadSerial();

            if (instructionByte == READ_TEMP)
            {
                TEMP_CONST = (float)TempConstantAdjuster.Value;
                char temp = (char)((firstByte << 8) + secondByte);

                writeSerial(TXCHECK);
                ReadSerial();

                double tempF = temp * TEMP_CONST / 0xFFFF;


                graphUpdater.Update(tempF); //Plot the temp
                SendToDatabase((float)Math.Round(tempF, 2)); //Send the data to the database

                TempLabel.Text = tempF.ToString();

                Invalidate();
                Update();
            }
        }

        //
        // Control the seven segment display
        //
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
    }
}