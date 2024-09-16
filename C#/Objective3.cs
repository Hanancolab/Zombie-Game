using UnityEngine;

public class Objective3 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Objective.occurence.GetobjectivesDone(true, true, true, true);
            Destroy(gameObject, 1f);
        }
    }
}
