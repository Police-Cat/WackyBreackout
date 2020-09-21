using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeColliders : MonoBehaviour
{
    [SerializeField] EdgeCollider2D topEdgeCollider2D;
    [SerializeField] EdgeCollider2D leftEdgeCollider2D;
    [SerializeField] EdgeCollider2D rightEdgeCollider2D;

    Vector2 leftTopCorner;
    Vector2 leftBottomCorner;
    Vector2 rightTopCorner;
    Vector2 rightBottomCorner;

    List<Vector2> leftEdgeColliderPoints;
    List<Vector2> topEdgeColliderPoints;
    List<Vector2> rightEdgeColliderPoints;


    // Create 3 Edge colliders depending on the screen size
    void Start()
    {
        leftTopCorner = new Vector2(ScreenUtils.ScreenLeft, ScreenUtils.ScreenTop);
        leftBottomCorner = new Vector2(ScreenUtils.ScreenLeft, ScreenUtils.ScreenBottom);
        rightTopCorner = new Vector2(ScreenUtils.ScreenRight, ScreenUtils.ScreenTop);
        rightBottomCorner = new Vector2(ScreenUtils.ScreenRight, ScreenUtils.ScreenBottom);

        leftEdgeColliderPoints = new List<Vector2>() { leftTopCorner, leftBottomCorner };
        topEdgeColliderPoints = new List<Vector2>() { leftTopCorner, rightTopCorner };
        rightEdgeColliderPoints = new List<Vector2>() { rightTopCorner, rightBottomCorner };

        leftEdgeCollider2D.points = leftEdgeColliderPoints.ToArray();
        topEdgeCollider2D.points = topEdgeColliderPoints.ToArray();
        rightEdgeCollider2D.points = rightEdgeColliderPoints.ToArray();
        
    }
}
