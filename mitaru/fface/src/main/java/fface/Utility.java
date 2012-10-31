package fface;

public class Utility {

	static String getNullTerminatedString(byte[] bytes,int offset) {
		int i = offset;
		while (bytes[i] != 0 && i < bytes.length) {
			i++;
		}
		return new String(bytes,offset,i-offset);
	}
	
}
