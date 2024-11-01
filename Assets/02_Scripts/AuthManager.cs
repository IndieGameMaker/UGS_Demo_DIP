using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using System.Threading.Tasks;
using Unity.Services.Authentication;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Button logoutButton;
    [SerializeField] private Button playerNameSaveButton;

    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_InputField playerNameIf;
    

    private async void Awake()
    {
        UnityServices.Initialized += () =>
        {
            messageText.text = "UnityService Initalized !!!";
            EventBinding();
        };

        // USG 서비스 초기화
        await UnityServices.InitializeAsync();

        // Login Button 연결
        loginButton.onClick.AddListener(async () =>
        {
            // 익명 로그인 요청
            await LoginAsync();
        });

        // Logout Button 연결
        logoutButton.onClick.AddListener(() =>
        {
            AuthenticationService.Instance.SignOut();
        });

        // PlayerName Save Button 연결
        playerNameSaveButton.onClick.AddListener(async () =>
        {
            await SetPlayerName(playerNameIf.text);
        });
    }

    // 익명 로그인 처리
    private async Task LoginAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("익명 로그인 성공");
        }
        catch(AuthenticationException e)
        {
            Debug.Log(e.Message);
        }
    }


    // 인증 이벤트 연결
    private void EventBinding()
    {
        // 로그인 이벤트
        AuthenticationService.Instance.SignedIn += () =>
        {
            messageText.text += $"\nPlayer Id : {AuthenticationService.Instance.PlayerId}";
        };
        // 로그아웃 이벤트
        AuthenticationService.Instance.SignedOut += () =>
        {
            messageText.text += "\nLogout !!";
        };
        // 로그인 실패
        AuthenticationService.Instance.SignInFailed += (ex) =>
        {
            Debug.Log($"SignIn Failed {ex.Message}");
        };
    }

    // 플레이어 이름 저장
    private async Task SetPlayerName(string playerName)
    {
        try
        {
            await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);

            var _playerName = AuthenticationService.Instance.PlayerName;
            messageText.text = $"Player Name {_playerName.Split('#')[0]}";
        }
        catch (AuthenticationException e)
        {
            Debug.Log(e.Message);
        }
    }
}

/*
 50자 허용
 공백 허용안됨
 Zack#1234
*/