using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public Image[] backgrounds;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i< backgrounds.Length; i++)
        {
            backgrounds[i].enabled = false;
        }

        index = UnityEngine.Random.Range(0, backgrounds.Length);

        backgrounds[index].enabled = true;

    }

    
    
}
