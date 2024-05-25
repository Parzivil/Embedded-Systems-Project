using System.IO.Ports;

namespace Embedded_Systems_Project
{
    internal class SerialController
    {
        //Read instruction bytes
        public readonly byte TX_CHECK = 0x00;
        public readonly byte READ_PINA = 0x01;
        public readonly byte READ_POT1 = 0x02;
        public readonly byte READ_POT2 = 0x03;
        public readonly byte READ_TEMP = 0x04;
        public readonly byte READ_LIGHT = 0x05;
        private readonly byte TX_RETURN = 0x0F;

        //Write instruction bytes
        public readonly byte SET_PORTC = 0x0A;
        public readonly byte SET_HEATER = 0x0B;
        public readonly byte SET_LIGHT = 0x0C;
        public readonly byte SET_MOTOR = 0x0D;

        //Start and stop instruction bytes
        public readonly byte START_BYTE = 0x53;
        public readonly byte STOP_BYTE = 0xAA;

        //byte variables to store the incoming bytes
        private static byte instructionByte, firstByte, secondByte;

        //Create a serial port
        public SerialPort serialPort = new();

        public string[] portNames = SerialPort.GetPortNames();

        public float TEMP_CONST = 14247;

        public bool COM_Selected = false;
        public bool Baud_Selected = false;
        public readonly bool SerialConnected = false;

        public int PORTC = 0x00;
        public char[] SevenSegDisplayChars = new char[2];

        public Exception? Connect(string portName, int baudRate)
        {
            try
            {
                //Set a new serial port
                serialPort = new SerialPort(portName, baudRate);

                serialPort.Open(); //Open the serial port

                if (serialPort.IsOpen)
                {
                    return null;
                }
                else
                {
                    return new Exception("Error on connection: " + portName + " is not open");
                }
            }
            catch (Exception ex)
            {
                return new Exception("Connnection Error: " + ex.Message); //Show error message if conenction failed
            }
        }

        public byte Instruction
        {
            get { return instructionByte; }

        }

        public byte FirstByte
        {
            get { return firstByte; }
        }

        public byte SecondByte
        {
            get { return secondByte; }
        }

        public char getData()
        {
            return (char)((firstByte << 8) + secondByte);
        }

        //Read Serial functions
        /// <summary>
        /// Reads the serialPort byte values and stores it in global variables 
        /// </summary>
        public void ReadSerial()
        {
            if (serialPort.IsOpen)
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
        }
        /// <summary>
        /// Reads the serial port and outputs a byte array containing the data read
        /// </summary>
        /// <returns>returns a byte array containing the data from the serial port</returns>
        public byte[]? ReadSerialPackage()
        {
            List<byte> packageBytes = new(); //List to store bytes
            bool packageStartFound = false; //Boolen to store if the first byte is found

            if (serialPort.IsOpen)
            {
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
            }
            return null;
        }

        /// <summary>
        /// Reads the next serial package but discards the data to clear the RX buffer
        /// </summary>
        public void DiscardSerial()
        {
            if (serialPort.IsOpen) _ = ReadSerialPackage();
        }


        //Write serial functions
        /// <summary>
        /// Sends just and instruction byte to the serial device
        /// </summary>
        /// <param name="instruction"></param>
        public void writeSerial(byte instruction)
        {

            byte[] bytes = { START_BYTE, instruction, STOP_BYTE };
            string output = System.Text.Encoding.Default.GetString(bytes);

            if (serialPort.IsOpen) serialPort.Write(output);
        }
        /// <summary>
        /// Sends instruction and data bytes to the serial device
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="firstByte"></param>
        /// <param name="secondByte"></param>
        public void writeSerial(byte instruction, byte firstByte, byte secondByte)
        {
            byte[] bytes = {START_BYTE , //Send the start byte
                            instruction , //Send the instruction
                            firstByte , //Send the first byte
                            secondByte , //Send the second byte
                            STOP_BYTE };

            if(serialPort.IsOpen) serialPort.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Sends a short with an instruction byte (auto converts short into bytes)
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="val"></param>
        public void writeSerial(byte instruction, char val)
        {
            byte[] bytes = {START_BYTE , //Send the start byte
                            instruction , //Send the instruction
                            (byte)(val & 0xff), //Send the first byte
                            (byte)((val >> 8) & 0xff) , //Send the second byte
                            STOP_BYTE };


            //string output = System.Text.Encoding.Default.GetString(bytes);
            //serialPort.WriteLine(output);

            if (serialPort.IsOpen) serialPort.Write(bytes, 0, bytes.Length);
        }
    }
}
