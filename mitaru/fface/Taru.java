import fface.PartyMember;

class Taru {
	
	private native void print();

	private native void getItem();

	private native int createInstance(int pid);
	
	private native void sendString(int instance,String string);

	public static void main(String[] args) throws InterruptedException {
		int pid = Integer.parseInt(System.getenv("pid")==null?"3056":System.getenv("pid"));
		
		Taru taru = new Taru();
		int instance = taru.createInstance(pid);
		
		
		while (true) {
			byte[] partyMember = taru.GetPartyMember(instance, 0);
			PartyMember member = new PartyMember(partyMember);
			System.out.println(member.name);
			System.out.println(member.currentHP);
			System.out.println(String.format("id=%x", member.id));
			
			System.out.println(String.format("x=%f", taru.GetNPCPosX(instance,member.id)));
			System.out.println(String.format("z=%f", taru.GetNPCPosZ(instance,member.id)));
			Thread.sleep(1000);
		}
		//for (String string : args) {
		//	taru.sendString(instance,string);
		//}
		//System.out.println(taru.GetNPCPosX(instance, 1137));
		//System.out.println(taru.SetNPCPosH(instance, 1137, 0));
		
		//taru.CKHSetKey(instance, KeyCode.LetterA, true);
		//Thread.sleep(1000);
		//taru.CKHSetKey(instance, KeyCode.ForwardslashKEy, false);
		//Thread.sleep(1000);
	}

	static {
		System.loadLibrary("Taru");
	}

	private native float GetNPCPosX(int instance,int index);
	private native float GetNPCPosY(int instance,int index);
	private native float GetNPCPosZ(int instance,int index);
	private native boolean SetNPCPosH(int instance,int index,float value);
	private native void CKHSetKey(int instance,byte key,boolean down);
	private native byte[] GetPartyMember(int instance, int index);
	
}
