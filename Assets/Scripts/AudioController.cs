using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

    public List<AudioClip> audioFiles = new List<AudioClip>();
    public List<AudioSource> channels = new List<AudioSource>();
    private VolumeController volumeController;

	void Awake() {
        volumeController = this.gameObject.GetComponent<VolumeController>();
	}

    public void playSound(string name){
        AudioClip source = findByName(name);
        if(source != null){
            AudioSource channel = findChannel();
            channel.clip = source;

            changeVolume(source.name, channel);
            changePitch(source.name, channel);
            channel.loop = volumeController.isLooping(name);

            channel.Play();
        } else {
        Debug.LogWarning("Found no audioclip named " + name + " did not play");
        }
    }

    public void loopSound(string name){
        AudioClip source = findByName(name);
        if(source != null){
            AudioSource channel = findChannel();
            channel.clip = source;

            changeVolume(source.name, channel);
            changePitch(source.name, channel);
            channel.loop = true;

            channel.Play();
        } else {
        Debug.LogWarning("Found no audioclip named " + name + " did not play");
        }
    }

    public void stopLooping(string name){
        foreach( AudioSource channel in channels){
            if(channel.clip != null && channel.clip.name == name && channel.loop == true){
                channel.loop = false;
                channel.Stop();
                break;
            }
        }
    }

    private void changeVolume(string name, AudioSource channel){
        channel.volume = volumeController.getVolume(name);
    }

    private void changePitch(string name, AudioSource channel){
        channel.pitch = volumeController.getPitch(name);
    }

    private AudioClip findByName(string name){
        foreach (AudioClip audioFile in audioFiles){
            if(audioFile.name == name){
                return audioFile;
            }
        }
        return null; 
    }


    private AudioSource findChannel(){
        foreach( AudioSource channel in channels){
            if(!channel.isPlaying){
                return channel;
            }
        }
        AudioSource ac = this.gameObject.AddComponent<AudioSource>();
        ac.playOnAwake = false;
        ac.loop = false;
        channels.Add(ac);
        Debug.Log("Found no free channels for " + name + ", created new");
        return ac;
    }
}
