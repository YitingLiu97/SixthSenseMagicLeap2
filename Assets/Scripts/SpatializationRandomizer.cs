using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatializationRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    // randomize the location of the audio sources around the radius of the head 
    public GameObject playerCamera;
    public GameObject audioGroupAlarm;
    public List<GameObject> audioChildrenAlarm;
    public List<AudioSource> audioSourceChildrenAlarm;
    public List<Vector3> newLocationsAlarm;
    public GameObject audioGroupBook;
    public List<GameObject> audioChildrenBook;
    public List<AudioSource> audioSourceChildrenBook;
    public List<Vector3> newLocationsBook;
    public Vector2 range;
    public List<float> timerRemainingsforAlarm;
    public List<float> timerRemainingsforBook;
    public List<float> timerRemainingsforDoor;
    public float timerForAlarm, timerForAlarmRemaining;
    public float timerForBook, timerForBookRemaining;
    public bool experienceTimerOn = false;
    public bool alarm = false, book = false, tv = false, door = false;

    void Awake()
    {

        timerForAlarmRemaining = timerForAlarm;
        timerForBookRemaining = timerForBook;
        GetListNumberAndClipLength(audioGroupAlarm, audioChildrenAlarm, audioSourceChildrenAlarm, timerRemainingsforAlarm);
        GetListNumberAndClipLength(audioGroupBook, audioChildrenBook, audioSourceChildrenBook, timerRemainingsforBook);
        //        GetListNumberAndClipLength(audioGroupDoor, audioChildrenAlarm, audioSourceChildrenAlarm);
    }

    private void GetListNumberAndClipLength(GameObject _audioGroup, List<GameObject> _audioChildren, List<AudioSource> _audioSourceChildren, List<float> _timerRemainingsforType)
    {
        Transform[] gos = _audioGroup.GetComponentsInChildren<Transform>();
        int groupChildren = gos.Length;


        for(int i = 1; i < groupChildren; i++)
        {
            _audioChildren.Add(gos[i].gameObject);
        }

        foreach(var item in _audioChildren)
        {
            AudioSource a = item.GetComponent<AudioSource>();
            _audioSourceChildren.Add(a);

        }

        foreach(var item in _audioSourceChildren)
        {
            _timerRemainingsforType.Add(item.clip.length);
        }
    }

    Vector3 Randomization(Transform _player, Transform _audio)
    {
        Vector3 _newLocation = new Vector3(Random.Range(range.x, range.y), Random.Range(range.x, range.y), Random.Range(range.x, range.y));
        Vector3 center = _player.localPosition;
        float radius = Random.Range(range.x, range.y);

        float distance = Vector3.Distance(_newLocation, center); //distance from ~green object~ to *black circle*

        if(distance > radius) //If the distance is less than the radius, it is already within the circle.
        {
            Vector3 fromOriginToObject = _newLocation - center; //~GreenPosition~ - *BlackCenter*
            fromOriginToObject *= radius / distance; //Multiply by radius //Divide by Distance
            _newLocation = center + fromOriginToObject; //*BlackCenter* + all that Math
        }
        Debug.Log($"new location is {_newLocation}");

        return _newLocation;


    }


    //randomize after end of the audio 


    void CheckAudio(List<AudioSource> audioSourceChildren)
    {

        foreach(var item in audioSourceChildren)
        {
            if(item.isPlaying)
            {

                item.Stop();

            }
            else
            {
                item.Play();

            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        if(alarm)
        {

            if(experienceTimerOn)
            {
                if(timerForAlarmRemaining > 0)
                {

                    timerForAlarmRemaining -= Time.deltaTime;


                }
                else
                {

                    Debug.Log($"end audio for alarm");
                    timerForAlarmRemaining = timerForAlarm;
                    CheckAudio(audioSourceChildrenAlarm);
                }

            }

            UpdateLocations(playerCamera, audioChildrenAlarm, timerRemainingsforAlarm, audioSourceChildrenAlarm);
        }



        if(book)
        {

            if(experienceTimerOn)
            {
                if(timerForBookRemaining > 0)
                {

                    timerForBookRemaining -= Time.deltaTime;


                }
                else
                {

                    Debug.Log($"end audio for book");
                    timerForBookRemaining = timerForBook;
                    CheckAudio(audioSourceChildrenBook);
                }

            }

            UpdateLocations(playerCamera, audioChildrenBook, timerRemainingsforBook, audioSourceChildrenBook);
        }
    }

    
/*    private void EndWithTimer(float _timerForGroupRemaining, float _timerForType, List<AudioSource> audioSourceChildrenGroup)
    {
        Debug.Log($"end audio {_timerForType}, {_timerForGroupRemaining}");

        if(_timerForGroupRemaining > 0)
        {

            _timerForGroupRemaining -= Time.deltaTime;


        }
        else
        {

            Debug.Log($"end audio");
            _timerForGroupRemaining = _timerForType;
            CheckAudio(audioSourceChildrenGroup);
        }
    }*/

    void UpdateLocations(GameObject _player, List<GameObject> _audioChildren, List<float> timerRemainings, List<AudioSource> audioSourceChildren)
    {

        for(int i = 0; i < _audioChildren.Count; i++)
        {

            if(timerRemainings[i] * 10 > 0)
            {


                timerRemainings[i] -= Time.deltaTime;
            }
            else
            {
                _audioChildren[i].transform.localPosition = Randomization(_player.transform, _audioChildren[i].transform);

                timerRemainings[i] = audioSourceChildren[i].clip.length;

            }


        }
    }


}
