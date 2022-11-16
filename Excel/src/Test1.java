import org.junit.Assert;
import org.junit.Test;

public class Test1 {

	@Test
	public void start() {
		
		Express exp = new Express(); 
		
		int a = exp.start("3+5*2");
		int b = exp.start("(3+5)*2");
		int c = exp.start("5 + 6^2");
		
		int a1 = 13;
		int b1 = 16;
		int c1 = 41;
		
		System.out.println(a);
		System.out.println(b);
		System.out.println(c);
		
		Assert.assertEquals(a,a1);
		Assert.assertEquals(b,b1);
		Assert.assertEquals(c,c1);
		
	}
	
	
}
