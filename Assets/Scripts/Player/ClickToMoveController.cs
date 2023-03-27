using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMoveController : MonoBehaviour
{
    [field: SerializeField]
    [field: Tooltip("Player agent")] 
    public NavMeshAgent Agent { get; private set; }

    private Vector3 _destination = Vector3.one;
    private KeyCode _movementKey = KeyCode.Mouse0;
    private void Update()
    {
        if (GameManager.Instance.gameIsOver) return;

        Movement();
    }
    /// <summary>
    /// Player movement
    /// </summary>
    private void Movement()
    {
        if (!IsMovement()) return;

#if UNITY_EDITOR
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, LayerMask.GetMask("Ground"))) return;

        _destination = hit.point;
#else
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out RaycastHit hit, LayerMask.GetMask("Ground"))) return;

        _destination = hit.point;
#endif

        Agent.SetDestination(_destination);
    }

    /// <summary>
    /// Return true if player clicked to the ground
    /// </summary>
    /// <returns></returns>
    private bool IsMovement()
    {
#if UNITY_EDITOR
        return Input.GetKeyDown(_movementKey);
#else
        return Input.touchCount > 0;
#endif
    }
}
