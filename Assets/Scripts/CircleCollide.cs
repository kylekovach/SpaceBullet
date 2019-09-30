using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GetComponentInParent<EnemyController>().to_player = true;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GetComponentInParent<EnemyController>().to_player = false;
        }
    }
}
