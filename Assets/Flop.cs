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
			var d = new Dictionary<int, Vector3>();
			for (int i = 0; i < Content.childCount; i++)
			{
				var child = Content.GetChild(i);
				d.Add(i, child.localPosition);
			}
			var order = 0;
			foreach (KeyValuePair<int, Vector3> i in d.OrderBy(key => Mathf.Abs(key.Value.x)))
			{
				Content.GetChild(i.Key).SetSiblingIndex(d.Count - order++);
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
