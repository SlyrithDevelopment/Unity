using System.Collections.Generic;
using UnityEngine;

namespace Ultar.SimplePooling {

    public class ObjectPool : MonoBehaviour {
		#region [ Structs ]
		[System.Serializable]
		public struct PoolVariant {
			public string PoolName;
			public Transform Prefab;
			public int PoolSize;
		}
		#endregion

		public static ObjectPool Instance;
		private Dictionary<string, Queue<GameObject>> _pools;
		[SerializeField] private List<PoolVariant> _poolVariants;

		private void Awake() {
			if (ObjectPool.Instance == null) Instance = this;
			else if(ObjectPool.Instance != this) Destroy(gameObject); // remove as duplicate...
			_pools = new Dictionary<string, Queue<GameObject>>(); // create new dictionary
		}

		public void Start() {
			InitPools();
		}

		private void InitPools() {
			var cntr = _poolVariants.Count;
			if (cntr < 1) return;
			PoolVariant v;
			for (int i = 0; i < cntr; i++) {
				v = _poolVariants[i];
				if (v.PoolName.Equals("") || v.Prefab == null) continue;
				var ttl = Mathf.Max(1, v.PoolSize);
				if (!_pools.ContainsKey(v.PoolName)) {
					var queue = new Queue<GameObject>();
					GameObject go = new GameObject(v.PoolName);
					go.transform.parent = transform;
					for (int a = 0; a < v.PoolSize; a++) {
						Transform obj = Instantiate(v.Prefab, Vector3.zero, Quaternion.identity, go.transform);
						obj.gameObject.SetActive(false);
						queue.Enqueue(obj.gameObject);
					}
					_pools.Add(v.PoolName, queue);
				}
			}
		}

		public GameObject Spawn(string poolName, Transform transform, Quaternion rotation) {
			if (!_pools.ContainsKey(poolName)) return null;
			var que = _pools[poolName];
			var obj = que.Dequeue();
			obj.SetActive(false);
			obj.transform.SetPositionAndRotation(transform.position, rotation);
			obj.SetActive(true);
			que.Enqueue(obj);
			return obj;
		}
	}
}
