using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [Header("ZombieSpawn Var")]

    public GameObject zombiePrefab;
    public Transform zombieSpawnPosition;
    public GameObject dangerZone1;
    private float repeatCycle = 1f;

    public AudioSource audio;
    public AudioClip dangerZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);
            audio.PlayOneShot(dangerZone);
            StartCoroutine(zoneActivation());
            Destroy(gameObject, 10f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    void EnemySpawner()
    {
        Instantiate(zombiePrefab, zombieSpawnPosition.position, zombieSpawnPosition.rotation);
    }
    IEnumerator zoneActivation()
    {
        dangerZone1.SetActive(true);
        yield return new WaitForSeconds(3f);
        dangerZone1.SetActive(false);
    }
}