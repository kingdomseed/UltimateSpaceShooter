using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    private int powerUpId = 0; // 0 for TripleShot, 1 for SpeedBoost, 2 for Shields

    [SerializeField]
    private float _speed = 3.0f;

	private void Update () {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
	}

    public int getPowerUpId()
    {
        return powerUpId;
    }

}
