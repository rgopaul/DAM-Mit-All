using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class moveWater : MonoBehaviour
{
    // private Vector3 scaleChange, positionChange;
    public Slider WaterLevel;
    public float time= 0;
    // Start is called before the first frame update
    void Start()
    {
        // healthbar.GetComponent<RectTransform>();  
        // transform.localScale= new Vector3(1,0,0);
        // waterLevel.value;
        Debug.Log("Water level");
        Debug.Log(WaterLevel.value);
    }
    // Update is called once per frame
    void Update()
    {   
        WaterLevel.value += (Time.deltaTime);
        Debug.Log(WaterLevel.value);
    }
    public float IncreaseSize(float speed){
        return WaterLevel.value+=speed;
    }
}
