using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class otherPlayerInfoBoxDisplay : MonoBehaviour
{
    public Image infoBox;

    public Sprite newImage;

    public Sprite restore;

    public Color onHover;

    public Color notHover;

    void Start()
    {
        restore = infoBox.sprite;

    }

    void OnMouseOver()
    {
        infoBox.sprite = newImage;
        infoBox.color =  onHover;
    }

    void OnMouseExit()
    {
        infoBox.sprite = restore;
        infoBox.color = notHover;
    }
}
