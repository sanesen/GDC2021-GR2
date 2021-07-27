using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mysteryBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider tank)
    {

        if (tank.gameObject.tag=="Player1"||tank.gameObject.tag=="Player2")
        {
            tank.gameObject.GetComponent<TankMovement>().mysterybox_collision();
            Destroy(this.gameObject);
        }
    }
}
