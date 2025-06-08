using System.Collections;
using UnityEngine;

public class DashState : State
{
    public ChaseState chaseState;
    private Animator marks;
    public bool dashed = false;
    bool change = false;
    bool dashing = false;
    Vector2 dashDir;
    public override void EnterState()
    {
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
        //marks.Play("exclamation_mark");
        yield return new WaitForSeconds(1f);
        Rigidbody2D rb = cerberus.GetComponent<Rigidbody2D>();
        GameObject.FindGameObjectWithTag("dashTrail").GetComponent<ParticleSystem>().Play();
        rb.AddForce(dashDir * 25000f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.25f);
        dashing = false;
        //cerberus.GetComponent<Animator>().Play("dash");
    }
    IEnumerator StopDash()
    {
        dashed = false;
        yield return new WaitForSeconds(0.2f);
        //marks.Play("stars");
        cerberus.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(1.8f);
        change = true;

        
    }
    public override State RunCurrentState()
    {
        //kad udari dashed = true pa ce se ovo uraditi, to ce se gledati u alternativnoj skripti, gde se trazi udarac u zid/igraca.
        //mozda najbolje u state manageru, jer ocu i long range da pokrijem!
        if(dashed == true && dashing == false)
            StartCoroutine("StopDash");
        if (change)
        {
            
            return chaseState;
        }
        return this;
    }
    public override void ExitState()
    {
        change = false;
        dashing = false;
        //marks.enabled = false;
    }
}
