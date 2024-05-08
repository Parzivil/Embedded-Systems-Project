using Bulb;
using System.IO.Ports;

namespace Embedded_Systems_Project
{
    public partial class BoardControlForm : Form
    {

        //Read instruction bytes
        public const byte TXCHECK = 0x00;
        public const byte READ_PINA = 0x01;
        public const byte READ_POT1 = 0x02;
        public const byte READ_POT2 = 0x03;
        public const byte READ_TEMP = 0x04;
        public const byte READ_LIGHT = 0x05;

        //Write instruction bytes
        public const byte SET_PORTC = 0x0A;
        public const byte SET_HEATER = 0x0B;
        public const byte SET_LIGHT = 0x0C;
        public const byte SET_MOTOR = 0x0D;

        //Start and stop instruction bytes
        public const byte START_BYTE = 0x53;
        public const byte STOP_BYTE = 0xAA;

        //byte variables to store the incoming bytes
        private static byte instructionByte, firstByte, secondByte;

        static SerialPort serialPort = new(); //Create a new serial port

        private bool COM_Selected = false;
        private bool Baud_Selected = false;
        private bool SerialConnected = false;

        private ushort PORTC = 0x0000;
        private char[] SevenSegDisplayChars = new char[2];

        private LedBulb[] PORTA_lights;

        /// <summary>
        /// Constructor for BoardControlForm Class
        /// </summary>
        public BoardControlForm()
        {
            InitializeComponent();

            PORTA_lights = new LedBulb[8] { PA0, PA1, PA2, PA3, PA4, PA5, PA6, PA7 };

            string[] portNames = SerialPort.GetPortNames(); //Grab available ports


            int[] baudrates = { 4800, 9600, 19200, 38400, 57600, 115200, 230400, 460800, 921600 };

            foreach (string portName in portNames) _ = COM_Port_Dropdown.Items.Add(portName); //Add ports to list

            foreach (int baudrate in baudrates) _ = BaudrateSelection.Items.Add(baudrate); //Add Baud rates to list

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
                                            Int32.Parse(BaudrateSelection.SelectedItem.ToString()));

                    serialPort.Open(); //Open the serial port

                    DisconnectButton.Enabled = true; //Enable the disconnect button
                    ConnectButton.Enabled = false; //Disable the connect button

                    RefreshButton.Enabled = true; //Enable the refresh button

