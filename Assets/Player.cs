using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Variables
    public float movementSpeed = 6;
    public float jumpStrength = 9;
    public float climbSpeed = 5;
    public float killSpeed = 1;

    public string yourMurderer;

    public bool gettingKilled = false;
    public bool canJump = true;
    public bool canClimb;

    public float timer;
    public float xPosition;

    // GameObjects
    public GameObject Border;

    public GameObject BluePlat;
    public GameObject RedPlat;
    public GameObject PurplePlat;

    // GameObject accessors
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;
    public InputHandling inputHandling;

    // Other files
    private Sprite bluePlayer;
    private Sprite redPlayer;
    private Sprite purplePlayer;

    private Platform bluePlatScript;
    private Platform redPlatScript;
    private Platform purplePlatScript;

    private GameLogic gameLogic;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.FindGameObjectWithTag("Player Logic").GetComponent<GameLogic>();
        inputHandling = GameObject.FindGameObjectWithTag("Player Logic").GetComponent<InputHandling>();
        Border = GameObject.FindGameObjectWithTag("Border");

        bluePlayer = Resources.Load<Sprite>("Blue Player");
        redPlayer = Resources.Load<Sprite>("Red Player");
        purplePlayer = Resources.Load<Sprite>("Purple Player");

        if (gameObject.tag == "Player1")
        {
            spriteRenderer.sprite = bluePlayer;
        }
        else if (gameObject.tag == "Player2")
        {
            spriteRenderer.sprite = redPlayer;
        }
        else if (gameObject.tag == "Player3")
        {
            spriteRenderer.sprite = purplePlayer;
        }
    }

    public void AssignPlatforms()
    {
        BluePlat = GameObject.FindGameObjectWithTag("Plat1");
        RedPlat = GameObject.FindGameObjectWithTag("Plat2");
        PurplePlat = GameObject.FindGameObjectWithTag("Plat3");

        if (BluePlat != null)
        {
            bluePlatScript = BluePlat.GetComponent<Platform>();
        }
        else
        {
            Debug.LogWarning("Blue platform not found.");
        }
        if (RedPlat != null)
        {
            redPlatScript = RedPlat.GetComponent<Platform>();
        }
        else
        {
            Debug.Log("Red platform not found.");
        }
        if (PurplePlat != null)
        {
            purplePlatScript = PurplePlat.GetComponent<Platform>();
        }
        else
        {
            Debug.Log("Purple platform not found.");
        }
    }

    void Update()
    {
        if (xPosition <= -8.5 || xPosition >= 8.4)
        {
            canClimb = true;
            if (xPosition <= -8.5)
            {
                xPosition = -8.5f;
            }
            if (xPosition >= 8.4)
            {
                xPosition = 8.4f;
            }
        }
        else
        {
            canClimb = false;
        }

        if (gettingKilled)
        {
            if (timer < killSpeed)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                inputHandling.gameStart = false;
                Debug.Log("Player killed!");
                gameLogic.StartRound(yourMurderer);
            }
        }

        xPosition = transform.position.x;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void MoveLeft()
    {
        if (rigidBody != null)
        {
            if (xPosition >= -8.5)
            {
                rigidBody.velocity = new Vector2(-movementSpeed, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }
        }
    }
    public void MoveRight()
    {
        if (rigidBody != null)
        {
            if (xPosition <= 8.4)
            {
                rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }
        }
    }

    public void Jump()
    {
        if (canClimb)
        {
            rigidBody.velocity = new Vector2(transform.position.x,climbSpeed);
        }
        else if (canJump)
        {
            rigidBody.velocity = Vector2.up * jumpStrength;
            canJump = false;
        }
    }

    private void EnterKillZone()
    {
        Debug.Log("Touched kill zone!");
        rigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
        canJump = false;
        gettingKilled = true;
    }
    private void ExitKillZone()
    {
        Debug.Log("Exit kill zone!");
        rigidBody.constraints = RigidbodyConstraints2D.None;
        canJump = true;
        gettingKilled = false;
        timer = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;

        if (inputHandling.numPlayers >= 2)
        {
            if (collision.collider == bluePlatScript.killZone)
            {
                yourMurderer = "Blue";
                EnterKillZone();
            }
            if (collision.collider == redPlatScript.killZone)
            {
                yourMurderer = "Red";
                EnterKillZone();
            }
        }
        if (inputHandling.numPlayers >= 3)
        {
            if (collision.collider == purplePlatScript.killZone)
            {
                yourMurderer = "Purple";
                EnterKillZone();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canClimb = false;
       
        if (inputHandling.numPlayers >= 2)
        {
            if (collision.collider == bluePlatScript.platform || collision.collider == redPlatScript.platform)
            {
                canJump = false;
            }
            else if (collision.collider == bluePlatScript.killZone || collision.collider == redPlatScript.killZone)
            {
                ExitKillZone();
            }
        }
        if (inputHandling.numPlayers >= 3)
        {
            if (collision.collider == purplePlatScript.platform)
            {
                canJump = false;
            }
            else if (collision.collider == purplePlatScript.killZone)
            {
                ExitKillZone();
            }
        }
    }
}
