
/* FreeRTOS.org includes. */
#include "FreeRTOS.h"
#include "task.h"
#include "queue.h"

// Demais includes
#include "basic_io.h"
#include "hal.h"
#include "sm.h"

//#include "driver/gpio.h"
//#include "esp_log.h"
//#include "esp_system.h"
// 
//Defines
#define DELAY 333.333 //((1000 / 1.5)/2);

TaskHandle_t *interface_handler = NULL;
TaskHandle_t *criaReq_handler = NULL;
TaskHandle_t *Req_handler = NULL;
TaskHandle_t *ouveServer_handler = NULL;
xTaskHandle id_interface;
xTaskHandle id_criaReq;
xTaskHandle id_Req;
xTaskHandle id_ouveServer;
extern xQueueHandle fila1;
extern xQueueHandle fila2;
//gpio_config_t io_conf;
#define GPIO_OUTPUT_IO_0    15
#define GPIO_OUTPUT_IO_1    16
#define GPIO_INPUT_IO_0     4
#define GPIO_INPUT_IO_1     5
// 
// 
//TASKS
void task_interface(void* params) {
	boolean led1;
	boolean led2;
	boolean botAcrescimo;
	boolean botDecrescimo;
	boolean botConfirm;
	//LCD
	//escreve fila 1
	//le fila 2
}

void task_criaReq(void* params) {
	//le fila 1 e cria tasks de requisicao com parametros de acordo com fila 1
	xTaskCreate(task_Req, "REQ1", 1000, NULL, 1, NULL);
	xTaskCreate(task_Req, "REQ2", 1000, NULL, 1, NULL);
}

void task_Req(void* params) {
	//MQTT para enviar info
	//MQTT colocar resposta na fila 2
}

void task_ouveServer(void* params) {
	//MQTT para adquirir info
	//Coloca info na fila 2 para task_interface ler
}


int main_(void)
{
	gpio_config_t io_conf;
	//set as output mode
	io_conf.mode = GPIO_MODE_OUTPUT;
	//bit mask of the pins that you want to set,e.g.GPIO15/16
	io_conf.pin_bit_mask = GPIO_OUTPUT_PIN_SEL;
	//disable pull-down mode
	io_conf.pull_down_en = 0;
	//disable pull-up mode
	io_conf.pull_up_en = 0;
	//configure GPIO with the given settings
	gpio_config(&io_conf);
	//bit mask of the pins, use GPIO4/5 here
	io_conf.pin_bit_mask = GPIO_INPUT_PIN_SEL;
	//set as input mode
	io_conf.mode = GPIO_MODE_INPUT;
	//configure GPIO with the given settings
	gpio_config(&io_conf);

	InitHAL();
	fila1 = xQueueCreate(10, sizeof(uint32));
	fila2 = xQueueCreate(10, sizeof(uint32));
	xTaskCreate(task_interface, "INTERFACE", 1000, NULL, 1, NULL);
	xTaskCreate(task_criaReq, "CRIA REQ", 1000, NULL, 1, NULL);
	xTaskCreate(task_ouveServer, "OUVE SERVER", 1000, NULL, 1, NULL);

	vTaskStartScheduler();
	for (;;);

	return 0;
}