                    if (serialPort.IsOpen)
                    {
                        SerialPortStatusBulb.On = true; //Turn connection status bulb on
                    }
                    else SerialPortStatusBulb.Blink(30); //Blink if there is an issue

                }
                catch (Exception ex)
                {
                    SerialConnectionErrorLabel.Text = "Connnection Error: " + ex.Message; //Show error message if conenction failed
                    SerialConnectionErrorLabel.Show(); //Show the error message
                }

            }

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
        private byte[] ReadSerialPackage()
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
                        if (currentByte == STOP_BYTE) return packageBytes.ToArray();
                    }
                }

                else break; //Break out of the loop if no bytes are found
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


            string output = System.Text.Encoding.Default.GetString(bytes);
            serialPort.WriteLine(output);
        }

        //Disconnect from serial
        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            serialPort.Close();
            SerialPortStatusBulb.On = false;

            DisconnectButton.Enabled = false;
            ConnectButton.Enabled = true;

            RefreshButton.Enabled = false;
        }

        private void COM_Port_Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            COM_Selected = true;

        }

        private void BaudrateSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            Baud_Selected = true;
        }

        //
        // Control the seven segment display
        //
        private void PC7_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC0_CheckBox.Checked) PORTC |= 1 << 7;
            else PORTC &= 0 << 7;

            refreshSevenSeg();

        }

        private void PC6_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC0_CheckBox.Checked) PORTC |= 1 << 6;
            else PORTC &= 0 << 6;

            refreshSevenSeg();
        }

        private void PC5_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC0_CheckBox.Checked) PORTC |= 1 << 5;
            else PORTC &= 0 << 5;

            refreshSevenSeg();
        }

        private void PC4_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC0_CheckBox.Checked) PORTC |= 1 << 4;
            else PORTC &= 0 << 4;

            refreshSevenSeg();
        }

        private void PC3_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC0_CheckBox.Checked) PORTC |= 1 << 3;
            else PORTC &= 0 << 3;

            refreshSevenSeg();
        }

        private void PC2_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC0_CheckBox.Checked) PORTC |= 1 << 2;
            else PORTC &= 0 << 2;

            refreshSevenSeg();
        }

        private void PC1_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC0_CheckBox.Checked) PORTC |= 1 << 1;
            else PORTC &= 0 << 1;

            refreshSevenSeg();
        }

        private void PC0_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PC0_CheckBox.Checked) PORTC |= 1 << 0;
            else PORTC &= 0 << 0;
            refreshSevenSeg();

        }

        /// <summary>
        /// Takes in the current binary value of the switches, 
        /// converts it to a hex value and displays it on the seven segment displays
        /// </summary>
        private void refreshSevenSeg()
        {
            string portC_hex = PORTC.ToString("X4"); //Convert to hex 
            sevenSeg_1.Value = portC_hex[portC_hex.Length-2].ToString();
            sevenSeg_2.Value = portC_hex[portC_hex.Length - 1].ToString();

            writeSerial(SET_PORTC, (byte)PORTC, (byte)PORTC);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            writeSerial(SET_PORTC, (byte)PORTC, (byte)PORTC);
        }

        private void Set_PORTA_Lights(char val)
        {
            int intValue = val;
            for (int i = 0; i < 8; i++)
            {
                PORTA_lights[i].On = ((intValue >> i) & 0x01) == 1; //Shift the value right and grab the last bit
            }
        }

        private void EnableGaugeTimers(bool enable)
        {
            if (enable)
            {
                //Enable the ports 
                POT1_TIMER.Enabled = true;
                POT2_TIMER.Enabled = true;
                LIGHT_TIMER.Enabled = true;

                PORTC_LIGHTS_TIMER.Enabled = false;
                SevenSegTimer.Enabled = false;
            }
           
            else
            {
                POT1_TIMER.Enabled = false;
                POT2_TIMER.Enabled = false;
                LIGHT_TIMER.Enabled = false;
            }

        }

        private void TabController_Selecting(object sender, EventArgs e)
        {
            TabPage currentTab = (sender as TabControl).SelectedTab;

            if (currentTab == PortLightsPage)
            {
                EnableGaugeTimers(true);
            }
            else if (currentTab == DigitalPage)
            {
                EnableGaugeTimers(false);
                PORTC_LIGHTS_TIMER.Enabled = true;
                SevenSegTimer.Enabled = true;
            }
        }

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
                float val = secondByte << 8 | firstByte;
                PotGauge1.Value = val / (0xFFFF / PotGauge2.MaxValue);


            }
        }

        private void POT2_TIMER_Tick(object sender, EventArgs e)
        {
            writeSerial(READ_POT2);
            ReadSerial();

            if (instructionByte == READ_POT2)
            {
                float val = secondByte << 8 | firstByte;
                PotGauge2.Value = val / (0xFFFF / PotGauge2.MaxValue);
            }
        }

        private void LIGHT_TIMER_Tick(object sender, EventArgs e)
        {
            int percentage = 100 - LightScrollBar.Value;
            LightPercentageLabel.Text = percentage.ToString() + "%";

            //Set the light value here::::

            writeSerial(READ_LIGHT);
            ReadSerial();

            if (instructionByte == READ_LIGHT)
            {
                float val = secondByte << 8 | firstByte;
                LightGauge.Value = val;
            }
        }

        private void SevenSegTimer_Tick(object sender, EventArgs e)
        {
            writeSerial(SET_PORTC, (byte)PORTC, (byte)PORTC);
        }
    }
}