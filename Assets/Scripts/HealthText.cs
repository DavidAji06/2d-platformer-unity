using System;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 80, 0);
    RectTransform textTransform;
    public float timeToFade = 1f;
    private float timeElapsed = 0f;
    TextMeshProUGUI textMeshPro;
    private Color startColor;


    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;

    }

    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;
        float fadeAlpha = startColor.a * (1 - timeElapsed / timeToFade);

        if (timeElapsed < timeToFade)
        {
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }

        else
        {
            Destroy(gameObject);
        }

    }

}
