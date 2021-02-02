using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BoyAnimatonStateController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = this.gameObject.GetComponent<Animator>();
    }

    public void SetWalkingAnim(bool isWalking)
    {
        _animator.SetBool("isWalking", isWalking);
    }
}
