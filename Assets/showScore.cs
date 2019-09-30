using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class showScore : MonoBehaviour
{
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = gm.score.ToString();
    }
}
