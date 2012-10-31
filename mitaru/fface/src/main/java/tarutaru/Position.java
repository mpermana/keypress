package tarutaru;

public class Position {

	float x;
	float y;
	float z;
	
	public Position(float x, float y, float z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public float distanceTo(Position p) {
		float dx = p.x-x;
		float dz = p.z-z;
		return (float)Math.sqrt(dx*dx+dz*dz);
	}
	
	public String toString() {
		return String.format("%.3f,%.3f",x,z);
	}
	
}
