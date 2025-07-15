using UnityEngine;

public class CloseRangeSignal : MonoBehaviour
{
    public bool playerInside = false;
    GameObject cerberus;
    private void Start()
    {
        cerberus = GameObject.FindGameObjectWithTag("Cerberus");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
    private void Update()
    {
        if(playerInside && cerberus.GetComponent<StateManager>().stateName == "DashState")
        {
            cerberus.GetComponent<BossHealth>().DashDamage();
        }
    }
}
