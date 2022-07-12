using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicLeapTools;
using UnityEngine.Video; 

public class ChangeObject : MonoBehaviour
{
   ControlInput controlInput;
    public int counter;
    [SerializeField] SpatializationRandomizer sRandomizer;
    public GameObject fireAlarm, book, tv;
    private AudioSource fireAlarmAS, bookAS, tvAS, tvChildAS;
    private VideoPlayer tvVP; 
    void Start()
    {

        controlInput = GetComponent<ControlInput>();
        controlInput.OnTouchHold.AddListener(AddCounter);
        fireAlarmAS = fireAlarm.GetComponent<AudioSource>();
        bookAS = book.GetComponent<AudioSource>();
        tvAS = tv.GetComponent<AudioSource>();
       //tvChildAS = tv.GetComponentInChildren<AudioSource>();

        tvVP = tv.GetComponentInChildren<VideoPlayer>();
    }

    void AddCounter()
    {

        counter++;
        if(counter % 3 == 0)
        {
            Debug.Log("show alarm scene");
            //alarm scene 
            sRandomizer.alarm = true;
            ToggleActivityBook(false);
            ToggleActivityAlarm(true);
            ToggleActivityTV(false);

        }
        else if(counter % 3 == 1)
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
        //fireAlarm.SetActive(active);

        if(active)
        {
            if(!fireAlarmAS.isPlaying)
            {
                fireAlarmAS.Play();
            }
        }
        else
        {
            fireAlarmAS.Stop();

        }


        foreach(var item in sRandomizer.audioSourceChildrenAlarm)
        {
            item.gameObject.SetActive(active);
        }
    }
    private void ToggleActivityBook(bool active)
    {
        sRandomizer.audioGroupBook.gameObject.SetActive(active);
        // book.SetActive(active);
        if(active)
        {
            if(!bookAS.isPlaying)
            {
                bookAS.Play();
            }
        }
        else
        {
            bookAS.Stop();

        }
        foreach(var item in sRandomizer.audioSourceChildrenBook)
        {
            item.gameObject.SetActive(active);
        }
    }

    private void ToggleActivityTV(bool active)
    {
        sRandomizer.audioGroupTV.gameObject.SetActive(active);
        //  tv.SetActive(active);
        if(active)
        {
            if(!tvAS.isPlaying)
            {
                tvAS.Play();
            } 
          /*  if(!tvChildAS.isPlaying)
            {
                tvChildAS.Play();
            }*/
            if(!tvVP.isPlaying)
            {

                tvVP.Play();
            }

         
         
        }
        else
        {
            tvAS.Stop();
          //  tvChildAS.Stop();
            tvVP.Stop();
        }

        foreach(var item in sRandomizer.audioSourceChildrenTV)
        {
            item.gameObject.SetActive(active);
        }
    }


   
}
