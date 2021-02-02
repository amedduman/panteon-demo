using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GirlAnimationStateController : MonoBehaviour
{
    [SerializeField] OpponentCharacterMovement _opponentChar;
    private Animator _animator;


    private void Awake()
    {
        _animator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetBool("isStopped", _opponentChar.IsStopped);
    }
}
