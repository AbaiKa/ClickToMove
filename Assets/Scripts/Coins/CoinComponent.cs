using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class CoinComponent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Explosion effect (ParticeSystem)")]
    private GameObject _explosionEffect;

    private Collider _collider;
    private Rigidbody _rigidbody;

    /// <summary>
    /// Fires as soon as the player touches this object
    /// </summary>
    public UnityEvent onCollisionWithPlayer = new UnityEvent();

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();

        // INIT
        _collider.isTrigger = true;
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ��� ��� � ������ ����� ClickToMoveController, ���� ��� ��� �� ������
        // � ������� ����� �������� �� Player.cs ��� �� ��� �� ��������
        if(other.TryGetComponent(out ClickToMoveController player))
        {
            onCollisionWithPlayer?.Invoke();
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
