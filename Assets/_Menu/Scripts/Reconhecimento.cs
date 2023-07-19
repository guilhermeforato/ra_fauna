using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reconhecimento : MonoBehaviour
{
    private void Start() {
        print("capa: " + MenuContrl.qualCapa);
        if (GameObject.FindGameObjectWithTag("help"))
        {
            print("tem: " + GameObject.FindGameObjectWithTag("help").name);
        }        
    }
}
