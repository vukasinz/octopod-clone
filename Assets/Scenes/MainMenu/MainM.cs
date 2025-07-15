using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainM : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;
    public void OnPlay()
    {
        StartCoroutine(OnPlayC());
    }
    public void OnExit()
    {
        StartCoroutine(OnExitC());
    }
    public IEnumerator OnPlayC()
    {
        
        playButton.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");

    }
    public IEnumerator OnExitC()
    {
        exitButton.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1.5f);
        Application.OpenURL("about:blank");
    }
}
