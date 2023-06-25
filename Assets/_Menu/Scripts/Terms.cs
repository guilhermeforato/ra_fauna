using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terms : MonoBehaviour
{
    [SerializeField] Text txt1, txt2;
    void Start()
    {
        txt1.text = Random.Range(1, 6).ToString();
        txt2.text = Random.Range(1, 6).ToString();
    }
}
