using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    protected Dictionary<State, List<Transition>> _stateTransitions = new Dictionary<State, List<Transition>>();

    protected List<Transition> _currentTransitions = new List<Transition>();
    protected List<Transition> _anyStateTransitions = new List<Transition>();

    protected State _anyState;
    protected State _currentState;   
    
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
