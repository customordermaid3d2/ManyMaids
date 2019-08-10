using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class MouseDrag3 : MonoBehaviour
{
	// Token: 0x06000019 RID: 25 RVA: 0x0000521C File Offset: 0x0000341C
	public void Update()
	{
		this.doubleTapTime += Time.deltaTime;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00005230 File Offset: 0x00003430
	public void OnMouseDown()
	{
		if (this.maid != null)
		{
			this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
			this.mouseIti = Input.mousePosition;
			if (this.ido == 1 || this.ido == 4 || this.ido == 8)
			{
				this.rotate = this.head.localEulerAngles;
			}
			if (this.ido == 2 || this.ido == 5)
			{
				this.rotate1 = this.Spine1a.localEulerAngles;
				this.rotate2 = this.Spine1.localEulerAngles;
				this.rotate3 = this.Spine0a.localEulerAngles;
				this.rotate4 = this.Spine.localEulerAngles;
			}
			if (this.ido == 3 || this.ido == 6)
			{
				this.rotate5 = this.Pelvis.localEulerAngles;
			}
			if (this.ido == 7)
			{
				this.rotateR = this.maid.body0.quaDefEyeR.eulerAngles;
				this.rotateL = this.maid.body0.quaDefEyeL.eulerAngles;
				this.quaL = this.maid.body0.quaDefEyeL;
				this.quaR = this.maid.body0.quaDefEyeR;
			}
			if (this.ido == 9)
			{
				this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
				this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
				this.off2 = new Vector3(this.obj.transform.position.x - this.head.position.x, this.obj.transform.position.y - this.head.position.y, this.obj.transform.position.z - this.head.position.z);
				this.mouseIti = Input.mousePosition;
			}
			this.isSelect = true;
			this.isPlay = this.maid.body0.m_Bones.GetComponent<Animation>().isPlaying;
			if (!this.isPlay)
			{
				this.isStop = true;
			}
			if (this.doubleTapTime < 0.3f && this.isClick2 && this.ido == this.idoOld)
			{
				this.isHead = true;
			}
			if (this.doubleTapTime >= 0.3f && this.isClick)
			{
				this.isClick = false;
			}
			if (this.doubleTapTime >= 0.3f && this.isClick2)
			{
				this.isClick2 = false;
			}
			this.doubleTapTime = 0f;
			this.pos3 = Input.mousePosition - this.mouseIti;
		}
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00005534 File Offset: 0x00003734
	public void OnMouseUp()
	{
		if (this.maid != null && this.doubleTapTime < 0.3f)
		{
			if (this.ido == 7)
			{
				this.isClick = true;
			}
			this.isClick2 = true;
			this.doubleTapTime = 0f;
			this.idoOld = this.ido;
		}
	}

	// Token: 0x0600001C RID: 28 RVA: 0x0000558C File Offset: 0x0000378C
	public void OnMouseDrag()
	{
		if (!(this.maid != null))
		{
			return;
		}
		if (this.isPlay && this.mouseIti != Input.mousePosition)
		{
			this.maid.body0.m_Bones.GetComponent<Animation>().Stop();
			this.isStop = true;
			this.isPlay = false;
		}
		if (this.reset)
		{
			this.reset = false;
			this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
			if (this.ido == 1 || this.ido == 4 || this.ido == 8)
			{
				this.rotate = this.head.localEulerAngles;
			}
			if (this.ido == 2 || this.ido == 5)
			{
				this.rotate1 = this.Spine1a.localEulerAngles;
				this.rotate2 = this.Spine1.localEulerAngles;
				this.rotate3 = this.Spine0a.localEulerAngles;
				this.rotate4 = this.Spine.localEulerAngles;
			}
			if (this.ido == 3 || this.ido == 6)
			{
				this.rotate5 = this.Pelvis.localEulerAngles;
			}
			if (this.ido == 7)
			{
				this.rotateR = this.maid.body0.quaDefEyeR.eulerAngles;
				this.rotateL = this.maid.body0.quaDefEyeL.eulerAngles;
				this.quaL = this.maid.body0.quaDefEyeL;
				this.quaR = this.maid.body0.quaDefEyeR;
			}
			this.mouseIti = Input.mousePosition;
			if (this.ido == 9)
			{
				this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
				this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
				this.off2 = new Vector3(this.obj.transform.position.x - this.head.position.x, this.obj.transform.position.y - this.head.position.y, this.obj.transform.position.z - this.head.position.z);
				this.mouseIti = Input.mousePosition;
			}
		}
		if (!(this.mouseIti != Input.mousePosition))
		{
			return;
		}
		Vector3 position = default(Vector3);
		position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z);
		Vector3 vector = Input.mousePosition - this.mouseIti;
		Transform transform = GameMain.Instance.MainCamera.gameObject.transform;
		Vector3 vector2 = transform.TransformDirection(Vector3.right);
		Vector3 vector3 = transform.TransformDirection(Vector3.forward);
		if (this.mouseIti2 != Input.mousePosition)
		{
			if (this.ido == 1)
			{
				this.isIdo = true;
				this.head.localEulerAngles = this.rotate;
				this.head.RotateAround(this.head.position, new Vector3(vector2.x, 0f, vector2.z), vector.y / 3f);
				this.head.RotateAround(this.head.position, new Vector3(vector3.x, 0f, vector3.z), (0f - vector.x) / 4.5f);
			}
			if (this.ido == 4)
			{
				this.isIdo = true;
				this.head.localEulerAngles = this.rotate;
				this.head.localRotation = Quaternion.Euler(this.head.localEulerAngles) * Quaternion.AngleAxis(vector.x / 3f, Vector3.right);
			}
			if (this.ido == 8)
			{
				this.isIdo = true;
				this.head.localEulerAngles = this.rotate;
				this.head.localRotation = Quaternion.Euler(this.head.localEulerAngles) * Quaternion.AngleAxis((0f - vector.x) / 3f, Vector3.forward);
			}
			if (this.ido == 9)
			{
				Vector3 vector4 = Camera.main.ScreenToWorldPoint(position) + this.off - this.off2;
				this.head.transform.position = new Vector3(this.head.transform.position.x, vector4.y, this.head.transform.position.z);
			}
			if (this.ido == 2)
			{
				this.Spine1a.localEulerAngles = this.rotate1;
				this.Spine1.localEulerAngles = this.rotate2;
				this.Spine0a.localEulerAngles = this.rotate3;
				this.Spine.localEulerAngles = this.rotate4;
				float num = 1.5f;
				float num2 = 1f;
				float num3 = 0.03f;
				float num4 = 0.1f;
				float num5 = 0.09f;
				float num6 = 0.07f;
				this.Spine1a.RotateAround(this.Spine1a.position, new Vector3(vector2.x, 0f, vector2.z), vector.y / num2 * num3);
				this.Spine1a.RotateAround(this.Spine1a.position, new Vector3(vector3.x, 0f, vector3.z), (0f - vector.x) / num * num3);
				this.Spine1.RotateAround(this.Spine1.position, new Vector3(vector2.x, 0f, vector2.z), vector.y / num2 * num4);
				this.Spine1.RotateAround(this.Spine1.position, new Vector3(vector3.x, 0f, vector3.z), (0f - vector.x) / num * num4);
				this.Spine0a.RotateAround(this.Spine0a.position, new Vector3(vector2.x, 0f, vector2.z), vector.y / num2 * num5);
				this.Spine0a.RotateAround(this.Spine0a.position, new Vector3(vector3.x, 0f, vector3.z), (0f - vector.x) / num * num5);
				this.Spine.RotateAround(this.Spine.position, new Vector3(vector2.x, 0f, vector2.z), vector.y / num2 * num6);
				this.Spine.RotateAround(this.Spine.position, new Vector3(vector3.x, 0f, vector3.z), (0f - vector.x) / num * num6);
			}
			if (this.ido == 5)
			{
				this.Spine1a.localEulerAngles = this.rotate1;
				this.Spine1.localEulerAngles = this.rotate2;
				this.Spine0a.localEulerAngles = this.rotate3;
				this.Spine.localEulerAngles = this.rotate4;
				this.Spine1a.localRotation = Quaternion.Euler(this.Spine1a.localEulerAngles) * Quaternion.AngleAxis(vector.x / 1.5f * 0.084f, Vector3.right);
				this.Spine0a.localRotation = Quaternion.Euler(this.Spine0a.localEulerAngles) * Quaternion.AngleAxis(vector.x / 1.5f * 0.156f, Vector3.right);
				this.Spine.localRotation = Quaternion.Euler(this.Spine.localEulerAngles) * Quaternion.AngleAxis(vector.x / 1.5f * 0.156f, Vector3.right);
			}
			if (this.ido == 3)
			{
				this.Pelvis.localEulerAngles = this.rotate5;
				this.Pelvis.RotateAround(this.Pelvis.position, new Vector3(vector2.x, 0f, vector2.z), vector.y / 4f);
				this.Pelvis.RotateAround(this.Pelvis.position, new Vector3(vector3.x, 0f, vector3.z), vector.x / 6f);
			}
			if (this.ido == 6)
			{
				this.Pelvis.localEulerAngles = this.rotate5;
				this.Pelvis.localRotation = Quaternion.Euler(this.Pelvis.localEulerAngles) * Quaternion.AngleAxis(vector.x / 3f, Vector3.right);
			}
			if (this.ido == 7)
			{
				Vector3 vector5 = default(Vector3);
				vector5 = new Vector3(this.rotateR.x, this.rotateR.y + vector.x / 10f, this.rotateR.z + vector.y / 10f);
				if (this.shodaiFlg)
				{
					if (vector5.z < 345.7f && vector5.z > 335.7f)
					{
						this.pos3.y = vector.y;
					}
					if (vector5.y < 347.6f && vector5.y > 335.6f)
					{
						this.pos3.x = vector.x;
					}
				}
				else
				{
					if (vector5.z < 354.8f && vector5.z > 344.8f)
					{
						this.pos3.y = vector.y;
					}
					if (vector5.y < 354f && vector5.y > 342f)
					{
						this.pos3.x = vector.x;
					}
				}
				this.maid.body0.quaDefEyeR.eulerAngles = new Vector3(this.rotateR.x, this.rotateR.y + this.pos3.x / 10f, this.rotateR.z + this.pos3.y / 10f);
				this.maid.body0.quaDefEyeL.eulerAngles = new Vector3(this.rotateL.x, this.rotateL.y - this.pos3.x / 10f, this.rotateL.z - this.pos3.y / 10f);
			}
		}
		this.mouseIti2 = Input.mousePosition;
	}

	// Token: 0x04000046 RID: 70
	private Vector3 worldPoint;

	// Token: 0x04000047 RID: 71
	private Vector3 off;

	// Token: 0x04000048 RID: 72
	private Vector3 off2;

	// Token: 0x04000049 RID: 73
	public GameObject obj;

	// Token: 0x0400004A RID: 74
	public int ido;

	// Token: 0x0400004B RID: 75
	public bool reset;

	// Token: 0x0400004C RID: 76
	public bool isSelect;

	// Token: 0x0400004D RID: 77
	public Transform head;

	// Token: 0x0400004E RID: 78
	public Transform Spine1a;

	// Token: 0x0400004F RID: 79
	public Transform Spine1;

	// Token: 0x04000050 RID: 80
	public Transform Spine0a;

	// Token: 0x04000051 RID: 81
	public Transform Spine;

	// Token: 0x04000052 RID: 82
	public Transform Pelvis;

	// Token: 0x04000053 RID: 83
	public bool isIdo;

	// Token: 0x04000054 RID: 84
	public Maid maid;

	// Token: 0x04000055 RID: 85
	public int no;

	// Token: 0x04000056 RID: 86
	public bool shodaiFlg;

	// Token: 0x04000057 RID: 87
	private Vector3 rotateL;

	// Token: 0x04000058 RID: 88
	private Vector3 rotateR;

	// Token: 0x04000059 RID: 89
	private Quaternion quaL;

	// Token: 0x0400005A RID: 90
	private Quaternion quaR;

	// Token: 0x0400005B RID: 91
	private Vector3 pos3;

	// Token: 0x0400005C RID: 92
	private Vector3 rotate;

	// Token: 0x0400005D RID: 93
	private Vector3 rotate1;

	// Token: 0x0400005E RID: 94
	private Vector3 rotate2;

	// Token: 0x0400005F RID: 95
	private Vector3 rotate3;

	// Token: 0x04000060 RID: 96
	private Vector3 rotate4;

	// Token: 0x04000061 RID: 97
	private Vector3 rotate5;

	// Token: 0x04000062 RID: 98
	private Vector3 mouseIti;

	// Token: 0x04000063 RID: 99
	private Vector3 mouseIti2;

	// Token: 0x04000064 RID: 100
	public bool isHead;

	// Token: 0x04000065 RID: 101
	private float doubleTapTime;

	// Token: 0x04000066 RID: 102
	public bool isStop;

	// Token: 0x04000067 RID: 103
	public bool isPlay;

	// Token: 0x04000068 RID: 104
	public bool isClick;

	// Token: 0x04000069 RID: 105
	public bool isClick2;

	// Token: 0x0400006A RID: 106
	private int idoOld;
}
