using UnityEngine;

public class Objective2 : MonoBehaviour
{
    public GameObject gate;
    public TruckController truck;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Objective.occurence.GetobjectivesDone(true, true, false, false);
            gate.SetActive(true);
            truck.enabled= true;
            Destroy(gameObject, 1f);
        }
    }
}
