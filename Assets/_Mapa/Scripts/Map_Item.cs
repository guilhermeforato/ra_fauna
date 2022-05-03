using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Map_Item : MonoBehaviour
{
    public GameObject region;
    public string m_capital;
    GameObject m_gameThis;
    Map_Contrl m_contrl;
    [HideInInspector]
    public bool m_btnSelect = false;
    void Start()
    {
        m_contrl = GameObject.FindObjectOfType<Map_Contrl>();
        m_gameThis = transform.parent.gameObject;
        m_btnSelect = name == "Estado" ? true : false;
    }
    void OnMouseDown()
    {
        if (transform.parent.name == "Botoes")
        {
            if (!m_btnSelect)
            {
                resetMap();
                transform.DOScale(new Vector3(.65f, .65f, .65f), .2f).OnComplete(() => transform.DOScale(new Vector3(.8f, .8f, .8f), .2f));
            }
            m_contrl.Estado = name == "Estado" ? true : false;

            m_contrl.m_regions.SetActive(!m_contrl.Estado);

            foreach (Transform item in transform.parent)
            {
                item.GetComponent<TextMesh>().color = Color.black;
                item.transform.GetChild(0).gameObject.SetActive(false);
                item.GetComponent<Map_Item>().m_btnSelect = false;
            }
            GetComponent<TextMesh>().color = Color.white;
            transform.GetChild(0).gameObject.SetActive(true);
            m_btnSelect = true;
        }
        else
        {
            if (m_contrl.Estado)
                Set_State();
            else
                Set_Region();

            SetData();
        }
    }
    void Set_Region()
    {
        resetMap();
        region.SetActive(true);
        m_contrl.m_regions.SetActive(true);
        foreach (Transform item in transform.parent.parent)
            item.GetComponent<SpriteRenderer>().color = item.parent.name.ToLower().Contains("norte") ? m_contrl.colors[0] : item.parent.name.ToLower().Contains("nordeste") ? m_contrl.colors[1] : item.parent.name.ToLower().Contains("oeste") ? m_contrl.colors[2] : item.parent.name.ToLower().Contains("sudeste") ? m_contrl.colors[3] : m_contrl.colors[4];
    }
    void resetMap()
    {
        foreach (Transform item in m_contrl.m_regions.transform)
            item.gameObject.SetActive(false);
        foreach (var item in GameObject.FindObjectsOfType<Map_Item>())
            if (item.transform.parent.name != "Botoes")
            {
                item.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                item.transform.parent.GetChild(1).gameObject.SetActive(false);
            }
        m_contrl.tx_Estado.text = "ESTADO: ";
        m_contrl.tx_Região.text = "REGIÃO: ";
        m_contrl.tx_Capital.text = "CAPITAL: ";
    }
    void Set_State()
    {
        resetMap();
        m_contrl.m_regions.SetActive(false);
        m_gameThis.GetComponent<SpriteRenderer>().color = transform.parent.parent.name.ToLower().Contains("norte") ? m_contrl.colors[0] : transform.parent.parent.name.ToLower().Contains("nordeste") ? m_contrl.colors[1] : transform.parent.parent.name.ToLower().Contains("oeste") ? m_contrl.colors[2] : transform.parent.parent.name.ToLower().Contains("sudeste") ? m_contrl.colors[3] : m_contrl.colors[4];
        transform.parent.GetChild(1).gameObject.SetActive(true);
    }
    void SetData()
    {
        m_contrl.tx_Estado.text = ("ESTADO: " + m_gameThis.name).ToUpper();
        m_contrl.tx_Região.text = ("REGIÃO: " + m_gameThis.transform.parent.name).ToUpper();
        m_contrl.tx_Capital.text = ("CAPITAL: " + m_capital).ToUpper();
    }
}
