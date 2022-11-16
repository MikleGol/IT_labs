
import javax.swing.*;
import javax.swing.event.TableModelEvent;
import javax.swing.event.TableModelListener;
import javax.swing.table.DefaultTableModel;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;

public class Excel extends JFrame {
	
	
	public static void main(String[] args) {
		new Excel();	
	}
	
	
	private static final long serialVersionUID = 1L;

	private DefaultTableModel tableModel;
	
	private JTable table1;

	private Object[][] array = new String[3][4];

	private Object[] columnsHeader;

	
	
	JButton btn_addRow = new JButton("Додати ряд");
	JButton btn_addCol = new JButton("Додати колонку");
	
	JButton btn_removeRow = new JButton("Видалити рядок");
	JButton btn_removeCol = new JButton("Видалити колонку");
	
	JButton btn_excel = new JButton("Зберегти");
	

	public Excel() {
		

		super("Excel");

		columnsHeader = new String[3];

		setDefaultCloseOperation(EXIT_ON_CLOSE);

		tableModel = new DefaultTableModel();
		tableModel.setColumnIdentifiers(columnsHeader);

		btn_excel.setMnemonic(KeyEvent.VK_X);
		btn_excel.addActionListener(new Listener1());

		for (int i = 0; i < array.length; i++)
			tableModel.addRow(array[i]);

		table1 = new JTable(tableModel);
		
		

		btn_addRow.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {

				tableModel.addTableModelListener(table1);
				try {
					int idx = table1.getSelectedRow();

					tableModel.insertRow(idx + 1, new String[] { " ", " " });

				} catch (Exception ex) {}
			}
		});
		
		
				
		btn_addCol.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {

				tableModel.addTableModelListener(table1);
				try {
					tableModel.setColumnCount(tableModel.getColumnCount() + 1);

				} catch (Exception ex) {}
			}
		});
		
		
				
		btn_removeCol.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				
				tableModel.addTableModelListener(table1);				
				try {
				tableModel.setColumnCount(tableModel.getColumnCount() - 1);
				
				} catch(Exception ex) {}
			}
		});
		
		
				
		btn_removeRow.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				
				tableModel.addTableModelListener(table1);				
				try {
				int idx = table1.getSelectedRow();
				tableModel.removeRow(idx);
				
				} catch(Exception ex) {}
			}
		});

		
		
		tableModel.addTableModelListener(new TableModelListener() {

			@Override
			public void tableChanged(TableModelEvent e) {
				int a = table1.getSelectedRow();
				int b = table1.getSelectedColumn();

				String s = (String)tableModel.getValueAt(a, b);
				

				Express exp = new Express();

				try {
					tableModel.setValueAt(exp.start(s), a, b);
				} catch (Exception ex) {}
			}
		});

		Box contents = new Box(BoxLayout.Y_AXIS);
		contents.add(new JScrollPane(table1));

		getContentPane().add(contents);

		JPanel buttons = new JPanel();
		buttons.add(btn_addRow);
		buttons.add(btn_addCol);
		buttons.add(btn_removeCol);
		buttons.add(btn_removeRow);
		buttons.add(btn_excel);
		getContentPane().add(buttons, "South");

		setSize(900, 500);
		setVisible(true);
		table1.setRowHeight(35);
	}


	public void toExcel(JTable table, File file) {
		try {

			FileWriter excel = new FileWriter(file);

			String s = "";

			for (int i = 0; i < tableModel.getColumnCount(); i++) {

			    s = tableModel.getColumnName(i);
			    
				excel.write(s + "\t");
				
			}

			excel.write("\n");

			for (int i = 0; i < tableModel.getRowCount(); i++) {

				for (int j = 0; j < tableModel.getColumnCount(); j++) {

					s = String.valueOf(tableModel.getValueAt(i, j)); 

					excel.write(s + "\t");
				}
				excel.write("\n");
			}
			excel.close();

		} catch (IOException e) {
			System.out.println(e);
		}
     
	}

	class Listener1 implements ActionListener {
		public void actionPerformed(ActionEvent e) {

			JFileChooser fc = new JFileChooser();
			int option = fc.showSaveDialog(Excel.this);
			if (option == JFileChooser.APPROVE_OPTION) {
				String filename = fc.getSelectedFile().getName();
				String path = fc.getSelectedFile().getParentFile().getPath();

				int len = filename.length();
				String ext = "";
				String file = "";

				if (len > 4) {
					ext = filename.substring(len - 4, len);
				}

				if (ext.equals(".xls")) {
					file = path + "\\" + filename;
				} else {
					file = path + "\\" + filename + ".xls";
				}
				toExcel(table1, new File(file));
				
			}
		}
	}
	
	public int getRows() {
		
		return tableModel.getRowCount();
	}

	public int getCols() {
		
		return tableModel.getColumnCount();	
	}
	
	
}