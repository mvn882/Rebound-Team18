﻿using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
//using UnityEditor;

public class ScoreScreen : MonoBehaviour
{
    public Text NameText;
    public Text _scoreText;

    private int _score;
    private int _count;
    private int _offset = 100;
    private bool _baseScore = false;
    private HighScores _highScoreManager;
    public GameObject _FacebookManager;
    public int _levelIndex;
    private bool _highscore = false;
    private string lowestUser = "";
    private string _name = "";
    private Highscore[] _list;
    public Button continueButton;
    public Button continueButton2;
    public Text continueText;
    public Text continueText2;
    private int minScore = 10000;

    // Use this for initialization
    void Start ()
    {
       // _FacebookManager.GetComponent<FBScript>().Init();
       // _FacebookManager.GetComponent<FBScript>().FBloginWithPermissions();
        _highscore = true;
        _levelIndex = PlayerPrefs.GetInt("Scene");
        var scoreMultiplier = _levelIndex - 4;
        
        _score = PlayerPrefs.GetInt("Score");
        _score += (int)(PlayerPrefs.GetFloat("time") * 100.0f);
        _score *= scoreMultiplier;
        PlayerPrefs.SetInt("Score", 0);
        if (_score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", _score);
        }

        _highScoreManager = GetComponent<HighScores>();
        //list = highScoreManager.GetHighscoreslist();
	    /*highScoreManager.SetLeaderboard(_levelIndex - 3);*///tutorial is at index 3
        print(_levelIndex);

        //   int lowestScore = 10000000;

        //int length = list.Length;
        //   for (int i = 0; i < length; ++i)
        //   {
        //       if (list[i].levelIndex == _levelIndex)
        //       {
        //           ++NrOfNames;
        //           if (list[i].score < lowestScore)
        //           {
        //               lowestScore = list[i].score;
        //               lowestUser = list[i].username;
        //           }
        //           if (list[i].score < _score)
        //           {
        //               highscore = true;
        //           }
        //       }
        //   }

        //if (NrOfNames < 5)
        //{
        //       highscore = true;
        //   }

        minScore *= scoreMultiplier;
       
        //unlock level:
        //-------------
        if (_levelIndex == 24)
        {
            continueText.text = "Game Completed!";
            continueText2.text = "Game Completed!";
        }
        else
        {
            continueText.text = "Get " + minScore + " Points\n to unlock the next level!";
            continueText2.text = "Get " + minScore + " Points\n to unlock the next level!";
        }

        if (PlayerPrefs.GetInt("Highscore") > minScore && _levelIndex < 24)
        {
            continueText.enabled = false;
            continueText2.enabled = false;
            continueButton.interactable = true;
            continueButton2.interactable = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
	{

        if (!_baseScore)
	    {
	        if (_count < _score)
	        {
                _scoreText.GetComponent<Text>().text = _count.ToString();
	            _count += _offset;
	        }
	        else
	        {
                _scoreText.GetComponent<Text>().text = _score.ToString();
	            _baseScore = true;
	        }   
        }

        //if (highscore && name != "")//check if highscore && name entered
        //{
        //    //if (NrOfNames >= 5)
        //    //    highScoreManager.DeleteHighscore(lowestUser);
        //    highScoreManager.AddNewHighscore(name , _score, _levelIndex);
        //    highscore = false;
        //}
    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        if (_levelIndex < 7)//max levelindex for now
        {
            SceneManager.LoadScene(_levelIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    public void AddScore()
    {
        _FacebookManager.GetComponent<FBScript>().SetScore(_score);
    }

    public void UnlockLevel(int score)
    {
        //unlock level:
        //-------------
        if (_levelIndex == 24)
        {
            continueText.text = "Game Completed!";
            continueText2.text = "Game Completed!";
        }
        else
        {
            continueText.text = "Get " + minScore + " Points\n to unlock the next level!";
            continueText2.text = "Get " + minScore + " Points\n to unlock the next level!";
        }

        if (score > minScore && _levelIndex < 24)
        {
            continueText.enabled = false;
            continueText2.enabled = false;
            continueButton.interactable = true;
            continueButton2.interactable = true;
        }
    }
}
