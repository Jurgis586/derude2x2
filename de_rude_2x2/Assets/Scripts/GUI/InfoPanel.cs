using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public Text info;

    public void setInfoText(string text)
    {
        gameObject.SetActive(true);
        info.text = text;
        StartCoroutine(remove());
    }

    IEnumerator remove()
    {
        yield return new WaitForSeconds(5);
        info.text = "";
        gameObject.SetActive(false);
    }
}
