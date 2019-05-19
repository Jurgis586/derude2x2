using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class labyrinth_upper_floor_trigger : MonoBehaviour
{
    public AudioClip music;
    public AudioSource audio_player;
    public float volume_mult = 1;

    public labyrinth_boss boss;

    private float old_volume;
    // Start is called before the first frame update
    void Start()
    {
        //audio_player = GameObject.Find("Background_music_player").GetComponent<AudioSource>();
        // need to check if the labyrinth has been beaten before
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player has entered the labyrinth");

            boss.activate();

            audio_player.Stop();
            audio_player.clip = music;
            old_volume = audio_player.volume;
            audio_player.volume *= volume_mult;
            audio_player.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            audio_player.volume = old_volume;
            Debug.Log("player has left the labyrinth");
            audio_player.Stop();
        }
    }
}
