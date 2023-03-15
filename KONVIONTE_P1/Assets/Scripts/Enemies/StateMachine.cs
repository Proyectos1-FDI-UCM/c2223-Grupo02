using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
este script irá atachado a cada enemigo
todas las referencias a componentes y objetos deberan ir en este script
habra una lista de de estados, con todos los estados del tipo de enemigo
las referencias de cada estado, se inicializaran con el constructor de dicho estado
habra un estado anyState, para gestionar las transiciones de cualquier estado

en este script estarán las funciones que gestionan las transiciones
se hara un Func<> para cada funcion, para poder pasar dicha funcion a cada transicion
las transiciones se inicializaran con el constructor,
indicando el estado en el que están, al que van y el Func de la condicion que se tiene que cumplir

una vez inicializadas las transiciones, se añadiran a la lista correspondiente del diccionario

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

    private Dictionary<State, List<Transition>> _stateTransitions;


    private List<Transition> _currentTransitions;
    private List<Transition> _anyStateTransitions;

    private List<State> _states;
    private State _anyState;
    private State _currentState;

    // Start is called before the first frame update
    void Start()
    {
        //inicializacion del diccionario
        _stateTransitions = new Dictionary<State, List<Transition>>();
        for (int i = 0; i < _states.Count; i++) _stateTransitions.Add(_states[i], new List<Transition>());
        
        //los estados son scripts de que heredan de la clase estado
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    #region Methods
    
    private void InicializaTransicion(State from,State to,Func<bool> condition)
    {
        
    }


    #endregion

}
