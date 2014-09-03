using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
public class Flop : UIBehaviour, IDragHandler
{
	public float Offset = 64f;
	public Transform LookAt;
	public Transform Content;
	public bool Reorder = false;
	protected override void Start()
	{
		for (int i = 0; i < Content.childCount; i++)
		{
			var x = i * Offset;
			Drag(x, Content.GetChild(i));
		}
	}
	public void Drag(PointerEventData e)
	{
		foreach (Transform i in Content)
		{
			var x = i.position.x + e.delta.x;
			Drag(x, i);
		}
		if (Reorder)
		{
			var list = new List<Transform>(Content.childCount);
			for (int i = 0; i < Content.childCount; i++)
			{
				var child = Content.GetChild(i);
				list.Add(child);
			}
			var order = 0;
			var sorted = from i in list orderby Mathf.Abs(i.position.x) select i;
			foreach (var i in sorted)
			{
				i.SetSiblingIndex(list.Count - order++);
			}
		}
	}
	private void Drag(float x, Transform t)
	{
		t.position = new Vector3(x, t.position.y, x < 0 ? -x : x);
		t.rotation = Quaternion.LookRotation(LookAt.position - t.position);
	}
	public void OnDrag(PointerEventData e)
	{
		Drag(e);
	}
}
