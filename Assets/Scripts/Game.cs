using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject button;
    private float timestamp;
    public int clicks;
    [SerializeField] private string _Name;
    [SerializeField] private string phone;
    [SerializeField] private GameObject cookie;
    [SerializeField] private Vector3 movement;
    private Text Clicktext;
    private Text TimeText;
    [SerializeField] private int Seconds = 30;
    private bool Playing = false;
    private float timeleft;
    public bool GameOver = false;
    private Vector3 buttonBackup;
    private bool haveSent = false;
    private bool showGUI = false;
    [SerializeField] private GameObject sendGUI;
    [SerializeField] private string url = "http://10.2.2.98:8000";
    [SerializeField] private GameObject startText;
    public GameObject cookieSpawner;
    public bool yuh = false;
    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("ButtonObj");
        Clicktext = GameObject.Find("ClickText").GetComponent<Text>();
        TimeText = GameObject.Find("TimeText").GetComponent<Text>();
        buttonBackup = button.transform.position;
        sendGUI = GameObject.Find("_Submit");
        startText = GameObject.Find("startText");
        startText.SetActive(true);
        cookieSpawner = GameObject.Find("CookieSpawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (!Playing && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!GameOver)
            {
                Playing = true;
            }
            timeleft = Seconds;
            cookieSpawner.GetComponent<Cookiespawner>().cookieDelay -= clicks / 200;
        }


        if (Playing)
        {
            startText.SetActive(false);
            buttonMovement();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                clicks += 1;
                Vector3 random2dPosition = new Vector3() + Random.insideUnitSphere * Random.Range(0, 2f);
                random2dPosition.y = 5;
                GameObject _cookie = Instantiate(cookie, random2dPosition, Quaternion.identity);
            }
            timeleft -= Time.deltaTime;
            if (timeleft <= 0)
            {
                Playing = false;
                timeleft = Seconds;
                GameOver = true;
            }
            TimeText.text = "Time: " + Mathf.RoundToInt(timeleft).ToString();
            Clicktext.text = "Clicks: " + clicks.ToString();
        }
        if (GameOver)
        {
            if (!yuh) {
            showGUI = true;
            }
        float CPS;
        CPS = clicks / Seconds;
        Clicktext.text = "Average Clicks / second: " + CPS.ToString();
        TimeText.text = "";
        button.transform.position = buttonBackup;

    }
        if (showGUI)
        {
            sendGUI.SetActive(true);
        }
        else
{
    sendGUI.SetActive(false);
}
    }

    void buttonMovement()
{

    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
        button.transform.Translate(Vector3.down * movement.y, Space.Self);
    }
    if (Input.GetKeyUp(KeyCode.Mouse0))
    {
        button.transform.Translate(Vector3.down * -1f * movement.y, Space.Self);
    }
}
public void SendFromButton()
{
    if (checkForm())
    {
        Debug.Log(checkForm());
        if (!haveSent)
        {
            UploadFormData();
            haveSent = true;
        }
    }
}

bool checkForm()
{
    _Name = GameObject.Find("NameInput").GetComponent<InputField>().text;
    phone = GameObject.Find("Phone").GetComponent<InputField>().text;
    if (_Name == "")
    {
        return false;
    }
    return true;
}


void UploadFormData()
{
    WWWForm form = new WWWForm();
    form.AddField("navn", _Name);
    form.AddField("tlf", phone);
    form.AddField("ak", clicks);
    form.AddField("cid", 5);
    UnityWebRequest www = UnityWebRequest.Post(url, form);
    www.SendWebRequest();
    if (www.result == UnityWebRequest.Result.ConnectionError)
    {
        Debug.Log("Error: " + www.error);
    }
    else
    {
        Debug.Log("Form upload complete!");
        Debug.Log("Callback: " + www.result);
    }
    yuh = true;
    showGUI = false;
    startText.GetComponent<Text>().text = "Press Home to play again!";
    startText.SetActive(true);
}


}