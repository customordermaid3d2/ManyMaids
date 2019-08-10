using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class MouseDrag5 : MonoBehaviour
{
	// Token: 0x06000021 RID: 33 RVA: 0x00006462 File Offset: 0x00004662
	public void OnMouseDown()
	{
		if (this.maid != null)
		{
			if (this.ido == 1)
			{
				this.isClick = true;
			}
			if (this.ido == 2)
			{
				this.isClick = true;
			}
		}
	}

	// Token: 0x04000079 RID: 121
	public Maid maid;

	// Token: 0x0400007A RID: 122
	public int no;

	// Token: 0x0400007B RID: 123
	public GameObject obj;

	// Token: 0x0400007C RID: 124
	public int ido;

	// Token: 0x0400007D RID: 125
	public bool isClick;
}
