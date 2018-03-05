using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceModelManager : MonoBehaviour {

	[SerializeField]
	private List<GameObject> _faceModelList = new List<GameObject>();

	[SerializeField]
	private UnityARFaceAnchorManager _faceAnchorManager;

	private int _currentIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeFaceModel(int index)
	{
		if (index == _currentIndex) 
		{
			return;	
		}

		_faceModelList [_currentIndex].SetActive (false);
		_faceModelList [index].SetActive (true);
		_faceAnchorManager.SetAnchorPrefab (_faceModelList [index]);
		_currentIndex = index;
	}
}
