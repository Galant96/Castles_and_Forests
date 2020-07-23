using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	[SerializeField]
	private Collider2D platformCollider = null;

    // Update is called once per frame
    void Update()
    {
		CheckPlayerVerticalPosition();
	}

	private void CheckPlayerVerticalPosition()
	{
		if (PlayerCharacter.Instance.transform.position.y < transform.position.y)
		{
			platformCollider.isTrigger = true;
		}
		else if (PlayerCharacter.Instance.transform.position.y > transform.position.y)
		{
			platformCollider.isTrigger = false;
		}
	}
}
