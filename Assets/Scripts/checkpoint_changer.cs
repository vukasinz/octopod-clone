using UnityEditor;
using UnityEngine;

public class checkpoint_changer : MonoBehaviour
{
    public Camera camera;
    public GameObject g;
    public GameObject healthBar;
    public void Start()
    {
      
        g.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            traps.last_checkpoint = this.transform;
            Debug.Log(this.transform);
            switch(traps.last_checkpoint.name)
            {
                case "level 1":
                    camera.transform.position = new Vector3(4.82f, 1.28f, -10);
                    break;
                case "level 2":
                    camera.transform.position = new Vector3(38.19f, 7.95f, -10);
                    break;
                case "level 3":
                    camera.transform.position = new Vector3(69.48f, 7.95f, -10);
                    break;
                case "level 4":
                    camera.transform.position = new Vector3(99.48f, 9.22f, -10);
                    break;
                case "level 5":
                    camera.transform.position = new Vector3(132.11f, 9.22f, -10);
                    break;
                case "level 6":
                    camera.transform.position = new Vector3(164.61f, 9.22f, -10);
                    break;
                case "level 7":
                    camera.transform.position = new Vector3(199.01f, 17.13f, -10);
                    break;
                case "level 8":
                    camera.transform.position = new Vector3(230.92f, 15.43f, -10);
                    break;
                case "level 9":
                    camera.transform.position = new Vector3(265.38f, 12.87f, -10);
                    break;
                case "level 10":
                    if (g != null && g.activeSelf)
                        g.SetActive(false);
                    camera.transform.position = new Vector3(296.98f, 6.2f, -10);
                    break;
                case "level 11":
                    healthBar.SetActive(true);
                    camera.transform.position = new Vector3(328.8f, 6.2f, -10);
                    if (g != null)
                        g.SetActive(true);
                    break;
                default:
                    Debug.Log("Unknown checkpoint");
                    break;
            }
        }
    }
       
}
