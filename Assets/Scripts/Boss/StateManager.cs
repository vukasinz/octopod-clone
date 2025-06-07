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
  
    }
    void FixedUpdate()
    {
     
            if (currentState is ChaseState chase)
            {
                Rigidbody2D rb = chase.cerberus.GetComponent<Rigidbody2D>();
                rb.MovePosition(Vector2.MoveTowards(rb.position, chase.target, 6f * Time.fixedDeltaTime));
            }
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
