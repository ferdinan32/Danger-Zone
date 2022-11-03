using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour {

	public Image splashImage;
	public Sprite[] splashSprite;
	float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer +=Time.deltaTime;
		if(timer>3){
			splashImage.sprite = splashSprite[1];
		}if(timer>6){
			splashImage.sprite = splashSprite[2];
		}if(timer>9){
			SceneManager.LoadScene(1);
		}
		
	}
}
