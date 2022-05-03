using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Balde : MonoBehaviour
{
    GameObject Any;
    public GameObject prefabSplash, stonesParent, treesParent;
    public FlorestaController controller;
    bool draging = false;
    Vector3 startPosition;

    void Start() => startPosition = transform.parent.position;

    void OnMouseDown() => draging = true;

    void OnMouseUp()
    {
        if (Any != null)
        {
            GameObject game = Instantiate(prefabSplash);
            game.transform.SetParent(Any.transform);
            game.transform.localPosition = Vector3.zero;
            game.transform.localScale = new Vector3(7, 7, 7);

            if (Any.transform.GetChild(1).localScale.x > 3)
                Any.transform.GetChild(1).localScale = new Vector3(3, 3, 3);
            else
            {
                Destroy(Any.transform.GetChild(1).gameObject);
                Any.transform.GetChild(0).gameObject.SetActive(false);
                Any.GetComponent<BoxCollider>().enabled = false;
            }
            controller.cont--;
            if (controller.cont == 0)
            {
                for (int i = 0; i < stonesParent.transform.childCount; i++)
                    stonesParent.transform.GetChild(i).DOScale(0, .5f);

                for (int i = 0; i < treesParent.transform.childCount; i++)
                    treesParent.transform.GetChild(i).DOScale(0, .5f);
                
                controller.AbrePainel();

                gameObject.transform.parent.DOScale(0, .5f);
            }
        }

        transform.parent.position = startPosition;
        draging = false;
        Any = null;
    }

    void OnMouseDrag()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.parent.position).z;
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        transform.parent.position = new Vector3(pos_move.x, .3f, pos_move.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Tree"))
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            Any = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Tree"))
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            Any = null;
        }
    }
}
