using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSpawner : MonoBehaviour
{
    // GameObjects
    public GameObject Parent;

    // Other files
    public AssignTagToChildren parentScript;
    public InputHandling inputHandling;
    public Points points;

    public void Spawner()
    {
        inputHandling = GameObject.FindGameObjectWithTag("Player Logic").GetComponent<InputHandling>();
        points = GameObject.FindGameObjectWithTag("Player Logic").GetComponent<Points>();

        for (int i = 0; i < inputHandling.numPlayers; i++)
        {
            GameObject playerParent = Instantiate(Parent, transform.position, transform.rotation);
            int tag = i + 1;
            string playerTag = "Player" + tag;
            string platTag = "Plat" + tag;
            string textTag = "Text" + tag;
            parentScript = playerParent.GetComponent<AssignTagToChildren>();
            parentScript.AssignTags(playerTag,platTag,textTag);
        }

        inputHandling.AssignObjects();
        points.AssignText();
    }
}
