using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class MouseDrag : MonoBehaviour
{
	// Token: 0x06000010 RID: 16 RVA: 0x00003560 File Offset: 0x00001760
	public void OnMouseDown()
	{
		if (!(this.maid != null))
		{
			return;
		}
		this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
		this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
		this.off2 = new Vector3(this.obj.transform.position.x - this.HandL.position.x, this.obj.transform.position.y - this.HandL.position.y, this.obj.transform.position.z - this.HandL.position.z);
		if (!this.initFlg)
		{
			this.IK.Init(this.UpperArmL, this.ForearmL, this.HandL, this.maid.body0);
			this.initFlg = true;
			if (!this.initFlg2)
			{
				this.initFlg2 = true;
				this.HandL2.transform.position = this.HandL.position;
				this.UpperArmL2.transform.position = this.UpperArmL.position;
				this.ForearmL2.transform.position = this.ForearmL.position;
				this.HandL2.transform.localRotation = this.HandL.localRotation;
				this.UpperArmL2.transform.localRotation = this.UpperArmL.localRotation;
				this.ForearmL2.transform.localRotation = this.ForearmL.localRotation;
			}
		}
		if (this.onFlg)
		{
			this.onFlg2 = true;
			this.IK.Init(this.UpperArmL, this.ForearmL, this.HandL, this.maid.body0);
		}
		this.mouseIti = Input.mousePosition;
		this.isPlay = this.maid.body0.m_Bones.GetComponent<Animation>().isPlaying;
		this.isSelect = true;
		this.rotate = this.HandL.localEulerAngles;
		this.rotate2 = this.UpperArmL.localEulerAngles;
		this.isMouseUp = false;
		this.isMouseDown = true;
		if (this.ido == 10)
		{
			Vector3 position = default(Vector3);
			position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z);
			Vector3 vector = Camera.main.ScreenToWorldPoint(position) + this.off - this.off2;
			this.IK.Init(this.UpperArmL2.transform, this.ForearmL2.transform, this.HandL2.transform, this.maid.body0);
			for (int i = 0; i < 10; i++)
			{
				this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
			}
		}
		if (this.shoki == -1000f)
		{
			this.shoki = this.UpperArmL.localEulerAngles.x;
			if (this.shoki > 300f)
			{
				this.shoki -= 360f;
			}
		}
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00003962 File Offset: 0x00001B62
	public void OnMouseUp()
	{
		this.isMouseUp = true;
		this.isMouseDown = false;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00003974 File Offset: 0x00001B74
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
			this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
			this.off2 = new Vector3(this.obj.transform.position.x - this.HandL.position.x, this.obj.transform.position.y - this.HandL.position.y, this.obj.transform.position.z - this.HandL.position.z);
			this.rotate = this.HandL.localEulerAngles;
			this.rotate2 = this.UpperArmL.localEulerAngles;
			this.mouseIti = Input.mousePosition;
			if (this.onFlg)
			{
				this.IK.Init(this.UpperArmL, this.ForearmL, this.HandL, this.maid.body0);
			}
		}
		if (this.mouseIti != Input.mousePosition)
		{
			Vector3 position = default(Vector3);
			position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z);
			Vector3 vector = Camera.main.ScreenToWorldPoint(position) + this.off - this.off2;
			this.isMouseDrag = true;
			if (!this.isPlay)
			{
				this.isStop = true;
			}
			this.isIdo = false;
			if (this.ido == 0)
			{
				this.isIdo = true;
				this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				if (!this.onFlg)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				}
				this.HandLangles = new Vector3(this.HandL.localEulerAngles.x, this.HandL.localEulerAngles.y, this.HandL.localEulerAngles.z);
				this.UpperArmLangles = new Vector3(this.UpperArmL.localEulerAngles.x, this.UpperArmL.localEulerAngles.y, this.UpperArmL.localEulerAngles.z);
			}
			else if (this.ido == 11)
			{
				this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				if (!this.onFlg)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				}
				float num = this.UpperArmL.localEulerAngles.x;
				if (num > 250f + this.shoki && num < 352f + this.shoki)
				{
					num = 352f + this.shoki;
				}
				if (num < 100f + this.shoki && num > 8f + this.shoki)
				{
					num = 8f + this.shoki;
				}
				float num2 = this.UpperArmL.localEulerAngles.z;
				if (num2 > 140f && num2 < 240f)
				{
					num2 = 240f;
				}
				if (num2 <= 140f && num2 > 7f)
				{
					num2 = 7f;
				}
				this.HandL.localEulerAngles = new Vector3(this.rotate.x, this.rotate.y, this.HandL.localEulerAngles.z);
				this.UpperArmL.localEulerAngles = new Vector3(num, this.rotate2.y, num2);
			}
			else if (this.ido == 12)
			{
				this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				if (!this.onFlg)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				}
				float num3 = this.UpperArmL.localEulerAngles.x;
				if (num3 > 250f + this.shoki && num3 < 357f + this.shoki)
				{
					num3 = 357f + this.shoki;
				}
				if (num3 < 100f + this.shoki && num3 > 3f + this.shoki)
				{
					num3 = 3f + this.shoki;
				}
				float num4 = this.UpperArmL.localEulerAngles.z;
				if (num4 > 140f && num4 < 270f)
				{
					num4 = 270f;
				}
				if (num4 <= 140f && num4 > 15f)
				{
					num4 = 15f;
				}
				this.HandL.localEulerAngles = new Vector3(this.rotate.x, this.rotate.y, this.HandL.localEulerAngles.z);
				this.UpperArmL.localEulerAngles = new Vector3(num3, this.rotate2.y, num4);
			}
			else if (this.ido == 17)
			{
				this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				if (!this.onFlg)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				}
				float num5 = this.UpperArmL.localEulerAngles.x;
				if (num5 > 250f + this.shoki && num5 < 357f + this.shoki)
				{
					num5 = 357f + this.shoki;
				}
				if (num5 < 100f + this.shoki && num5 > 3f + this.shoki)
				{
					num5 = 3f + this.shoki;
				}
				float num6 = this.UpperArmL.localEulerAngles.z;
				if (num6 > 220f && num6 < 345f)
				{
					num6 = 345f;
				}
				if (num6 <= 220f && num6 > 90f)
				{
					num6 = 90f;
				}
				this.HandL.localEulerAngles = new Vector3(this.rotate.x, this.rotate.y, this.HandL.localEulerAngles.z);
				this.UpperArmL.localEulerAngles = new Vector3(num5, this.rotate2.y, num6);
			}
			else if (this.ido == 13)
			{
				this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				if (!this.onFlg)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				}
				float num7 = this.UpperArmL.localEulerAngles.x;
				if (num7 > 250f && num7 < 345f)
				{
					num7 = 345f;
				}
				if (num7 > 250f || num7 > 25f)
				{
				}
				float num8 = this.UpperArmL.localEulerAngles.z;
				if (num8 > 160f && num8 < 275f)
				{
					num8 = 275f;
				}
				if (num8 <= 160f && num8 > 60f)
				{
					num8 = 60f;
				}
				float num9 = this.UpperArmL.localEulerAngles.y;
				if (num9 > 250f && num9 < 345f)
				{
					num9 = 345f;
				}
				if (num9 > 250f || num9 > 20f)
				{
				}
				this.HandL.localEulerAngles = new Vector3(this.rotate.x, this.HandL.localEulerAngles.y, this.HandL.localEulerAngles.z);
				this.UpperArmL.localEulerAngles = new Vector3(this.UpperArmL.localEulerAngles.x, this.UpperArmL.localEulerAngles.y, num8);
			}
			else if (this.ido == 14)
			{
				this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				if (!this.onFlg)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				}
				float num10 = this.UpperArmL.localEulerAngles.x;
				if (num10 > 250f + this.shoki && num10 < 345f + this.shoki)
				{
					num10 = 345f + this.shoki;
				}
				if (num10 < 100f + this.shoki && num10 > 15f + this.shoki)
				{
					num10 = 15f + this.shoki;
				}
				float num11 = this.UpperArmL.localEulerAngles.z;
				if (num11 > 150f && num11 < 240f)
				{
					num11 = 240f;
				}
				if (num11 <= 150f && num11 > 30f)
				{
					num11 = 30f;
				}
				this.HandL.localEulerAngles = new Vector3(this.rotate.x, this.rotate.y, this.HandL.localEulerAngles.z);
				this.UpperArmL.localEulerAngles = new Vector3(num10, this.rotate2.y, num11);
			}
			else if (this.ido == 15)
			{
				this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				if (!this.onFlg)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				}
				float num12 = this.UpperArmL.localEulerAngles.x;
				if (num12 > 250f + this.shoki && num12 < 357f + this.shoki)
				{
					num12 = 357f + this.shoki;
				}
				if (num12 < 100f + this.shoki && num12 > 3f + this.shoki)
				{
					num12 = 3f + this.shoki;
				}
				float num13 = this.UpperArmL.localEulerAngles.z;
				if (num13 > 150f && num13 < 270f)
				{
					num13 = 270f;
				}
				if (num13 <= 150f && num13 > 30f)
				{
					num13 = 30f;
				}
				this.HandL.localEulerAngles = new Vector3(this.rotate.x, this.rotate.y, this.HandL.localEulerAngles.z);
				this.UpperArmL.localEulerAngles = new Vector3(num12, this.rotate2.y, num13);
			}
			else if (this.ido == 16)
			{
				Vector3 vector2 = Input.mousePosition - this.mouseIti;
				Transform transform = GameMain.Instance.MainCamera.gameObject.transform;
				transform.TransformDirection(Vector3.right);
				transform.TransformDirection(Vector3.forward);
				if (this.mouseIti2 != Input.mousePosition)
				{
					this.UpperArmL.localEulerAngles = this.rotate2;
					this.UpperArmL.localRotation = Quaternion.Euler(this.UpperArmL.localEulerAngles) * Quaternion.AngleAxis(vector2.x / 2.2f, Vector3.right);
				}
			}
			else
			{
				Vector3 vector3 = Input.mousePosition - this.mouseIti;
				Transform transform2 = GameMain.Instance.MainCamera.gameObject.transform;
				Vector3 vector4 = transform2.TransformDirection(Vector3.right);
				Vector3 vector5 = transform2.TransformDirection(Vector3.forward);
				if (this.mouseIti2 != Input.mousePosition)
				{
					if (this.ido <= 4)
					{
						this.HandL.localEulerAngles = this.rotate;
					}
					else
					{
						this.UpperArmL.localEulerAngles = this.rotate2;
					}
					if (this.ido == 1)
					{
						this.HandL.localRotation = Quaternion.Euler(this.HandL.localEulerAngles) * Quaternion.AngleAxis(vector3.x / 1.5f, Vector3.up);
						this.HandL.localRotation = Quaternion.Euler(this.HandL.localEulerAngles) * Quaternion.AngleAxis(vector3.y / 1.5f, Vector3.forward);
					}
					if (this.ido == 2)
					{
						this.HandL.localRotation = Quaternion.Euler(this.HandL.localEulerAngles) * Quaternion.AngleAxis(vector3.x / 1.5f, Vector3.right);
					}
					if (this.ido == 3)
					{
						this.HandL.RotateAround(this.HandL.position, new Vector3(vector4.x, 0f, vector4.z), vector3.y / 1f);
						this.HandL.RotateAround(this.HandL.position, new Vector3(vector5.x, 0f, vector5.z), vector3.x / 1.5f);
					}
					if (this.ido == 4)
					{
						this.HandL.localRotation = Quaternion.Euler(this.HandL.localEulerAngles) * Quaternion.AngleAxis((0f - vector3.x) / 1.5f, Vector3.right);
					}
					if (this.ido == 5)
					{
						this.UpperArmL.localRotation = Quaternion.Euler(this.UpperArmL.localEulerAngles) * Quaternion.AngleAxis((0f - vector3.x) / 1.5f, Vector3.right);
					}
				}
			}
		}
		this.mouseIti2 = Input.mousePosition;
	}

	// Token: 0x04000015 RID: 21
	private TBody.IKCMO IK = new TBody.IKCMO();

	// Token: 0x04000016 RID: 22
	private Vector3 worldPoint;

	// Token: 0x04000017 RID: 23
	public Vector3 off;

	// Token: 0x04000018 RID: 24
	public Vector3 off2;

	// Token: 0x04000019 RID: 25
	public Maid maid;

	// Token: 0x0400001A RID: 26
	public Transform HandL;

	// Token: 0x0400001B RID: 27
	public Transform UpperArmL;

	// Token: 0x0400001C RID: 28
	public Transform ForearmL;

	// Token: 0x0400001D RID: 29
	public bool isStop;

	// Token: 0x0400001E RID: 30
	public bool isPlay;

	// Token: 0x0400001F RID: 31
	public bool isSelect;

	// Token: 0x04000020 RID: 32
	private Vector3 mouseIti;

	// Token: 0x04000021 RID: 33
	public bool isIdo;

	// Token: 0x04000022 RID: 34
	public Vector3 HandLangles;

	// Token: 0x04000023 RID: 35
	public Vector3 UpperArmLangles;

	// Token: 0x04000024 RID: 36
	public bool isMouseUp;

	// Token: 0x04000025 RID: 37
	public bool isMouseDown;

	// Token: 0x04000026 RID: 38
	public bool isMouseDrag;

	// Token: 0x04000027 RID: 39
	public bool initFlg;

	// Token: 0x04000028 RID: 40
	public bool initFlg2;

	// Token: 0x04000029 RID: 41
	public bool onFlg;

	// Token: 0x0400002A RID: 42
	public bool onFlg2;

	// Token: 0x0400002B RID: 43
	public GameObject obj;

	// Token: 0x0400002C RID: 44
	public int ido;

	// Token: 0x0400002D RID: 45
	public bool reset;

	// Token: 0x0400002E RID: 46
	public float shoki = -1000f;

	// Token: 0x0400002F RID: 47
	private Vector3 rotate;

	// Token: 0x04000030 RID: 48
	private Vector3 rotate2;

	// Token: 0x04000031 RID: 49
	private Vector3 mouseIti2;

	// Token: 0x04000032 RID: 50
	private GameObject HandL2 = new GameObject();

	// Token: 0x04000033 RID: 51
	private GameObject UpperArmL2 = new GameObject();

	// Token: 0x04000034 RID: 52
	private GameObject ForearmL2 = new GameObject();
}
