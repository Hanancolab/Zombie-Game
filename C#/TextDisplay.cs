using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public Text text;
    public string textToShown;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(textShown());
        }
    }
    IEnumerator textShown()
    {
        text.text = textToShown;
        yield return new WaitForSeconds(3f);
        text.text = "";
        yield return new WaitForSeconds(0.5f);

    }
}
