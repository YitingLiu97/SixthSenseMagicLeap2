using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatializationRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    // randomize the location of the audio sources around the radius of the head 
    public GameObject playerCamera;
    public Vector2 range;
    public GameObject audioGroupAlarm;
    public List<GameObject> audioChildrenAlarm;
    public List<AudioSource> audioSourceChildrenAlarm;
    //  public List<Vector3> newLocationsAlarm;
    public List<float> timerRemainingsforAlarm;
    public float timerForAlarm, timerForAlarmRemaining;

    public GameObject audioGroupBook;
    public List<GameObject> audioChildrenBook;
    public List<AudioSource> audioSourceChildrenBook;
    //    public List<Vector3> newLocationsBook;
    public List<float> timerRemainingsforBook;
    public float timerForBook, timerForBookRemaining;

    public GameObject audioGroupTV;
    public List<GameObject> audioChildrenTV;
    public List<AudioSource> audioSourceChildrenTV;
    //    public List<Vector3> newLocationsTV;
    public List<float> timerRemainingsforTV;
    public float timerForTV, timerForTVRemaining;

    //public List<float> timerRemainingsforDoor;

    public bool experienceTimerOn = false;
    public bool alarm = false, book = false, tv = false, door = false;

    void Awake()
    {

        timerForAlarmRemaining = timerForAlarm;
        timerForBookRemaining = timerForBook;
        timerForTVRemaining = timerForTV;
        GetListNumberAndClipLength(audioGroupAlarm, audioChildrenAlarm, audioSourceChildrenAlarm, timerRemainingsforAlarm);
        GetListNumberAndClipLength(audioGroupBook, audioChildrenBook, audioSourceChildrenBook, timerRemainingsforBook);
        GetListNumberAndClipLength(audioGroupTV, audioChildrenTV, audioSourceChildrenTV, timerRemainingsforTV);
        //GetListNumberAndClipLength(audioGroupDoor, audioChildrenAlarm, audioSourceChildrenAlarm);
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


    //randomize after end of the audio 
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


        if(tv)
        {

            if(experienceTimerOn)
            {
                if(timerForTVRemaining > 0)
                {

                    timerForTVRemaining -= Time.deltaTime;


                }
                else
                {

                    Debug.Log($"end audio for TV");
                    timerForTVRemaining = timerForTV;
                    CheckAudio(audioSourceChildrenTV);
                }

            }

            UpdateLocations(playerCamera, audioChildrenTV, timerRemainingsforTV, audioSourceChildrenTV);
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

            if(timerRemainings[i] > 0)
            {


                timerRemainings[i] -= Time.deltaTime;
            }
            else
            {
                timerRemainings[i] = audioSourceChildren[i].clip.length;

                _audioChildren[i].transform.localPosition = Randomization(_player.transform, _audioChildren[i].transform);


            }


        }
    }


}
