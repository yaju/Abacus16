using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
	public string Status = "";
	public string OnOff = "Off";
	public string ClickAbacus = "";
	public bool isHex = true;
	public Text GUIText;

	// 全クリア
	public void ClearAll() {
		GameObject[] abacus = GameObject.FindGameObjectsWithTag("Abacus");
		foreach(GameObject child in abacus) { 
			FindObjectOfType<AbacusBeads>().Clear(child);
		} 
	}

	// 計算処理
	public void Calc() {
		int result = 0;
		int count = 0;
		int carry = 10;

		if (isHex) carry = 16;
		GameObject[] abacus = GameObject.FindGameObjectsWithTag("Abacus");
		foreach(GameObject child in abacus) {
			if (child.name == "AbacusBeads") count = 0;
			if (child.name == "AbacusBeads2") count = 1;
			if (child.name == "AbacusBeads3") count = 2;

			int sum = 0;
			int add = 1;
			for (int i=0; i<child.transform.childCount; i++) {
				if (!child.name.Contains("Bead")) continue;
				if(i>=5) add = 5;
				if (child.transform.GetChild(i).gameObject.tag == "On") {
					sum += add;
				}
			}

			result += sum * (int)System.Math.Pow((double)carry, (double)count);
			count++;
		}

		if (isHex) {
			GUIText.text = result.ToString("X");
		} else {
			GUIText.text = result.ToString();
		}
	}
}
