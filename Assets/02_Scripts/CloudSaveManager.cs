using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;
using Auth = Unity.Services.Authentication.AuthenticationService;

[System.Serializable]
public struct PlayerData
{
    public string name;
    public int level;
    public int xp;
    public int gold;

    public List<ItemData> items;
}

[System.Serializable]
public struct ItemData
{
    public string name;
    public int count;
    public string iconPath;
}

public class CloudSaveManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button singleDataSaveButton;
    [SerializeField] private Button multiDataSaveButton;

    [SerializeField] private Button singleDataLoadButton;
    [SerializeField] private Button multiDataLoadButton;


    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;

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
        multiDataSaveButton.onClick.AddListener(async () =>
            await SaveMultiData<PlayerData>("PLAYER_DATA", playerData));

        singleDataLoadButton.onClick.AddListener(async () => await LoadData());
    }

    // 단일 데이터 저장 로직
    private async Task SingleDataSave()
    {
        // 저장할 데이터
        var data = new Dictionary<string, object>
        {
            {"player_name", "Zack"},
            {"level", 30},
            {"xp", 2000},
            {"gold", 100}
        };

        // 저장 메소드 호출
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        Debug.Log("데이터 저장 완료!");
    }

    // 복수 데이터 저장 로직
    private async Task SaveMultiData<T>(string key, T saveData)
    {
        var data = new Dictionary<string, object>
        {
            { key, saveData}
        };

        // 저장 메소드
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        Debug.Log("복수 데이터 저장 완료");
    }

    // 싱글 데이터 로드
    /*
    HashSet
    */
    private async Task LoadData()
    {
        var query = new HashSet<string>()
        {
            "player_name", "level", "xp", "gold"
        };
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(query);

        if (data.TryGetValue("player_name", out var playerName))
        {
            Debug.Log("Player Name : " + playerName.Value.GetAs<string>());
        }

        if (data.TryGetValue("level", out var level))
        {
            Debug.Log("Level : " + level.Value.GetAs<int>());
        }
    }
}
