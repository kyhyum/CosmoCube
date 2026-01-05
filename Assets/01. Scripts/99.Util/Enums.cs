using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolItemType
{
    MetaItem,
    HpRecoverItem,
    OxygenRecoveryItem,
    DestoryObj,
    OxygenObj,
    BladeObj,
}

public enum UIName
{
    Initlayer,
    SettingLayer,
    StageSelectLayer,
    AlertLayer,
    ClearLayer,
    PauseLayer
}
public enum DynamicList
{
    VerticalList,
    HorizontalList,
    GridList
}
public enum GameStep
{
    Init,
    InGame
}

public enum GameStage
{
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage5,
    Stage6,
    Stage7,
    Stage8,
    Stage9,
    Stage10
}

public enum PlayerState
{
    Idle,
    Up,
    Down,
    Right,
    Left,
    Destroy,
    Meta,
}

public enum CubeColor
{
    Meta,
    Blue,
    Cyan,
    Green,
    Yellow,
    Purple,
    Red
}