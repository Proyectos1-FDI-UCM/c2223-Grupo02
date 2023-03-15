using System;
public class Transition
{
    //los estados implicados en la transicion
    public State from { get;private set; }
    public State to { get;private set; }

    //la funcion que evalua si se cumple la transicion
    private Func<bool> condition;

    //constructor de la transicion
    public Transition(State from, State to, Func<bool> condition)
    {
        this.from = from;
        this.to = to;
        this.condition = condition;
    }
    //metodo publico para saber si se cumple la condicion o no
    public bool Condicion()
    {
        return condition();
    }    
}
