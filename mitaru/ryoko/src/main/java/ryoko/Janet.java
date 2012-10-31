package ryoko;

// http://www.eclipse.org/articles/article.php?file=Article-Understanding-Layouts/index.html

import org.eclipse.swt.SWT;
import org.eclipse.swt.events.DisposeEvent;
import org.eclipse.swt.events.DisposeListener;
import org.eclipse.swt.events.SelectionAdapter;
import org.eclipse.swt.events.SelectionEvent;
import org.eclipse.swt.graphics.Color;
import org.eclipse.swt.graphics.Font;
import org.eclipse.swt.layout.FillLayout;
import org.eclipse.swt.layout.GridData;
import org.eclipse.swt.layout.GridLayout;
import org.eclipse.swt.widgets.Button;
import org.eclipse.swt.widgets.Combo;
import org.eclipse.swt.widgets.Composite;
import org.eclipse.swt.widgets.Display;
import org.eclipse.swt.widgets.Event;
import org.eclipse.swt.widgets.Group;
import org.eclipse.swt.widgets.Label;
import org.eclipse.swt.widgets.Listener;
import org.eclipse.swt.widgets.Shell;
import org.eclipse.swt.widgets.Table;
import org.eclipse.swt.widgets.TableColumn;
import org.eclipse.swt.widgets.TableItem;
import org.eclipse.swt.widgets.Text;

import utility.File;

public class Janet {

	Color green = display.getSystemColor(SWT.COLOR_GREEN);
	Color red = display.getSystemColor(SWT.COLOR_RED);
	Color blue = display.getSystemColor(SWT.COLOR_BLUE);
	Color white = display.getSystemColor(SWT.COLOR_WHITE);
	Color gray = display.getSystemColor(SWT.COLOR_GRAY);

	Text commandArguments;
	Combo commandTemplate;
	Text consoleText;
	Text commandName;

	Text taruName;
	Text taruMobs;
	Button start;
	Button stop;
	Table table;

	Janitor janitor = new Janitor();

	static Display display;

	public Janet() {
		Mitaru.janet = this;
	}

	public void onEvent(final Mitaru mitaru, final String line) {
		display.syncExec(new Runnable() {

			@Override
			public void run() {
				if (line == Mitaru.processDied) {
					resetTable();
				} else if (mitaru == janitor.activeMitaru) {
					updateConsoleText(line);
				}
			}

		});
	}

	public void resetTable() {
		table.setItemCount(janitor.mitarus.size());
		table.clearAll();
	}
	
	public static void main(String[] args) {
		display = new Display();
		Janet janet = new Janet();

		Shell shell = janet.createShell(display);
		shell.open();
		while (!shell.isDisposed()) {
			if (!display.readAndDispatch())
				display.sleep();
		}
		janet.janitor.save();
	}

