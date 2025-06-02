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
        cerberus.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.25f);

        BoxCollider2D col = cerberus.GetComponent<BoxCollider2D>();
        col.offset = new Vector2(-col.offset.x, col.offset.y);
        cerberus.GetComponent<SpriteRenderer>().flipX = flipX;

        yield return new WaitForSeconds(0.25f);
        cerberus.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
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
