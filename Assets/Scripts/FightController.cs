using Profile;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FightController : BaseController
{
    private readonly ProfilePlayer _profilePlayer;
    private readonly FightWindowView _view;

    private Enemy _enemy;

    private Money _money;
    private Health _health;
    private Power _power;
    private Crime _crime;

    private int _allCountMoneyPlayer;
    private int _allCountHealthPlayer;
    private int _allCountPowerPlayer;
    private int _allCountCrimePlayer;
    public FightController(ProfilePlayer profilePlayer, FightWindowView view)
    {
        _profilePlayer = profilePlayer;
        _view = view;
        AddGameObjects(_view.gameObject);
        _enemy = new Enemy("Flappy");

        _money = new Money(nameof(Money));
        _money.Attach(_enemy);

        _health = new Health(nameof(Health));
        _health.Attach(_enemy);

        _power = new Power(nameof(Power));
        _power.Attach(_enemy);

        _crime = new Crime(nameof(Crime));
        _crime.Attach(_enemy);
        SubscribeButtons();
    }

    private void SubscribeButtons()
    {
        _view.AddCrime.onClick.AddListener(() => ChangeCrime(true));
        _view.AddHealth.onClick.AddListener(() => ChangeHealth(true));
        _view.AddMoney.onClick.AddListener(() => ChangeMoney(true));
        _view.AddPower.onClick.AddListener(() => ChangePower(true));
        _view.MinusCrime.onClick.AddListener(() => ChangeCrime(false));
        _view.MinusHealth.onClick.AddListener(() => ChangeHealth(false));
        _view.MinusMoney.onClick.AddListener(() => ChangeMoney(false));
        _view.MinusPower.onClick.AddListener(() => ChangePower(false));
        _view.Fight.onClick.AddListener(() => ChangePower(false));
        _view.MinusPower.onClick.AddListener(() => ChangePower(false));
        _view.Fight.onClick.AddListener(Fight);
        _view.Leave.onClick.AddListener(Leave);
    }

    private void ChangePower(bool isAddCount)
    {
        if (isAddCount)
            _allCountPowerPlayer++;
        else
            _allCountPowerPlayer--;

        ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
    }
    private void ChangeHealth(bool isAddCount)
    {
        if (isAddCount)
            _allCountHealthPlayer++;
        else
            _allCountHealthPlayer--;

        ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
    }
    private void ChangeMoney(bool isAddCount)
    {
        if (isAddCount)
            _allCountMoneyPlayer++;
        else
            _allCountMoneyPlayer--;

        ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
    }
    private void ChangeCrime(bool isAddCount)
    {
        if (isAddCount)
            _allCountCrimePlayer++;
        else
            _allCountCrimePlayer--;

        ChangeDataWindow(_allCountCrimePlayer, DataType.Crime);
    }
    private void Fight()
    {
        if (_allCountPowerPlayer > _enemy.Power)
        {
            Debug.Log("Win!");
            ChangeCrime(true);
        }
        else if (_allCountPowerPlayer < _enemy.Power)
            Debug.Log("Lose!");
        else
            Debug.Log("Draw!");
        _profilePlayer.CurrentState.Value = GameState.Game;
    }
    private void Leave()
    {
        Debug.Log("Left the battle!");
        _profilePlayer.CurrentState.Value = GameState.Game;
    }
    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _view.CountMoneyText.text = $"Player Money: {countChangeData}";
                _money.CountMoney = countChangeData;
                break;

            case DataType.Health:
                _view.CountHealthText.text = $"Player Health: {countChangeData}";
                _health.CountHealth = countChangeData;
                break;

            case DataType.Power:
                _view.CountPowerText.text = $"Player Power: {countChangeData}";
                _power.CountPower = countChangeData;
                break;
            case DataType.Crime:
                _view.CountCrimeText.text = $"Player Crime: {countChangeData}";
                _crime.CountCrime = countChangeData;
                break;
        }

        _view.CountPowerEnemyText.text = $"Enemy Power: {_enemy.Power}";

        if (_allCountCrimePlayer <= 2)
        {
            _view.Leave.GetComponent<Image>().color = Color.green;
            _view.Leave.interactable = true;
        }
        else
        {
            _view.Leave.GetComponent<Image>().color = Color.red;
            _view.Leave.interactable = false;
        }
    }
    protected override void OnDispose()
    {
        _money.Detach(_enemy);
        _health.Detach(_enemy);
        _power.Detach(_enemy);
        _crime.Detach(_enemy);
        base.OnDispose();
    }
}