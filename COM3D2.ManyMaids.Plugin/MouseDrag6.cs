using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class MouseDrag6 : MonoBehaviour
{
	// Token: 0x06000023 RID: 35 RVA: 0x00006492 File Offset: 0x00004692
	public void Update()
	{
		this.doubleTapTime += Time.deltaTime;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000064A8 File Offset: 0x000046A8
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
				if (this.isScale2)
				{
					this.maid.transform.localScale = this.scale2;
				}
				else
				{
					this.maid.transform.localScale = new Vector3(1f, 1f, 1f);
				}
			}
			if (this.ido == 4 || this.ido == 6)
			{
				this.maid.transform.eulerAngles = new Vector3(this.angles.x, this.maid.transform.eulerAngles.y, this.angles.z);
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
		if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
		{
			this.isAlt = !this.isAlt;
		}
		this.xList = new List<float>();
		this.yList = new List<float>();
		this.zList = new List<float>();
		if (this.maidArray != null)
		{
			for (int i = 0; i < this.maidArray.Length; i++)
			{
				this.xList.Add(this.maid.transform.position.x - this.maidArray[i].transform.position.x);
				this.yList.Add(this.maid.transform.position.y - this.maidArray[i].transform.position.y);
				this.zList.Add(this.maid.transform.position.z - this.maidArray[i].transform.position.z);
			}
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00006800 File Offset: 0x00004A00
	public void OnMouseUp()
	{
		if (this.maid != null)
		{
			this.isOn = false;
			if (this.doubleTapTime < 0.3f)
			{
				this.isClick = true;
				this.doubleTapTime = 0f;
				this.idoOld = this.ido;
			}
			if (this.ido == 7)
			{
				this.del = true;
			}
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00006860 File Offset: 0x00004A60
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
			if (this.maidArray != null)
			{
				for (int i = 0; i < this.maidArray.Length; i++)
				{
					if (this.mArray[i].isAlt)
					{
						this.maidArray[i].transform.position = new Vector3(vector.x - this.xList[i], this.maidArray[i].transform.position.y, vector.z - this.zList[i]);
					}
				}
			}
			this.isIdo = true;
		}
		if (this.ido == 2)
		{
			this.maid.transform.position = new Vector3(this.maid.transform.position.x, vector.y, this.maid.transform.position.z);
			if (this.maidArray != null)
			{
				for (int j = 0; j < this.maidArray.Length; j++)
				{
					if (this.mArray[j].isAlt)
					{
						this.maidArray[j].transform.position = new Vector3(this.maidArray[j].transform.position.x, vector.y - this.yList[j], this.maidArray[j].transform.position.z);
					}
				}
			}
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
				this.maid.transform.localRotation = Quaternion.Euler(this.maid.transform.localEulerAngles) * Quaternion.AngleAxis((0f - vector6.x) / 2.2f, Vector3.up);
			}
			this.isIdo = true;
			this.mouseIti2 = Input.mousePosition;
		}
		if (this.ido == 5)
		{
			Vector3 vector7 = Input.mousePosition - this.mouseIti;
			float num = this.size + vector7.y / 200f;
			if (num < 0.01f)
			{
				num = 0.01f;
			}
			if (this.isScale)
			{
				this.maid.transform.localScale = new Vector3(this.scale.x * num, this.scale.y * num, this.scale.z * num);
			}
			else
			{
				this.maid.transform.localScale = new Vector3(num, num, num);
			}
		}
		if (this.ido == 15)
		{
			Vector3 vector8 = Input.mousePosition - this.mouseIti;
			float num2 = this.size + vector8.y / 2f;
			if (num2 < 0.01f)
			{
				num2 = 0.01f;
			}
			if (num2 > 150f)
			{
				num2 = 150f;
			}
			this.maid.transform.localScale = new Vector3(num2, num2, num2);
		}
	}

	// Token: 0x0400007E RID: 126
	private Vector3 worldPoint;

	// Token: 0x0400007F RID: 127
	private Vector3 off;

	// Token: 0x04000080 RID: 128
	private Vector3 off2;

	// Token: 0x04000081 RID: 129
	public GameObject obj;

	// Token: 0x04000082 RID: 130
	public int ido;

	// Token: 0x04000083 RID: 131
	public bool reset;

	// Token: 0x04000084 RID: 132
	public bool isSelect;

	// Token: 0x04000085 RID: 133
	public bool isScale;

	// Token: 0x04000086 RID: 134
	public bool isScale2;

	// Token: 0x04000087 RID: 135
	public bool isAlt;

	// Token: 0x04000088 RID: 136
	public Vector3 angles;

	// Token: 0x04000089 RID: 137
	public Vector3 scale;

	// Token: 0x0400008A RID: 138
	public Vector3 scale2;

	// Token: 0x0400008B RID: 139
	private List<float> xList = new List<float>();

	// Token: 0x0400008C RID: 140
	private List<float> yList = new List<float>();

	// Token: 0x0400008D RID: 141
	private List<float> zList = new List<float>();

	// Token: 0x0400008E RID: 142
	public bool del;

	// Token: 0x0400008F RID: 143
	public GameObject maid;

	// Token: 0x04000090 RID: 144
	public GameObject[] maidArray;

	// Token: 0x04000091 RID: 145
	public MouseDrag6[] mArray;

	// Token: 0x04000092 RID: 146
	public bool isIdo;

	// Token: 0x04000093 RID: 147
	private Vector3 rotate;

	// Token: 0x04000094 RID: 148
	private float size;

	// Token: 0x04000095 RID: 149
	private Vector3 mouseIti;

	// Token: 0x04000096 RID: 150
	private Vector3 mouseIti2;

	// Token: 0x04000097 RID: 151
	public bool isClick;

	// Token: 0x04000098 RID: 152
	private float doubleTapTime;

	// Token: 0x04000099 RID: 153
	public int count;

	// Token: 0x0400009A RID: 154
	public bool isOn;

	// Token: 0x0400009B RID: 155
	private int idoOld;
}
