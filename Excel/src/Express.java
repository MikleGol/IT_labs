import java.util.LinkedList;

public class Express {
    
   
    public boolean isOperation(char c) 
    {
        return c == '+' || c == '-' || c == '/' || c == '%' || c == '*' || c == '^'; 
    } 
    
    public boolean interval(char c) 
    {
        return c == ' '; 
    }
    public int opearatorsPriority(char operand) 
    {
        switch (operand) {
            case '+':
            case '-':
                return 1;
            case '*':	
            case '/':
            case '%':
                return 2;
            case '^':
            	return 3;
            default:
                return -1;
        }
    }
    public void operator(LinkedList<Integer> number, char sign) 
    {
        int r = number.removeLast();
        int l = number.removeLast();
            switch (sign) {
                case '^':
            	    number.add((int) Math.pow(l,r));
            	    break;
                case '+':
                    number.add(l + r);
                    break;
                case '-':
                    number.add(l - r);
                    break;
                case '*':
                    number.add(l * r);
                    break;
                case '/':
                    number.add(l / r);
                    break;
                case '%':
                    number.add(l % r);
                    break;
        }
    }
    
    
 
 
    public int make(String s) 
    {
        Express obj = new Express();
        LinkedList<Integer> h = new LinkedList<Integer>();
        LinkedList<Character> op = new LinkedList<Character>();
      
            for (int i = 0; i < s.length(); i++) {
                char c = s.charAt(i);
                    if (obj.interval(c))
                        continue;
                    
                    if (c == '(') 
                    {      
                        op.add('('); 
                    }   
                    else if (c == ')') 
                    {
                        while (op.getLast() != '(')  
                            operator(h, op.removeLast());
                            op.removeLast();
                    } 
                    else if (obj.isOperation(c)) 
                    {  
                        while (!op.isEmpty() && obj.opearatorsPriority(op.getLast()) >= obj.opearatorsPriority(c))
                           
                            operator(h, op.removeLast());
                            op.add(c);
                    } else 
                    {
                        String operand = "";
                            while (i < s.length() && Character.isDigit(s.charAt(i)))
                               
                                operand += s.charAt(i++); 
                --i;
                h.add(Integer.parseInt(operand)); 
                    }
        }
            
        while (!op.isEmpty())
            operator(h, op.removeLast());
            return h.get(0);
        
    }
    public int start(String a)
    {
            
        Express object = new Express();
        String s = a;
        return (Integer)object.make(s);
 
    }
    
}