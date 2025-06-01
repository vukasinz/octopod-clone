using UnityEngine;

public class AttackState : State
{
    
    public State ChaseState;
    public bool isInChaseRange;
    public override void EnterState()
    {
        SetReferences(
        GameObject.FindGameObjectWithTag("Player"),
        GameObject.FindGameObjectWithTag("Cerberus")
    );
        Debug.Log("Entering Attack State");
    }
    public override void ExitState()
    {
        Debug.Log("Exiting Attack State");
    }
    public override State RunCurrentState()
    {
        Vector2 distance = player.transform.position - cerberus.transform.position;
        isInChaseRange = distance.magnitude > 1.5f; 

        if (isInChaseRange)
        {
            return ChaseState;
        }
        cerberus.GetComponent<Animator>().Play("close_range_attack");
        return this;
    }
}
