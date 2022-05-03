using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth : MonoBehaviour
{
	[SerializeField, Range(0, 1)] float m_Attenuation = 0.9f;

	Vector3 m_Campos;

	Quaternion m_Camrot;

	bool m_Focus = false;

	void LateUpdate()
	{
		if(m_Focus)
		{
			m_Campos = Camera.main.transform.localPosition;
			m_Camrot = Camera.main.transform.localRotation;
			m_Focus = false;
			return;
		}

		m_Campos = Vector3.Lerp(Camera.main.transform.localPosition, m_Campos, m_Attenuation);
		m_Camrot = Quaternion.Slerp(Camera.main.transform.localRotation, m_Camrot, m_Attenuation);

		Camera.main.transform.localPosition = m_Campos;
		Camera.main.transform.localRotation = m_Camrot;
	}

	void OnEnable() => m_Focus = true;
}
