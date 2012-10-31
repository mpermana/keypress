#include <windows.h>
#include <windef.h>
#include <stdio.h>
 
//#include "structures.h"
#define FFACE_API
#include "fface4.h"
#include "jni.h"

extern "C" {

JNIEXPORT void JNICALL
Java_fface_JNIFFACE_print(JNIEnv *env, jobject obj)
{
	printf("shit");
	/*
	FFACE_API void *instance;
	instance = CreateInstance(g_pid);
	PLAYERINFO player;
	GetPlayerInfo(instance, &player);
	printf("HPMax=%d\n",player.HPMax);

    CKHSendString(instance,"Hello World!\n");
    */
    return;
}

JNIEXPORT jint JNICALL
Java_fface_JNIFFACE_createInstance(JNIEnv *env, jobject obj, jint pid) {
	return (jint)CreateInstance(pid);
}

JNIEXPORT void JNICALL
Java_fface_JNIFFACE_sendString(JNIEnv *env, jobject obj, void * instance, jstring string) {
	const char *nativeString = env->GetStringUTFChars(string, 0);
	CKHSendString(instance,(char *)nativeString);
	return;
}



JNIEXPORT void Java_fface_JNIFFACE_DeleteInstance(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT bool Java_fface_JNIFFACE_Access(JNIEnv *env, jobject obj,void * instance) { return false; }
JNIEXPORT void Java_fface_JNIFFACE_CTHCreateTextObject(JNIEnv *env, jobject obj,void * instance, jstring name) { }
JNIEXPORT void Java_fface_JNIFFACE_CTHDeleteTextObject(JNIEnv *env, jobject obj,void * instance, jstring name) { }
JNIEXPORT void Java_fface_JNIFFACE_CTHSetText(JNIEnv *env, jobject obj,void * instance, jstring name, jstring text) { }
JNIEXPORT void Java_fface_JNIFFACE_CTHSetVisibility(JNIEnv *env, jobject obj,void * instance, jstring name, bool visible) { }
JNIEXPORT void Java_fface_JNIFFACE_CTHSetFont(JNIEnv *env, jobject obj,void * instance, jstring name, jstring font, int height) { }
JNIEXPORT void Java_fface_JNIFFACE_CTHSetColor(JNIEnv *env, jobject obj,void * instance, jstring name, unsigned char a, unsigned char r, unsigned char g, unsigned char b) { }
JNIEXPORT void Java_fface_JNIFFACE_CTHSetLocation(JNIEnv *env, jobject obj,void * instance, jstring name, float x, float y) { }
JNIEXPORT void Java_fface_JNIFFACE_CTHSetBold(JNIEnv *env, jobject obj,void * instance, jstring name, bool bold) { }
JNIEXPORT void Java_fface_JNIFFACE_CTHSetItalic(JNIEnv *env, jobject obj,void * instance, jstring name, bool italic) { }
JNIEXPORT void Java_fface_JNIFFACE_CTHSetBGColor(JNIEnv *env, jobject obj,void * instance, jstring name, unsigned char a, unsigned char r, unsigned char g, unsigned char b) { }
JNIEXPORT void Java_fface_JNIFFACE_CTHSetBGBorderSize(JNIEnv *env, jobject obj,void * instance, jstring name, float pixels) { }
JNIEXPORT void Java_fface_JNIFFACE_CTHSetBGVisibility(JNIEnv *env, jobject obj,void * instance, jstring name, bool visible) { }
JNIEXPORT void Java_fface_JNIFFACE_CTHSetRightJustified(JNIEnv *env, jobject obj,void * instance, jstring name, bool justified) { }
JNIEXPORT void Java_fface_JNIFFACE_CTHFlushCommands(JNIEnv *env, jobject obj,void * instance) { }

JNIEXPORT void Java_fface_JNIFFACE_CKHSetKey(JNIEnv *env, jobject obj,void * instance, unsigned char key, bool down) 
{
	printf("key=%d\n",key);
	printf("key=%d\n",down);
 	CKHSetKey(instance,key,down);
}

JNIEXPORT void Java_fface_JNIFFACE_CKHBlockInput(JNIEnv *env, jobject obj,void * instance, bool block) { }
JNIEXPORT void Java_fface_JNIFFACE_CKHSendString(JNIEnv *env, jobject obj,void * instance, jstring string) { }
JNIEXPORT int Java_fface_JNIFFACE_CCHIsNewCommand(JNIEnv *env, jobject obj,void * instance) { return 0; }
JNIEXPORT int Java_fface_JNIFFACE_CCHGetArgCount(JNIEnv *env, jobject obj,void * instance) { return 0; }
JNIEXPORT void Java_fface_JNIFFACE_CCHGetArg(JNIEnv *env, jobject obj,void * instance, int index, jstring buffer) { }
JNIEXPORT void Java_fface_JNIFFACE_GetPlayerInfo(JNIEnv *env, jobject obj,void * instance, PLAYERINFO* PLAYER) { }
JNIEXPORT float Java_fface_JNIFFACE_GetCastMax(JNIEnv *env, jobject obj,void * instance) { return 1.23; }
JNIEXPORT float Java_fface_JNIFFACE_GetCastCountDown(JNIEnv *env, jobject obj,void * instance) { return 1.23; }
JNIEXPORT float Java_fface_JNIFFACE_GetCastPercent(JNIEnv *env, jobject obj,void * instance) { return 1.23; }
JNIEXPORT short Java_fface_JNIFFACE_GetCastPercentEx(JNIEnv *env, jobject obj,void * instance) { return 1.23; }
JNIEXPORT char Java_fface_JNIFFACE_GetViewMode(JNIEnv *env, jobject obj,void * instance) { return 'M'; }
JNIEXPORT char Java_fface_JNIFFACE_GetPlayerStatus(JNIEnv *env, jobject obj,void * instance) { return 'M'; }
JNIEXPORT char Java_fface_JNIFFACE_GetPLayerServerID(JNIEnv *env, jobject obj,void *Instanceance) { return 'M'; }
JNIEXPORT void Java_fface_JNIFFACE_GetAllianceInfo(JNIEnv *env, jobject obj,void * instance, ALLIANCEINFO* AI) { }

JNIEXPORT void Java_fface_JNIFFACE_GetPartyMembers(JNIEnv *env, jobject obj,void * instance, PARTYMEMBERS* ptm) 
{ 
}

JNIEXPORT jbyteArray Java_fface_JNIFFACE_GetPartyMember(JNIEnv *env, jobject obj,void * instance, char index)
{
	PARTYMEMBER partyMember;
	GetPartyMember(instance,index,&partyMember);
	printf("index=%d\n",index);
	printf("Name=%s\n",partyMember.Name);
	
	jbyteArray bArray = env->NewByteArray(sizeof(partyMember));
	env->SetByteArrayRegion(bArray,0,sizeof(partyMember),(const jbyte *)&partyMember);
	
	return bArray;
	
}

JNIEXPORT BOOL Java_fface_JNIFFACE_IsNewLine(JNIEnv *env, jobject obj,void * instance) { return true; }
JNIEXPORT void Java_fface_JNIFFACE_GetChatLineR(JNIEnv *env, jobject obj,void * instance, int index, void *buffer, int* size) { }
JNIEXPORT void Java_fface_JNIFFACE_GetChatLine(JNIEnv *env, jobject obj,void * instance, int index, void *buffer, int* size) { }
JNIEXPORT void Java_fface_JNIFFACE_GetChatLineEx(JNIEnv *env, jobject obj,void * instance, int index, void *buffer, int* size, CHATEXTRAINFO* ExtraInfo) { }
JNIEXPORT int Java_fface_JNIFFACE_GetChatLineCount(JNIEnv *env, jobject obj,void * instance) { return 69; }
JNIEXPORT short Java_fface_JNIFFACE_GetSpellRecast(JNIEnv *env, jobject obj,void * instance, short id) { return 69; }
JNIEXPORT char Java_fface_JNIFFACE_GetAbilityID(JNIEnv *env, jobject obj,void * instance, char index) { return 'M'; }
JNIEXPORT int Java_fface_JNIFFACE_GetAbilityRecast(JNIEnv *env, jobject obj,void * instance, char index) { return 69; }
JNIEXPORT int Java_fface_JNIFFACE_GetVanaUTC(JNIEnv *env, jobject obj,void * instance) { return 69; }
/*
JNIEXPORT bool Java_fface_JNIFFACE_IsNPCclaimed(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT int Java_fface_JNIFFACE_GetNPCclaimID(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT char Java_fface_JNIFFACE_GetNPCType(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT BOOL Java_fface_JNIFFACE_NPCIsActive(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT void Java_fface_JNIFFACE_GetNPCName(JNIEnv *env, jobject obj,void * instance, int index, void *buffer, int* size) { }
*/

JNIEXPORT float Java_fface_JNIFFACE_GetNPCPosX(JNIEnv *env, jobject obj,void *instance, int index)
{
	return GetNPCPosX(instance,index);
}
JNIEXPORT float Java_fface_JNIFFACE_GetNPCPosY(JNIEnv *env, jobject obj,void * instance, int index)
{
	return GetNPCPosY(instance,index);
}
JNIEXPORT float Java_fface_JNIFFACE_GetNPCPosZ(JNIEnv *env, jobject obj,void * instance, int index)
{
	return GetNPCPosZ(instance,index);
}

JNIEXPORT float Java_fface_JNIFFACE_GetNPCPosH(JNIEnv *env, jobject obj,void * instance, int index)
{
	return GetNPCPosH(instance,index);
}

JNIEXPORT bool Java_fface_JNIFFACE_SetNPCPosH(JNIEnv *env, jobject obj,void *instance, int index, float value)
{
	printf("SetNPCPosH %f",value);
	return SetNPCPosH(instance,index,value);
}


/*
JNIEXPORT char Java_fface_JNIFFACE_GetNPCHPP(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT short Java_fface_JNIFFACE_GetNPCTP(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT char Java_fface_JNIFFACE_GetNPCStatus(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT int Java_fface_JNIFFACE_GetNPCPetID(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT float Java_fface_JNIFFACE_GetNPCDistance(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT char Java_fface_JNIFFACE_GetNPCBit(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT double Java_fface_JNIFFACE_GetNPCHeadingToNPC(JNIEnv *env, jobject obj,void * instance, int index0, int index1) { }
JNIEXPORT void Java_fface_JNIFFACE_GetTargetInfo(JNIEnv *env, jobject obj,void * instance, TARGETINFO* Target) { }
JNIEXPORT BOOL Java_fface_JNIFFACE_SetTargetName(JNIEnv *env, jobject obj,void * instance, char name[], int Size) { }
JNIEXPORT BOOL Java_fface_JNIFFACE_SetTarget(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT char Java_fface_JNIFFACE_GetInventoryMax(JNIEnv *env, jobject obj,void * instance)  { }
JNIEXPORT char Java_fface_JNIFFACE_GetSafeMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_JNIFFACE_GetStorageMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_JNIFFACE_GetTempMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_JNIFFACE_GetLockerMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_JNIFFACE_GetSatchelMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_JNIFFACE_GetSackMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT INVENTORYITEM Java_fface_JNIFFACE_GetInventoryItem(JNIEnv *env, jobject obj,void * instance,  int index) { }
JNIEXPORT INVENTORYITEM Java_fface_JNIFFACE_GetSafeItem(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT INVENTORYITEM Java_fface_JNIFFACE_GetStorageItem(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT INVENTORYITEM Java_fface_JNIFFACE_GetTempItem(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT INVENTORYITEM Java_fface_JNIFFACE_GetLockerItem(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT INVENTORYITEM Java_fface_JNIFFACE_GetSatchelItem(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT INVENTORYITEM Java_fface_JNIFFACE_GetSackItem(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT void Java_fface_JNIFFACE_GetSelectedItemName(JNIEnv *env, jobject obj,void * instance, void *buffer, int* maxlength) { }
JNIEXPORT int Java_fface_JNIFFACE_GetSelectedItemNum(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_JNIFFACE_GetSelectedItemIndex(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_JNIFFACE_GetEquippedItemIndex(JNIEnv *env, jobject obj,void * instance, char slot) { }
JNIEXPORT TREASUREITEM Java_fface_JNIFFACE_GetTreasureItem(JNIEnv *env, jobject obj,void * instance, char index) { }
JNIEXPORT BOOL Java_fface_JNIFFACE_FishOnLine(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_JNIFFACE_GetRodPosition(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_JNIFFACE_GetFishHPMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_JNIFFACE_GetFishHP(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT short Java_fface_JNIFFACE_GetFishOnlineTime(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT short Java_fface_JNIFFACE_GetFishTimeout(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_JNIFFACE_GetFishID1(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_JNIFFACE_GetFishID2(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_JNIFFACE_GetFishID3(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_JNIFFACE_GetFishID4(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT short Java_fface_JNIFFACE_GetDialogID(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_JNIFFACE_GetDialogIndexCount(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT short Java_fface_JNIFFACE_GetDialogIndex(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT void Java_fface_JNIFFACE_GetDialogStrings(JNIEnv *env, jobject obj,void * instance, long buffer, int* size) { }
JNIEXPORT BOOL Java_fface_JNIFFACE_MenuIsOpen(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT void Java_fface_JNIFFACE_MenuName(JNIEnv *env, jobject obj,void * instance, long buffer, int* size) { }
JNIEXPORT void Java_fface_JNIFFACE_MenuSelection(JNIEnv *env, jobject obj,void * instance, long buffer, int* size) { }
JNIEXPORT void Java_fface_JNIFFACE_MenuHelp(JNIEnv *env, jobject obj,void * instance, long buffer, int* size) { }
JNIEXPORT char Java_fface_JNIFFACE_ShopQuantityMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT void Java_fface_JNIFFACE_GetNPCTradeInfo(JNIEnv *env, jobject obj,void * instance, TRADEINFO* TI) { }
JNIEXPORT BOOL Java_fface_JNIFFACE_IsSynthesis(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_JNIFFACE_GetSearchTotalCount(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_JNIFFACE_GetSearchPageCount(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_JNIFFACE_GetSearchZone(JNIEnv *env, jobject obj,void * instance, short index) { }
JNIEXPORT char Java_fface_JNIFFACE_GetSearchMainJob(JNIEnv *env, jobject obj,void * instance, short index) { }
JNIEXPORT char Java_fface_JNIFFACE_GetSearchSubJob(JNIEnv *env, jobject obj,void * instance, short index) { }
JNIEXPORT char Java_fface_JNIFFACE_GetSearchMainlvl(JNIEnv *env, jobject obj,void * instance, short index) { }
JNIEXPORT char Java_fface_JNIFFACE_GetSearchSublvl(JNIEnv *env, jobject obj,void * instance, short index) { }
JNIEXPORT void Java_fface_JNIFFACE_GetSearchName(JNIEnv *env, jobject obj,void * instance, short index, long Buffer, int* BufferSize) { }
*/

}

int main(int argc,char *argv[])
{
	FFACE_API void *instance;
	instance = CreateInstance(8680);
	PLAYERINFO player;
	GetPlayerInfo(instance, &player);
	printf("HPMax=%d\n",player.HPMax);

	//CKHSendString(instance,"Test");

	for (int i=0;i<=80;i++) {
		INVENTORYITEM item;
		item = GetInventoryItem(instance,i);
		printf("id=%d\n",item.id);
		printf("index=%d\n",item.index);
		printf("count=%d\n",item.count);
	}
}
