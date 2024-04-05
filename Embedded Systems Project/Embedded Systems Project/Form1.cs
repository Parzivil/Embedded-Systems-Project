using System.Globalization;
using System.IO.Ports;
using System.Text;


namespace Embedded_Systems_Project
{
    public partial class BoardControlForm : Form
    {
        public const Byte TXCHECK = 0x00;
        public const Byte READ_PINA = 0x01;
        public const Byte READ_POT1 = 0x02;
        public const Byte READ_POT2 = 0x03;
        public const Byte READ_TEMP = 0x04;
        public const Byte READ_LIGHT = 0x05;

        public const Byte SET_PORTC = 0x0A;
        public const Byte SET_HEATER = 0x0B;
        public const Byte SET_LIGHT = 0x0C;
        public const Byte SET_MOTOR = 0x0D;

        public const Byte START_BYTE = 0x53;
        public const Byte STOP_BYTE = 0xAA;

        private static Byte instructionByte, firstByte, secondByte;

        private static SerialPort serialPort = new(); //Create a new serial port

        private bool COM_Selected = false;
        private bool Baud_Selected = false;

        private ushort PORTC = 1;
        private char[] SevenSegDisplayChars = new char[2];


        public BoardControlForm()
        {
            InitializeComponent();

            string[] portNames = SerialPort.GetPortNames(); //Grab available ports

            int[] baudrates = { 4800, 9600, 19200, 38400, 57600, 115200, 230400, 460800, 921600 };

            foreach (string portName in portNames) COM_Port_Dropdown.Items.Add(portName); //Add ports to list

            foreach (int baudrate in baudrates) BaudrateSelection.Items.Add(baudrate); //Add Baud rates to list

        }


        //Connect to serial
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

                    //if (!testConnection()) throw new Exception("Connection Error");

                    if (serialPort.IsOpen) SerialPortStatusBulb.On = true; //Turn connection status bulb on
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
        /// Tests the connection with serial device by sending a test byte and waiting for the correct response
        /// </summary>
        /// <returns></returns>
        private bool testConnection()
        {
            writeSerial(TXCHECK);
            readSerial();
            if (firstByte == '0' && secondByte == 'f') return true;
            else return false;
        }

        /// <summary>
        /// Reads from the serial port
        /// </summary>
        public static void readSerial()
        {
            Byte[] input; //Create array to store the input data

            string bytes = serialPort.ReadTo("AA"); //Read the port to AA 
            input = Encoding.UTF8.GetBytes(bytes); //Convert to bytes in an array

            if (input.Length > 0 && (input[0] == START_BYTE))
            {
                instructionByte = input[1]; //Instruction byte
                if (input[2] != STOP_BYTE)
                {
                    firstByte = input[2];  //First data byte
                    secondByte = input[3]; //Second data byte
                }
            }
        }

        /// <summary>
        /// Sends just and instruction byte to the serial device
        /// </summary>
        /// <param name="instruction"></param>
        public static void writeSerial(Byte instruction)
        {
            string output = START_BYTE.ToString() + instruction.ToString() + STOP_BYTE.ToString();
            serialPort.Write(output);
        }

        /// <summary>
        /// Sends instruction and data bytes to the serial device
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="firstByte"></param>
        /// <param name="secondByte"></param>
        public static void writeSerial(Byte instruction, string firstByte, string secondByte)
        {
            string output = START_BYTE.ToString() + //Send the start byte
                            instruction.ToString() + //Send the instruction
                            firstByte + //Send the first byte
                            secondByte + //Send the second byte
                            STOP_BYTE.ToString();
            serialPort.Write(output);
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

        private void DigitalPage_Click(object sender, EventArgs e)
        {

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
            string portC_hex = PORTC.ToString("x"); //Convert to hex 

            portC_hex = portC_hex.PadLeft(2, '0'); //Add leading zeros

            SevenSegDisplayChars[0] = portC_hex[0];
            SevenSegDisplayChars[1] = portC_hex[1];
            sevenSeg_1.Value = SevenSegDisplayChars[0].ToString();
            sevenSeg_2.Value = SevenSegDisplayChars[1].ToString();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            writeSerial(SET_PORTC, SevenSegDisplayChars[0].ToString(), SevenSegDisplayChars[0].ToString());

            SentData.Text = SET_PORTC.ToString() + binToHex(SevenSegDisplayChars[0].ToString()) + binToHex(SevenSegDisplayChars[0].ToString());


        }

        private string binToHex(string bin)
        {
            return Convert.ToInt32(bin, 2).ToString("X");
        }
    }
}