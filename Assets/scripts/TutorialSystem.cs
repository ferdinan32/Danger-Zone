using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour {

	public GameObject PlayerCharacter;
	public GameObject tutorialDialog;
	public Image dialogImage;
	public Image tutorialImage;
	public Sprite[] dialogSprite;
	public Sprite[] tutorialSprite;
	int indexTutorial;

	public void NextTutorial(){
		indexTutorial++;

		if(indexTutorial>dialogSprite.Length-1){
			tutorialDialog.SetActive(false);
			PlayerCharacter.GetComponent<Animator>().enabled=true;
			PlayerCharacter.GetComponent<PlayerController>().enabled=true;			
		}else{
			dialogImage.sprite = dialogSprite[indexTutorial];
			tutorialImage.sprite = tutorialSprite[indexTutorial];
		}				
	}
}
