using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <  gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<TMPro.TMP_Text>().text = "" + RollStat();
        }
    }

    int RollStat()
    {
        int sum = 0;
        int[] vals = new int[4];
        int currLowest = 0;
        for (int i = 0; i < 4; i++)
        {
            vals[i] = Random.Range(1, 7);
            if (vals[i] < currLowest) currLowest = i;
        }
        for (int i = 0; i < 4; i++)
        {
            if (i == currLowest) continue;
            sum += vals[i];
        }
        return sum;
    }
}
