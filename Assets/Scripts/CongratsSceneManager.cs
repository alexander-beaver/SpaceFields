using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CongratsSceneManager : MonoBehaviour
{
    public Button homeBtn;
    // Start is called before the first frame update
    void Start()
    {
        homeBtn.onClick.AddListener(GoHome);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GoHome()
    {
        SceneManager.LoadScene("Launch");
    }
}
