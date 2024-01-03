using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private float FollowSpeed = 2f;
    [SerializeField] private float yOffset = 1f;
    [SerializeField] private Transform target;

    // Update is called once per frame
    private void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
