using Unity.Services.Core;
using UnityEngine;
using Auth = Unity.Services.Authentication.AuthenticationService;

public class CloudSaveManager : MonoBehaviour
{
    private async void Awake()
    {
        // 유니티 서비스 초기화
        await UnityServices.InitializeAsync();

        // 로그인 콜백
        Auth.Instance.SignedIn += () =>
        {
            var _playerId = Auth.Instance.PlayerId;
            Debug.Log($"로그인 완료 : {_playerId}");
        };

        // 익명 로그인
        await Auth.Instance.SignInAnonymouslyAsync();
    }
}
