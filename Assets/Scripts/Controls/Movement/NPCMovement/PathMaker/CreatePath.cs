using UnityEngine;

// Used Chatgpt 
public enum PathType { NGon, Linear, Custom }

public class CreatePath : MonoBehaviour
{
    public Transform[] Construct(int numWaypoints = 5, float totalDistance = 10.0f, PathType pathType = PathType.Linear, Transform startTransform = null, Vector3[] customPathPoints = null)
    {
        Transform[] waypoints = new Transform[numWaypoints];

        // Calculate the distance between each waypoint based on the path type
        float[] distances = new float[numWaypoints - 1];
        switch (pathType)
        {
            case PathType.NGon:
                for (int i = 0; i < numWaypoints - 1; i++)
                {
                    distances[i] = totalDistance / (numWaypoints - 1);
                }
                break;
            case PathType.Linear:
                for (int i = 0; i < numWaypoints - 1; i++)
                {
                    distances[i] = Vector3.Distance(startTransform.position, startTransform.position + new Vector3(totalDistance / (numWaypoints - 1), 0.0f, 0.0f));
                }
                break;
            case PathType.Custom:
                for (int i = 0; i < numWaypoints - 1; i++)
                {
                    distances[i] = Vector3.Distance(customPathPoints[i], customPathPoints[i + 1]);
                }
                break;
        }

        // Calculate the positions of each waypoint based on the distances and starting position
        Vector3 currentPosition = startTransform.position;
        for (int i = 0; i < numWaypoints; i++)
        {
            if (i > 0)
            {
                currentPosition += (waypoints[i - 1].position - currentPosition).normalized * distances[i - 1];
            }

            // Add the custom shape to the position of the waypoint if applicable
            if (pathType == PathType.Custom)
            {
                currentPosition = customPathPoints[i];
            }

            GameObject newWaypoint = new GameObject("Waypoint " + i);
            newWaypoint.transform.position = currentPosition;
            waypoints[i] = newWaypoint.transform;
        }

        return waypoints;
    }
}

