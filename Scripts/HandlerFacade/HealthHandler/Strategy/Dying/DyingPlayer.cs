using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DyingPlayer : IDying
{
    private IHasHealthHandler _owner;

    public DyingPlayer(IHasHealthHandler owner)
    {
        _owner = owner;
    }

    public void ApplyDeath(float health)
    {
        if (health > 0) return;

        SceneManager.LoadScene("Gameplay");
    }
}
