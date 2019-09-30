using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int score = 0;

    #region unity_func
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region scene_transitions
    public void StartGame()
    {
        //Debug.Log("Start GAME");
        SceneManager.LoadScene("SampleScene");
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("Lose");
        //Text scoreT = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        //scoreT.text = score.ToString();
    }
    #endregion
}
