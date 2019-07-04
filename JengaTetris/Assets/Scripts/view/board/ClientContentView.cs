using UnityEngine;
using System.Collections;
using Assets.Scripts.view.common;

public class ClientContentView : GameComponent
{

    public Transform pieceContent;
    private BoardBottom bottom;

    void Awake()
    {
        bottom = FindAndAdd<BoardBottom>(transform, "Bottom");
    }

    public int id { get; private set; }

    public ClientContentView Initiate(int id)
    {
        this.id = id;
        return this;
    }
    
}
