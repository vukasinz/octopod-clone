using Unity.VisualScripting;
using UnityEngine;

public class LongrangeAttackState : State
{
    public State ChaseState;
    public bool isInChaseRange;
    public override void EnterState()
    {
        SetReferences(
            GameObject.FindGameObjectWithTag("Player"),
            GameObject.FindGameObjectWithTag("Cerberus")
        );
        Debug.Log("Entering Long Range Attack State");
    }
    public override void ExitState()
    {
       Debug.Log("Exiting Long Range Attack State");
    }
    public override State RunCurrentState()
    {
        Vector2 distance = player.transform.position - cerberus.transform.position;
        isInChaseRange = distance.magnitude > 2f && distance.magnitude < 10f;
        if (isInChaseRange)
            return ChaseState;
        cerberus.GetComponent<Animator>().Play("long_range_attack");
        return this;
    }
}
