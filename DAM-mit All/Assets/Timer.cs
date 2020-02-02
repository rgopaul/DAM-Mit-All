using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float time = 90;
    Text timerText;
    public GameState GS;
    void Start()
    {
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
          time -= Time.deltaTime;
          int itime = (int)time;

          timerText.text = "Time: " + itime.ToString();

        if (time <= 0)
        {
            GS.GameOver();
            timerText.text = "GAME OVER.";
        }
            
        //   Debug.Log(time);
        //   uiText.text=timer.ToString("F");
    }
}
