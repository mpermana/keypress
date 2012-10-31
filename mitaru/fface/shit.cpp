#include <windows.h>
#include <windef.h>
#include <stdio.h>
 
//#include "structures.h"
#define FFACE_API
#include "fface4.h"
#include "jni.h"

extern "C" {

JNIEXPORT void JNICALL
Java_fface_Taru_print(JNIEnv *env, jobject obj)
{
/*
	printf("shit");
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
Java_fface_Taru_createInstance(JNIEnv *env, jobject obj, jint pid) {
	return (jint)CreateInstance(pid);
}

JNIEXPORT void JNICALL
Java_fface_Taru_sendString(JNIEnv *env, jobject obj, void * instance, jstring string) {
	const char *nativeString = env->GetStringUTFChars(string, 0);
	CKHSendString(instance,(char *)nativeString);
	return;
}



JNIEXPORT void Java_fface_Taru_DeleteInstance(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT bool Java_fface_Taru_Access(JNIEnv *env, jobject obj,void * instance) { return false; }
JNIEXPORT void Java_fface_Taru_CTHCreateTextObject(JNIEnv *env, jobject obj,void * instance, jstring name) { }
JNIEXPORT void Java_fface_Taru_CTHDeleteTextObject(JNIEnv *env, jobject obj,void * instance, jstring name) { }
JNIEXPORT void Java_fface_Taru_CTHSetText(JNIEnv *env, jobject obj,void * instance, jstring name, jstring text) { }
JNIEXPORT void Java_fface_Taru_CTHSetVisibility(JNIEnv *env, jobject obj,void * instance, jstring name, bool visible) { }
JNIEXPORT void Java_fface_Taru_CTHSetFont(JNIEnv *env, jobject obj,void * instance, jstring name, jstring font, int height) { }
JNIEXPORT void Java_fface_Taru_CTHSetColor(JNIEnv *env, jobject obj,void * instance, jstring name, unsigned char a, unsigned char r, unsigned char g, unsigned char b) { }
JNIEXPORT void Java_fface_Taru_CTHSetLocation(JNIEnv *env, jobject obj,void * instance, jstring name, float x, float y) { }
JNIEXPORT void Java_fface_Taru_CTHSetBold(JNIEnv *env, jobject obj,void * instance, jstring name, bool bold) { }
JNIEXPORT void Java_fface_Taru_CTHSetItalic(JNIEnv *env, jobject obj,void * instance, jstring name, bool italic) { }
JNIEXPORT void Java_fface_Taru_CTHSetBGColor(JNIEnv *env, jobject obj,void * instance, jstring name, unsigned char a, unsigned char r, unsigned char g, unsigned char b) { }
JNIEXPORT void Java_fface_Taru_CTHSetBGBorderSize(JNIEnv *env, jobject obj,void * instance, jstring name, float pixels) { }
JNIEXPORT void Java_fface_Taru_CTHSetBGVisibility(JNIEnv *env, jobject obj,void * instance, jstring name, bool visible) { }
JNIEXPORT void Java_fface_Taru_CTHSetRightJustified(JNIEnv *env, jobject obj,void * instance, jstring name, bool justified) { }
JNIEXPORT void Java_fface_Taru_CTHFlushCommands(JNIEnv *env, jobject obj,void * instance) { }

JNIEXPORT void Java_fface_Taru_CKHSetKey(JNIEnv *env, jobject obj,void * instance, unsigned char key, bool down) 
{
	printf("key=%d\n",key);
	printf("key=%d\n",down);
 	CKHSetKey(instance,key,down);
}

JNIEXPORT void Java_fface_Taru_CKHBlockInput(JNIEnv *env, jobject obj,void * instance, bool block) { }
JNIEXPORT void Java_fface_Taru_CKHSendString(JNIEnv *env, jobject obj,void * instance, jstring string) { }
JNIEXPORT int Java_fface_Taru_CCHIsNewCommand(JNIEnv *env, jobject obj,void * instance) { return 0; }
JNIEXPORT int Java_fface_Taru_CCHGetArgCount(JNIEnv *env, jobject obj,void * instance) { return 0; }
JNIEXPORT void Java_fface_Taru_CCHGetArg(JNIEnv *env, jobject obj,void * instance, int index, jstring buffer) { }
JNIEXPORT void Java_fface_Taru_GetPlayerInfo(JNIEnv *env, jobject obj,void * instance, PLAYERINFO* PLAYER) { }
JNIEXPORT float Java_fface_Taru_GetCastMax(JNIEnv *env, jobject obj,void * instance) { return 1.23; }
JNIEXPORT float Java_fface_Taru_GetCastCountDown(JNIEnv *env, jobject obj,void * instance) { return 1.23; }
JNIEXPORT float Java_fface_Taru_GetCastPercent(JNIEnv *env, jobject obj,void * instance) { return 1.23; }
JNIEXPORT short Java_fface_Taru_GetCastPercentEx(JNIEnv *env, jobject obj,void * instance) { return 1.23; }
JNIEXPORT char Java_fface_Taru_GetViewMode(JNIEnv *env, jobject obj,void * instance) { return 'M'; }
JNIEXPORT char Java_fface_Taru_GetPlayerStatus(JNIEnv *env, jobject obj,void * instance) { return 'M'; }
JNIEXPORT char Java_fface_Taru_GetPLayerServerID(JNIEnv *env, jobject obj,void *Instanceance) { return 'M'; }
JNIEXPORT void Java_fface_Taru_GetAllianceInfo(JNIEnv *env, jobject obj,void * instance, ALLIANCEINFO* AI) { }

JNIEXPORT void Java_fface_Taru_GetPartyMembers(JNIEnv *env, jobject obj,void * instance, PARTYMEMBERS* ptm) 
{ 
}

JNIEXPORT jbyteArray Java_fface_Taru_GetPartyMember(JNIEnv *env, jobject obj,void * instance, char index)
{
	PARTYMEMBER partyMember;
	GetPartyMember(instance,index,&partyMember);
	printf("index=%d\n",index);
	printf("Name=%s\n",partyMember.Name);
	
	jbyteArray bArray = env->NewByteArray(sizeof(partyMember));
	env->SetByteArrayRegion(bArray,0,sizeof(partyMember),(const jbyte *)&partyMember);
	
	return bArray;
	
}

JNIEXPORT BOOL Java_fface_Taru_IsNewLine(JNIEnv *env, jobject obj,void * instance) { return true; }
JNIEXPORT void Java_fface_Taru_GetChatLineR(JNIEnv *env, jobject obj,void * instance, int index, void *buffer, int* size) { }
JNIEXPORT void Java_fface_Taru_GetChatLine(JNIEnv *env, jobject obj,void * instance, int index, void *buffer, int* size) { }
JNIEXPORT void Java_fface_Taru_GetChatLineEx(JNIEnv *env, jobject obj,void * instance, int index, void *buffer, int* size, CHATEXTRAINFO* ExtraInfo) { }
JNIEXPORT int Java_fface_Taru_GetChatLineCount(JNIEnv *env, jobject obj,void * instance) { return 69; }
JNIEXPORT short Java_fface_Taru_GetSpellRecast(JNIEnv *env, jobject obj,void * instance, short id) { return 69; }
JNIEXPORT char Java_fface_Taru_GetAbilityID(JNIEnv *env, jobject obj,void * instance, char index) { return 'M'; }
JNIEXPORT int Java_fface_Taru_GetAbilityRecast(JNIEnv *env, jobject obj,void * instance, char index) { return 69; }
JNIEXPORT int Java_fface_Taru_GetVanaUTC(JNIEnv *env, jobject obj,void * instance) { return 69; }
/*
JNIEXPORT bool Java_fface_Taru_IsNPCclaimed(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT int Java_fface_Taru_GetNPCclaimID(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT char Java_fface_Taru_GetNPCType(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT BOOL Java_fface_Taru_NPCIsActive(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT void Java_fface_Taru_GetNPCName(JNIEnv *env, jobject obj,void * instance, int index, void *buffer, int* size) { }
*/

JNIEXPORT float Java_fface_Taru_GetNPCPosX(JNIEnv *env, jobject obj,void *instance, int index)
{
	return GetNPCPosX(instance,index);
}
JNIEXPORT float Java_fface_Taru_GetNPCPosY(JNIEnv *env, jobject obj,void * instance, int index)
{
	return GetNPCPosY(instance,index);
}
JNIEXPORT float Java_fface_Taru_GetNPCPosZ(JNIEnv *env, jobject obj,void * instance, int index)
{
	return GetNPCPosZ(instance,index);
}

JNIEXPORT float Java_fface_Taru_GetNPCPosH(JNIEnv *env, jobject obj,void * instance, int index)
{
	return GetNPCPosH(instance,index);
}

JNIEXPORT bool Java_fface_Taru_SetNPCPosH(JNIEnv *env, jobject obj,void *instance, int index, float value)
{
	printf("SetNPCPosH %f",value);
	return SetNPCPosH(instance,index,value);
}


/*
JNIEXPORT char Java_fface_Taru_GetNPCHPP(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT short Java_fface_Taru_GetNPCTP(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT char Java_fface_Taru_GetNPCStatus(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT int Java_fface_Taru_GetNPCPetID(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT float Java_fface_Taru_GetNPCDistance(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT char Java_fface_Taru_GetNPCBit(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT double Java_fface_Taru_GetNPCHeadingToNPC(JNIEnv *env, jobject obj,void * instance, int index0, int index1) { }
JNIEXPORT void Java_fface_Taru_GetTargetInfo(JNIEnv *env, jobject obj,void * instance, TARGETINFO* Target) { }
JNIEXPORT BOOL Java_fface_Taru_SetTargetName(JNIEnv *env, jobject obj,void * instance, char name[], int Size) { }
JNIEXPORT BOOL Java_fface_Taru_SetTarget(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT char Java_fface_Taru_GetInventoryMax(JNIEnv *env, jobject obj,void * instance)  { }
JNIEXPORT char Java_fface_Taru_GetSafeMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_Taru_GetStorageMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_Taru_GetTempMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_Taru_GetLockerMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_Taru_GetSatchelMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_Taru_GetSackMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT INVENTORYITEM Java_fface_Taru_GetInventoryItem(JNIEnv *env, jobject obj,void * instance,  int index) { }
JNIEXPORT INVENTORYITEM Java_fface_Taru_GetSafeItem(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT INVENTORYITEM Java_fface_Taru_GetStorageItem(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT INVENTORYITEM Java_fface_Taru_GetTempItem(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT INVENTORYITEM Java_fface_Taru_GetLockerItem(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT INVENTORYITEM Java_fface_Taru_GetSatchelItem(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT INVENTORYITEM Java_fface_Taru_GetSackItem(JNIEnv *env, jobject obj,void * instance, int index) { }
JNIEXPORT void Java_fface_Taru_GetSelectedItemName(JNIEnv *env, jobject obj,void * instance, void *buffer, int* maxlength) { }
JNIEXPORT int Java_fface_Taru_GetSelectedItemNum(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_Taru_GetSelectedItemIndex(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_Taru_GetEquippedItemIndex(JNIEnv *env, jobject obj,void * instance, char slot) { }
JNIEXPORT TREASUREITEM Java_fface_Taru_GetTreasureItem(JNIEnv *env, jobject obj,void * instance, char index) { }
JNIEXPORT BOOL Java_fface_Taru_FishOnLine(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_Taru_GetRodPosition(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_Taru_GetFishHPMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_Taru_GetFishHP(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT short Java_fface_Taru_GetFishOnlineTime(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT short Java_fface_Taru_GetFishTimeout(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_Taru_GetFishID1(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_Taru_GetFishID2(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_Taru_GetFishID3(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_Taru_GetFishID4(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT short Java_fface_Taru_GetDialogID(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_Taru_GetDialogIndexCount(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT short Java_fface_Taru_GetDialogIndex(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT void Java_fface_Taru_GetDialogStrings(JNIEnv *env, jobject obj,void * instance, long buffer, int* size) { }
JNIEXPORT BOOL Java_fface_Taru_MenuIsOpen(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT void Java_fface_Taru_MenuName(JNIEnv *env, jobject obj,void * instance, long buffer, int* size) { }
JNIEXPORT void Java_fface_Taru_MenuSelection(JNIEnv *env, jobject obj,void * instance, long buffer, int* size) { }
JNIEXPORT void Java_fface_Taru_MenuHelp(JNIEnv *env, jobject obj,void * instance, long buffer, int* size) { }
JNIEXPORT char Java_fface_Taru_ShopQuantityMax(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT void Java_fface_Taru_GetNPCTradeInfo(JNIEnv *env, jobject obj,void * instance, TRADEINFO* TI) { }
JNIEXPORT BOOL Java_fface_Taru_IsSynthesis(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT int Java_fface_Taru_GetSearchTotalCount(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_Taru_GetSearchPageCount(JNIEnv *env, jobject obj,void * instance) { }
JNIEXPORT char Java_fface_Taru_GetSearchZone(JNIEnv *env, jobject obj,void * instance, short index) { }
JNIEXPORT char Java_fface_Taru_GetSearchMainJob(JNIEnv *env, jobject obj,void * instance, short index) { }
JNIEXPORT char Java_fface_Taru_GetSearchSubJob(JNIEnv *env, jobject obj,void * instance, short index) { }
JNIEXPORT char Java_fface_Taru_GetSearchMainlvl(JNIEnv *env, jobject obj,void * instance, short index) { }
JNIEXPORT char Java_fface_Taru_GetSearchSublvl(JNIEnv *env, jobject obj,void * instance, short index) { }
JNIEXPORT void Java_fface_Taru_GetSearchName(JNIEnv *env, jobject obj,void * instance, short index, long Buffer, int* BufferSize) { }
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
