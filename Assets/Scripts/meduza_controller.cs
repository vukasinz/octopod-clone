using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class meduza_controller : MonoBehaviour
{
    Vector2 start_position;
    public Sprite meduza0;
    void Start()
    {
        start_position = new Vector2(this.transform.position.x, this.transform.position.y);
    }
    private void Update()
    {
       
    }
    // Update is called once per frame
    public void vrati()
    {
        StopAllCoroutines();
            this.gameObject.GetComponent<Animator>().Play("meduza_idle");
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.transform.position = start_position;
    }



    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
        this.GetComponent<Animator>().Play("meduza_death1");
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Shake(0.4f, 0.1f));
            
            
        }
    }
}
