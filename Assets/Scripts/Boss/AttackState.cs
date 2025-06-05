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
    public bool isDone()
    {
        AnimatorStateInfo stateInfo = cerberus.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("close_range_attack") && stateInfo.normalizedTime >= 1f)
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
        Debug.Log("Exiting Attack State");
    }
    public override State RunCurrentState()
    {
        Flip();
        Vector2 distance = player.transform.position - cerberus.transform.position;
        isInChaseRange = distance.magnitude > 2f; 
        cerberus.GetComponent<Animator>().Play("close_range_attack");

        if (isInChaseRange && isDone())
        {
            return ChaseState;
        }
        return this;
    }
}
