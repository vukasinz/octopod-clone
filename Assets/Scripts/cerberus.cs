using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class cerberus : MonoBehaviour
{
    GameObject player;
    Animator anim;
    public GameObject throwables_cerberus;
    public bool attacking = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Follow(float distance)
    {
        if (Mathf.Abs(distance) < 30f && Mathf.Abs(distance) > 1f && !attacking)
        {
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = player.transform.position;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, 10f * Time.deltaTime);
         
                transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);

            anim.Play("walk");

        }
    }

    void Shoot()
    {
        GameObject throwable = Instantiate(throwables_cerberus, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Rigidbody2D rb = throwable.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.transform.position - throwable.transform.position).normalized;
        rb.linearVelocity = direction * 35f;
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.4f, 0.55f, 20, 180f, false, true);
    }

    IEnumerator ShortRangeAttack()
    {
        attacking = true;
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.4f, 1f, 30, 180f, false, true);
        anim.Play("close_range_attack");
        yield return new WaitForSeconds(1.5f);
        attacking = false;
    }
    IEnumerator LongRangeAttack()
    {
        attacking = true;
        anim.Play("long_range_attack");
        yield return new WaitForSeconds(2f);
        attacking = false;
        Debug.Log("Zavrsen long range attack"); 
    }
    void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        float distance = (this.transform.position.x - player.transform.position.x);
        if (distance < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
            this.GetComponent<SpriteRenderer>().flipX = false;

        if (Mathf.Abs(distance) <= 1f && !attacking)
        {
            StartCoroutine(ShortRangeAttack());
        }
        else if (Mathf.Abs(distance) > 10f && Mathf.Abs(distance) < 15f && !attacking)
        {
            StartCoroutine(LongRangeAttack());
        }
        else if (Mathf.Abs(distance) > 1f && Mathf.Abs(distance) <= 10f && !attacking)
        {
            Follow(distance);
        }
    }


    
}
