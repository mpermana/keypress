package fface;

public class JNIFFACE {

	static {
		System.loadLibrary("jnifface");
	}

	protected native void print();

	protected native void getItem();

	protected native int createInstance(int pid);
	
	protected native void sendString(int instance,String string);
	protected native float GetNPCPosX(int instance,int index);
	protected native float GetNPCPosY(int instance,int index);
	protected native float GetNPCPosZ(int instance,int index);
	protected native boolean SetNPCPosH(int instance,int index,float value);
	protected native void CKHSetKey(int instance,byte key,boolean down);
	protected native byte[] GetPartyMember(int instance, int index);
	
}
