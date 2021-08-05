/*
Camada de Abstracao do Hardware (HAL) para execucao no Visual Studio
*/

#include "hal.h"
#include "FreeRTOS.h"
#include "task.h"
#include "queue.h"
#include <stdio.h>
#include <conio.h>

// IMPORTANTE: fique a vontade para criar outros metodos e variaveis

// Variaveis da Camada de Abstracao do Hardware (HAL)
boolean pinTurnSignal_LEFT;		// Representa o estado do pino de saida do microcontrolador ligado ao sinalizador ESQUERDO
boolean pinTurnSignal_RIGHT;	// Representa o estado do pino de saida do microcontrolador ligado ao sinalizador DIREITO
boolean pin_BREAK;				// Representa o estado do pino de saida do microcontrolador ligado ao Freio
tuCommand lastCommand;			// Armazena o estado atual dos sinalizadores. Veja tambem: tuCommand
xQueueHandle queue;

extern enum tdTurningCommands;

void Display();

void sendCommandToQueue() {
	void* c = (void*)&lastCommand;
	xQueueSendToBack(queue,c,0);
}

// Esta é a task que recebe os comandos do teclado e atualiza o valor de lastCommand
void task_Key(void *pParam){

	for (;;) {
		char key = 0;
		if (kbhit()) //Verifica se há um caracter no buffer
			key = getch(); //pega o proximo caracter no buffer
		switch (key) {
		case 'b':
			lastCommand.Breaks = 1 - lastCommand.Breaks;
			Display();
			sendCommandToQueue();
			break;
		case 'a':
			lastCommand.Alert = 1 - lastCommand.Alert;
			sendCommandToQueue();
			break;
		case 'i':
			lastCommand.Ignition = 1 - lastCommand.Ignition;
			Display();
			sendCommandToQueue();
			break;
		case 'l':
			lastCommand.TurnCommands = command_Left;
			sendCommandToQueue();
			break;
		case 'r':
			lastCommand.TurnCommands = command_Right;
			sendCommandToQueue();
			break;
		case ((char)32):
			lastCommand.TurnCommands = command_None;
			sendCommandToQueue();
			break;
		}

		vTaskDelay(100);
	}

}

// Inicializa a Camada de Abstracao de Hardware.
void InitHAL() {
	queue = xQueueCreate(10, sizeof(tuCommand));
	TurnSignalRight(FALSE);
	TurnSignalLeft(FALSE);
	xTaskCreate(task_Key, "Task keypress", 1000, NULL, 2, NULL);
}
// Metodo que retorna o estado da  alavanca de comando dos sinalizadores ("alavanda das setas junto ao volante"). Ver também tuCommand
tuCommand getTurnCommand() {
	return lastCommand;
}


// Mostra no console o valor atual das lampadas/piscas
void Display() {
	printf("L: %d  R: %d  B: %d   | I: %d | B: %d \r", pinTurnSignal_LEFT, pinTurnSignal_RIGHT,pin_BREAK,lastCommand.Ignition, lastCommand.Breaks);
}

/* Liga ou desliga o sinalizador direito.
- s: estado do sinalizador (TRUE = acende / FALSE = apaga)
*/
void TurnSignalRight(boolean s) {
	pinTurnSignal_RIGHT = s;
	Display();
}

/* Liga ou desliga o sinalizador esquerdo.
- s: estado do sinalizador (TRUE = acende / FALSE = apaga)
*/
void TurnSignalLeft(boolean s) {
	pinTurnSignal_LEFT = s;
	Display();
}

// Inverte o estado do sinalizador direito. Se estava apagado, acende. Se estava aceso, apaga.
void ToggleTurnSignalRight() {
	pinTurnSignal_RIGHT = !pinTurnSignal_RIGHT;
	Display();
}

// Inverte o estado do sinalizador direito. Se estava apagado, acende. Se estava aceso, apaga.
void ToggleTurnSignalLeft() {
	pinTurnSignal_LEFT = !pinTurnSignal_LEFT;
	Display();
}

//Retorna o valor atual da lampada/pisca esquerdo
boolean IsLeftON() {
	return pinTurnSignal_LEFT;
}

//Retorna o valor atual da lampada/pisca direito
boolean IsRightON() {
	return pinTurnSignal_RIGHT;
}

/* Liga ou desliga o Freio.
- s: estado do freio (TRUE = liga / FALSE = desliga)
*/
void TurnBreak(boolean s) {
	pin_BREAK = s;
	Display();
}
