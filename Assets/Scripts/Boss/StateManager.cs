using Unity.VisualScripting;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;
    
    void Update()
    {
       
        
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
