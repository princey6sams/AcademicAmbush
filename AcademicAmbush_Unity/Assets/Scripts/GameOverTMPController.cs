using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverTMPController : MonoBehaviour
{
    public TMP_Text gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameOver.text = "";
    }
}
