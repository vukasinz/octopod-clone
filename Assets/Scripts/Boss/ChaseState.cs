using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChaseState : State
{
    public AttackState attackState;

    public bool isInAttackRange;

    public override State RunCurrentState()
    {
        transform.position = Vector2.MoveTowards(cerberus.transform.position, player.transform.position, 5 * Time.deltaTime);
        Vector2 distance = player.transform.position - cerberus.transform.position;
        isInAttackRange = distance.magnitude < 1.5f; 
        if (isInAttackRange)
        {
            return attackState;
        }
        return this;
    }
    public override void EnterState()
    {
        SetReferences(
        GameObject.FindGameObjectWithTag("Player"),
        GameObject.FindGameObjectWithTag("Cerberus")
    );
        Debug.Log("Entering Chase State");
        cerberus.GetComponent<Animator>().Play("walk");
    }
    public override void ExitState()
    {

        Debug.Log("Exiting Chase State");
    }

}
