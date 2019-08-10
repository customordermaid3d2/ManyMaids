using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class ImportCM2 : MonoBehaviour
{
	// Token: 0x0600000C RID: 12 RVA: 0x000024CC File Offset: 0x000006CC
	public static GameObject LoadSkinMesh_R(string filename, string[] filename2, string slotname, TBodySkin bodyskin, int layer)
	{
		try
		{
			AFileBase afileBase = GameUty.FileOpen(filename, null);
			try
			{
				if (ImportCM2.m_skinTempFile == null)
				{
					ImportCM2.m_skinTempFile = new byte[Math.Max(500000, afileBase.GetSize())];
				}
				else if (ImportCM2.m_skinTempFile.Length < afileBase.GetSize())
				{
					ImportCM2.m_skinTempFile = new byte[afileBase.GetSize()];
				}
				afileBase.Read(ref ImportCM2.m_skinTempFile, afileBase.GetSize());
			}
			finally
			{
				AFileBase afileBase2 = afileBase;
				if (afileBase2 != null)
				{
					afileBase2.Dispose();
				}
			}
		}
		catch (Exception ex)
		{
			NDebug.Assert("ファイルが開けませんでした。" + filename + "\n" + ex.Message, false);
		}
		BinaryReader binaryReader = new BinaryReader(new MemoryStream(ImportCM2.m_skinTempFile), Encoding.UTF8);
		TBodySkin.OriVert oriVert = bodyskin.m_OriVert;
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("seed")) as GameObject;
		gameObject.layer = layer;
		GameObject gameObject2 = null;
		Hashtable hashtable = new Hashtable();
		string text = binaryReader.ReadString();
		if (text != "CM3D2_MESH")
		{
			NDebug.Assert("LoadSkinMesh_R 例外 : ヘッダーファイルが不正です。" + text, false);
		}
		int num = binaryReader.ReadInt32();
		string str = binaryReader.ReadString();
		gameObject.name = "_SM_" + str;
		string b = binaryReader.ReadString();
		int num2 = binaryReader.ReadInt32();
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < num2; i++)
		{
			GameObject gameObject3 = UnityEngine.Object.Instantiate(Resources.Load("seed")) as GameObject;
			gameObject3.layer = layer;
			gameObject3.name = binaryReader.ReadString();
			list.Add(gameObject3);
			if (gameObject3.name == b)
			{
				gameObject2 = gameObject3;
			}
			hashtable[gameObject3.name] = gameObject3;
			if (binaryReader.ReadByte() != 0)
			{
				GameObject gameObject4 = UnityEngine.Object.Instantiate(Resources.Load("seed")) as GameObject;
				gameObject4.name = gameObject3.name + "_SCL_";
				gameObject4.transform.parent = gameObject3.transform;
				hashtable[gameObject3.name + "&_SCL_"] = gameObject4;
			}
		}
		for (int j = 0; j < num2; j++)
		{
			int num3 = binaryReader.ReadInt32();
			if (num3 >= 0)
			{
				list[j].transform.parent = list[num3].transform;
			}
			else
			{
				list[j].transform.parent = gameObject.transform;
			}
		}
		for (int k = 0; k < num2; k++)
		{
			Transform transform = list[k].transform;
			float x = binaryReader.ReadSingle();
			float y = binaryReader.ReadSingle();
			float z = binaryReader.ReadSingle();
			transform.localPosition = new Vector3(x, y, z);
			float x2 = binaryReader.ReadSingle();
			float y2 = binaryReader.ReadSingle();
			float z2 = binaryReader.ReadSingle();
			float w = binaryReader.ReadSingle();
			transform.localRotation = new Quaternion(x2, y2, z2, w);
			if (2001 <= num && binaryReader.ReadBoolean())
			{
				float x3 = binaryReader.ReadSingle();
				float y3 = binaryReader.ReadSingle();
				float z3 = binaryReader.ReadSingle();
				transform.localScale = new Vector3(x3, y3, z3);
			}
		}
		int num4 = binaryReader.ReadInt32();
		int num5 = binaryReader.ReadInt32();
		int num6 = binaryReader.ReadInt32();
		oriVert.VCount = num4;
		oriVert.nSubMeshCount = num5;
		gameObject2.AddComponent(typeof(SkinnedMeshRenderer));
		gameObject.AddComponent(typeof(Animation));
		SkinnedMeshRenderer skinnedMeshRenderer = gameObject2.GetComponent<Renderer>() as SkinnedMeshRenderer;
		skinnedMeshRenderer.updateWhenOffscreen = true;
		Transform[] array = new Transform[num6];
		for (int l = 0; l < num6; l++)
		{
			string text2 = binaryReader.ReadString();
			if (hashtable.ContainsKey(text2))
			{
				GameObject gameObject5 = (!hashtable.ContainsKey(text2 + "&_SCL_")) ? ((GameObject)hashtable[text2]) : ((GameObject)hashtable[text2 + "&_SCL_"]);
				array[l] = gameObject5.transform;
			}
		}
		skinnedMeshRenderer.bones = array;
		Mesh mesh = new Mesh();
		skinnedMeshRenderer.sharedMesh = mesh;
		Mesh mesh2 = mesh;
		Matrix4x4[] array2 = new Matrix4x4[num6];
		for (int m = 0; m < num6; m++)
		{
			for (int n = 0; n < 16; n++)
			{
				array2[m][n] = binaryReader.ReadSingle();
			}
		}
		mesh2.bindposes = array2;
		Vector3[] array3 = new Vector3[num4];
		Vector3[] array4 = new Vector3[num4];
		Vector2[] array5 = new Vector2[num4];
		BoneWeight[] array6 = new BoneWeight[num4];
		for (int num7 = 0; num7 < num4; num7++)
		{
			float num8 = binaryReader.ReadSingle();
			float num9 = binaryReader.ReadSingle();
			float new_z = binaryReader.ReadSingle();
			array3[num7].Set(num8, num9, new_z);
			num8 = binaryReader.ReadSingle();
			num9 = binaryReader.ReadSingle();
			new_z = binaryReader.ReadSingle();
			array4[num7].Set(num8, num9, new_z);
			num8 = binaryReader.ReadSingle();
			num9 = binaryReader.ReadSingle();
			array5[num7].Set(num8, num9);
		}
		mesh2.vertices = array3;
		mesh2.normals = array4;
		mesh2.uv = array5;
		oriVert.vOriVert = array3;
		oriVert.vOriNorm = array4;
		int num10 = binaryReader.ReadInt32();
		if (num10 > 0)
		{
			Vector4[] array7 = new Vector4[num10];
			for (int num11 = 0; num11 < num10; num11++)
			{
				float x4 = binaryReader.ReadSingle();
				float y4 = binaryReader.ReadSingle();
				float z4 = binaryReader.ReadSingle();
				float w2 = binaryReader.ReadSingle();
				array7[num11] = new Vector4(x4, y4, z4, w2);
			}
			mesh2.tangents = array7;
		}
		for (int num12 = 0; num12 < num4; num12++)
		{
			array6[num12].boneIndex0 = (int)binaryReader.ReadUInt16();
			array6[num12].boneIndex1 = (int)binaryReader.ReadUInt16();
			array6[num12].boneIndex2 = (int)binaryReader.ReadUInt16();
			array6[num12].boneIndex3 = (int)binaryReader.ReadUInt16();
			array6[num12].weight0 = binaryReader.ReadSingle();
			array6[num12].weight1 = binaryReader.ReadSingle();
			array6[num12].weight2 = binaryReader.ReadSingle();
			array6[num12].weight3 = binaryReader.ReadSingle();
		}
		mesh2.boneWeights = array6;
		mesh2.subMeshCount = num5;
		oriVert.bwWeight = array6;
		oriVert.nSubMeshCount = num5;
		oriVert.nSubMeshOriTri = new int[num5][];
		for (int num13 = 0; num13 < num5; num13++)
		{
			int num14 = binaryReader.ReadInt32();
			int[] array8 = new int[num14];
			for (int num15 = 0; num15 < num14; num15++)
			{
				array8[num15] = (int)binaryReader.ReadUInt16();
			}
			oriVert.nSubMeshOriTri[num13] = array8;
			mesh2.SetTriangles(array8, num13);
		}
		int num16 = binaryReader.ReadInt32();
		Material[] array9 = new Material[num16];
		for (int num17 = 0; num17 < num16; num17++)
		{
			array9[num17] = ImportCM2.ReadMaterial(binaryReader, bodyskin, null, filename2[1 + num17]);
		}
		skinnedMeshRenderer.materials = array9;
		binaryReader.Close();
		return gameObject;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002C1C File Offset: 0x00000E1C
	public static Material ReadMaterial(BinaryReader r, TBodySkin bodyskin, Material existmat, string filename)
	{
		existmat = null;
		if (ImportCM2.m_hashPriorityMaterials == null)
		{
			ImportCM2.m_hashPriorityMaterials = new Dictionary<int, KeyValuePair<string, float>>();
			string[] list = GameUty.FileSystem.GetList("prioritymaterial", 3);
			if (list != null && list.Length != 0)
			{
				for (int i = 0; i < list.Length; i++)
				{
					if (Path.GetExtension(list[i]) == ".pmat")
					{
						string text = list[i];
						AFileBase afileBase = GameUty.FileOpen(text, null);
						try
						{
							NDebug.Assert(afileBase.IsValid(), text + "を開けませんでした");
							using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(afileBase.ReadAll()), Encoding.UTF8))
							{
								NDebug.Assert(binaryReader.ReadString() == "CM3D2_PMATERIAL", "ヘッダーエラー\n" + text);
								binaryReader.ReadInt32();
								int key = binaryReader.ReadInt32();
								string key2 = binaryReader.ReadString();
								float value = binaryReader.ReadSingle();
								NDebug.Assert(!ImportCM2.m_hashPriorityMaterials.ContainsKey(key), "すでにハッシュが登録されています");
								ImportCM2.m_hashPriorityMaterials.Add(key, new KeyValuePair<string, float>(key2, value));
							}
						}
						finally
						{
							AFileBase afileBase2 = afileBase;
							if (afileBase2 != null)
							{
								afileBase2.Dispose();
							}
						}
					}
				}
			}
		}
		string name = r.ReadString();
		string text2 = r.ReadString();
		string str = r.ReadString();
		string path = "DefMaterial/" + str;
		Material material2;
		if (existmat == null)
		{
			Material material = Resources.Load(path, typeof(Material)) as Material;
			if (material == null)
			{
				return material;
			}
			material2 = UnityEngine.Object.Instantiate<Material>(material);
		}
		else
		{
			material2 = existmat;
			NDebug.Assert(material2.shader.name == text2, "マテリアル入れ替えエラー。違うシェーダーに入れようとしました。 " + text2 + " -> " + material2.shader.name);
		}
		material2.name = name;
		int hashCode = material2.name.GetHashCode();
		if (ImportCM2.m_hashPriorityMaterials != null && ImportCM2.m_hashPriorityMaterials.ContainsKey(hashCode))
		{
			KeyValuePair<string, float> keyValuePair = ImportCM2.m_hashPriorityMaterials[hashCode];
			if (keyValuePair.Key == material2.name)
			{
				material2.SetFloat("_SetManualRenderQueue", keyValuePair.Value);
				material2.renderQueue = (int)keyValuePair.Value;
			}
		}
		Vector2 value2 = default(Vector2);
		Vector2 value3 = default(Vector2);
		Color value4 = default(Color);
		Vector4 value5 = default(Vector4);
		for (;;)
		{
			string a = r.ReadString();
			if (a == "end")
			{
				break;
			}
			string name2 = r.ReadString();
			if (a == "tex")
			{
				string a2 = r.ReadString();
				if (a2 == "null")
				{
					material2.SetTexture(name2, null);
				}
				else
				{
					if (a2 == "tex2d")
					{
						try
						{
							string text3 = r.ReadString();
							r.ReadString();
							byte[] data = ImportCM.LoadTexture(GameUty.FileSystem, text3 + ".tex", false).data;
							value2.x = r.ReadSingle();
							value2.y = r.ReadSingle();
							value3.x = r.ReadSingle();
							value3.y = r.ReadSingle();
							if (filename == null || !filename.Contains(".mate"))
							{
								Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
								texture2D.LoadImage(data);
								texture2D.name = text3;
								texture2D.wrapMode = TextureWrapMode.Clamp;
								material2.SetTexture(name2, texture2D);
								material2.SetTextureOffset(name2, value2);
								material2.SetTextureScale(name2, value3);
							}
							continue;
						}
						catch
						{
							break;
						}
					}
					if (a2 == "texRT")
					{
						r.ReadString();
						r.ReadString();
					}
				}
			}
			else if (a == "col")
			{
				value4.r = r.ReadSingle();
				value4.g = r.ReadSingle();
				value4.b = r.ReadSingle();
				value4.a = r.ReadSingle();
				material2.SetColor(name2, value4);
			}
			else if (a == "vec")
			{
				value5.x = r.ReadSingle();
				value5.y = r.ReadSingle();
				value5.z = r.ReadSingle();
				value5.w = r.ReadSingle();
				material2.SetVector(name2, value5);
			}
			else if (a == "f")
			{
				float value6 = r.ReadSingle();
				material2.SetFloat(name2, value6);
			}
		}
		if (filename != null && filename.Contains(".mate"))
		{
			try
			{
				AFileBase afileBase3 = GameUty.FileOpen(filename, null);
				try
				{
					if (ImportCM2.m_matTempFile == null)
					{
						ImportCM2.m_matTempFile = new byte[Math.Max(10000, afileBase3.GetSize())];
					}
					else if (ImportCM2.m_matTempFile.Length < afileBase3.GetSize())
					{
						ImportCM2.m_matTempFile = new byte[afileBase3.GetSize()];
					}
					afileBase3.Read(ref ImportCM2.m_matTempFile, afileBase3.GetSize());
				}
				finally
				{
					AFileBase afileBase4 = afileBase3;
					if (afileBase4 != null)
					{
						afileBase4.Dispose();
					}
				}
			}
			catch
			{
			}
			BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(ImportCM2.m_matTempFile), Encoding.UTF8);
			string text4 = binaryReader2.ReadString();
			NDebug.Assert(text4 == "CM3D2_MATERIAL", "ProcScriptBin 例外 : ヘッダーファイルが不正です。" + text4);
			binaryReader2.ReadInt32();
			binaryReader2.ReadString();
			r = binaryReader2;
			name = r.ReadString();
			text2 = r.ReadString();
			str = r.ReadString();
			path = "DefMaterial/" + str;
			if (existmat == null)
			{
				Material material3 = Resources.Load(path, typeof(Material)) as Material;
				if (material3 == null)
				{
					return material3;
				}
				material2 = UnityEngine.Object.Instantiate<Material>(material3);
			}
			else
			{
				material2 = existmat;
				NDebug.Assert(material2.shader.name == text2, "マテリアル入れ替えエラー。違うシェーダーに入れようとしました。 " + text2 + " -> " + material2.shader.name);
			}
			material2.name = name;
			hashCode = material2.name.GetHashCode();
			if (ImportCM2.m_hashPriorityMaterials != null && ImportCM2.m_hashPriorityMaterials.ContainsKey(hashCode))
			{
				KeyValuePair<string, float> keyValuePair2 = ImportCM2.m_hashPriorityMaterials[hashCode];
				if (keyValuePair2.Key == material2.name)
				{
					material2.SetFloat("_SetManualRenderQueue", keyValuePair2.Value);
					material2.renderQueue = (int)keyValuePair2.Value;
				}
			}
			for (;;)
			{
				string a3 = r.ReadString();
				if (a3 == "end")
				{
					break;
				}
				string name3 = r.ReadString();
				if (a3 == "tex")
				{
					string a4 = r.ReadString();
					if (a4 == "null")
					{
						material2.SetTexture(name3, null);
					}
					else
					{
						if (a4 == "tex2d")
						{
							try
							{
								string text5 = r.ReadString();
								r.ReadString();
								byte[] data2 = ImportCM.LoadTexture(GameUty.FileSystem, text5 + ".tex", false).data;
								Texture2D texture2D2 = new Texture2D(1, 1, TextureFormat.RGBA32, false);
								texture2D2.LoadImage(data2);
								texture2D2.name = text5;
								texture2D2.wrapMode = TextureWrapMode.Clamp;
								material2.SetTexture(name3, texture2D2);
								value2.x = r.ReadSingle();
								value2.y = r.ReadSingle();
								material2.SetTextureOffset(name3, value2);
								value3.x = r.ReadSingle();
								value3.y = r.ReadSingle();
								material2.SetTextureScale(name3, value3);
								continue;
							}
							catch
							{
								break;
							}
						}
						if (a4 == "texRT")
						{
							r.ReadString();
							r.ReadString();
						}
					}
				}
				else if (a3 == "col")
				{
					value4.r = r.ReadSingle();
					value4.g = r.ReadSingle();
					value4.b = r.ReadSingle();
					value4.a = r.ReadSingle();
					material2.SetColor(name3, value4);
				}
				else if (a3 == "vec")
				{
					value5.x = r.ReadSingle();
					value5.y = r.ReadSingle();
					value5.z = r.ReadSingle();
					value5.w = r.ReadSingle();
					material2.SetVector(name3, value5);
				}
				else if (a3 == "f")
				{
					float value7 = r.ReadSingle();
					material2.SetFloat(name3, value7);
				}
			}
		}
		return material2;
	}

	// Token: 0x0400000F RID: 15
	private static byte[] m_skinTempFile = null;

	// Token: 0x04000010 RID: 16
	private static Dictionary<int, KeyValuePair<string, float>> m_hashPriorityMaterials = null;

	// Token: 0x04000011 RID: 17
	private static Dictionary<string, Material> m_dicCacheMaterial = new Dictionary<string, Material>();

	// Token: 0x04000012 RID: 18
	private static byte[] m_matTempFile = null;

	// Token: 0x04000013 RID: 19
	private static string[] properties = new string[]
	{
		"m_LocalRotation.x",
		"m_LocalRotation.y",
		"m_LocalRotation.z",
		"m_LocalRotation.w",
		"m_LocalPosition.x",
		"m_LocalPosition.y",
		"m_LocalPosition.z"
	};

	// Token: 0x04000014 RID: 20
	public static byte[] m_aniTempFile = null;
}
