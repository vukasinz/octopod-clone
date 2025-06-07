using System.Collections;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [HideInInspector] public GameObject cerberus;
    
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
        isFlipping = true;
        float r = Random.Range(0.25f, 1.5f);
        cerberus.GetComponent<Animator>().Play("idle");
        yield return new WaitForSeconds(r/1.5f);
        BoxCollider2D hitbox = GameObject.FindGameObjectWithTag("hitbox").GetComponent<BoxCollider2D>();
        BoxCollider2D col = cerberus.GetComponent<BoxCollider2D>();
        hitbox.offset = new Vector2(-hitbox.offset.x, hitbox.offset.y);
        col.offset = new Vector2(-col.offset.x, col.offset.y);
        cerberus.GetComponent<SpriteRenderer>().flipX = flipX;

        yield return new WaitForSeconds(r/1.5f);

        StateManager sm = cerberus.GetComponent<StateManager>();
        
        ChaseState chaseState = cerberus.GetComponentInChildren<ChaseState>();
        if (sm.currentState.GetType() != typeof(DeathState))
        {
            sm.SwitchToTheNextState(chaseState);
        }
        isFlipping = false;
        
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
