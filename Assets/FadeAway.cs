using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeAway : MonoBehaviour
{

    public Image img;

    public void win()
    {
        StartCoroutine(FadeImage());
    }

    IEnumerator FadeImage()
    {
        yield return new WaitForSeconds(3);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
}

