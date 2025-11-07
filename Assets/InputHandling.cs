using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InputHandling : MonoBehaviour
{
    // Variables 
    public bool gameStart = false;

    public int numPlayers = 2;

    private string blueDirection;
    private string redDirection;
    private string purpleDirection;

    // GameObjects

    // NO. PLAYERS BUTTONS
    public GameObject one;
    public GameObject two;
    public GameObject three;

    // PLAYERS
    public GameObject Blue;
    public GameObject Red;
    public GameObject Purple;

    // PLATFORMS
    public GameObject BluePlat;
    public GameObject RedPlat;
    public GameObject PurplePlat;

    // PLAYER SCRIPTS
    private Player blueScript;
    private Player redScript;
    private Player purpleScript;

    // PLATFORM SCRIPTS
    private Platform bluePlatScript;
    private Platform redPlatScript;
    private Platform purplePlatScript;

    // Other files
    public Points points;

    private void Start()
    {
        points = GameObject.FindGameObjectWithTag("Player Logic").GetComponent<Points>();
    }

    public void AssignObjects()
    {
        Blue = GameObject.FindGameObjectWithTag("Player1");
        BluePlat = GameObject.FindGameObjectWithTag("Plat1");

        if (numPlayers >= 2)
        {
            Red = GameObject.FindGameObjectWithTag("Player2");
            RedPlat = GameObject.FindGameObjectWithTag("Plat2");
        }
        if (numPlayers >= 3)
        {
            Purple = GameObject.FindGameObjectWithTag("Player3");
            PurplePlat = GameObject.FindGameObjectWithTag("Plat3");
        }
        
        if (Blue != null)
        {
            blueScript = Blue.GetComponent<Player>();
            bluePlatScript = BluePlat.GetComponent<Platform>();
        }
        else
        {
            Debug.LogWarning("Blue player not found.");
        }
        if (Red != null)
        {
            redScript = Red.GetComponent<Player>();
            redPlatScript = RedPlat.GetComponent<Platform>();
        }
        else
        {
            Debug.Log("Red player not found.");
        }
        if (Purple != null)
        {
            purpleScript = Purple.GetComponent<Player>();
            purplePlatScript = PurplePlat.GetComponent<Platform>();
        }
        else
        {
            Debug.Log("Purple player not found.");
        }

        blueScript.AssignPlatforms();
        if (numPlayers >= 2)
        {
            redScript.AssignPlatforms();
        }
        if (numPlayers >= 3)
        {
            purpleScript.AssignPlatforms();
        }
    }
    
    public void TeleportObjects()
    {
        Transform bluePlayerTransform = blueScript.GetComponent<Transform>();
        Transform bluePlatformTransform = bluePlatScript.GetComponent<Transform>();
        bluePlayerTransform.position = new Vector2(-8, 1.5f);
        bluePlatformTransform.position = new Vector2(-8, 0);
        if (numPlayers >= 2)
        {
            Transform redPlayerTransform = redScript.GetComponent<Transform>();
            Transform redPlatformTransform = redPlatScript.GetComponent<Transform>();
            redPlayerTransform.position = new Vector2(8, 1.5f);
            redPlatformTransform.position = new Vector2(8, 0);
        }
        if (numPlayers >= 3)
        {
            Transform purplePlayerTransform = purpleScript.GetComponent<Transform>();
            Transform purplePlatformTransform = purplePlatScript.GetComponent<Transform>();
            purplePlayerTransform.position = new Vector2(0, 1.5f);
            purplePlatformTransform.position = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            // BLUE PLAYER //
            if (Input.GetKey(KeyCode.W))
            {
                blueScript.Jump();
            }

            if (Input.GetKey(KeyCode.A))
            {
                blueDirection = "left";
            }
            if (Input.GetKey(KeyCode.D))
            {
                blueDirection = "right";
            }

            if (blueDirection == "left")
            {
                blueScript.MoveLeft();
            }
            else if (blueDirection == "right")
            {
                blueScript.MoveRight();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                bluePlatScript.teleport(Blue);
            }

            // RED PLAYER //
            if (Input.GetKey(KeyCode.UpArrow))
            {
                redScript.Jump();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                redDirection = "left";
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                redDirection = "right";
            }

            if (redDirection == "left")
            {
                redScript.MoveLeft();
            }
            else if (redDirection == "right")
            {
                redScript.MoveRight();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                redPlatScript.teleport(Red);
            }

            // PURPLE PLAYER //
            if (Input.GetKey(KeyCode.U))
            {
                purpleScript.Jump();
            }

            if (Input.GetKey(KeyCode.H))
            {
                purpleDirection = "left";
            }
            if (Input.GetKey(KeyCode.K))
            {
                purpleDirection = "right";
            }

            if (purpleDirection == "left")
            {
                purpleScript.MoveLeft();
            }
            else if (purpleDirection == "right")
            {
                purpleScript.MoveRight();
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                purplePlatScript.teleport(Purple);
            }
        }
    }

    public void OnePlayer()
    {
        Debug.Log("Changed number of players to 1");
        one.GetComponent<Image>().color = new Color32(0x13, 0xCF, 0x3A, 0xFF); // green
        two.GetComponent<Image>().color = new Color32(0xA8, 0x95, 0x95, 0xFF); // brown
        three.GetComponent<Image>().color = new Color32(0xA8, 0x95, 0x95, 0xFF); // brown
        numPlayers = 1;
    }
    public void TwoPlayers()
    {
        Debug.Log("Changed number of players to 2");
        one.GetComponent<Image>().color = new Color32(0xA8, 0x95, 0x95, 0xFF); // brown
        two.GetComponent<Image>().color = new Color32(0x13, 0xCF, 0x3A, 0xFF); // green
        three.GetComponent<Image>().color = new Color32(0xA8, 0x95, 0x95, 0xFF); // brown
        numPlayers = 2;
    }
    public void ThreePlayers()
    {
        Debug.Log("Changed number of players to 3");
        one.GetComponent<Image>().color = new Color32(0xA8, 0x95, 0x95, 0xFF); // brown
        two.GetComponent<Image>().color = new Color32(0xA8, 0x95, 0x95, 0xFF); // brown
        three.GetComponent<Image>().color = new Color32(0x13, 0xCF, 0x3A, 0xFF); // green
        numPlayers = 3;
    }

    public void checkWhoKilled(string killingPlatform)
    {
        if (killingPlatform == "Blue")
        {
            points.bluePoint();
        }
        else if (killingPlatform == "Red")
        {
            points.redPoint();
        }
        else if (killingPlatform == "Purple")
        {
            points.purplePoint();
        }
    }
}