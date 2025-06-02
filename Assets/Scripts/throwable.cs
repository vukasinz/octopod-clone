using DG.Tweening;
using UnityEngine;

public class throwable : MonoBehaviour
{
    GameObject player;
    Animator anim;
    public GameObject throwables_cerberus;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }
    void Shoot()
    {
        GameObject throwable = Instantiate(throwables_cerberus, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Rigidbody2D rb = throwable.GetComponent<Rigidbody2D>();
        Destroy(throwable, 5f);
        Vector2 direction = (player.transform.position - throwable.transform.position).normalized;
        rb.linearVelocity = direction * 35f;
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.4f, 0.55f, 20, 180f, false, true);
    }
}
