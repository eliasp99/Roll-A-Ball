using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;

    [Header("UI Stuff")]
    public GameObject gameOverScreen;

    private Rigidbody rb;
    private int pickUpCount;
    private Timer timer;
    private bool gameOver = false;
    public GameObject resetPoint;
    bool resetting = false;
    bool grounded = true;
    Color originalColour;

    [Header("UI")]
    public TMP_Text pickUpText;
    public TMP_Text timerText;
    public TMP_Text winTimeText;
    public GameObject winPanel;
    public GameObject inGamePanel;

    // Start is called before the first frame update
    

     void Start()
    {
        //Turn off our in game panel
        inGamePanel.SetActive(true);
        //Turn on our win panel
        winPanel.SetActive(false);

        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in our scene
        pickUpCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Run the Check Pickups Function
        CheckPickUps();
        //Get the timer object and start the timer
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();

        gameOverScreen.SetActive(false);
        //resetPoint = GameObject.Find("Reset Point");
        print("Reset Point" + resetPoint.transform.position);
        originalColour = GetComponent<Renderer>().material.color;

    }
    private void Update()
    {
        timerText.text = "Time: " + timer.GetTime().ToString("F2");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameOver == true)
            return;

        if (resetting)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);

        private void OnCollisionStay(Collision collision)
        {
            if (collision.collider.CompareTag("Ground"))
                grounded = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.collider.CompareTag("Ground"))
                grounded = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            print(other.name);
            Destroy(other.gameObject);
            //Decrement the pickup count
            pickUpCount--;
            //Run the Check Pick Ups function
            CheckPickUps();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;
    }

    void CheckPickUps()
    {
        pickUpText.text = "Pick Ups Left: " + pickUpCount;
        if(pickUpCount == 0)
        {

            WinGame();

        }
    }

    void WinGame()
    {
       //Set our game over to true
        gameOver = true;
        //Turn off our in game panel
        inGamePanel.SetActive(false);
        //Turn on our win panel
        winPanel.SetActive(true);
        pickUpText.color = Color.green;
        pickUpText.fontSize = 60;
        //Stop the timer
        timer.StopTimer();
        //Display our time to the win time text
        winTimeText.text = "Your time was: " + timer.GetTime().ToString("F2");

        //Stop the ball from moving
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void SetCountText()
    {
        pickUpText.text = "Count: " + pickUpCount.ToString();
        if(pickUpCount >= 12)
        {
            WinGame();
        }
    }
    

    //Temporary - Remove when doing A2 modules
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
