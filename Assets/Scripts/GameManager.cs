using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is the GameManager class. This contains all the public references to other calsses
/// </summary>
public class GameManager : MonoBehaviour
{

    public LevelController level;
    public GameController game;
    public LineController line;

    public int levelNumber;

    public static GameManager pInstance;
    private void Start()
    {
        if(pInstance == null)
        {
            pInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Home()
    {
        SceneManager.LoadScene("LevelSelector");
    }

}
