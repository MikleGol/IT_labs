import org.junit.Assert;
import org.junit.Test;

public class Test2 {

	@Test
	public void Excel() {
		
		Excel excel = new Excel(); 
		
		int a = excel.getRows();
		int b = excel.getCols();
		
		System.out.print(a + "\t");
		System.out.println(b);
		
		excel.btn_addRow.doClick();
		excel.btn_addCol.doClick();
		
	    a = excel.getRows();
		b = excel.getCols();
		
		System.out.print(a + "\t");
		System.out.println(b);
		
		int a1 = 4;
		int b1 = 4;
		
		Assert.assertEquals(a,a1);
		Assert.assertEquals(b,b1);
		
	}
	
	
}
