using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChaseState : State
{
    public AttackState attackState;
    public LongrangeAttackState longRangeAttackState;
    [HideInInspector]public Vector2 target;

    public bool isInAttackRange;
    public bool isInLongAttackRange;

    public override State RunCurrentState()
    {

        Flip();
        if (isFlipping)
            return this;
        Rigidbody2D rb = cerberus.GetComponent<Rigidbody2D>();
        target = new Vector2(player.transform.position.x, rb.position.y);
        rb.MovePosition(Vector2.MoveTowards(rb.position, target, 6f * Time.fixedDeltaTime));

        Vector2 distance = player.transform.position - cerberus.transform.position;
        isInAttackRange = distance.magnitude < 2f;
        isInLongAttackRange = distance.magnitude > 20f;

        if (isInLongAttackRange)
            return longRangeAttackState;
        if (isInAttackRange)
            return attackState;

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
        SetReferences(
           GameObject.FindGameObjectWithTag("Player"),
           GameObject.FindGameObjectWithTag("Cerberus")
       );
        Debug.Log("Exiting Chase State");
    }

}
