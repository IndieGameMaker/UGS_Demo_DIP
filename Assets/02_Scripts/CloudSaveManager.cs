using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;
using Auth = Unity.Services.Authentication.AuthenticationService;

public class CloudSaveManager : MonoBehaviour
{
    [SerializeField] private Button singleDataSaveButton;

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

    private void OnEnable()
    {
        singleDataSaveButton.onClick.AddListener(async () => await SingleDataSave());
    }

    private async Task SingleDataSave()
    {
        // 저장할 데이터
        var data = new Dictionary<string, object>
        {
            {"player_name", "Zack"},
            {"level", 30},
            {"xp", 2000}
            {"gold", 100}
        };

        // 저장 메소드 호출
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        Debug.Log("데이터 저장 완료!");
    }
}
