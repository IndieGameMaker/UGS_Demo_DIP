using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Auth = Unity.Services.Authentication;
using Score = Unity.Services.Leaderboards;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField scoreIf;
    [SerializeField] private Button scoreSaveButton;
}
