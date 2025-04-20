using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class traps : MonoBehaviour
{
    public GameObject player_prefab;
    public GameObject Camera;
    GameObject last_player;
    GameObject player;
    public Transform last_checkpoint;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    public enum TrapType
    {
        Spike,
        
    }
    public TrapType trapType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            switch(trapType)
            {
                case TrapType.Spike:
                    Spikes();
                    break;
            }
        }
    }
    private void Spikes()
    {
        Debug.Log("Player hit the spikes!");
        StartCoroutine(SpawnAnotherPlayer());
       // StartCoroutine(MoveCameraToCheckpoint());




        //eksplodira igrac - animacija pa onda --> destroy zapravo, i napravi se novi, tacnije klonira.
        //kamera se vrati nazad igrac padne odozgo i tek tada moze da se kontrolise.

    }
    private IEnumerator MoveCameraToCheckpoint()
    {

        Vector3 startPosition = Camera.transform.position;
        Vector3 targetPosition = last_checkpoint.position;
        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime * 6f;
            Camera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            yield return null; 
        }
        Camera.transform.position = targetPosition;
    }
    IEnumerator SpawnAnotherPlayer()
    {
        last_player = player;
        last_player.GetComponent<player_mov>().enabled = false;
        last_player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player = Instantiate(player_prefab, new Vector2(last_checkpoint.position.x,last_checkpoint.position.y), Quaternion.identity,null);
        last_player.GetComponentInChildren<Animator>().SetTrigger("isDead");
        last_player.transform.GetChild(0).AddComponent<Rigidbody2D>();
        last_player.transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = 4;
        last_player.transform.GetChild(0).transform.parent = null;
       // Camera.transform.parent = null;
        Destroy(last_player);
        player.GetComponent<player_mov>().enabled = false;

        yield return new WaitForSeconds(1f);
        player.GetComponent<player_mov>().enabled = true;
       // Camera.transform.SetParent(player.transform);

    }

}
