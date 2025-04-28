using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class platform_shaker : MonoBehaviour
{
    Vector2 start_position;  // Ensure this is set in the Inspector or through code

    public void vratiPlatforme()
    {
        if (this.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("platform_shake"))
        {
            this.GetComponent<Animator>().Rebind();
            this.GetComponent<Animator>().Update(0f);
            this.transform.position = start_position;
        }
        else
            this.transform.position = start_position;
    }
    private void Start()
    {
        start_position = new Vector2(this.transform.position.x,this.transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            this.GetComponent<Animator>().SetTrigger("shake");
        }
    }

 
}
