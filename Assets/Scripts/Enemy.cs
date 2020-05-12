using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{

	// Start is called before the first frame update
	protected override void Start()
    {
		base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public override void Attack()
	{
		throw new System.NotImplementedException();
	}

	public override void Die()
	{
		throw new System.NotImplementedException();
	}

	public override void Move()
	{
		throw new System.NotImplementedException();
	}

}
