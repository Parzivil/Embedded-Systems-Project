using Bulb;
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

        static SerialPort serialPort = new(); //Create a new serial port

        private bool COM_Selected = false;
        private bool Baud_Selected = false;
        private bool SerialConnected = false;

        private ushort PORTC = 1;
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

            foreach (string portName in portNames) COM_Port_Dropdown.Items.Add(portName); //Add ports to list

            foreach (int baudrate in baudrates) BaudrateSelection.Items.Add(baudrate); //Add Baud rates to list

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
                        PORTC_LIGHTS_TIMER.Enabled = true;
                        POT1_TIMER.Enabled = true;
                        POT2_TIMER.Enabled = true;
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
        /// Reads from the serial port
        /// </summary>
        private void ReadSerial()
        {

            string inputLine = serialPort.ReadExisting();

            
            if(inputLine != "") label18.Text = (string.Join("\n", System.Text.Encoding.Default.GetBytes(inputLine).Take(5)));



            byte[] input = Encoding.Default.GetBytes(inputLine);

            try
            {
                if (input.Length > 4 && (input[0] == START_BYTE))
                {
                    instructionByte = input[1]; //Instruction byte
                    firstByte = input[2];  //First data byte
                    secondByte = input[3]; //Second data byte
                }
            }
            catch { }

        }

        /// <summary>
        /// Sends just and instruction byte to the serial device
        /// </summary>
        /// <param name="instruction"></param>
        public static void writeSerial(Byte instruction)
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
        public static void writeSerial(Byte instruction, byte firstByte, byte secondByte)
        {
            byte[] bytes = {START_BYTE , //Send the start byte
                            instruction , //Send the instruction
                            firstByte , //Send the first byte
                            secondByte , //Send the second byte
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

            PORTC_LIGHTS_TIMER.Enabled = false;
            POT1_TIMER.Enabled = false;
            POT2_TIMER.Enabled = false;

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

                writeSerial(SET_PORTC, (byte)SevenSegDisplayChars[0], (byte)SevenSegDisplayChars[0]);
  

        }

        private void Set_PORTA_Lights(char val)
        {
            int intValue = (int)val;
            for (int i = 0; i < 8; i++)
            {
                if (((intValue >> i) & 0x01) == 1)
                {
                    PORTA_lights[i].On = true;
                }
                else
                {
                    PORTA_lights[i].On = false;
                }
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
                PotGauge1.Value = val;
                PotGauge1.Refresh();
            }
        }

        private void POT2_TIMER_Tick(object sender, EventArgs e)
        {
            writeSerial(READ_POT2);
            ReadSerial();

            if (instructionByte == READ_POT2)
            {
                float val = secondByte << 8 | firstByte;
                PotGauge2.Value = val;
                PotGauge2.Refresh();
            }
        }
    }
}