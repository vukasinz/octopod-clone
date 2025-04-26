using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class platform_shaker : MonoBehaviour
{
    Transform start_position;
    private void Start()
    {
        start_position = this.transform;
        
    }
    /* private void OnTriggerEnter2D(Collider2D collision)
     {
         if (collision.gameObject.CompareTag("Player"))
         {
             StartCoroutine(Shake(0.5f, 0.1f));

         }
     }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            this.GetComponent<Animator>().SetBool("shake", true);
        }
    }
    public void vratiPlatforme()
    {
        this.transform.position = start_position.position;
    }
  /*  IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null; // wait for next frame
        }

      
        this.gameObject.AddComponent<Rigidbody2D>();
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
        this.gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
    }*/
}
