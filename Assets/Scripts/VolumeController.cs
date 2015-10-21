using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VolumeController : MonoBehaviour {

    public Hashtable volumes = new Hashtable();
    public Hashtable randVolumes = new Hashtable();
    public Hashtable pitches = new Hashtable();
    public Hashtable randPitches = new Hashtable();
    public Hashtable looping = new Hashtable();
   
    void Awake(){
        volumes["PlayerHit"] = 0.3f;
        volumes["PlayerSpawn"] = 0.7f;
        volumes["MusicLoop"] = 0.5f;
        volumes["explosion_player"] = 0.5f;

        pitches["PlayerJump"] = 3.0f;
        pitches["PlayerHit"] = 3.0f;
        pitches["explosion_player"] = 1.0f;

        randVolumes["PlayerWalk"] = newRand(0.1f,0.3f);
        randPitches["PlayerWalk"] = newRand(0.5f,3.0f);

        looping["PlayerWalk"] = true;
    } 

    public bool isLooping(string name){
        if(looping.Contains(name)){
            return (bool) looping[name];
        }
        return false;
    }

    // Public methods
    public float getVolume(string name){
        if(hasVolume(name))
            return (float) volumes[name];
        if(hasRandVolume(name))
            return getRandVolume(name);
        Debug.LogWarning("No volume with name " + name + " found, defaulted to 1.");
        return 1f;
    }

    public float getPitch(string name){
        if(hasPitch(name)){
            return (float) pitches[name];
        }
        if(hasRandPitch(name)){
            return getRandPitch(name);
        }
        Debug.LogWarning("No pitch with name " + name + " found, defaulted to 1.");
        return 1f;
    }

    // private Volume
    private float getRandVolume(string name){
        float[] values = (float[])randVolumes[name];
        return Random.Range(values[0], values[1]);
    }

    private bool hasVolume(string name){
        return volumes.Contains(name);
    }
    private bool hasRandVolume(string name){
        return randVolumes.Contains(name);
    }

    // private Pitch 
    private float getRandPitch(string name){
        float[] values = (float[])randPitches[name];
        return Random.Range(values[0], values[1]);
    }

    private bool hasPitch(string name){
        return pitches.Contains(name);
    }

    private bool hasRandPitch(string name){
        return randPitches.Contains(name);
    }

    // constructor helper
    private float[] newRand(float min, float max){
        float[] tmp = new float[2];
        tmp[0] = min; // Min
        tmp[1] = max; // Max
        return tmp;
    }
}
