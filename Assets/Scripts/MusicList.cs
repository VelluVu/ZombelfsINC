using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicList : MonoBehaviour {

    public List<AudioClip> musicFiles = new List<AudioClip>();
    public AudioSource source;
    string sceneName;
    int count;

	void Awake() {
        
        sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("sceneName");
        Debug.Log(SceneManager.GetActiveScene().name);
        count = 0;
    }

    private void Update()
    {
        if(count == 0) {
            if (sceneName == SceneManager.GetActiveScene().name)
            {
                switch (sceneName)
                {
                    case "MainMenu":
                        source.clip = musicFiles[0];                      
                        break;

                    case "Map":
                        source.clip = musicFiles[1];                     
                        break;

                    case "Level1":
                        source.clip = musicFiles[2];                 
                        break;

                    case "Level2":
                        source.clip = musicFiles[2];            
                        break;
                    case "Level3":
                        source.clip = musicFiles[2];                   
                        break;

                    case "Level4":
                        source.clip = musicFiles[2];                   
                        break;

                    case "Level5":
                        source.clip = musicFiles[3];               
                        break;

                    default:
                        break;
                }
                count++;
                source.PlayDelayed(0.1f);
                Debug.Log(source.isPlaying);
            }
        }
    }

}
