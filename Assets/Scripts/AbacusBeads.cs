using UnityEngine;

public class AbacusBeads : MonoBehaviour {

	private GameObject obj;
	private Manager manager;

	// 初期化
	void Start () {
		manager = FindObjectOfType<Manager>();
	}
	
	// 更新処理
	void Update () {
        obj = getClickObject();
		if (obj != null)
        {
			// クリック側以外は対象外
			if (manager.ClickAbacus != gameObject.name)	return;

			// バーをクリックなら1列クリア
			if (obj.name.Contains ("bar")) {
				Clear (gameObject);
			}

			if (!obj.name.Contains("Bead")) return;
			
            // 珠クリック処理
			Rigidbody2D rd = obj.GetComponent<Rigidbody2D>();
			rd.simulated = true;
			int value = 10;
			if (obj.name == "Bead6" || obj.name == "Bead7") {
				if (obj.tag == "Off") value *= -1;
			} else {
				if (obj.tag == "On") value *= -1;
			}
			rd.velocity = transform.up * value;
			manager.OnOff = "On";
			if(obj.tag == "On")	manager.OnOff = "Off";
			manager.Status = "Start";
        }
    }

    // 左クリックしたオブジェクトを取得する関数(2D)
    private GameObject getClickObject()
    {
        GameObject result = null;

        // 左クリックされた場所のオブジェクトを取得
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
            if (collition2d)
            {
            	result = collition2d.transform.gameObject;
				manager.ClickAbacus = result.transform.parent.name;
            }
        }
        return result;
    }

	// 1列クリア
	public void Clear(GameObject abacusBeans)
	{
		int count = abacusBeans.transform.childCount;
		for (int i=0; i<count; i++) {
			GameObject child = abacusBeans.transform.GetChild(i).gameObject;
			if (!child.name.Contains("Bead")) continue;
			child.tag = "Off";
		}
		ViewAbacus(abacusBeans);
		manager.Status = "";
		manager.OnOff = "Off";
	}

	// 1列珠表示
	public void ViewAbacus(GameObject abacusBeans)
	{
		int count = abacusBeans.transform.childCount;
		for (int i=0; i<count; i++) {
			GameObject child = abacusBeans.transform.GetChild(i).gameObject;
			if (!child.name.Contains("Bead")) continue;

			float x = 0f;
			float add = 0f;
			Vector3 pos = Vector3.zero;

			if (child.tag == "On") add = 0.6f;
			if (i < 5) {
				pos = new Vector3 (x, -2.0f + add - 0.8f * i, 0);
			}
			if (i == 5) {
				pos = new Vector3(x, 0.4f - add, 0);
			}
			if (i == 6) {
				pos = new Vector3(x, 1.2f - add, 0);
			}
			child.transform.localPosition = pos;
			child.transform.localRotation = Quaternion.identity;

			Rigidbody2D rd = child.GetComponent<Rigidbody2D>();
			rd.velocity = Vector2.zero;
			rd.simulated = true;
		}

		manager.Calc();
	}
}
