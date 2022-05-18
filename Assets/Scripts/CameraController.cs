using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Tilemap map;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private float halfHeight;
    private float halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        // target = PlayerController.instance.transform;
        target = FindObjectOfType<PlayerController>().transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = map.localBounds.min + new Vector3(halfWidth, halfHeight, 0);
        topRightLimit = map.localBounds.max - new Vector3(halfWidth, halfHeight, 0);

        PlayerController.instance.SetBounds(map.localBounds.min, map.localBounds.max);
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // keep the camera within bound
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(target.position.y, bottomLeftLimit.y, topRightLimit.y), 
            transform.position.z
        );
    }
}
