using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightWindowView : MonoBehaviour
{
    [SerializeField]
    public TMP_Text CountMoneyText;

    [SerializeField]
    public TMP_Text CountHealthText;

    [SerializeField]
    public TMP_Text CountPowerText;

    [SerializeField]
    public TMP_Text CountCrimeText;

    [SerializeField]
    public TMP_Text CountPowerEnemyText;

    [SerializeField]
    private Button _addMoneyButton;

    [SerializeField]
    private Button _minusMoneyButton;

    [SerializeField]
    private Button _addHealthButton;

    [SerializeField]
    private Button _minusHealthButton;

    [SerializeField]
    private Button _addPowerButton;

    [SerializeField]
    private Button _minusPowerButton;

    [SerializeField]
    private Button _addCrimeButton;

    [SerializeField]
    private Button _minusCrimeButton;

    [SerializeField]
    private Button _fightButton;
    [SerializeField]
    private Button _leaveButton;

    public Button AddMoney => _addMoneyButton;
    public Button AddHealth => _addHealthButton;
    public Button AddPower => _addPowerButton;
    public Button AddCrime => _addCrimeButton;
    public Button MinusMoney => _minusMoneyButton;
    public Button MinusHealth => _minusHealthButton;
    public Button MinusPower => _minusPowerButton;
    public Button MinusCrime => _minusCrimeButton;
    public Button Fight => _fightButton;
    public Button Leave => _leaveButton;
}
