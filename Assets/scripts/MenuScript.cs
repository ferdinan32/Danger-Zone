using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	[Header("Main Menu")]
	public Animator titleAnimation;
	public Button playButton;
	public GameObject[] stageUnlock;
	public Transform[] panelMenu;

	[Header("")]
	[Header("Settings")]
	public Image musicImage;
	public Image sfxImage;
	public GameObject[] soundSystem;

	[Header("")]
	[Header("	Class Room")]	
	
	[Header("Class Room Menu")]
	public GameObject pageMenu;
	public GameObject pageClass;	
	public GameObject[] panelClassRoom;
	
	[Header("Class Room Dialog")]
	public GameObject dialogClassroom;
	public Image diaologImage;		
	public Sprite[] dialogSprite;

	[Header("")]
	[Header("Tutorial")]
	public GameObject tutorialDialog;
	public Image tutorialImage;
	public Sprite[] tutorialSprite;

	int indexClass;
	int countClass;
	int goToScene;
	int tutorialCount;
	bool isMusic;
	bool isSfx;
	bool isClassRoom;

	void Awake()
	{
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
		InvokingAnimation();
	}

	void Start()
	{
		ShowMenu(0);
		if(PlayerPrefs.GetInt("unlock")==1){
			stageUnlock[0].SetActive(false);
		}
		else if(PlayerPrefs.GetInt("unlock")>=2){
			stageUnlock[0].SetActive(false);
			stageUnlock[1].SetActive(false);
		}
		tutorialDialog.SetActive(false);

	}

	void InvokingAnimation(){
		Invoke("InvokingAnimation",10);
		titleAnimation.SetTrigger("play");
	}

	public void ShowMenu(int index){
		if(index==9){
			Application.OpenURL("www.google.com");
		}		
		else{
			for (int i = 0; i < panelMenu.Length; i++)
			{
				panelMenu[i].position = new Vector2(-20,0);
			}

			panelMenu[index].position = new Vector2(0,0);
			
			if(index==2){				
				dialogClassroom.SetActive(true);
				diaologImage.sprite = dialogSprite[0];
			}
		}
	}

	public void Quit()
	{
		Application.Quit ();
	}

	public void PlayGame(int indexScene)
	{
		goToScene=indexScene;
		playButton.interactable=false;
		Invoke("GoToGame",.3f);		
    }

	void GoToGame()
	{						
		SceneManager.LoadScene(goToScene);
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

		ShowMenu(0);
	}

	public void OpenClass(int index){
		countClass=0;
		indexClass=index;

		pageMenu.SetActive(false);
		pageClass.SetActive(true);

		for (int i = 0; i < (panelClassRoom.Length-1); i++)
		{
			panelClassRoom[i].SetActive(false);
		}

		panelClassRoom[index].SetActive(true);
		
		isClassRoom=true;

		ListAllClass(panelClassRoom[index]);		
	}	

	public void CloseClass(){		

		pageMenu.SetActive(true);
		pageClass.SetActive(false);

		for (int i = 0; i < panelClassRoom.Length; i++)
		{
			panelClassRoom[i].SetActive(false);
		}

		if(isClassRoom){
			isClassRoom=false;			
		}else{
			ShowMenu(0);
		}
	}

	public void NextClass(){
		if(countClass < panelClassRoom[indexClass].transform.childCount-1)
			countClass++;

		ListAllClass(panelClassRoom[indexClass]);
	}

	public void PrevClass(){
		if(countClass > 0)
			countClass--;

		ListAllClass(panelClassRoom[indexClass]);
	}

	void ListAllClass(GameObject chosenClass){
		for (int i = 0; i < chosenClass.transform.childCount; i++)
		{
			chosenClass.transform.GetChild(i).gameObject.SetActive(false);
		}
		chosenClass.transform.GetChild(countClass).gameObject.SetActive(true);
	}

	public void NextDialog(){		
		if(diaologImage.sprite!=dialogSprite[1]){
			diaologImage.sprite = dialogSprite[1];
		}else
		{
			dialogClassroom.SetActive(false);
		}
	}

	public void ResetSaveData(){
		PlayerPrefs.DeleteAll();
	}

	public void OpenTutorial(){
		tutorialCount=0;
		tutorialImage.sprite = tutorialSprite[tutorialCount];
		tutorialDialog.SetActive(true);
	}

	public void NextTutorial(){
		if(tutorialCount<tutorialSprite.Length-1){
			tutorialCount++;			
		}
		else{
			tutorialDialog.SetActive(false);
			tutorialCount=0;
		}
		tutorialImage.sprite = tutorialSprite[tutorialCount];
	}

}
