using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowAnimation : MonoBehaviour
{

    public Image arrowImage;

    private void Start()
    {
        arrowImage = GetComponent<Image>();
    }
    public void MakeInvisible()
    {
        Color tempColor = arrowImage.color;
        tempColor.a = 0f;
        arrowImage.color = tempColor;
        
    }

    public void MakeVisible()
    {
        Color tempColor = arrowImage.color;
        tempColor.a = 100f;
        arrowImage.color = tempColor;
    }
}

