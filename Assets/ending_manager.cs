using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class ending_manager : MonoBehaviour
{
    public UnityEngine.UI.Image panel;
    public UnityEngine.UI.Image cerberus;
    public GameObject[] sliders;
    public TextMeshProUGUI proceed;
    public TextMeshProUGUI defeat;
    public TextMeshProUGUI pressr;
    bool ended = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("End");
        }
    }
    public void Update()
    {
        if(ended && Input.GetKey(KeyCode.R))
            SceneManager.LoadScene("MainMenu");

    }
    IEnumerator End()
    {
        foreach(GameObject slider in sliders)
        {
            slider.SetActive(false);
        }
        panel.gameObject.SetActive(true);
        float t = 0f;
        while (t < 2f)
        {
            t += Time.deltaTime;
            float a = t / 2f;
            panel.color = new Color(0, 0, 0, a);
            yield return null;
        }

        t = 0f;
        cerberus.gameObject.SetActive(true);
        while (t < 1.5f)
        {
            t += Time.deltaTime;
            float a = t / 1.5f;
            cerberus.color = new Color(cerberus.color.r, cerberus.color.g, cerberus.color.b, a);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        defeat.gameObject.SetActive(true);
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            Color color = Color.Lerp(Color.black, Color.white, t / 1f);
            defeat.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        proceed.gameObject.SetActive(true);
        pressr.gameObject.SetActive(true);
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            Color color = Color.Lerp(Color.black, Color.red, t / 1f);
            proceed.color = color;
            pressr.color = color;
            yield return null;
            
        }
        ended = true;
    }


}
