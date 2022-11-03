
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 10f;
	public float moveSpeed = 1f;	
	Animator anim;
	public float jumpForce=700;

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public int bulletSpeed;
	public float bulletLifeTime;
	public int lifePoint;
	public int bulletPoint;
	float bulletRechargeTimer;
	public Button jumpButton;
	public Sprite IdleSprite;

	void Start () {
		anim = GetComponent<Animator>();
		Invoke("SkorNambah",1);
	}
	
	void FixedUpdate () {		
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
	}

	void Update()
	{	
		GetComponent<Rigidbody2D>().velocity = new Vector2 (moveSpeed * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		
		if(bulletPoint<10){
			bulletRechargeTimer+=Time.deltaTime;
			if(bulletRechargeTimer>5){
				bulletPoint+=1;
				bulletRechargeTimer=0;
				InGameController.Instance.bulletPointImage[bulletPoint-1].enabled=true;
			}
		}		
    }

	public void JumpAction(){
		InGameController.Instance.PlaySFX(1);
		anim.SetTrigger("jump");
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		StartCoroutine(CanJump());
	}
	public void AttackAction(){		
		if(bulletPoint>0){
			anim.SetTrigger("attack");
			Fire();
		}		
	}

	public void Fire()
	{
		InGameController.Instance.PlaySFX(0);

		var bullet = (GameObject)Instantiate (
			bulletPrefab,
			bulletSpawn.position,
			Quaternion.identity);

		bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed,0);		
		bulletPoint--;
		InGameController.Instance.bulletPointImage[bulletPoint].enabled=false;
		Destroy(bullet, bulletLifeTime);
	}	

	IEnumerator CanJump(){
		jumpButton.interactable=false;
		yield return new WaitForSeconds(1);
		jumpButton.interactable=true;
	}

	public void GetHit(){
		SkorPlus(false,1);
		if(InGameController.Instance.starValue>0){
			InGameController.Instance.starValue-=1;
		}
		anim.SetTrigger("Hit");
		//GetComponent<SpriteRenderer>().material.color = new Color(255,255,255,0);
		//StartCoroutine(GetHitAnimation());
	}

	IEnumerator GetHitAnimation(){
		GetComponent<SpriteRenderer>().color = new Color(255,255,255,0);
		yield return new WaitForSeconds(1);
		GetComponent<SpriteRenderer>().color = new Color(255,255,255,255);
	}

	void SkorNambah(){
		if(!InGameController.Instance.isFinish){
			Invoke("SkorNambah",1);
			SkorPlus(true,1);
		}
		else
		{
			CancelInvoke("SkorNambah");
		}	
	}

	public void SkorPlus(bool isNambah,int baseValue){
		if(isNambah){
			InGameController.Instance.skorValue+=baseValue;
		}
		else{
			InGameController.Instance.skorValue-=baseValue;
		}
		
		InGameController.Instance.skorAkhirText.text = InGameController.Instance.skorText.text = InGameController.Instance.skorValue.ToString();
	}
}
