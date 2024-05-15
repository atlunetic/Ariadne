using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink_faster : MonoBehaviour
{
    float time;

    // Update is called once per frame
    void Update()
    {
        if (time < 0.2f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time);
            if (time > 0.5f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
    }
}
