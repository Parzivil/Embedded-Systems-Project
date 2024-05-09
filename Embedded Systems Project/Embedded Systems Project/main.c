/*
 * main.c
 *
 * Created: 3/28/2024 10:37:11 AM
 *  Author: nfq0092
 */ 

#include <avr/io.h>
#include <avr/interrupt.h>

//Define Start and stop bytes for Rx/Tx
#define STOP 0xAA
#define START 0x53

//Read instructions
#define TXCHECK 0x00
#define READ_PINA 0x01
#define READ_POT1 0x02
#define READ_POT2 0x03
#define READ_TEMP 0x04
#define READ_LIGHT 0x05

//Set instructions
#define SET_PORTC 0x0A
#define SET_HEATER 0x0B
#define SET_LIGHT 0x0C
#define SET_MOTOR 0x0D

#define TX_RETURN 0x0F //Return the tx check byte

#define TX_EMPTY (UCSR1A & (1<<UDRE1))

#define PWM_FREQUENCY 50

#define ADC_CHANNEL_0 0x00
#define ADC_CHANNEL_1 0x01
#define ADC_CHANNEL_2 0x02
#define ADC_CHANNEL_3 0x03

#define ADMUX_DEFAULT 0x60 //ADMUX channel 0 

//ADC commands to change ports
#define ENABLE_LIGHT_ADC() (ADMUX = (ADMUX & ADMUX_DEFAULT) | ADC_CHANNEL_0) //ADC Channel 0 (Light)
#define ENABLE_POT2_ADC() (ADMUX = (ADMUX & ADMUX_DEFAULT) | ADC_CHANNEL_1)//ADC Channel 1 (POT_2)
#define ENABLE_POT1_ADC() (ADMUX = (ADMUX & ADMUX_DEFAULT) | ADC_CHANNEL_2) //ADC Channel 2 (POT_1)
#define ENABLE_TEMP_ADC() (ADMUX = (ADMUX & ADMUX_DEFAULT) | ADC_CHANNEL_3) //ADC Channel 3 (Temp)

#define ENABLE_HEATER() (TCCR1A |= 1<<2) //Macro function to enable the heater
#define DISABLE_HEATER() (TCCR1A &= ~1<<2)

#define ENABLE_FAN() (TCCR1A |= 1<<6) //Macro function to enable the fan
#define DISABLE_FAN() (TCCR1A &= ~1<<6)

#define ENABLE_LIGHT() (TCCR1A |= 1<<4) //Macro function to enable the light
#define DISABLE_LIGHT() (TCCR1A &= ~1<<4)

//States for receiving data via UART
#define StartByte 0
#define InstByte 1
#define FirstDataByte 2
#define SecondDataByte 3
#define StopByte 4


unsigned char conversionComplete = 0;
unsigned short conversion;

unsigned char Inst_Ready = 0; //Var to store if the instruction command has been recived

//Least significant byte, most significant byte
unsigned char LSB, MSB; //Chars to store the received data

unsigned char state; //Current receiving state

unsigned char INS = 0x00; //Char to store instruction received

volatile unsigned char* p_data; //Create pointer for data register


//Setup the MCU and its registers
void Setup(){
	state = StartByte; //Set the state to start
	
	cli(); //Clear interrupts
	sei(); //Enable interrupts
	
	//Configure USART
	UCSR1B |= (1<<4); //Enable USART Receiver
	UCSR1B |= (1<<3); //Enable USART Transmitter
	UCSR1B |= (1<<7); //Enable USART Receiver interrupt

	UCSR1C |= (1<<2) | (1<<1); //Set USART to 8 Bits per character
	
	UBRR1L = 12; //Set USART Baud rate to 38400
	
	DDRA = 0x00; //Set PORTA as input
	DDRC = 0xFF; //Set PORTC as output
	DDRB = 0b11100000;
	
	//Setup OCR 
	TCCR1A = 0b10100010; //Enable OCR1-ABC
	TCCR1B = 0b00011010; //8x pre-scaler default PWM mode

	ICR1 = PWM_FREQUENCY; //Set ICR to 20khz (50us)
	
	//Setup the ADC 
	ADMUX = 0b11100000;
	ADCSRA = 0b11101000;
	
	//Use switches	
	DDRE = 3;
	PORTE = 0x00;
	
	DISABLE_LIGHT();
	DISABLE_HEATER();
	DISABLE_FAN();	
}

//Send data to UDR1 register to be transmitted once the register is empty
void sendTo_tx(unsigned char data){
	while(!TX_EMPTY); //Wait for data register to be empty
	UDR1 = data; //Send the data to the tx register
}

//Send entire data stream
void Transmit_Stream(unsigned char instruction, unsigned char data_0, unsigned char data_1){
	sendTo_tx(START); //Send the start byte
	sendTo_tx(instruction); //Send the instruction byte
	sendTo_tx(data_0); //Send the first data byte
	sendTo_tx(data_1); //Send the second data byte
	sendTo_tx(STOP); //Send the stop byte
}

