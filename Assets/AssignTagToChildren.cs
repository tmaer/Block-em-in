using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AssignTagToChildren : MonoBehaviour
{
    // GameObjects
    public GameObject Player;
    public GameObject Platform;
    public GameObject Text;

    public void AssignTags(string playerTag, string platTag, string textTag) 
    {
        RectTransform textTransform = Text.GetComponent<RectTransform>();

        Player.tag = playerTag;
        Platform.tag = platTag;
        Text.tag = textTag;

        Debug.Log("Player Spawned, assigned with tag " + playerTag);
        Debug.Log("Platform Spawned, assigned with tag " + platTag);
        Debug.Log("Text Spawned, assigned with tag " + textTag);

        if (textTag == "Text1")
        {
            textTransform.anchoredPosition += new Vector2(-750, 360);
        }
        else if (textTag == "Text2")
        {
            textTransform.anchoredPosition += new Vector2(900, 360);
        }
        else
        {
            textTransform.anchoredPosition += new Vector2(100, 360);
        }
    }
}
