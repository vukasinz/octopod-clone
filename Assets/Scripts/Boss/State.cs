using UnityEngine;

public abstract class State : MonoBehaviour
{
    [HideInInspector] public GameObject cerberus;
    
    [HideInInspector] public GameObject player;

    public abstract State RunCurrentState();
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public void SetReferences(GameObject playerRef, GameObject cerberusRef)
    {
        player = playerRef;
        cerberus = cerberusRef;
    }
}
