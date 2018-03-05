﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ExampleClass : MonoBehaviour 
{
	private float left;
	private float right;
	private float top;
	private float bottom;

	[SerializeField]
	private float _factor = 0.1f;

	[SerializeField]
	private float _initialLeft = -0.1f;
	[SerializeField]
	private float _initialRight = 0.1f;
	[SerializeField]
	private float _initialTop = 0.05f;
	[SerializeField]
	private float _initialBottom = -0.05f;

//	private void Start()
//	{
//		_initialLeft = left;
//		_initialRight = right;
//		_initialTop = top;
//		_initialBottom = bottom;
//	}

	void LateUpdate() {
		Camera cam = Camera.main;

		var camPos = cam.transform.position;
		left = _initialLeft -camPos.x * _factor;
		right = _initialRight -camPos.x * _factor;
		top = _initialTop -camPos.y * _factor;
		bottom = _initialBottom -camPos.y * _factor;

		Debug.LogFormat("{0},{1},{2},{3}", left,right,top,bottom);

		Matrix4x4 m = PerspectiveOffCenter(left, right, bottom, top, cam.nearClipPlane, cam.farClipPlane);
		cam.projectionMatrix = m;
	}
	static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far) {
		float x = 2.0F * near / (right - left);
		float y = 2.0F * near / (top - bottom);
		float a = (right + left) / (right - left);
		float b = (top + bottom) / (top - bottom);
		float c = -(far + near) / (far - near);
		float d = -(2.0F * far * near) / (far - near);
		float e = -1.0F;
		Matrix4x4 m = new Matrix4x4();
		m[0, 0] = x;
		m[0, 1] = 0;
		m[0, 2] = a;
		m[0, 3] = 0;
		m[1, 0] = 0;
		m[1, 1] = y;
		m[1, 2] = b;
		m[1, 3] = 0;
		m[2, 0] = 0;
		m[2, 1] = 0;
		m[2, 2] = c;
		m[2, 3] = d;
		m[3, 0] = 0;
		m[3, 1] = 0;
		m[3, 2] = e;
		m[3, 3] = 0;
		return m;
	}
}