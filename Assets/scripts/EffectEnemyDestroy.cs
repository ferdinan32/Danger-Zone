using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEnemyDestroy : MonoBehaviour {

	public void DestroyEffect(){
		if(PlayerPrefs.GetInt("sfx")==1){
			this.GetComponent<AudioSource>().Play();
		}
		Destroy(this.gameObject);
	}
}
