using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicLeapTools;


public class CailbrationManager : MonoBehaviour
{
    public GameObject[] objs;
    public GameObject[] toDisableDraggableObjs;
    public LineRenderer controlerPointerLineRenderer;
    public ControlInput controlInput; 
    
    List <MagicLeapTools.PointerReceiver> pointReceivers = new List<MagicLeapTools.PointerReceiver>(); 
    void Start()
    {
        objs = GameObject.FindGameObjectsWithTag("ToHide");
        toDisableDraggableObjs = GameObject.FindGameObjectsWithTag("ToDisable");

        foreach(var i in toDisableDraggableObjs)
        {
            pointReceivers.Add(i.GetComponent<MagicLeapTools.PointerReceiver>());

        }

        Debug.Log("showing in calibration manager");
        Debug.Log($"{objs.Length}, objs");

    }

   public void ToggleVisibility()
    {
        foreach(var item in objs)
        {
            item.SetActive(!item.activeInHierarchy);
        }

        controlerPointerLineRenderer.enabled = !controlerPointerLineRenderer.enabled;

        foreach (var i in pointReceivers)
        {
            i.draggable = !i.draggable;
        }

        Debug.Log("Toggle visibility" + controlerPointerLineRenderer.enabled);


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
