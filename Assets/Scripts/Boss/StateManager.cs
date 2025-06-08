using Unity.VisualScripting;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;
    public string stateName;
    
    void Update()
    {
        stateName = currentState.name;
        
    }
    void Start()
    {
        
        currentState?.EnterState();
  
    }
    void FixedUpdate()
    {
        RunStateMachine();
       
               
            
    }
    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();
        if (nextState != null && nextState != currentState)
        {
            SwitchToTheNextState(nextState);
        }
    }
    public void SwitchToTheNextState(State nextState)
    {
        currentState?.ExitState();
        currentState = nextState;
        currentState?.EnterState();
    }
}
