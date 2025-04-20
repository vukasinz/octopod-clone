using UnityEngine;

public class wall_slide : MonoBehaviour
{
    public bool isWalled;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isWalled = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isWalled = false;
    }
}
