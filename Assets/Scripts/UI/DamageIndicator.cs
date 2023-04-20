using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine fadeAway;

    public void Flash ()
    {
        // stop all currently running FadeAway coroutines
        if ( fadeAway != null )
        {
            StopCoroutine(fadeAway);
        }

        // reset the image and fatde it away
        image.enabled = true;
        image.color = Color.white;
        fadeAway = StartCoroutine(FadeAway());
    }

    // fades the image away over time
    IEnumerator FadeAway ()
    {
        float a = 1.0f;
        while(a > 0.0f)
        {
            a -= (1.0f / flashSpeed) * Time.deltaTime;
            image.color = new Color(1.0f, 1.0f, 1.0f, a);
            yield return null;
        }

        image.enabled = false;
    }
}
