using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class AuthUserNamePassword : MonoBehaviour
{
    [SerializeField] private TMP_InputField userNameIf, passwordIf;
    [SerializeField] private Button signInButton;
    [SerializeField] private Button logInButton;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        Debug.Log("Unity Services 초기화");
    }

    // 회원가입 로직
    private async Task SignInUser(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("회원가입 성공");
        }
        catch (AuthenticationException e)
        {
            Debug.Log(e.Message);
        }
        catch (RequestFailedException e)
        {
            Debug.Log(e.Message);
        }
    }
}
