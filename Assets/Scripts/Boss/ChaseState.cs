using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChaseState : State
{
    public AttackState attackState;
    public DashState dashState;
    public LongrangeAttackState longRangeAttackState;
    [HideInInspector]public Vector2 target;
    public bool isInAttackRange;
    public bool isInLongAttackRange;
    
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
        if(DashCheck())
        {
            return dashState;
        }
        
            Flip();
        if (isFlipping)
            return this;
        
        Rigidbody2D rb = cerberus.GetComponent<Rigidbody2D>();
        target = new Vector2(player.transform.position.x, rb.position.y);
        rb.MovePosition(Vector2.MoveTowards(rb.position, target, 8f * Time.fixedDeltaTime));

        float z = transform.rotation.eulerAngles.z;
        if (z > 180) z -= 360;
        z = Mathf.Clamp(z, -10f, 10f);
        transform.rotation = Quaternion.Euler(0, 0, z);

        Vector2 distance = player.transform.position - cerberus.transform.position;
        isInAttackRange = distance.magnitude < 2f;
        isInLongAttackRange = distance.magnitude > 15f;
        
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
