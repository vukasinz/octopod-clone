using Unity.VisualScripting;
using UnityEditor;
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
    public bool isDone()
    {
        AnimatorStateInfo stateInfo = cerberus.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("long_range_attack") && stateInfo.normalizedTime >= 1f)
        {
            return true;
        }
        return false;
    }
    public override void ExitState()
    {
        SetReferences(
           GameObject.FindGameObjectWithTag("Player"),
           GameObject.FindGameObjectWithTag("Cerberus")
       );
        Debug.Log("Exiting Long Range Attack State");
    }
    public override State RunCurrentState()
    {
        Vector2 distance = player.transform.position - cerberus.transform.position;
        isInChaseRange = distance.magnitude > 2f && distance.magnitude < 10f;
        cerberus.GetComponent<Animator>().Play("long_range_attack");
        if (isInChaseRange && isDone())
            return ChaseState;
        return this;
    }
}
