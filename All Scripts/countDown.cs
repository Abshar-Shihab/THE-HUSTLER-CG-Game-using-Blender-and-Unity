using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countDown : MonoBehaviour
{
    public float timeStart = 60;
    public Text Textbox;

    void Start()
    {
        Textbox.text = timeStart.ToString();
    }

    void Update()
    {
        timeStart -= Time.deltaTime;
        Textbox.text = timeStart.ToString("0");

        if(timeStart <= 0)
        {
            Textbox.enabled= false;
            FindObjectOfType<GameManager>().EndGame();
        }
    }

}
