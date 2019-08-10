using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class MouseDrag4 : MonoBehaviour
{
	// Token: 0x0600001E RID: 30 RVA: 0x0000608C File Offset: 0x0000428C
	public void OnMouseDown()
	{
		if (this.maid != null)
		{
			this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
			this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
			this.off2 = new Vector3(this.obj.transform.position.x - this.HandL.position.x, this.obj.transform.position.y - this.HandL.position.y, this.obj.transform.position.z - this.HandL.position.z);
			this.mouseIti = Input.mousePosition;
			this.isSelect = true;
			this.rotate = this.HandL.localEulerAngles;
		}
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000061AC File Offset: 0x000043AC
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
			this.off2 = new Vector3(this.obj.transform.position.x - this.HandL.position.x, this.obj.transform.position.y - this.HandL.position.y, this.obj.transform.position.z - this.HandL.position.z);
			this.rotate = this.HandL.localEulerAngles;
			this.mouseIti = Input.mousePosition;
		}
		if (this.mouseIti != Input.mousePosition)
		{
			Vector3 position = default(Vector3);
			position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z);
			Camera.main.ScreenToWorldPoint(position) + this.off - this.off2;
			Vector3 vector = Input.mousePosition - this.mouseIti;
			Transform transform = GameMain.Instance.MainCamera.gameObject.transform;
			Vector3 vector2 = transform.TransformDirection(Vector3.right);
			Vector3 vector3 = transform.TransformDirection(Vector3.forward);
			if (this.mouseIti2 != Input.mousePosition)
			{
				this.HandL.localEulerAngles = this.rotate;
				if (this.ido == 1)
				{
					this.HandL.RotateAround(this.HandL.position, new Vector3(vector2.x, 0f, vector2.z), vector.y / 1f);
					this.HandL.RotateAround(this.HandL.position, new Vector3(vector3.x, 0f, vector3.z), (0f - vector.x) / 1.5f);
				}
				if (this.ido == 2)
				{
					this.HandL.localRotation = Quaternion.Euler(this.HandL.localEulerAngles) * Quaternion.AngleAxis(vector.x / 1.5f, Vector3.right);
				}
			}
		}
		this.mouseIti2 = Input.mousePosition;
	}

	// Token: 0x0400006B RID: 107
	private Vector3 worldPoint;

	// Token: 0x0400006C RID: 108
	private Vector3 off;

	// Token: 0x0400006D RID: 109
	private Vector3 off2;

	// Token: 0x0400006E RID: 110
	public Maid maid;

	// Token: 0x0400006F RID: 111
	public Transform HandL;

	// Token: 0x04000070 RID: 112
	public bool isStop;

	// Token: 0x04000071 RID: 113
	public bool isPlay;

	// Token: 0x04000072 RID: 114
	public bool isSelect;

	// Token: 0x04000073 RID: 115
	private Vector3 mouseIti;

	// Token: 0x04000074 RID: 116
	public GameObject obj;

	// Token: 0x04000075 RID: 117
	public int ido;

	// Token: 0x04000076 RID: 118
	public bool reset;

	// Token: 0x04000077 RID: 119
	private Vector3 rotate;

	// Token: 0x04000078 RID: 120
	private Vector3 mouseIti2;
}
