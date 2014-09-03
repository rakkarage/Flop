using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
public class Flop : UIBehaviour, IDragHandler
{
	public float Offset = 64f;
	public Transform LookAt;
	public Transform Content;
	protected override void Start()
	{
		for (int i = 0; i < Content.childCount; i++)
		{
			var x = i * Offset;
			Drag(x, Content.GetChild(i));
		}
		Order();
	}
	public void Drag(PointerEventData e)
	{
		foreach (Transform i in Content)
		{
			var x = i.position.x + e.delta.x;
			Drag(x, i);
		}
		Order();
	}
	private void Order()
	{
		var children = GetComponentsInChildren<Transform>();
		var sorted = from child in children orderby child.position.z descending select child;
		for (int i = 0; i < sorted.Count(); i++)
		{
			sorted.ElementAt(i).SetSiblingIndex(i);
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
