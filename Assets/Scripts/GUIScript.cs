using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIScript : MonoBehaviour {

	public Text timeLeftText;
	public Text highScoreText;

	public float time = 120f;
	float timeStart;
	int highscore = 0;

	// Use this for initialization
	void Start () {
		timeStart = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		timeLeftText.text = ((int)GetTimeLeft()).ToString();
	}

	float GetTimeLeft(){
		return time - (Time.time - timeStart);
	}

	public void IncreaseHighScore(int score){
		highscore += score;
		highScoreText.text = highscore.ToString ();
	}
}
