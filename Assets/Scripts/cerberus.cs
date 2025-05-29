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
    bool attacking = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Follow(float distance)
    {
        if (Mathf.Abs(distance) < 30f && Mathf.Abs(distance) > 1f)
        {
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = player.transform.position;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, 10f * Time.deltaTime);
            if (!attacking)
            {
                transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);
                ResetAllBools();
                anim.SetBool("walking", true);
            }
           
        }
    }
    void ResetAllBools()
    {
        anim.SetBool("close_range_attack", false);
        anim.SetBool("long_range_attack", false);
        anim.SetBool("walking", false);
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
    void LongRangeAttack(float distance)
    {
        if (Mathf.Abs(distance) > 10f && Mathf.Abs(distance) < 15f && attacking == false)
        {

            StartCoroutine(longRangeCoroutine());

        }

    }
    void ShortRangeAttack(float distance)
    {
        if ( Mathf.Abs(distance) < 1f && attacking == false)
        {
        
            StartCoroutine(shortRangeCoroutine());
        }
      
    }
    IEnumerator shortRangeCoroutine()
    {
        attacking = true;
        ResetAllBools();
       
        anim.SetBool("close_range_attack",true);
        yield return new WaitForSeconds(1f);
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.4f, 1f, 30, 180f, false, true);
        attacking = false;
    }
    IEnumerator longRangeCoroutine()
    {
        attacking = true;
        ResetAllBools();
        
        anim.SetBool("long_range_attack",true);
        yield return new WaitForSeconds(1.5f);
        attacking = false;
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

        Follow(distance);
            ShortRangeAttack(distance);
            LongRangeAttack(distance);

    }
}
