using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimItem : MonoBehaviour
{
    public AudioClip clip;
    [HideInInspector]
    public Vector3 m_scale;
    void Awake() => m_scale = transform.localScale;
    void Update()
    {
        if (transform.parent.GetComponent<AnimCtrl>())
            transform.Rotate(0f, 40 * Time.deltaTime, 0f);
    }
    void OnMouseDown()
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }
}
