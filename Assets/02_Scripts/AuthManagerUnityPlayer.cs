using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class AuthManagerUnityPlayer : MonoBehaviour
{
    [SerializeField] Button loginButton;
    [SerializeField] Button logoutButton;

    [SerializeField] TMP_Text messageText;

    private async void Awake()
    {
        // UGS 초기화
        await UnityServices.InitializeAsync();

        loginButton.onClick.AddListener(async () => 
        {
            // 로그인 시작 메소드 호출
            await PlayerAccountService.Instance.StartSignInAsync();
        });

        PlayerAccountService.Instance.SignedOut += () => Debug.Log("로그 아웃");
        
        logoutButton.onClick.AddListener( () => PlayerAccountService.Instance.SignOut());

        // 로그인 후 인증절차
        PlayerAccountService.Instance.SignedIn += OnSignInAsync;
    }

    private async void OnSignInAsync()
    {
        // 엑세스 토큰 
        string accessToken = PlayerAccountService.Instance.AccessToken;

        try
        {
            // 전달 받은 엑세스 토큰으로 접속 시도
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
            Debug.Log("접속 완료");
        }
        catch (PlayerAccountsException e)
        {
            Debug.Log(e.Message);
        }
        catch (AuthenticationException e)
        {
            Debug.Log(e.Message);
        }
    }
}
