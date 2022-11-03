using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public GameObject character;
    public SpriteRenderer questionBoard;
    public GameObject ledakanPrefab;
    PlayerController characterScript;
    float questionTimer;
    bool isQuestion;

    void Start () {

        character = GameObject.FindGameObjectWithTag("Player");
        characterScript = character.GetComponent<PlayerController>();
    }

    void Update()
    {
        if(isQuestion){
            questionTimer+=Time.deltaTime;
            if(Time.timeScale==0){
                questionBoard.enabled=false;
            }else if(Time.timeScale==1){
                if(questionTimer<3){
                    questionBoard.enabled=true;
                }
                else if(questionTimer>3){                
                    questionBoard.enabled=false;                
                    isQuestion=false;
                    Destroy(this.gameObject);
                }
            }            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(this.tag == "Enemy")
        {
            Instantiate (ledakanPrefab,this.transform.position,Quaternion.identity);
            if (other.tag == "Attack")
            {
                characterScript.SkorPlus(true,1);
                Destroy(this.gameObject);
                Destroy(other.gameObject);
            }
            else
            {                
                DamageToPlayer();
                Destroy(this.gameObject);
            }
        }
        else if(this.tag == "skor")
        {
            characterScript.SkorPlus(true,1);
            InGameController.Instance.starValue+=2;
            isQuestion=true;
            questionBoard.enabled=true;
            Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            Destroy(this.transform.GetChild(0).gameObject);
            this.gameObject.GetComponent<SpriteRenderer>().enabled=false;
        }
        else if(this.tag == "dead")
        {
            InGameController.Instance.soundSystem[0].SetActive(false);
            InGameController.Instance.PlaySFX(2);            
            Destroy(character);
            InGameController.Instance.StarCondition();
            InGameController.Instance.panelLose.SetActive(true);
        }
        else if(this.tag == "Finish")
        {            
            InGameController.Instance.PlaySFX(3);
            character.GetComponent<Animator>().enabled = false; 
            character.GetComponent<SpriteRenderer>().sprite = characterScript.IdleSprite;
            characterScript.enabled = false;
            PlayerPrefs.SetInt("unlock",InGameController.Instance.stageGame);
            InGameController.Instance.StarCondition();
            InGameController.Instance.panelWin.SetActive(true);
            InGameController.Instance.isFinish=true;
        }
        else if (this.tag == "trap")
        {
            DamageToPlayer();
            Destroy(this.gameObject.GetComponent<BoxCollider2D>());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(this.tag == "obj"){
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger=false;
        }
    }

    void DamageToPlayer(){
        characterScript.GetHit();
        characterScript.lifePoint-=2;

        InGameController.Instance.lifePointImage[characterScript.lifePoint].enabled=false;
        InGameController.Instance.lifePointImage[characterScript.lifePoint+1].enabled=false;

        if(characterScript.lifePoint<1){
            InGameController.Instance.soundSystem[0].SetActive(false);
            InGameController.Instance.PlaySFX(2);
            Destroy(character);
            InGameController.Instance.StarCondition();
            InGameController.Instance.panelLose.SetActive(true);
        }
    }

    public void EnemyShoot(int indexEnemy){
        InGameController.Instance.PlaySFX(indexEnemy);
    }
}
