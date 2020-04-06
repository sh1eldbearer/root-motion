/// <summary>
/// Designates which player an object is assigned to. Each player value has a corresponding integer value.
/// </summary>
public enum PlayerNumbers
{
    Unassigned = -1,
    P1 = 0,
    P2 = 1,
    P3 = 2,
    P4 = 3
}

public enum PlayerStatus
{
    Inactive = -2, // Disable player object group
    NotJoined = -1, // Press button to join
    Joined = 0, // Pick color
    Ready = 1, // Color picked, awaiting game start
    Alive = 2, // Playing game
    Dead = 3, // Dead but awaiting respawn
    GameOver = 4 // Dead and out of lives
}

/// <summary>
/// Allows for easy selection of a color in the inspector. Can also be quickly reference the
/// appropriate element of the skin colors list on the SkinManager.
/// </summary>
public enum ColorNames
{
    Disabled = 0,
    Pink = 1,
    Red = 2,
    Orange = 3,
    Gold = 4,
    Green = 5,
    Aqua = 6,
    Blue = 7,
    Violet = 8
}

/// <summary>
/// Designates which set of animations a pawn's animator should use to move.
/// </summary>
public enum LocomotionState
{
    Walking = 0,
    Crouching = 1,
    Sprinting = 2
}

public enum WeaponQuality
{
    Base = 0,
    Uncommon = 1,
    Rare = 2,
    Epic = 3,
    Legendary = 4
}