using UnityEngine;

public class FootStepsSound : MonoBehaviour
{
    private AudioSource audioSource;
    [Header("FootSteps Sources")]
    public AudioClip[] footstepsSound;

    private void Awake()

    {
        audioSource = GetComponent<AudioSource>();
    }
    private AudioClip GetRandomFootStep()
    {
        return footstepsSound[Random.Range(0, footstepsSound.Length)];
    }
    private void Step()
    {
        AudioClip clip = GetRandomFootStep();
        audioSource.PlayOneShot(clip);
    }
}
