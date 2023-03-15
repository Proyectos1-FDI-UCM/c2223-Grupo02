using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
este script ir� atachado a cada enemigo
todas las referencias a componentes y objetos deberan ir en este script
habra una lista de de estados, con todos los estados del tipo de enemigo
las referencias de cada estado, se inicializaran con el constructor de dicho estado
habra un estado anyState, para gestionar las transiciones de cualquier estado

en este script estar�n las funciones que gestionan las transiciones
se hara un Func<> para cada funcion, para poder pasar dicha funcion a cada transicion
las transiciones se inicializaran con el constructor,
indicando el estado en el que est�n, al que van y el Func de la condicion que se tiene que cumplir

una vez inicializadas las transiciones, se a�adiran a la lista correspondiente del diccionario

establecemos el estado inicial y la lista de transiciones inicial

Despues de todas las inicializaciones, se ejecutara la gestion de una maquina de estados promedio
Con la siguiente estructura:
-Analizar si hay alguna transicion disponible,primero desde las anyState y despues desde las transiciones actuales
    -Si la hay, se hace onExit del from, y el onEnter del to, y el currentState pasa a ser el que era el estado to de la transicion
    -Tambien se actualiza la lista de transiciones actuales
-Despues se hace hace el Tick() del estado actual
Fazil zencillo y pa toa la familia

 */
public class StateMachine : MonoBehaviour
{
    protected Dictionary<State, List<Transition>> _stateTransitions;

    protected List<Transition> _currentTransitions;
    protected List<Transition> _anyStateTransitions;

    protected State _anyState;
    protected State _currentState;

    // Start is called before the first frame update
    void Start()
    {
        //inicializacion del diccionario
        _stateTransitions = new Dictionary<State, List<Transition>>();


        //inicializacion de los estados, con sus constructores


        //inicailizacion de las transiciones


        
        //los estados son scripts de que heredan de la clase estado
    }

    // Update is called once per frame
    void Update()
    {
        Tick();
    }
    
    #region Methods
    
    protected void Tick()
    {
        TryTransitions();
        _currentState.Tick();
    }

    protected void InicializaTransicion(State from,State to,Func<bool> condition)
    {
        Transition newTransition = new Transition(from, to, condition);
        _stateTransitions[from].Add(newTransition);
    }
    
    private void TryTransitions()
    {
        if (!AnalizaTransiciones(_anyStateTransitions)) AnalizaTransiciones(_currentTransitions);
    }
    private bool AnalizaTransiciones(List<Transition> transiciones)
    {
        int i = 0;
        while (i < transiciones.Count && !transiciones[i].Condicion()) i++;
        if (i != transiciones.Count) { MakeTransition(transiciones[i]); return true; } 
        else return false;
    }

    private void MakeTransition(Transition transition)
    {
        _currentState.OnExit();
        transition.to.OnEnter();
        _currentState = transition.to;
        _currentTransitions = _stateTransitions[_currentState];
    }
    #endregion

}
