using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DoorManager : ItemBase
{
	[SerializeField] private GameObject exitDoor;
	[SerializeField] private int exitOffsetDistance = 2;
	[SerializeField] private Color unlockedColor;
	[SerializeField] private Color lockedColor;
	[SerializeField] private bool locked = false;
	[SerializeField] private long unlockScore = 0;
	[SerializeField] private bool unlockKey = false;

    // Start is called before the first frame update
    protected override void Start()
    {
    	base.Start();   
    	if (locked) {
    		GetComponent<SpriteRenderer>().color = lockedColor;
    	}
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void PlayerCollided(GameObject player) {
    	if (locked) {
    		if (unlockScore > 0 && true /* score above threshold */)
    		{
    			locked = false;
    			GetComponent<SpriteRenderer>().color = unlockedColor;
    		}
    		else if (unlockKey && GameManager.instance.GetInventoryManager().HasKey() /* player has key*/) {
    			GameManager.instance.GetInventoryManager().RemoveKey();
    			locked = false;
    			GetComponent<SpriteRenderer>().color = unlockedColor;
    		}
    	}

    	if (exitDoor && !locked) {
    		playerManager.TeleportToExit(exitDoor, exitOffsetDistance);
    	}
    }

    public int GetExitOffsetDistance() {
    	return exitOffsetDistance;
    }

    // transform of exit door (if it exists)
    public Transform GetExitTransform()
    {
    	return exitDoor.transform;
    }

    public bool HasExitDoor() {
    	return exitDoor != null;
    }
}

// Draw Gizmos for this object regardless of selection
public class DoorManagerGizmoDrawer
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
    static void DrawGizmoForDoorManager(DoorManager scr, GizmoType gizmoType)
    {
    	DrawExitDirectionGizmo(scr);
        DrawExitConnectionGizmo(scr);
    }

    // draw arrow in exit direction (showing exit offset size too)
    static void DrawExitDirectionGizmo(DoorManager scr){
    	Vector3 position = scr.transform.position;
        Vector3 up = scr.transform.up;

        Handles.color = Color.magenta;
   		Handles.Slider(position, up,scr.GetExitOffsetDistance(), Handles.ArrowHandleCap, 0.1f);
    }

    // bezier curve to linked exit door or error circle (no linked exit)
    static void DrawExitConnectionGizmo(DoorManager scr) {
    	if (scr.HasExitDoor()) {
    		Transform exitTransform = scr.GetExitTransform();
    		Handles.DrawBezier(scr.transform.position, exitTransform.position, scr.transform.up, exitTransform.up, Color.magenta, null, 2f);
    	} 
    	else
    	{
    		Handles.color = Color.red;
    		Handles.DrawSolidDisc(scr.transform.position, new Vector3(0,0,1), 1f);
    	}
	}
}
