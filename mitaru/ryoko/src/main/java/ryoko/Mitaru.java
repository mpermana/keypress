package ryoko;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.Serializable;
import java.util.Date;
import java.util.LinkedList;

public class Mitaru implements Serializable, Comparable<Mitaru> {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	public static String processDied = "";

	String command;
	LinkedList<String> inputStreamHistory = new LinkedList<String>();

	transient Process process;
	transient public Thread inputStreamReader;
	
	public static Janet janet;
	
	Date startDate;
	private String name = "";

	class StreamReader implements Runnable {

		@Override
		public void run() {
			String line;
			try {
				BufferedReader input = new BufferedReader(
						new InputStreamReader(process.getInputStream()));
				process.getErrorStream().close();
				while ((line = input.readLine()) != null) {
					System.out.println(line);
					inputStreamHistory.add(line);
					if (inputStreamHistory.size() > 1000)
						inputStreamHistory.pop();
					janet.onEvent(Mitaru.this,line);
				}
				input.close();
			} catch (Exception e) {
				e.printStackTrace();
				inputStreamHistory.clear();
				inputStreamHistory.add(e.getMessage());
			}
			System.out.println("exiting StreamReader");
			process = null;
			janet.onEvent(Mitaru.this,processDied);
		}

	}
	
	class ProcessCleaner implements Runnable {

		@Override
		public void run() {
			try {
				System.out.println("ProcessCleaner.waitFor()");
				process.waitFor();
				System.out.println("ProcessCleaner.waitFor() done");
			} catch (InterruptedException e) {
			}
			try {
				process.getInputStream().close();
			} catch (IOException e) {
			}
			try {
				process.getOutputStream().close();
			} catch (IOException e) {
			}
			try {
				process.getErrorStream().close();
			} catch (IOException e) {
			}
			
			//inputStreamReader.
			process = null;
		}
		
	}

	public void startInputStreamReader() {
		inputStreamReader = new Thread(new StreamReader());
		inputStreamReader.start();
		
	}

	public String getProcessStatus() {
		if (process == null) {
			return "stopped";
		} else {
			return "running";
		}
	}

	@Override
	public int compareTo(Mitaru mitaru) {
		boolean sameState = (this.process != null && mitaru.process != null) || (this.process == null && mitaru.process == null);  
		if (sameState) {
			if (startDate == null && mitaru.startDate == null) {
				return -command.compareTo(mitaru.command);
			}
			if (startDate == null)
				return 1;
			if (mitaru.startDate == null)
				return -1;
		}
		/*
		if (startDate == null && mitaru.startDate == null) {
			return command.compareTo(mitaru.command);
		}
		*/
//		if (this.process == null)
//			return 1;
//		else
//			return -1;
		if (startDate == null)
			return 1;
		if (mitaru.startDate == null)
			return -1;
		return -startDate.compareTo(mitaru.startDate);
	}

	public String getStartDate() {		
		return startDate == null?"":startDate.toString();
	}

	public String getName() {
		if (name == null) {
			name = "";
		}
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

}
