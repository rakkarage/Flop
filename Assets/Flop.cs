using UnityEngine;
using UnityEngine.EventSystems;
public class Flop : UIBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	public float Offset = 64f;
	public Transform LookAt;
	public Transform Content;
	protected override void Start()
	{
		for (int i = 0; i < Content.childCount; i++)
		{
			var x = i * Offset;
			Pan(x, Content.GetChild(i));
		}
	}
	public void Pan(PointerEventData e)
	{
		foreach (Transform i in Content)
		{
			var x = i.position.x + e.delta.x;
			Pan(x, i);
		}
	}
	private void Pan(float x, Transform t)
	{
		t.position = new Vector3(x, t.position.y, x < 0 ? -x : x);
		t.rotation = Quaternion.LookRotation(LookAt.position - t.position);
	}
	public void OnBeginDrag(PointerEventData e)
	{
		Pan(e);
	}
	public void OnDrag(PointerEventData e)
	{
		Pan(e);
	}
	public void OnEndDrag(PointerEventData e)
	{
		Pan(e);
	}
}
