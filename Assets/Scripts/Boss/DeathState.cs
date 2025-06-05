using Unity.VisualScripting;
using UnityEngine;

public class DeathState : State
{
  
    public override State RunCurrentState()
    {
        cerberus = GameObject.FindGameObjectWithTag("Cerberus");
        cerberus.GetComponent<Animator>().Play("death");
        return this;
    }
}
