using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicLeapTools;

public class ChangeObject : MonoBehaviour
{
    ControlInput controlInput;
    public int counter;
    [SerializeField]SpatializationRandomizer sRandomizer;
    public GameObject fireAlarm, book, tv;
    void Start()
    {
  
        controlInput = GetComponent<ControlInput>();
        controlInput.OnTouchHold.AddListener(AddCounter);
    }

    void AddCounter() {

        counter++;
        if(counter % 3 == 1)
        {
            Debug.Log("show alarm scene");
            //alarm scene 
            sRandomizer.alarm = true;
            ToggleActivityBook(false);
            ToggleActivityAlarm(true);
            ToggleActivityTV(false);

        }
        else if(counter % 3 == 2)
        {
            Debug.Log("show book scene");

            sRandomizer.book = true;
            ToggleActivityAlarm(false);
            ToggleActivityBook(true);
            ToggleActivityTV(false);

        }
        else
        {
            Debug.Log("show tv scene");

            sRandomizer.tv = true;
            ToggleActivityAlarm(false);
            ToggleActivityBook(false);
            ToggleActivityTV(true);



        }


    }
    private void ToggleActivityAlarm(bool active)
    {
        sRandomizer.audioGroupAlarm.gameObject.SetActive(active);
        fireAlarm.SetActive(active);
        foreach(var item in sRandomizer.audioSourceChildrenAlarm)
        {
            item.gameObject.SetActive(active);
        }
    }
    private void ToggleActivityBook(bool active)
    {
        sRandomizer.audioGroupBook.gameObject.SetActive(active);
        book.SetActive(active);

        foreach(var item in sRandomizer.audioSourceChildrenBook)
        {
            item.gameObject.SetActive(active);
        }
    }

    private void ToggleActivityTV(bool active)
    {
        sRandomizer.audioGroupTV.gameObject.SetActive(active);
        tv.SetActive(active);

        foreach(var item in sRandomizer.audioSourceChildrenTV)
        {
            item.gameObject.SetActive(active);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
