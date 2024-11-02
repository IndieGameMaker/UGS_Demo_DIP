using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Auth = Unity.Services.Authentication.AuthenticationService;
using Score = Unity.Services.Leaderboards.LeaderboardsService;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField scoreIf;
    [SerializeField] private Button scoreSaveButton;

    private const string leaderboardId = "Ranking";

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await Auth.Instance.SignInAnonymouslyAsync();
        await Task.Delay(200);
        Debug.Log($"로그인 완료 : {Auth.Instance.PlayerId}");

        scoreSaveButton.onClick.AddListener(async () =>
        await AddScore(int.Parse(scoreIf.text)));

        await LoadScore();
    }

    // 점수 기록
    private async Task AddScore(int score)
    {
        var response = await Score.Instance.AddPlayerScoreAsync(leaderboardId, score);
        Debug.Log(JsonConvert.SerializeObject(response));
    }

    // 점수 로딩
    private async Task LoadScore()
    {
        var response = await Score.Instance.GetPlayerScoreAsync(leaderboardId);
        scoreIf.text = response.Score.ToString();
        Debug.Log($"Ranking : {response.Rank}");
    }
}
