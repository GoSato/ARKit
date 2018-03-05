﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class UnityARFaceAnchorManagerTest : MonoBehaviour {

	[SerializeField]
	private GameObject anchorPrefab;

	[SerializeField]
	private float _factor = 2.0f;

	private UnityARSessionNativeInterface m_session;

	// Use this for initialization
	void Start () {
		m_session = UnityARSessionNativeInterface.GetARSessionNativeInterface();

		Application.targetFrameRate = 60;
		ARKitFaceTrackingConfiguration config = new ARKitFaceTrackingConfiguration();
		config.alignment = UnityARAlignment.UnityARAlignmentGravity;
		config.enableLightEstimation = true;

		if (config.IsSupported ) {
			
			m_session.RunWithConfig (config);

			UnityARSessionNativeInterface.ARFaceAnchorAddedEvent += FaceAdded;
			UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
			UnityARSessionNativeInterface.ARFaceAnchorRemovedEvent += FaceRemoved;

		}

	}

	void FaceAdded (ARFaceAnchor anchorData)
	{
		anchorPrefab.transform.position = UnityARMatrixOps.GetPosition (anchorData.transform);
//		anchorPrefab.transform.rotation = UnityARMatrixOps.GetRotation (anchorData.transform);
		anchorPrefab.SetActive (true);
	}

	void FaceUpdated (ARFaceAnchor anchorData)
	{
		var newPos = Vector3.zero;
		var getPos = UnityARMatrixOps.GetPosition (anchorData.transform); 
		newPos.x = getPos.x * _factor;
		newPos.y = getPos.y * _factor;
		anchorPrefab.transform.position = newPos;
//		anchorPrefab.transform.position = UnityARMatrixOps.GetPosition (anchorData.transform);
//		anchorPrefab.transform.rotation = UnityARMatrixOps.GetRotation (anchorData.transform);
		Debug.Log(anchorPrefab.transform.position);
	}

	void FaceRemoved (ARFaceAnchor anchorData)
	{
		anchorPrefab.SetActive (false);
	}



	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy()
	{
		
	}

	/// <summary>
	/// 外からanchorPrefabを入れ替える
	/// </summary>
	/// <param name="obj">Object.</param>
	public void SetAnchorPrefab(GameObject obj)
	{
		anchorPrefab = obj;
	}
}
