using Unity.VisualScripting;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;
    
    void Update()
    {
       
        RunStateMachine();
    }
    void Start()
    {
        
        currentState?.EnterState();
        currentState.player = GameObject.FindGameObjectWithTag("Player");
        currentState.cerberus = GameObject.FindGameObjectWithTag("Cerberus");
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();
        if (nextState != null && nextState != currentState)
        {
            SwitchToTheNextState(nextState);
        }
    }
    private void SwitchToTheNextState(State nextState)
    {
        currentState?.ExitState();
        currentState = nextState;
        currentState?.EnterState();
    }
}
