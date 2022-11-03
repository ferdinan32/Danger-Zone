using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

	public Transform player;
	public bool isCamera;

	void Update () {
		if (player != null) {
			if(isCamera)
				transform.position = new Vector3 (player.position.x + 3, 0, -10);
			else{
				if(this.gameObject.GetComponent<SpriteRenderer>().enabled){
					transform.position = new Vector3 (player.position.x+3, 3, 0);
				}
			}
		}

		else if (player == null) {
			transform.position = new Vector3 (this.transform.position.x, 0, -10);
		}
	}
}
