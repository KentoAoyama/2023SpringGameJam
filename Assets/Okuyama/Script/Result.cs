using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField] Text _score = null;
    [SerializeField] string _nextScene;

    private void Start()
    {
        _score.text = $"{GameManager.Score}";
    }

    public void NextScene()
    {
        SceneManager.CreateScene(_nextScene);
    }
}
