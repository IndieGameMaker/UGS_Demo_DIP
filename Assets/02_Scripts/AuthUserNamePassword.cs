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

    private void OnEnable()
    {
        // 이벤트 바인딩
        signInButton.onClick.AddListener(async () =>
        {
            await SignUpUser(userNameIf.text, passwordIf.text);
        });
    }

    // 회원가입 로직
    /*
        회원이름 : 대소문자 구별없음, 3 ~ 20자, [. - @]
        비밀번호 : 대소문자 구별, 대문자 1, 소문자 1, 숫자 1, 특수문자 1 포함
    */
    private async Task SignUpUser(string username, string password)
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
