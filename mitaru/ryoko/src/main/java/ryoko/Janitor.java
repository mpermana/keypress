package ryoko;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;
import java.util.Iterator;

public class Janitor {

	static File executable = new File("mitaru.exe");
	
	public ArrayList<Mitaru> mitarus;

	Mitaru activeMitaru;

	public Janitor() {
		load();
	}

	public void addMitaru(String command) {
		Mitaru mitaru = new Mitaru();
		mitaru.command = command;
		mitarus.add(mitaru);
	}
	
	public void startMitaru() {
		if (null == activeMitaru)
			return;
		try {
			File tarutaru = new File("c:\\projects\\tarutaru");
			if (tarutaru.isDirectory()) {
				activeMitaru.process = Runtime.getRuntime().exec(
						executable.getAbsolutePath() + " " + activeMitaru.command, null, tarutaru);
			} else {
				activeMitaru.process = Runtime.getRuntime().exec(
					executable.getAbsolutePath() + " " + activeMitaru.command, null);
			}
			activeMitaru.startInputStreamReader();
			activeMitaru.startDate = new Date();
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		Collections.sort(mitarus);
	}
	
	public void stopMitaru() {
		if (null == activeMitaru)
			return;
		// activeMitaru.startDate = null;
		activeMitaru.process.destroy();
		activeMitaru.process = null;
	}

	public String[] getMitaru() {
		String[] result = new String[mitarus.size()];
		for (int i = 0; i < result.length; i++) {
			Mitaru mitaru = mitarus.get(i);
			result[i] = mitaru.command;
		}
		return result;
	}

	public void killMitaru(int selectionIndex) {
		if (-1 == selectionIndex)
			return;
		
		try {
		Mitaru mitaru = mitarus.get(selectionIndex);
		if (mitaru.process != null) {
			mitaru.process.destroy();
			mitaru.process = null;
		}
		
		mitarus.remove(selectionIndex);
		if (mitaru == activeMitaru)
			activeMitaru = null;
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public String getMitaruText(int selectionIndex) {
		Mitaru mitaru = mitarus.get(selectionIndex);
		StringBuilder sb = new StringBuilder();
		Iterator<String> i = mitaru.inputStreamHistory.iterator();
		while (i.hasNext()) {
			sb.append(i.next() + "\n");
		}
		return sb.toString();
	}

	void setActiveMitaru(int selectionIndex) {
		try {
			activeMitaru = mitarus.get(selectionIndex);
		} catch (Exception e) {
			
		}
	}

	public String getActiveMitaruText() {
		if (null == activeMitaru)
			return "no active mitaru";
		
		long start = System.currentTimeMillis();
		StringBuilder sb = new StringBuilder();
		// Iterator<String> i = activeMitaru.inputStreamHistory.iterator();
		String[] strings = activeMitaru.inputStreamHistory.toArray(new String[0]);
		for (String string : strings) {
			sb.append(string + "\n");
		}
//		while (i.hasNext()) {
//			sb.append(i.next() + "\n");
//		}
		long diff = System.currentTimeMillis() - start;
		String result = sb.toString();
		return result;
	}

	@SuppressWarnings("unchecked")
	void load() {
		this.mitarus = new ArrayList<Mitaru>();
		try {
			FileInputStream fis = new FileInputStream("savegame.dat");
			ObjectInputStream ois = new ObjectInputStream(fis);
			this.mitarus = (ArrayList<Mitaru>) ois.readObject();
			fis.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	void save() {
		try {
			FileOutputStream fos = new FileOutputStream("savegame.dat");
			ObjectOutputStream oos = new ObjectOutputStream(fos);
			oos.writeObject(this.mitarus);
			fos.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void stopAll() {		
		for (Mitaru mitaru : mitarus) {
			if (mitaru.process != null) {
				mitaru.process.destroy();
				mitaru.process = null;
			}
		}
	}
	
}
