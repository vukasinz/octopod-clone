using System.Collections;
using UnityEngine;

public class DashState : State
{
    public ChaseState chaseState;
    public Animator marks;
    public bool dashed = false;
    bool change = false;
    bool dashing = false;
    Vector2 dashDir;
    public override void EnterState()
    {
        print("Entering Dash State");

        change = false;
        dashing = false;
        // marks.enabled = true;
        SetReferences(
            GameObject.FindGameObjectWithTag("Player"),
            GameObject.FindGameObjectWithTag("Cerberus")
        );
        dashDir = (player.transform.position - cerberus.transform.position).normalized;
        StartCoroutine("Dash");
    }
   
    IEnumerator Dash()
    {
        dashing = true;
        marks.Play("exclamation_point", -1, 0f);
        yield return new WaitForSeconds(1f);
        Rigidbody2D rb = cerberus.GetComponent<Rigidbody2D>();
        GameObject.FindGameObjectWithTag("dashTrail").GetComponent<ParticleSystem>().Play();
        dashDir.y += 0.00005f;
        rb.AddForce(dashDir * 30000f, ForceMode2D.Impulse);
        dashing = false;
        yield return new WaitForSeconds(0.25f);
        cerberus.GetComponent<Animator>().Play("dash",-1,0);
    }
    IEnumerator StopDash()
    {
       
        marks.Play("confusion_mark");
        yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.9f);
        cerberus.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.9f);
        change = true;

        
    }
    public override State RunCurrentState()
    {
        //kad udari dashed = true pa ce se ovo uraditi, to ce se gledati u alternativnoj skripti, gde se trazi udarac u zid/igraca.
        //mozda najbolje u state manageru, jer ocu i long range da pokrijem!
        if (dashed == true && dashing == false)
        {
            dashed = false;
            StartCoroutine("StopDash");

        }
        if (change)
        {
            
            return chaseState;
        }
        return this;
    }
    public override void ExitState()
    {
        print("Exiting Dash State");
        change = false;
        dashing = false;
        //marks.enabled = false;
    }
}
