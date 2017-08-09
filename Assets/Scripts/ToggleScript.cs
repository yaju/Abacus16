using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour {

	private Toggle toggle = null;
	private Manager manager;

	// 初期化
	void Start () {
		toggle = GetComponent<Toggle>();
		manager = FindObjectOfType<Manager>();
	}

	// トグル変更
	public void ChangeToggle ()	{
		manager.isHex = (bool)toggle.isOn;
		// 全クリア
		manager.ClearAll();
	}
}
