using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Auth = Unity.Services.Authentication.AuthenticationService;
using Score = Unity.Services.Leaderboards.LeaderboardsService;
using System.Threading.Tasks;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField scoreIf;
    [SerializeField] private Button scoreSaveButton;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await Auth.Instance.SignInAnonymouslyAsync();
        await Task.Delay(200);
        Debug.Log($"로그인 완료 : {Auth.Instance.PlayerId}");
    }
}
