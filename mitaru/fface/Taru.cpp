#include <windows.h>
#include <windef.h>
#include <stdio.h>

#include <jni.h>
 
#include "structures.h"
#define FFACE_API
#include "fface4.h"

 
extern "C" {
JNIEXPORT void JNICALL Java_HelloWorld_print
  (JNIEnv *, jobject);
 
}



JNIEXPORT void JNICALL Java_HelloWorld_print(JNIEnv *env, jobject obj)
{
	FFACE_API void *instance;
	instance = CreateInstance(3332);
	PLAYERINFO player;
	GetPlayerInfo(instance, &player);
	// printf("HPMax=%d\n",player.HPMax);
	// CKHSendString(instance,"Hello World!\n");
	return;
}