	public Shell createShell(final Display display) {
		final Shell shell = new Shell(display);
		shell.setText("Mitaru Launcher");

		GridLayout gridLayout = new GridLayout();
		gridLayout.numColumns = 3;
		shell.setLayout(gridLayout);

		GridData gridData;

		// group row
		Group groupTemplate = new Group(shell, SWT.NONE);
		groupTemplate.setText("Command Template");
		gridLayout = new GridLayout();
		gridLayout.numColumns = 2;
		groupTemplate.setLayout(gridLayout);
		gridData = new GridData(GridData.FILL, GridData.CENTER, true, false);
		gridData.horizontalSpan = 3;
		groupTemplate.setLayoutData(gridData);
		// name
		new Label(groupTemplate, SWT.NONE).setText("Name:");
		taruName = new Text(groupTemplate, SWT.SINGLE | SWT.BORDER);
		taruName.setLayoutData(new GridData(GridData.FILL, GridData.CENTER,
				true, false));
		taruName.setText(System.getenv("name") != null ? System.getenv("name")
				: "");
		// mobs
		new Label(groupTemplate, SWT.NONE).setText("Mobs:");
		taruMobs = new Text(groupTemplate, SWT.SINGLE | SWT.BORDER);
		taruMobs.setLayoutData(new GridData(GridData.FILL, GridData.CENTER,
				true, false));
		taruMobs.setText("puk,soldier,worker,elemental,fly,bunny");
		// template
		new Label(groupTemplate, SWT.NONE).setText("Template:");
		commandTemplate = new Combo(groupTemplate, SWT.NONE | SWT.READ_ONLY);
		commandTemplate.setItems(File.getText("/template.txt").toArray(
				new String[0]));
		commandTemplate.setLayoutData(new GridData(GridData.FILL,
				GridData.CENTER, true, false));
		commandTemplate.addSelectionListener(new SelectionAdapter() {
			public void widgetSelected(SelectionEvent event) {
				String text = commandTemplate.getItem(commandTemplate
						.getSelectionIndex());
				text = text.replace("-find @find@", (taruMobs.getText().trim()
						.length() > 0) ? "-find " + taruMobs.getText().trim()
						: "");
				text = text.replace("-name @name@", (taruName.getText().trim()
						.length() > 0) ? "-name " + taruName.getText().trim()
						: "");
				commandArguments.setText(text);
				commandTemplate.select(0);
			}
		});
		commandTemplate.select(0);

		// command row
		new Label(shell, SWT.NONE).setText("Command:");
		commandArguments = new Text(shell, SWT.SINGLE | SWT.BORDER);
		gridData = new GridData(GridData.FILL, GridData.CENTER, true, false);
		gridData.horizontalSpan = 2;
		commandArguments.setLayoutData(gridData);

		// name it
		new Label(shell, SWT.NONE).setText("Name:");
		commandName = new Text(shell, SWT.SINGLE | SWT.BORDER);
		gridData = new GridData(GridData.FILL, GridData.CENTER, true, false);
		gridData.horizontalSpan = 2;
		commandName.setLayoutData(gridData);

		// buttons
		Composite compositeButtons = new Composite(shell, SWT.BORDER);
		gridData = new GridData(GridData.FILL, GridData.FILL, true, false);
		gridData.horizontalSpan = 3;
		compositeButtons.setLayoutData(gridData);
		compositeButtons.setLayout(new FillLayout());

		Button addCommand = new Button(compositeButtons, SWT.PUSH);
		addCommand.setText("Add Command");
		addCommand.addSelectionListener(new SelectionAdapter() {
			public void widgetSelected(SelectionEvent event) {
				janitor.addMitaru(commandArguments.getText());
				table.setItemCount(janitor.mitarus.size());
			}
		});

		Button editCommand = new Button(compositeButtons, SWT.PUSH);
		editCommand.setText("Edit Command");
		editCommand.addSelectionListener(new SelectionAdapter() {
			public void widgetSelected(SelectionEvent event) {
				if (table.getSelectionCount() > 0) {
					Mitaru mitaru = janitor.mitarus.get(table
							.getSelectionIndex());
					mitaru.command = commandArguments.getText();
					mitaru.setName(commandName.getText());
					resetTable();
					janitor.save();
				}
			}
		});

		start = new Button(compositeButtons, SWT.PUSH);
		start.setText("Start");
		start.setEnabled(false);
		// start.setLayoutData(new GridData(GridData.FILL, GridData.BEGINNING,
		// false, false));
		start.addSelectionListener(new SelectionAdapter() {
			public void widgetSelected(SelectionEvent event) {
				janitor.startMitaru();
				start.setEnabled(false);
				stop.setEnabled(true);
				table.setSelection(0);
				resetTable();

			}
		});
		stop = new Button(compositeButtons, SWT.PUSH);
		stop.setText("Stop");
		stop.setEnabled(false);
		stop.addSelectionListener(new SelectionAdapter() {
			public void widgetSelected(SelectionEvent event) {
				janitor.stopMitaru();
				start.setEnabled(true);
				stop.setEnabled(false);
				resetTable();
			}
		});

		Button deleteCommand = new Button(compositeButtons, SWT.PUSH);
		deleteCommand.setText("Delete Command");
		deleteCommand.addSelectionListener(new SelectionAdapter() {
			public void widgetSelected(SelectionEvent event) {
				if (table.getSelectionCount() > 0) {
					janitor.killMitaru(table.getSelectionIndex());
					resetTable();
				}
			}
		});

		// console row
		consoleText = new Text(shell, SWT.MULTI | SWT.BORDER | SWT.V_SCROLL
				| SWT.H_SCROLL);
		Font font = new Font(display, "Courier", 10, SWT.NORMAL);
		consoleText.setFont(font);
		gridData = new GridData(GridData.FILL, GridData.FILL, true, true);
		gridData.verticalSpan = 1;
		gridData.horizontalSpan = 3;
		gridData.minimumHeight = 300;
		consoleText.setLayoutData(gridData);

		table = new Table(shell, SWT.VIRTUAL | SWT.BORDER | SWT.V_SCROLL
				| SWT.H_SCROLL | SWT.FULL_SELECTION | SWT.LINE_SOLID);
		gridData = new GridData(GridData.FILL, GridData.FILL, true, true);
		gridData.verticalSpan = 1;
		gridData.horizontalSpan = 3;
		gridData.minimumHeight = 300;
		table.setLayoutData(gridData);

		table.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				janitor.setActiveMitaru(table.getSelectionIndex());
				if (null != janitor.activeMitaru) {
					commandArguments.setText(janitor.activeMitaru.command);
					commandName.setText(janitor.activeMitaru.getName());
					updateConsoleText(null);
					start.setEnabled(janitor.activeMitaru.process == null);
					stop.setEnabled(janitor.activeMitaru.process != null);
				}
			}
		});
		TableColumn column1 = new TableColumn(table, SWT.NONE);
		TableColumn column2 = new TableColumn(table, SWT.NONE);
		TableColumn column3 = new TableColumn(table, SWT.NONE);
		TableColumn column4 = new TableColumn(table, SWT.NONE);
		table.addListener(SWT.SetData, new Listener() {
			@Override
			public void handleEvent(Event e) {
				TableItem item = (TableItem) e.item;
				int index = table.indexOf(item);
				Mitaru mitaru = janitor.mitarus.get(index);

				item.setText(new String[] { mitaru.getProcessStatus(),
						mitaru.command, mitaru.getStartDate(), mitaru.getName() });
				item.setForeground(mitaru.getProcessStatus().equals("running") ? blue
						: null);
			}

		});
		table.setItemCount(janitor.mitarus.size());
		column1.setWidth(100);
		column2.setWidth(400);
		column3.setWidth(200);
		column4.setWidth(300);
		table.pack();

		shell.addDisposeListener(new DisposeListener() {
			public void widgetDisposed(DisposeEvent arg0) {
				janitor.stopAll();
			}
		});

		shell.pack();

		return shell;
	}

	protected void updateConsoleText(String line) {
		if (line != null) {
			consoleText.append(line + "\n");
		} else {
			consoleText.setText(janitor.getActiveMitaruText());
		}
		consoleText.setTopIndex(consoleText.getLineCount());
	}

}
