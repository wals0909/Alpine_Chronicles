using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text ScoreText;
    
    int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //get score 
        //ScoreText.text = Score.ToString();
    }
}
