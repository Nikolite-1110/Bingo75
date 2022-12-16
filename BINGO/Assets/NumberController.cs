using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberController : MonoBehaviour
{

    public int number;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Text>().text = number.ToString("D2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
