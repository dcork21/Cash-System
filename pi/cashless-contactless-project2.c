#include <stdio.h>
#include <pthread.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <unistd.h>
#include <arpa/inet.h>
#include <netinet/in.h>
#include <sys/socket.h>
#include <string.h>

#define SIZE 512
#define PORT 6556 //This needs to be set on MVC App/C# App.
#define BUFLEN 512 

/*udp variables up here*/
struct sockaddr_in si_me, si_other;
int s, i, slen = sizeof(si_other);
int recv_len;
char buf[SIZE];

/*thread function definition*/
void* udpFunction(void* args)
{
	int received_bytes;

	if ((s = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP)) == -1)
	{
		printf("Failed to create socket\n\n");
	}

	memset((char*)&si_me, 0, sizeof(si_me));
	si_me.sin_family = AF_INET;
	si_me.sin_port = htons(PORT);
	si_me.sin_addr.s_addr = htonl(INADDR_ANY);

	memset((char*)&si_other, 0, sizeof(si_other));
	si_other.sin_family = AF_INET;
	si_other.sin_port = htons(PORT);
	si_other.sin_addr.s_addr = htonl(INADDR_ANY);

	if (bind(s, (struct sockaddr*) & si_me, sizeof(si_me)) == -1) printf("bind failed.\n\n");

	while (1)
	{
		received_bytes = recvfrom(s, buf, BUFLEN, 0, (struct sockaddr*)&si_other,(socklen_t*)&slen);
		if (received_bytes > 0) printf("Here is the udp message: %s\n", buf);

		//printf("I am udpFunction.\n");
		usleep(500);
	}
	close(s);
}

void* nfcFunction(void* args)
{
	while (1)
	{
		//printf("I am nfcFunction.\n");
		usleep(500);
	}
}

void* maintenenceFunction(void* args)
{
	while (1)
	{
		//printf("I am maintenenceFunction.\n");
		usleep(500);
	}
}


int main()
{
	/*creating thread id*/
	pthread_t udp_id;
	pthread_t nfc_id;
	pthread_t maintenence_id;
	int ret;

	/*most of initialisation udp code here*/

	/*creating thread*/
	ret = pthread_create(&udp_id, NULL, &udpFunction, NULL);
	if (ret == 0) {
		printf("udp Thread created successfully.\n");
	}
	else {
		printf("udp Thread not created.\n");
		return 0; /*return from main*/
	}

	ret = pthread_create(&nfc_id, NULL, &nfcFunction, NULL);
	if (ret == 0) {
		printf("nfc Thread created successfully.\n");
	}
	else {
		printf("nfc Thread not created.\n");
		return 0; /*return from main*/
	}

	ret = pthread_create(&maintenence_id, NULL, &maintenenceFunction, NULL);
	if (ret == 0) {
		printf("maintenence Thread created successfully.\n");
	}
	else {
		printf("maintenence Thread not created.\n");
		return 0; /*return from main*/
	}

	while (1)
	{
		printf("I am main function.\n");
		usleep(500);
	}

	return 0;
}
