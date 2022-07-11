using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    public StateManager stateManager; 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
