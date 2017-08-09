using UnityEngine;

public class Bead : MonoBehaviour {

	private Manager manager;

	// 初期化
	void Start () {
		manager = FindObjectOfType<Manager>();
	}

	// 物理演算用更新処理
	void FixedUpdate () {
		// 向きを固定化
		gameObject.transform.localRotation = Quaternion.identity;
	}

	// 衝突処理
	void OnCollisionEnter2D(Collision2D collision){
		
		if (manager.Status == "") return;
			
		// バーに衝突
		if (collision.gameObject.tag == "Bar") {
			if (gameObject.tag != "Bar") {

				// 1列珠表示
				gameObject.tag = manager.OnOff;
				FindObjectOfType<AbacusBeads>().ViewAbacus(this.transform.parent.gameObject);
			}		
		} else {
			// 珠に衝突
			gameObject.tag = manager.OnOff;
		}
	}
}
