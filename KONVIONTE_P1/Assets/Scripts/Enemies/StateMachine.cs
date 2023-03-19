using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
Este script irá atachado a cada enemigo.
Todas las referencias a componentes y objetos deberán ir en este script.
Habrá una lista de estados, con todos los estados del tipo de enemigo
Las referencias de cada estado, se inicializaran con el constructor de dicho estado
Habrá un estado anyState, para gestionar las transiciones de cualquier estado

En este script estarán las funciones que gestionan las transiciones
Se hará un Func<> para cada función, para poder pasar dicha función a cada transición
Las transiciones se inicializarán con el constructor,
indicando el estado en el que están, al que van y el Func de la condición que se tiene que cumplir

Una vez inicializadas las transiciones, se añadirán a la lista correspondiente del diccionario

Establecemos el estado inicial y la lista de transiciones inicial

Despues de todas las inicializaciones, se ejecutara la gestión de una máquina de estados promedio
Con la siguiente estructura:
-Analizar si hay alguna transición disponible, primero desde las anyState y después desde las transiciones actuales
    -Si la hay, se hace onExit del from, y el onEnter del to, y el currentState pasa a ser el que era el estado to de la transición
    -También se actualiza la lista de transiciones actuales
-Después se hace hace el Tick() del estado actual

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
