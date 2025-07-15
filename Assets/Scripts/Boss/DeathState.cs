using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DeathState : State
{
    public GameObject ending;
    DeathState ds;
    private void Start()
    {
        ds = this;
    }
    public override State RunCurrentState()
    {
        cerberus = GameObject.FindGameObjectWithTag("Cerberus");
        cerberus.GetComponent<Animator>().Play("death");
        cerberus.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        StartCoroutine("WaitForDeathAnimation");
        return this;
    }
    IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(2f);
      
        ending.SetActive(true);
     
       
    }
}
