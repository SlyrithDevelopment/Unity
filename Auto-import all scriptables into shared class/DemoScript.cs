// [ Original scriptable object ]
using UnityEngine;
[CreateAssetMenu(fileName = "New MyDataType", menuName = "Scriptable/My Data Type", order = 512)]
public class MyDataType : ScriptableObject {
    public float Value;
    [SerializeField] protected float Chance;
    public float Version { get; private set; }
}

// [ =-=-=-=-=-=-=-=-=-=-=-=-= NEXT FILE HERE =-=-=-=-=-=-=-=-=-=-=-=-= ]

// [ Manager for all scriptable data ]
using UnityEditor;
using UnityEngine;

public class SharedGameData : MonoBehaviour {
	[SerializeField] MyDataType[] _allOfMyDataType;
	// access this data anywhere with just using SharedGameData.MyDataType !!!
	public static MyDataType[] AllOfMyDataType => Instance._allOfMyDataType;

	void Awake() => _instance = this;
	static SharedGameData _instance; // single class instance

	// singleton pattern
	public static SharedGameData Instance {
		get {
			if (_instance == null) _instance = FindObjectOfType<SharedGameData>();
			return _instance;
		}
	}

#if UNITY_EDITOR
	void OnValidate() {
		_allOfMyDataType = GetAllInstances<MyDataType>(); // get all instances of scriptable boject that type in project
	}

	public static T[] GetAllInstances<T>(string searchPath = null) where T : UnityEngine.Object {
		string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name); // FindAssets uses tags check documentation for more info
		if (searchPath != null)
			guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { searchPath }); // if path is specified - search there
		var gLength = guids.Length;
		T[] a = new T[gLength];
		for (int i = 0; i < gLength; i++) {
			string path = AssetDatabase.GUIDToAssetPath(guids[i]);
			a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
		}
		return a;
	}
#endif
}
