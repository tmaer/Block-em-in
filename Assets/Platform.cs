using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Variables
    public float yOffSet = -1.3f;

    // GameObject accessors
    public SpriteRenderer spriteRenderer;

    public BoxCollider2D platform;
    public BoxCollider2D killZone;

    // Other files
    private Sprite bluePlat;
    private Sprite redPlat;
    private Sprite purplePlat;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        platform = colliders[0];
        killZone = colliders[1];

        spriteRenderer = GetComponent<SpriteRenderer>();

        bluePlat = Resources.Load<Sprite>("Blue Plat");
        redPlat = Resources.Load<Sprite>("Red Plat");
        purplePlat = Resources.Load<Sprite>("Purple Plat");

        if (gameObject.tag == "Plat1")
        {
            spriteRenderer.sprite = bluePlat;
        }
        else if (gameObject.tag == "Plat2")
        {
            spriteRenderer.sprite = redPlat;
        }
        else if (gameObject.tag == "Plat3")
        {
            spriteRenderer.sprite = purplePlat;
        }
    }

    public void teleport(GameObject player)
    {
        transform.position = player.transform.position + new Vector3(0, yOffSet, 0); ;
    }
}
