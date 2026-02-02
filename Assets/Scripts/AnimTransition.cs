using UnityEngine;

public class AnimTransition : MonoBehaviour
{

    [SerializeField] private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.jumpPlayer.AddListener(jumpAnim);
        GameManager.Instance.landingPlayer.AddListener(runAnim);
        GameManager.Instance.crouchPlayer.AddListener(slideAnim);
        GameManager.Instance.standUpPlayer.AddListener(runAnim);
        GameManager.Instance.gameOver.AddListener(deadAnim);
    }
    private void OnDestroy()
    {
        GameManager.Instance.jumpPlayer.RemoveListener(jumpAnim);
        GameManager.Instance.landingPlayer.RemoveListener(runAnim);
        GameManager.Instance.crouchPlayer.RemoveListener(slideAnim);
        GameManager.Instance.standUpPlayer.RemoveListener(runAnim);
        GameManager.Instance.gameOver.RemoveListener(deadAnim);
    }


    private void jumpAnim()
    {
        anim.Play("Running Jumping");
    }

    private void runAnim()
    {
        anim.Play("Running");
    }

    private void slideAnim()
    {
        anim.Play("Running slide");
    }

    private void deadAnim(bool b)
    {
        anim.Play("Dead");
    }
    
}
