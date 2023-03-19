using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
Este script ir� atachado a cada enemigo.
Todas las referencias a componentes y objetos deber�n ir en este script.
Habr� una lista de estados, con todos los estados del tipo de enemigo
Las referencias de cada estado, se inicializaran con el constructor de dicho estado
Habr� un estado anyState, para gestionar las transiciones de cualquier estado

En este script estar�n las funciones que gestionan las transiciones
Se har� un Func<> para cada funci�n, para poder pasar dicha funci�n a cada transici�n
Las transiciones se inicializar�n con el constructor,
indicando el estado en el que est�n, al que van y el Func de la condici�n que se tiene que cumplir

Una vez inicializadas las transiciones, se a�adir�n a la lista correspondiente del diccionario

Establecemos el estado inicial y la lista de transiciones inicial

Despues de todas las inicializaciones, se ejecutara la gesti�n de una m�quina de estados promedio
Con la siguiente estructura:
-Analizar si hay alguna transici�n disponible, primero desde las anyState y despu�s desde las transiciones actuales
    -Si la hay, se hace onExit del from, y el onEnter del to, y el currentState pasa a ser el que era el estado to de la transici�n
    -Tambi�n se actualiza la lista de transiciones actuales
-Despu�s se hace hace el Tick() del estado actual

Fazil zencillo y pa toa la familia

 */
public class StateMachine : MonoBehaviour
{
    protected Dictionary<State, List<Transition>> _stateTransitions = new Dictionary<State, List<Transition>>();

    protected List<Transition> _currentTransitions = new List<Transition>();
    protected List<Transition> _anyStateTransitions = new List<Transition>();

    protected State _anyState;
    protected State _currentState;

    // Start is called before the first frame update
    void Start()
    {
        //inicializacion del diccionario
        //_stateTransitions = new Dictionary<State, List<Transition>>();


        //inicializacion de los estados, con sus constructores


        //inicailizacion de las transiciones


        
        //los estados son scripts de que heredan de la clase estado
    }

    // Update is called once per frame
    void Update()
    {
        //Tick();
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
