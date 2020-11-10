using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        image.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
