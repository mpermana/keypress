package fface;

import tarutaru.PartyMember;
import tarutaru.Position;

public class FFACE {

	JNIFFACE jnifface = new JNIFFACE();
	int instance;
	PartyMember player;

	public FFACE(int pid) {
		instance = jnifface.createInstance(pid);
	}

	public void sendString(int instance, String string) {
		jnifface.sendString(instance, string);
	}

	public float GetNPCPosX(int index) {
		return jnifface.GetNPCPosX(instance, index);
	}

	public float GetNPCPosY(int index) {
		return jnifface.GetNPCPosY(instance, index);
	}

	public float GetNPCPosZ(int index) {
		return jnifface.GetNPCPosZ(instance, index);
	}

	public boolean SetNPCPosH(int index, float value) {
		return jnifface.SetNPCPosH(instance, index, value);
	}

	public void CKHSetKey(byte key, boolean down) {
		jnifface.CKHSetKey(instance, key, down);
	}

	public PartyMember GetPartyMember(int index) {
		PartyMember partyMember = createPartyMember(jnifface.GetPartyMember(
				instance, index));
		return partyMember;
	}

	public float GetPlayerPosX() {
		return GetNPCPosX(GetPlayer().id);
	}

	public float GetPlayerPosY() {
		return GetNPCPosY(GetPlayer().id);
	}

	public float GetPlayerPosZ() {
		return GetNPCPosZ(GetPlayer().id);
	}

	public Position GetPlayerPosition() {
		Position position = new Position(GetPlayerPosX(),GetPlayerPosY(),GetPlayerPosZ());
		return position;
	}

	synchronized private PartyMember GetPlayer() {
		if (player == null) {
			player = createPartyMember(jnifface.GetPartyMember(instance, 0));
		}
		return player;
	}

	PartyMember createPartyMember(byte[] bytes) {
		// 0 int pad0;
		// 4 char Index;
		// 5 char MemberNumber;
		// 6 char Name[18];
		// 24 int SvrID;
		// 28 int ID;
		// 32 int unknown0;
		// 36 int CurrentHP;
		// 40 int CurrentMP;
		// 44 int CurrentTP;
		// 48 char CurrentHPP;
		// 49 char CurrentMPP;
		// 50 short Zone;
		// 52 int pad1;
		// 56 unsigned int FlagMask;
		// 60 char pad2[20];
		// 80 int SvrIDDupe;
		// 84 char CurrentHPPDupe;
		// 85 char CurrentMPPDupe;
		// 86 char Active;
		// 87 char pad3;
		ByteArrayReader byteArrayReader = new ByteArrayReader(bytes);
		byteArrayReader.readInt();
		PartyMember partyMember = new PartyMember();
		partyMember.index = byteArrayReader.readByte();
		partyMember.memberNumber = byteArrayReader.readByte();
		partyMember.name = byteArrayReader.readString(18);
		partyMember.serverId = byteArrayReader.readInt();
		partyMember.id = byteArrayReader.readInt();
		byteArrayReader.readInt();
		partyMember.currentHP = byteArrayReader.readInt();
		partyMember.currentMP = byteArrayReader.readInt();
		partyMember.currentTP = byteArrayReader.readInt();
		partyMember.currentHPP = byteArrayReader.readByte();
		partyMember.currentMPP = byteArrayReader.readByte();
		partyMember.zone = byteArrayReader.readShort();
		byteArrayReader.readInt();
		partyMember.flag = byteArrayReader.readInt();
		byteArrayReader.skip(20);
		byteArrayReader.readInt();
		byteArrayReader.readByte();
		byteArrayReader.readByte();
		partyMember.active = byteArrayReader.readByte();
		return partyMember;
	}

}
