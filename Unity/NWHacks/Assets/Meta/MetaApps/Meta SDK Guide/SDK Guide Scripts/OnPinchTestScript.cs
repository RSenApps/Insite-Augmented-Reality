﻿/************************************************************************************
 *  Copyright © 2014 Meta Company. All Rights Reserved. Use of this software source *
 *  code and binaries requires agreement and compliance with the META Licensed 		*
 *  Application End User License Agreement in the accompanying META EULA.txt file, 	*
 *  which must be included as part of this software for any use. 					*
 ************************************************************************************/

using UnityEngine;
using System.Collections;

namespace Meta.Apps.SDKGuide
{
    public class OnPinchTestScript : MonoBehaviour
	{
		public void OnPinch()
		{
			GetComponent<Renderer>().material.color = Color.red;
		}
		public void OnPinchHold()
		{
			GetComponent<Renderer>().material.color = Color.green;
		}
		public void OnPinchRelease()
		{
			GetComponent<Renderer>().material.color = Color.blue;
		}
	}
}
