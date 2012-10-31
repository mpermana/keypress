package hotchick;

import tarutaru.PartyMember;
import tarutaru.Position;
import fface.FFACE;

class SaveRoute {
	
	private native void print();

	private native void getItem();

	private native int createInstance(int pid);
	
	private native void sendString(int instance,String string);

	public static void main(String[] args) throws InterruptedException {
		int pid = Integer.parseInt(System.getenv("pid")==null?"7360":System.getenv("pid"));
		//pid = 4472;
		
		
		FFACE fface = new FFACE(pid);
		
		PartyMember partyMember = fface.GetPartyMember(0);
		System.out.println(partyMember.name);
		System.out.println(String.format("id=%x", partyMember.id));
		
		Position previousPosition = fface.GetPlayerPosition();
		System.out.print(previousPosition+";");
		while (true) {
			Position position = fface.GetPlayerPosition();
			if (position.distanceTo(previousPosition) > 10) {
				System.out.print(position+";");
				previousPosition = position;
				Thread.sleep(1000);
			}
		}

	}

	static {
		System.loadLibrary("jnifface");
	}

	private native float GetNPCPosX(int instance,int index);
	private native float GetNPCPosY(int instance,int index);
	private native float GetNPCPosZ(int instance,int index);
	private native boolean SetNPCPosH(int instance,int index,float value);
	private native void CKHSetKey(int instance,byte key,boolean down);
	private native byte[] GetPartyMember(int instance, int index);
	
}
