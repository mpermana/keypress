package fface;

public class ByteArrayReader {

	byte[] bytes;
	int i = 0;
	
	public ByteArrayReader(byte[] bytes) {
		this.bytes = bytes;
	}
	
	public byte readByte() {
		return bytes[i++];
	}
	
	public int readInt() {
		int temp = bytes[i++] & 0xff;
		temp |= (bytes[i++] & 0xff) << 8;
		temp |= (bytes[i++] & 0xff) << 16;
		temp |= (bytes[i++] & 0xff) << 24;
		return temp;
	}

	public String readString(int length) {
		String result = Utility.getNullTerminatedString(bytes,i);
		i += length;
		return result;
	}

	public short readShort() {
		return (short)(bytes[i++] + bytes[i++] << 8);
	}

	public void skip(int skip) {
		i+=skip;
	}
	
}
