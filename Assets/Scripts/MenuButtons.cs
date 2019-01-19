using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {

    public AudioMixer audioMixer;
    public Slider slider;
    public InputField textField;
    public AudioSource sound;

    private void Awake()
    {
        ScoreTable.currentPlayer = null;
    }

    public void StartGame()
    {
        
        if (textField.text.Length > 0 && !PlayerPrefs.HasKey(textField.text))
        {
            GameStatus.gameBegin = true;
            PlayerPrefs.SetString(textField.text, textField.text);
            PlayerPrefs.SetInt(textField.text + "levels", 1);
            PlayerPrefs.SetInt(textField.text + "scores", 0);
            //testing ways to do highscore
            ScoreTable.SetScore(textField.text, "level", 1);
            ScoreTable.SetScore(textField.text, "score", 0);
            ScoreTable.currentPlayer = textField.text;
            ScoreTable.SaveScores();
            Debug.Log("New Challenger! Name: " + textField.text + " Level: " + ScoreTable.GetScore(textField.text, "level") + ", Score: " + ScoreTable.GetScore(textField.text, "score"));
            SceneManager.LoadScene("Map");

        }
        else if (PlayerPrefs.HasKey(textField.text))
        {
            ScoreTable.currentPlayer = textField.text;
            Debug.Log(PlayerPrefs.GetString(textField.text));
            ScoreTable.LoadScores();
            SceneManager.LoadScene("Map");
        }
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void MasterVolume(float sliderValue)
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetGore(float sliderVal)
    {
        GameStatus.gore = Mathf.RoundToInt(sliderVal);
    }

    public void SoundOnPress()
    {
        sound = gameObject.GetComponent<AudioSource>();
        sound.Play();
    }
    //TODO : Load scores and player names to score table 
    //TODO : Settings volume slider needs to change game volume -- MinEffort Done --
}
