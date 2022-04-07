using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    class Key
    {
        public int keyColor;

        public Key(int keyColor)
        {
            this.keyColor = keyColor;
        }
    }

    [SerializeField] int jumpSpeed;
    [SerializeField] float gravity;
    [SerializeField] GameObject failUI;
    [SerializeField] GameObject successUI;
    [SerializeField] Material groundMaterial;
    [SerializeField] int rainbowStorySize;
    [SerializeField] Text scoreText;
    Rigidbody RB;
    Camera C;
    float score;
    bool isJumped;
    List<Key> inventory;
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        C = Camera.main;
        inventory = new List<Key>();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        score += Time.deltaTime;
        scoreText.text = "Score: " + (int) score;


        RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y - gravity, RB.velocity.z);
        
        if (transform.eulerAngles.z > 270 || (transform.eulerAngles.z > -90 && transform.eulerAngles.z <= 30)) transform.Rotate(new Vector3(0, 0, -1), Time.deltaTime * 100);
        else transform.rotation = Quaternion.Euler(0, 0, -90);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            RB.velocity = new Vector3(RB.velocity.x, jumpSpeed, RB.velocity.z);
            isJumped = true;
        }

        if (isJumped)
        {
            transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * 1000);

            if (transform.eulerAngles.z >= 25 && transform.eulerAngles.z < 270)
            {
                isJumped = false;
                transform.rotation = Quaternion.Euler(0, 0, 25);
            }
        }

        C.transform.position = Vector3.Lerp(C.transform.position, transform.position, 0.075F);
        C.transform.position = new Vector3(-2, C.transform.position.y, -30);

        groundMaterial.color = Color.HSVToRGB(Mathf.Clamp(transform.position.y, 0, rainbowStorySize * 15) / (rainbowStorySize * 15), .9F, .9F);
    }

    void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;

        if (tag == "Fail")
        {
            Time.timeScale = 0;
            failUI.SetActive(true);
        }
        else if (tag == "Coin")
        {
            Destroy(other.gameObject);
            score += 10;
        }
        else if (tag == "KeyRed")
        {
            Destroy(other.gameObject);
            inventory.Add(new Key(0));
            score += 10;
        }
        else if (tag == "KeyGreen")
        {
            Destroy(other.gameObject);
            inventory.Add(new Key(1));
            score += 10;
        }
        else if (tag == "KeyBlue")
        {
            Destroy(other.gameObject);
            inventory.Add(new Key(2));
            score += 10;
        }
        else if (tag == "DoorRed")
        {
            if (FindKey(0))
            {
                Destroy(other.gameObject);
            }
            else
            {
                Time.timeScale = 0;
                failUI.SetActive(true);
            }
        }
        else if (tag == "DoorGreen")
        {
            if (FindKey(1))
            {
                Destroy(other.gameObject);
            }
            else
            {
                Time.timeScale = 0;
                failUI.SetActive(true);
            }
        }
        else if (tag == "DoorBlue")
        {
            if (FindKey(2))
            {
                Destroy(other.gameObject);
            }
            else
            {
                Time.timeScale = 0;
                failUI.SetActive(true);
            }
        }
        else if (tag == "Finish")
        {
            Time.timeScale = 0;
            successUI.SetActive(true);
        }
    }

    bool FindKey(int keyColor)
    {
        foreach(Key key in inventory)
        {
            if (key.keyColor == keyColor)
            {
                inventory.Remove(key);
                return true;
            }
        }

        return false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
}
