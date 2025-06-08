using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LongrangeAttackState : State
{
    public ChaseState chaseState;
    public DashState dashState;
    public bool isInChaseRange;
    bool ShouldFlip()
    {
        float distance = cerberus.transform.position.x - player.transform.position.x;
        bool facingLeft = cerberus.GetComponent<SpriteRenderer>().flipX;
        bool needsFlip = (distance < 0 && facingLeft) || (distance > 0 && !facingLeft);

        if (!needsFlip) return false;

        float direction = distance < 0 ? 1f : -1f;
        Vector2 origin = cerberus.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * direction, 0.6f);

        if (hit.collider == null) return true; // No wall — safe to flip

        // Allow flip if hit player, otherwise block it
        return hit.collider.CompareTag("Player");
    }
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
    public bool DashCheck()
    {
        if (isFlipping)
            return false;
        dashTimer -= Time.fixedDeltaTime;
        if (dashTimer <= 0f)
        {
                dashTimer = 2f;
            if (Random.Range(0, 100) < 30)
                return true;
        }
        return false;
    }
    public override State RunCurrentState()
    {
        if (DashCheck())
        {
            return dashState;
        }
        Vector2 distance = player.transform.position - cerberus.transform.position;
        isInChaseRange = distance.magnitude > 2f && distance.magnitude < 10f;
        cerberus.GetComponent<Animator>().Play("long_range_attack");
        if (isInChaseRange && isDone())
            return chaseState;
        return this;
    }
}
