using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A standard block
/// </summary>
public class StandardBlock : Block
{
	/// <summary>
	/// Use this for initialization
	/// </summary>
	override protected void Start()
	{
        // set points and random sprite
        points = ConfigurationUtils.StandardBlockPoints;

		base.Start();
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		
	}
}
