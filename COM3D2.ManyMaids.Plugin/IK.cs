using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class IK
{
	// Token: 0x06000009 RID: 9 RVA: 0x000022F4 File Offset: 0x000004F4
	public void Init(Transform hip, Transform knee, Transform ankle, TBody b)
	{
		this.body = b;
		this.defLEN1 = (hip.position - knee.position).magnitude;
		this.defLEN2 = (ankle.position - knee.position).magnitude;
		this.knee_old = knee.position;
		this.defHipQ = hip.localRotation;
		this.defKneeQ = knee.localRotation;
		this.vechand = Vector3.zero;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002378 File Offset: 0x00000578
	public void Porc(Transform hip, Transform knee, Transform ankle, Vector3 tgt, Vector3 vechand_offset)
	{
		this.knee_old = this.knee_old * 0.5f + knee.position * 0.5f;
		Vector3 normalized = (this.knee_old - tgt).normalized;
		this.knee_old = tgt + normalized * this.defLEN2;
		Vector3 normalized2 = (this.knee_old - hip.position).normalized;
		this.knee_old = hip.position + normalized2 * this.defLEN1;
		default(Quaternion).SetLookRotation(normalized2);
		hip.transform.rotation = Quaternion.FromToRotation(knee.transform.position - hip.transform.position, this.knee_old - hip.transform.position) * hip.transform.rotation;
		knee.transform.rotation = Quaternion.FromToRotation(ankle.transform.position - knee.transform.position, tgt - knee.transform.position) * knee.transform.rotation;
	}

	// Token: 0x04000008 RID: 8
	private TBody body;

	// Token: 0x04000009 RID: 9
	private float defLEN1;

	// Token: 0x0400000A RID: 10
	private float defLEN2;

	// Token: 0x0400000B RID: 11
	private Vector3 knee_old;

	// Token: 0x0400000C RID: 12
	private Quaternion defHipQ;

	// Token: 0x0400000D RID: 13
	private Quaternion defKneeQ;

	// Token: 0x0400000E RID: 14
	private Vector3 vechand;
}
