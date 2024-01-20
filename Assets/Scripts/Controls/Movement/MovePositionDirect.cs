using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionDirect : MonoBehaviour
{
    private Vector3 movePostion;

    public void SetMovePostion(Vector3 movePostion)
    {
        this.movePostion = movePostion;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = movePostion - transform.position;
        GetComponent<IMoveVelocity>().SetVelocity(moveDir);

    }
}
