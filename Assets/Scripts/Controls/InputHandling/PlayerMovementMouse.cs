using UnityEngine;

public class PlayerMovementMouse : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {

        Vector3 mousePos = GetMouseWorldPostion();
        Debug.Log(mousePos);
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<MovePositionDirect>().SetMovePostion(mousePos);
        }

    }

    private static Vector3 GetMouseWorldPostion()
    {
        Vector3 screenPostion = Input.mousePosition;
        Vector3 worldPostion = Camera.main.ScreenToWorldPoint(screenPostion);
        worldPostion.z = 0;

        return worldPostion;
    }
}
