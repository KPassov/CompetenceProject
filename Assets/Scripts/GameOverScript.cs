using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public Text highScoreText;

	// Use this for initialization
	void Start () {
		highScoreText.text = PlayerPrefs.GetInt("highScore", 0).ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
