using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameController : MonoBehaviour {

	public int stageGame;
	public GameObject panelPause, panelSettings, panelWin, panelLose, panelEnd;
	public GameObject[] soundSystem;
	public Text skorText;
	public Text skorAkhirText;
	public Image musicImage;
	public Image sfxImage;
	public Image[] starImage;
	public Image[] lifePointImage;
	public Image[] bulletPointImage;
	public AudioSource[] sfx;
	public int skorValue;
	public float starValue; //100% at 30
	public bool isFinish;

	bool isMusic;
	bool isSfx;

	public static InGameController Instance;

	void Awake()
	{
		Instance=this;

		if(PlayerPrefs.GetInt("music")==1){
			isMusic = false;
			musicImage.color = new Color(255,255,255,0);			
		}else{
			isMusic = true;
			musicImage.color = new Color(255,255,255,255);
		}

		if(PlayerPrefs.GetInt("sfx")==1){
			isSfx = false;
			sfxImage.color = new Color(255,255,255,0);
		}else{
			isSfx = true;
			sfxImage.color = new Color(255,255,255,255);
		}

		soundSystem[0].SetActive(isMusic);
		soundSystem[1].SetActive(isSfx);
	}

	public void RestartGame(bool isRestart){
		ResumeGame();

		if(isRestart){
			SceneManager.LoadScene(stageGame+1);
		}
		else
		{
			SceneManager.LoadScene(stageGame+2);
		}
	}

	public void GoToMenu(){
		ResumeGame();
		SceneManager.LoadScene(1);
	}

	public void PauseGame(){
		Time.timeScale = 0;
		panelPause.SetActive(true);
	}

	public void ResumeGame(){
		Time.timeScale = 1;
		panelPause.SetActive(false);
	}

	public void ExitGame(){
		Application.Quit ();
	}

	public void SettingsGame(){
		panelSettings.SetActive(true);
	}

	public void MusicOnOff(){
		isMusic = !isMusic;

		if(isMusic){
			musicImage.color = new Color(255,255,255,255);			
		}else{
			musicImage.color = new Color(255,255,255,0);			
		}
	}
	public void SfxOnOff(){
		isSfx = !isSfx;

		if(isSfx){
			sfxImage.color = new Color(255,255,255,255);			
		}else{
			sfxImage.color = new Color(255,255,255,0);			
		}
	}

	public void SaveSettings(){
		if(isMusic){
			PlayerPrefs.SetInt("music",0);
		}else if(!isMusic){
			PlayerPrefs.SetInt("music",1);
		}

		if(isSfx){
			PlayerPrefs.SetInt("sfx",0);
		}else if(!isSfx){
			PlayerPrefs.SetInt("sfx",1);
		}

		soundSystem[0].SetActive(isMusic);
		soundSystem[1].SetActive(isSfx);

		panelSettings.SetActive(false);
	}

	public void PlaySFX(int IndexSound){
		if(soundSystem[1].activeInHierarchy){
			sfx[IndexSound].Play();
		}
	}

	public void StarCondition(){
		panelEnd.SetActive(true);
		if(starValue>=20){
			for (int i = 0; i < starImage.Length; i++)
			{
				starImage[i].enabled=true;
			}
		}
		else if(starValue>=10)
		{
			for (int i = 0; i < starImage.Length; i++)
			{
				starImage[i].enabled=true;
			}
			starImage[2].enabled=false;
		}
		else if(starValue>0)
		{
			for (int i = 0; i < starImage.Length; i++)
			{
				starImage[i].enabled=false;
			}
			starImage[0].enabled=true;
		}
		else
		{
			for (int i = 0; i < starImage.Length; i++)
			{
				starImage[i].enabled=false;
			}
		}
	}
}
