using System;
using Unity.VisualScripting;
using UnityEngine;

public class JumpButton : MonoBehaviour
{
    private float _plusJumpPower = 600.0f;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionGameObject;
        Rigidbody rb;
        collisionGameObject = collision.gameObject;

        rb = collisionGameObject.GetComponent<Rigidbody>();
        rb.AddForce(Vector2.up * _plusJumpPower,ForceMode.Impulse);
    }
}