//Send a short
void Transmit_Short(unsigned char instruction, unsigned short data){
	sendTo_tx(START); //Send the start byte
	sendTo_tx(instruction); //Send the instruction byte

	unsigned char lsb = data; // to copy the first 8 bits.
	unsigned char msb = data >> 8; //Get the lat 8 bits
	
	sendTo_tx(msb); //Send the first data byte
	sendTo_tx(lsb); //Send the second data byte
	sendTo_tx(STOP); //Send the stop byte
}

//Send just one byte
void Transmit_Byte(unsigned char data){
	sendTo_tx(START); //Send the start byte
	sendTo_tx(data); //Send the data byte
	sendTo_tx(STOP); //Send the stop byte
}

void Set(unsigned char INS){
	switch(INS){
		case SET_PORTC:
			PORTC = 0x00; //Clear PORTC
			PORTC = LSB; //Set PORTC
			Transmit_Byte(SET_PORTC); //Send back instruction to confirm
		break;
		
		//Set Heater PWM to inputed data
		case SET_HEATER:
			if(LSB == 0 && MSB == 0) DISABLE_HEATER();
			else{
				ENABLE_HEATER();
				OCR1C = LSB << 8 | MSB;
			}
			Transmit_Short(SET_HEATER, OCR1C); //Send back instruction to confirm
		break;
		
		//Set Light PWM to inputed data
		case SET_LIGHT:
			if(LSB == 0 && MSB == 0) DISABLE_LIGHT();
			else{
				ENABLE_LIGHT();	
				OCR1B = LSB << 8 | MSB;
			}
			Transmit_Short(SET_LIGHT, OCR1B); //Send back instruction to confirm
		break;
		
		//Set Motor PWM to inputed data
		case SET_MOTOR:
			if(LSB == 0 && MSB == 0) {
				DISABLE_FAN();
				OCR1A = 0;
			}
			else{
		
				ENABLE_FAN();
				OCR1A = LSB << 8 | MSB;

			}
			Transmit_Short(SET_MOTOR, OCR1A); //Send back instruction to confirm
		break;
	}
}

unsigned short Read(unsigned char INS){
	//Read the switches
	if(INS == READ_PINA){
		return PINA; //Return the value of PORTA
	}
	
	//Read POT1
	else if(INS == READ_POT1){
		ENABLE_POT1_ADC(); //Set the ADC to convert POT1
		if(conversionComplete == 1) return conversion; //Check if the conversion is complete and return the conversion
		else return 0; //If conversion is not complete return 0
	}
	
	//Read POT2
	else if(INS == READ_POT2){
		ENABLE_POT2_ADC(); //Set the ADC to convert POT2
		if(conversionComplete == 1) return conversion; //Check if the conversion is complete and return the conversion
		else return 0;//If conversion is not complete return 0
	}
	
	//Read Temp sensor
	else if(INS == READ_TEMP){
		ENABLE_TEMP_ADC(); //Set the ADC to convert the temp sensor
		if(conversionComplete == 1) return conversion; //Check if the conversion is complete and return the conversion
		else return 0;//If conversion is not complete return 0
	}
	
	//Read Light sensor
	else if(INS == READ_LIGHT){
		ENABLE_LIGHT_ADC(); //Set the ADC to convert the light sensor
		if(conversionComplete == 1) return conversion; //Check if the conversion is complete and return the conversion
		else return 0;//If conversion is not complete return 0
	}

	else return 0xFFFF; //Return max number if no case is met as an error
}


int main(void) {
	Setup(); //Run the setup function
	while(1) { //Loop forever
		if(Inst_Ready == 1){
			if(INS == READ_PINA || INS == READ_LIGHT || INS == READ_TEMP || INS == READ_POT1 ||  INS == READ_POT2){
				Transmit_Short(INS, Read(INS)); //Read the sensor and transmit it
				
			}
			else if(INS == TXCHECK){
				Transmit_Byte(TX_RETURN); //Return the TX return value
			}
			
			else{
				Set(INS);
			}
			
			Inst_Ready = 0;
		}
	}
}

//Interrupt when data is received
ISR(USART1_RX_vect){
	*p_data = UDR1;
	
	switch(state){
		//Start Reading data
		case StartByte:
			if(*p_data == START){ //Check that the data starts with the start byte
				state = InstByte; //Change state to reading instruction
			}
		break;
		
		//Read instruction
		case InstByte:
			INS = *p_data;
			state = FirstDataByte;
		break;
		
		//Read the first data byte
		case FirstDataByte:
		
			//Case for when there is no data bytes
			if(*p_data == STOP){
				state = StartByte;
				Inst_Ready = 1;
				break;
			}
			
			LSB = *p_data;
			state = SecondDataByte;
		break;
		
		//Read the second data byte
		case SecondDataByte:
			MSB = *p_data;
			state = StopByte;
		break;
		
		//Stop reading
		case StopByte:
			state = StartByte;
			Inst_Ready = 1;
		break;	
	}
}

//When ADC conversion complete 
ISR(ADC_vect){
	conversion = ADCL*0x100u+ADCH; //Cast the two registers as a short 
	conversionComplete = 1; //Register the conversion as complete
}
