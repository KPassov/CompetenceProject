using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIScript : MonoBehaviour {

	public Text timeLeftText;
	public Text highScoreText;

	public float time = 120f;
	float timeStart;
	int highscore = 0;
	public ScreenFader screenFader;
	public bool gameOver = false;
	private Text gameOverHighScoreText;

	// Use this for initialization
	void Start () {
		timeStart = Time.time;
		screenFader = GameObject.FindGameObjectWithTag ("FadeImg").GetComponent<ScreenFader>();
		NotificationCenter.DefaultCenter.AddObserver(this,"Explode");
	}
	
	// Update is called once per frame
	void Update () {
		if(GetTimeLeft() <= 0){
			screenFader.EndScene();
			PlayerPrefs.SetInt("highScore", highscore);
		}else{
			timeLeftText.text = ((int)GetTimeLeft()).ToString();
		}

	}

	void Explode (NotificationCenter.Notification notif ){
		Debug.Log("GUI Explode");
	}

	float GetTimeLeft(){
		return time - (Time.time - timeStart);
	}

	public void IncreaseHighScore(int score){
		highscore += score;
		if (highscore< 0){
			highscore = 0;
		}
		highScoreText.text = highscore.ToString ();
	}

	void OnDestroy(){
		NotificationCenter.DefaultCenter.RemoveObserver(this,"Explode");
	}
}

