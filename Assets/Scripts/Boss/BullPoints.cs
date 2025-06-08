using JetBrains.Annotations;
using UnityEngine;

public class BullPoints : MonoBehaviour
{

    public GameObject dashState;
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Cerberus"))
        {
            
            var stateManager = collision.GetComponent<StateManager>();
            if (stateManager.stateName.Equals("DashState"))
            {
                Debug.Log("hit!");
    
                    dashState.GetComponent<DashState>().dashed = true;
            }
        }
    }

}
