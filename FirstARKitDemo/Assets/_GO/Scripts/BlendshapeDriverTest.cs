using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class BlendshapeDriverTest : MonoBehaviour {

	[SerializeField]
	private string _blendShapeName = "blendShape2";

	SkinnedMeshRenderer skinnedMeshRenderer;
	Dictionary<string, float> currentBlendShapes;

	// Use this for initialization
	void Start () {
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();

		if (skinnedMeshRenderer) {
			UnityARSessionNativeInterface.ARFaceAnchorAddedEvent += FaceAdded;
			UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
		}
	}

	void FaceAdded (ARFaceAnchor anchorData)
	{
		currentBlendShapes = anchorData.blendShapes;
	}

	void FaceUpdated (ARFaceAnchor anchorData)
	{
		currentBlendShapes = anchorData.blendShapes;
	}


	// Update is called once per frame
	void Update () {

		if (currentBlendShapes != null) {
//			foreach(KeyValuePair<string, float> kvp in currentBlendShapes)
//			{
//				int blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex (_blendShapeName +  "." + kvp.Key);
//				if (blendShapeIndex >= 0 ) {
//					skinnedMeshRenderer.SetBlendShapeWeight (blendShapeIndex, kvp.Value * 100.0f);
//				}
//			}

			int blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex (_blendShapeName + "." + "Face5");
			if (blendShapeIndex >= 0) 
			{
				skinnedMeshRenderer.SetBlendShapeWeight (blendShapeIndex, (1.0f - currentBlendShapes ["jawOpen"]) * 100.0f);
			}
		}
	}
}
