package utility;

import java.util.ArrayList;
import java.util.Scanner;

public class File {

	public static ArrayList<String> getText(String filename) {
		Scanner scan;
		scan = new Scanner(Class.class.getResourceAsStream(filename));
		scan.useDelimiter("\\n");
		ArrayList<String> list = new ArrayList<String>();
		while (scan.hasNext()) {
			list.add(scan.next());
		}
		return list;
	}
}
