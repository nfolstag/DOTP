using UnityEngine;

public class OptionsHandler : MonoBehaviour
{
    private Animator animator;
    private string[] states =
    {
        Constants.TRANSITION_CONFIGURATIONMENU,
        Constants.TRANSITION_MAINMENU,
        Constants.TRANSITION_BATTLEMENU
    };
    public delegate OptionsHandler GetInstanceDelegate();
    public static GetInstanceDelegate GetInstance;

    public void Start ()
    {
        GetInstance = GetInstanceM;
        animator = GetComponent<Animator>();
    }    

    public OptionsHandler GetInstanceM()
    {
        return this;
    }

    public string SetMainMenuState()
    {
        string state = Constants.TRANSITION_MAINMENU;
        ChangeAnimatorTransitionParameter(state, true);
        return state;
    }

    public string SetBattleMenuState()
    {
        string state = Constants.TRANSITION_BATTLEMENU;
        ChangeAnimatorTransitionParameter(state, true);
        return state;
    }

    public string SetConfigurationMenuState()
    {
        string state = Constants.TRANSITION_CONFIGURATIONMENU;
        ChangeAnimatorTransitionParameter(state, true);
        return state;
    }

    private void ChangeAnimatorTransitionParameter(string parameter, bool val)
    {
        foreach (string state in states)
        {
            if (state == parameter)
                animator.SetBool(state, val);
            else
                animator.SetBool(state, false);
        }
    }
     
}
