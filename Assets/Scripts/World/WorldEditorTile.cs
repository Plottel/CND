using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEditorTile : MonoBehaviour
{
    public enum TileState
    {
        Inactive,
        Active,
        Valid,
        Invalid
    }

    public Vector2Int pos;
    public TileState state;
    public TileState? nextState;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        state = nextState ?? TileState.Inactive;
        nextState = null;

        switch (state)
        {
            case TileState.Inactive:
                sprite.color = Color.white;
                break;

            case TileState.Active:
                sprite.color = Color.grey;
                break;

            case TileState.Valid:
                sprite.color = Color.green;
                break;

            case TileState.Invalid:
                sprite.color = Color.red;
                break;
        }
    }
}
