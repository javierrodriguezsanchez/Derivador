namespace MathemathicExpresion;

public abstract class Expresion
//An abstraction for mathematic expresion, is made by constans, variables an expresions
{
    public abstract Expresion DerivateInVariable(char variable='\0');
    //returns the derivate of the experesion acording to the variable from the entry
    //if the variable is not specificated, dreivate in the first variable found 
    public abstract char GetAVariable();
    //obtains the first variable in the expresion, if there isn't variables, return '\0'
    public int Priotity{get;protected set;}
    //Operators have some priority, variables and constants has 0 priority(doesn't need parenthesis)
    
    
    //The next funtions defines the basic operations between expresions
    //The operator ^ wasn't included because it was making some problems
    static public Expresion operator +(Expresion a, Expresion b){return new Sum(a,b);}
    static public Expresion operator -(Expresion a, Expresion b){return new Diference(a,b);}
    static public Expresion operator -(Expresion a){return -1*a;}
    static public Expresion operator *(Expresion a, Expresion b){return new Multiplication(a,b);}
    static public Expresion operator /(Expresion a, Expresion b){return new Divition(a,b);}
    
    public static implicit operator Expresion(double a){return new Constant(a);}
    //add a implicit convertion from double to Constant
    //I doesn't implement the implicit operator from char to variable because 
    //the implicits operators to int of double and char was unconvinient
    
    public abstract double GetValue();
    //Helps to symplify expresions and may return a value
    //CAREFULL!! This method can throw an exception if there is a variable in the expresion
    public abstract Expresion Symplify();
    //Symplifys the expresion, nothing to comment
    public abstract Expresion Evaluate(char variable, double valor);
    //Change the variable for the value
    public abstract Expresion Modify(Expresion newInternal);
}

public abstract class UnaryExpresion:Expresion
//for trigonometrics and reversed trigonometrics functions
{
    protected string ExpresionOperator="";
    protected Expresion InternalExpresion=null!;

    public bool ExpressionNULL()
    {
        return InternalExpresion == null;
    }
    public override char GetAVariable()
    {
        return InternalExpresion.GetAVariable();
    }
    public override string ToString()
    {
        return ExpresionOperator+"("+(InternalExpresion==null?"???":InternalExpresion)+")";
    }
}

public abstract class BinaryExpresion:Expresion
//for basic operators and exponencial/potencial/logaritm operations
{
    protected string ExpresionOperator="";
    protected Expresion LeftExpresion=null!;
    protected Expresion RigthExpresion=null!;

    public bool ExpressionNULL()
    {
        return (RigthExpresion == null || LeftExpresion == null);
    }

    public override string ToString()
    {
        string ToReturn;
        if(LeftExpresion.Priotity<this.Priotity && LeftExpresion.Priotity!=0)
            ToReturn="("+(LeftExpresion==null?"???":LeftExpresion)+")"+ExpresionOperator;
        else
            ToReturn=(LeftExpresion==null?"???":LeftExpresion)+ExpresionOperator;
        
        if(RigthExpresion != null && RigthExpresion.Priotity<this.Priotity && RigthExpresion.Priotity!=0)
            return ToReturn+"("+(RigthExpresion)+")";
        else
            return ToReturn+(RigthExpresion==null?"???":RigthExpresion);   
    }
    
    public override char GetAVariable()
    {
        char variable=LeftExpresion.GetAVariable();
        if(variable=='\0')
            return RigthExpresion.GetAVariable();
        return variable;
    }

}