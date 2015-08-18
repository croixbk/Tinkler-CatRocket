using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    public Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowMenuPrincipal()
    {
        _animator.SetTrigger("Menu");
    }

    public void ShowCredits()
    {
        _animator.SetTrigger("Credits");
    }

    public void ShowConfigurations()
    {
        _animator.SetTrigger("Configuration");
    }

    public void PlayGame()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Menu"))
        {

        }
    }
}
