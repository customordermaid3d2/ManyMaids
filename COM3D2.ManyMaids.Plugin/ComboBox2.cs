using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class ComboBox2
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public int List(Rect rect, string buttonText, GUIContent[] listContent, GUIStyle listStyle)
	{
		return this.List(rect, new GUIContent(buttonText), listContent, "button", "box", listStyle);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002076 File Offset: 0x00000276
	public int List(Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle)
	{
		return this.List(rect, buttonContent, listContent, "button", "box", listStyle);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002097 File Offset: 0x00000297
	public int List(Rect rect, string buttonText, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
	{
		return this.List(rect, new GUIContent(buttonText), listContent, buttonStyle, boxStyle, listStyle);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020AD File Offset: 0x000002AD
	private int GetPix(int i)
	{
		return (int)((1f + ((float)Screen.width / 1280f - 1f) * 0.6f) * (float)i);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020D4 File Offset: 0x000002D4
	public int List(Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
	{
		if (ComboBox2.forceToUnShow)
		{
			ComboBox2.forceToUnShow = false;
			this.isClickedComboButton = false;
		}
		bool flag = false;
		int controlID = GUIUtility.GetControlID(FocusType.Passive);
		if (Event.current.GetTypeForControl(controlID) == EventType.MouseUp && this.isClickedComboButton && this.scrollPosOld == this.scrollPos)
		{
			flag = true;
		}
		if (GUI.Button(rect, buttonContent, buttonStyle))
		{
			if (ComboBox2.useControlID == -1)
			{
				ComboBox2.useControlID = controlID;
				this.isClickedComboButton = false;
			}
			if (ComboBox2.useControlID != controlID)
			{
				ComboBox2.forceToUnShow = true;
				ComboBox2.useControlID = controlID;
			}
			this.isClickedComboButton = true;
		}
		if (this.isClickedComboButton)
		{
			Rect position = default(Rect);
			position = new Rect(rect.x, rect.y + (float)this.GetPix(23), rect.width, listStyle.CalcHeight(listContent[0], 1f) * (float)listContent.Length);
			if (position.y + position.height > this.height)
			{
				position.height = this.height - position.y - 2f;
				position.width += 16f;
			}
			GUI.Box(position, "", boxStyle);
			if (Input.GetMouseButtonDown(0))
			{
				this.scrollPosOld = this.scrollPos;
			}
			Rect rect2 = default(Rect);
			rect2 = new Rect(rect.x, rect.y + listStyle.CalcHeight(listContent[0], 1f), rect.width, listStyle.CalcHeight(listContent[0], 1f) * (float)listContent.Length);
			this.scrollPos = GUI.BeginScrollView(position, this.scrollPos, rect2);
			int num = GUI.SelectionGrid(rect2, this.selectedItemIndex, listContent, 1, listStyle);
			if (num != this.selectedItemIndex)
			{
				this.selectedItemIndex = num;
			}
			GUI.EndScrollView();
		}
		if (flag)
		{
			this.isClickedComboButton = false;
		}
		return this.GetSelectedItemIndex();
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000022AC File Offset: 0x000004AC
	public int GetSelectedItemIndex()
	{
		return this.selectedItemIndex;
	}

	// Token: 0x04000001 RID: 1
	private static bool forceToUnShow = false;

	// Token: 0x04000002 RID: 2
	private static int useControlID = -1;

	// Token: 0x04000003 RID: 3
	public bool isClickedComboButton;

	// Token: 0x04000004 RID: 4
	public float height;

	// Token: 0x04000005 RID: 5
	public int selectedItemIndex;

	// Token: 0x04000006 RID: 6
	public Vector2 scrollPos = new Vector2(0f, 0f);

	// Token: 0x04000007 RID: 7
	private Vector2 scrollPosOld = new Vector2(0f, 0f);
}
