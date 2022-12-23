using UnityEditor; // necessary for menu feature!
using UnityEngine;

#if (UNITY_EDITOR)
public class DevMode {
// 'if' used to remove code from release project

	// creating menu item
	[MenuItem("Dev mode/New game")]
	public static void ResetField() { // this function MUST be static!!!
		GameManager.Instance.SetState(GameState.NewGame);
	}

	[MenuItem("Dev mode/Reset player prefs")]
	protected static void ResetPlayerPrefs() {
		PlayerPrefs.DeleteAll();
	}

	[MenuItem("Dev mode/Loose")]
	private static void Loose() {
		GameManager.Instance.SetState(GameState.Loose);
	}
}
#endif
