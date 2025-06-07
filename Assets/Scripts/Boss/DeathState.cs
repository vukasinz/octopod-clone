using Unity.VisualScripting;
using UnityEngine;

public class DeathState : State
{
  
    public override State RunCurrentState()
    {
        cerberus = GameObject.FindGameObjectWithTag("Cerberus");
        cerberus.GetComponent<Animator>().Play("death");
        cerberus.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        return this;
    }
}
