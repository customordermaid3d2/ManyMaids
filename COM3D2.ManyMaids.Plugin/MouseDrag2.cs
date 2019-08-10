using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class MouseDrag2 : MonoBehaviour
{
	// Token: 0x06000014 RID: 20 RVA: 0x00004A93 File Offset: 0x00002C93
	public void Update()
	{
		this.doubleTapTime += Time.deltaTime;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00004AA8 File Offset: 0x00002CA8
	public void OnMouseDown()
	{
		if (!(this.maid != null))
		{
			return;
		}
		this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
		this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
		this.off2 = new Vector3(this.obj.transform.position.x - this.maid.transform.position.x, this.obj.transform.position.y - this.maid.transform.position.y, this.obj.transform.position.z - this.maid.transform.position.z);
		this.mouseIti = Input.mousePosition;
		if (this.doubleTapTime < 0.3f && this.isClick && this.ido == this.idoOld)
		{
			if (this.ido == 5)
			{
				this.maid.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			if (this.ido == 4 || this.ido == 6)
			{
				this.maid.transform.eulerAngles = new Vector3(0f, this.maid.transform.eulerAngles.y, 0f);
			}
		}
		if (this.doubleTapTime >= 0.3f && this.isClick)
		{
			this.isClick = false;
		}
		this.doubleTapTime = 0f;
		this.rotate = this.maid.transform.localEulerAngles;
		this.size = this.maid.transform.localScale.x;
		this.isSelect = true;
		this.isOn = true;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00004CBC File Offset: 0x00002EBC
	public void OnMouseUp()
	{
		if (this.maid != null)
		{
			this.isOn = false;
		}
		if (this.doubleTapTime < 0.3f)
		{
			this.isClick = true;
			this.doubleTapTime = 0f;
			this.idoOld = this.ido;
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00004D0C File Offset: 0x00002F0C
	public void OnMouseDrag()
	{
		if (!(this.maid != null))
		{
			return;
		}
		if (this.reset)
		{
			this.reset = false;
			this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
			this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
			this.off2 = new Vector3(this.obj.transform.position.x - this.maid.transform.position.x, this.obj.transform.position.y - this.maid.transform.position.y, this.obj.transform.position.z - this.maid.transform.position.z);
			this.rotate = this.maid.transform.localEulerAngles;
			this.size = this.maid.transform.localScale.x;
			this.mouseIti = Input.mousePosition;
		}
		if (!(this.mouseIti != Input.mousePosition))
		{
			return;
		}
		Vector3 position = default(Vector3);
		position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z);
		Vector3 vector = Camera.main.ScreenToWorldPoint(position) + this.off - this.off2;
		this.isIdo = false;
		if (this.ido == 1)
		{
			this.maid.transform.position = new Vector3(vector.x, this.maid.transform.position.y, vector.z);
			this.isIdo = true;
		}
		if (this.ido == 2)
		{
			this.maid.transform.position = new Vector3(this.maid.transform.position.x, vector.y, this.maid.transform.position.z);
			this.isIdo = true;
		}
		if (this.ido == 3)
		{
			Vector3 vector2 = Input.mousePosition - this.mouseIti;
			this.maid.transform.eulerAngles = new Vector3(this.maid.transform.eulerAngles.x, this.rotate.y - vector2.x / 2.2f, this.maid.transform.eulerAngles.z);
		}
		if (this.ido == 4)
		{
			Vector3 vector3 = Input.mousePosition - this.mouseIti;
			Transform transform = GameMain.Instance.MainCamera.gameObject.transform;
			Vector3 vector4 = transform.TransformDirection(Vector3.right);
			Vector3 vector5 = transform.TransformDirection(Vector3.forward);
			if (this.mouseIti2 != Input.mousePosition)
			{
				this.maid.transform.localEulerAngles = this.rotate;
				this.maid.transform.RotateAround(this.maid.transform.position, new Vector3(vector4.x, 0f, vector4.z), vector3.y / 4f);
				this.maid.transform.RotateAround(this.maid.transform.position, new Vector3(vector5.x, 0f, vector5.z), (0f - vector3.x) / 6f);
			}
			this.isIdo = true;
			this.mouseIti2 = Input.mousePosition;
		}
		if (this.ido == 6)
		{
			Vector3 vector6 = Input.mousePosition - this.mouseIti;
			Transform transform2 = GameMain.Instance.MainCamera.gameObject.transform;
			transform2.TransformDirection(Vector3.right);
			transform2.TransformDirection(Vector3.forward);
			if (this.mouseIti2 != Input.mousePosition)
			{
				this.maid.transform.localEulerAngles = this.rotate;
				this.maid.body0.transform.localRotation = Quaternion.Euler(this.maid.body0.transform.localEulerAngles) * Quaternion.AngleAxis((0f - vector6.x) / 2.2f, Vector3.up);
			}
			this.isIdo = true;
			this.mouseIti2 = Input.mousePosition;
		}
		if (this.ido == 5)
		{
			Vector3 vector7 = Input.mousePosition - this.mouseIti;
			float num = this.size + vector7.y / 200f;
			if (num < 0.1f)
			{
				num = 0.1f;
			}
			this.maid.transform.localScale = new Vector3(num, num, num);
		}
	}

	// Token: 0x04000035 RID: 53
	private Vector3 worldPoint;

	// Token: 0x04000036 RID: 54
	private Vector3 off;

	// Token: 0x04000037 RID: 55
	private Vector3 off2;

	// Token: 0x04000038 RID: 56
	public GameObject obj;

	// Token: 0x04000039 RID: 57
	public int ido;

	// Token: 0x0400003A RID: 58
	public bool reset;

	// Token: 0x0400003B RID: 59
	public bool isSelect;

	// Token: 0x0400003C RID: 60
	public Maid maid;

	// Token: 0x0400003D RID: 61
	public bool isIdo;

	// Token: 0x0400003E RID: 62
	private Vector3 rotate;

	// Token: 0x0400003F RID: 63
	private float size;

	// Token: 0x04000040 RID: 64
	private Vector3 mouseIti;

	// Token: 0x04000041 RID: 65
	private Vector3 mouseIti2;

	// Token: 0x04000042 RID: 66
	public bool isOn;

	// Token: 0x04000043 RID: 67
	public bool isClick;

	// Token: 0x04000044 RID: 68
	private float doubleTapTime;

	// Token: 0x04000045 RID: 69
	private int idoOld;
}
