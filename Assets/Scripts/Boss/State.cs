using System.Collections;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [HideInInspector] public GameObject cerberus;
    public float dashTimer = 2f;

    [HideInInspector] public GameObject player;
    protected bool isFlipping = false;
    public void Flip()
    {
        float distance = (cerberus.transform.position.x - player.transform.position.x);
        bool shouldFlip = (distance < 0 && !cerberus.GetComponent<SpriteRenderer>().flipX) ||
                          (distance > 0 && cerberus.GetComponent<SpriteRenderer>().flipX);

        if (shouldFlip && !isFlipping)
        {
            StartCoroutine(FlipCoroutine(distance < 0));
        }
    }
    IEnumerator FlipCoroutine(bool flipX)
    {
        StateManager sm = cerberus.GetComponent<StateManager>();
        if (sm.stateName != "DashState")
        {
            isFlipping = true;
            float r = Random.Range(0f, 1.5f);
            cerberus.GetComponent<Animator>().Play("idle");
            cerberus.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            GameObject.FindGameObjectWithTag("question_mark").GetComponent<Animator>().Play("question_mark", -1, 0f);
            yield return new WaitForSeconds(r / 1.5f);

            CapsuleCollider2D col = cerberus.GetComponent<CapsuleCollider2D>();
            GameObject hitbox = GameObject.FindGameObjectWithTag("hitbox");
            hitbox.GetComponent<CapsuleCollider2D>().offset = new Vector2(-hitbox.GetComponent<CapsuleCollider2D>().offset.x, hitbox.GetComponent<CapsuleCollider2D>().offset.y);
            col.offset = new Vector2(-col.offset.x, col.offset.y);
            cerberus.GetComponent<SpriteRenderer>().flipX = flipX;

            yield return new WaitForSeconds(r / 1.5f);


            ChaseState chaseState = cerberus.GetComponentInChildren<ChaseState>();
            if (sm.currentState.GetType() != typeof(DeathState) && sm.currentState.GetType() != typeof(DashState))
            {
                sm.SwitchToTheNextState(chaseState);
            }
            float dir = flipX ? 1f : -1f;
            cerberus.transform.position = new Vector2(cerberus.transform.position.x + (dir*1.5f), cerberus.transform.position.y);
            isFlipping = false;
        }
    }
    public abstract State RunCurrentState();
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public void SetReferences(GameObject playerRef, GameObject cerberusRef)
    {
        player = playerRef;
        cerberus = cerberusRef;
    }
}
