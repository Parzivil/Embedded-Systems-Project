/*
 * main.c
 *
 * Created: 3/28/2024 10:37:11 AM
 *  Author: nfq0092
 */ 

#include <avr/io.h>
#include <avr/interrupt.h>

#define RX (PIND << 2)
#define TX (PIND << 3)

#define STOP 0xAA
#define START 0x53

#define TXCHECK 0x00
#define READ_PINA 0x01
#define READ_POT1 0x02
#define READ_POT2 0x03
#define READ_TEMP 0x04
#define READ_LIGHT 0x05

#define SET_PORTC 0x0A
#define SET_HEATER 0x0B
#define SET_LIGHT 0x0C
#define SET_MOTOR 0x0D

#define StartByte 0
#define InstByte 1
#define FirstDataByte 2
#define SecondDataByte 3
#define StopByte 4

char LSB, MSB;

char state = StartByte;

char txState = 0; //Set the state equals ready

char INST;
char* p_data; //Create pointer for data register

void Set(char INS){
	if(INS == SET_PORTC) {
		PORTC = 0x00; //Clear PORTC
		PORTC = LSB; //Set PORTC
	}
	else if(INS == SET_HEATER);
	else if(INS == SET_LIGHT);
	else if(INS == SET_MOTOR);
}

void Read(char INS){
	if(INS == READ_PINA){
		return PINA;
	}
	else if(INS == READ_POT1);
	else if(INS == READ_POT2);
	else if(INS == READ_TEMP);
	else if(INS == READ_LIGHT);
	
}

void Setup(){
	cli(); //Clear interrupts
	sei(); //Enable interrupts
	
	UCSR1B = 0b10011000;
	UCSR1C = 0b00000110; 
	UBRR1L = 12; //Set Baud rate to 38400
	
	DDRA = 0x00; //Set PORTA as input
	DDRC = 0xFF; //Set PORTC as output
	DDRB = 0b11100000;
}

int main(void)
{
	Setup();
	while(1)
	{
		if(INST ==  (READ_LIGHT|| READ_PINA || READ_TEMP || READ_POT1 || READ_POT2)){
			char data = Read(INST);
		} 
		else if(INST == TXCHECK){
			
		}
		else{
			Set(INST);
		}
		
	}
}

//Interrupt when data is received
ISR(USART1_RX_vect){
	*p_data = UDR1;
	
	switch(state){
		//Start Reading data
		case StartByte:
			if(*p_data = START){ //Check that the data starts with the start byte
				*p_data++; //Increment to the next byte
				state = InstByte; //Change state to reading instruction
			}
		break;
		
		//Read instruction
		case InstByte:
			INST = *p_data;
			if(INST == (READ_LIGHT || READ_PINA || READ_TEMP || READ_POT1 || READ_POT2)){
				state = StopByte;
			}
			else state = FirstDataByte;
			
			*p_data++; //Increment to the next byte
		break;
		
		//Read the first data byte
		case FirstDataByte:
			LSB = *p_data;
			*p_data++; //Increment to the next byte
			state = SecondDataByte;
		break;
		
		//Read the second data byte
		case SecondDataByte:
			MSB = *p_data;
			*p_data++; //Increment to the next byte
			state = StopByte;
		break;
		
		//Stop reading
		case StopByte:
			*p_data++; //Increment to the next byte
			state = StartByte;
		break;	
	}
}

