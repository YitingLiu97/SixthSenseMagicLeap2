using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum State { Intro, Alarm, Book, TV, Door, Outro, Filler };
    public State defaultState;
    public AudioSource introAudio;
    public List<AudioSource> alarmAudios;
    public List<AudioSource> bookAudios;
    public State currentState; 

    void Start()
    {
        currentState = State.Intro;
        StartCoroutine(AudioEnded(introAudio));
    }

    public IEnumerator AudioEnded(AudioSource audio)
    {
        Debug.Log($"{audio} is ended at {audio.clip.length}");
        currentState = State.Filler;
        yield return new WaitForSeconds(audio.clip.length);

    }

    void LoopAudioGroup(List<AudioSource> audios)
    {
        foreach(var item in audios)
        {
            item.loop = true;
            if(!item.isPlaying)
            {
                item.Play();

            }
            else
            {

                item.Stop();
            
            }
        }


    
        
        }
    // changes based on states 
    public void StateElements(State st)
    {
        switch(st)
        {
            case State.Intro:
            
    
           
          
            // play voice over audio from companion 
            // after the voice over is done, move on to the next state 

            break;

            case State.Filler:
            // if there is no collision, play filler audio source 

            break;

            case State.Alarm:
            Debug.Log("Alarm State");
            LoopAudioGroup(alarmAudios);
            // if the user collided
            // play alarm sound attached to the object 
            // turn off filler audio source 
            // turn on audio source group 
            // keep on repeating until use touches the book 

            break;
            case State.Book:
            Debug.Log("Alarm State");

            LoopAudioGroup(bookAudios);

            // collide with the book 
            // turn off filler audio source 
            // place audio source group two 
            // keep on repeating until user touches the door 

            break;
            case State.TV:
            // collide with the TV 
            // place audio source group three / additional - no need  
            // keep on repeating until user touches the door 
            // turn off filler audio source 


            break;
            case State.Door:
            // door opening sound effect 
            // end screen 
            // companion voice over 
            // turn off filler audio source 


            break;
            case State.Outro:
            // Filler state 
            // self talk audio 
            // screen fading black 


            break;

        }

    }


    void EndOfAudio()
    {
    }
}