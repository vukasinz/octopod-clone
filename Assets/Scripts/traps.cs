using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class traps : MonoBehaviour
{
    public GameObject player_prefab;
    GameObject last_player;
    GameObject player;
    public static Transform last_checkpoint;
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
        player = GameObject.FindGameObjectWithTag("Player");
        if (collision.gameObject.CompareTag("Player"))
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
    public void vrati()
    {
        foreach (GameObject platforms in GameObject.FindGameObjectsWithTag("Platform"))
        {
            platforms.GetComponent<platform_shaker>().vratiPlatforme();
        }
    }
    IEnumerator SpawnAnotherPlayer()
    {
        vrati();
        last_player = player;
        last_player.GetComponent<player_mov>().enabled = false;
        last_player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player = Instantiate(player_prefab, new Vector2(last_checkpoint.position.x, last_checkpoint.position.y), Quaternion.identity, null);
        last_player.GetComponentInChildren<Animator>().SetTrigger("isDead");
        last_player.transform.GetChild(0).AddComponent<Rigidbody2D>();
        last_player.transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = 4;
        last_player.transform.GetChild(0).transform.parent = null;
        player.GetComponent<player_mov>().enabled = false;

        Destroy(last_player);
        yield return new WaitForSeconds(1f);
        player.GetComponent<player_mov>().enabled = true;

    }

}
