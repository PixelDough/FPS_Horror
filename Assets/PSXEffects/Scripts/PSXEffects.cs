using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PSXEffects : MonoBehaviour {

	public enum DitherType {
		x2,
		x3,
		x4,
		x8
	};

	public int resolutionVert = Screen.height;
	public int resolutionFactor = 1;
	public RawImage imgTarget;
	public int limitFramerate = -1;
	public bool affineMapping = true;
	public float polygonalDrawDistance = 0f;
	public int vertexInaccuracy = 30;
	public int polygonInaccuracy = 10;
	public int colorDepth = 32;
	public bool scanlines = false;
	public int scanlineIntensity = 5;
	public Texture2D ditherTexture;
	public bool dithering = true;
	public float ditherThreshold = 1;
	public int ditherIntensity = 5;
	public int maxDarkness = 20;
	public int subtractFade = 0;
	public bool skyboxLighting = false;
	public float favorRed = 1.0f;
	public bool worldSpaceSnapping = false;
	public bool postProcessing = true;
	public bool verticalScanlines = true;
	public float shadowIntensity = 0.5f;
	public bool downscale = false;
	public bool useCanvas = true;
	public bool autoAdd = false;

	private Camera cam;
	private Material colorDepthMat;
	private int prevResFactor;
	private Vector2 screenRes;

	void Awake() {
		if (Application.isPlaying) {
			QualitySettings.vSyncCount = 0;
		}

		if (imgTarget != null && postProcessing) {
			CreateNewRendTexture();
		}

		prevResFactor = resolutionVert / resolutionFactor;
		screenRes = new Vector2(Screen.width, Screen.height);
	}

	void Update() {
		Shader.SetGlobalFloat("_AffineMapping", affineMapping ? 1.0f : 0.0f);
		Shader.SetGlobalFloat("_DrawDistance", polygonalDrawDistance);
		int val = Mathf.CeilToInt(Screen.height / (resolutionVert / resolutionFactor));
		if (val == 0 || useCanvas) val = 1;
		Shader.SetGlobalInt("_VertexSnappingDetail", vertexInaccuracy / val / 2);
		Shader.SetGlobalInt("_Offset", polygonInaccuracy);
		Shader.SetGlobalFloat("_DarkMax", (float)maxDarkness / 100);
		Shader.SetGlobalFloat("_SubtractFade", (float)subtractFade / 100);
		Shader.SetGlobalFloat("_SkyboxLighting", skyboxLighting ? 1.0f : 0.0f);
		Shader.SetGlobalFloat("_WorldSpace", worldSpaceSnapping ? 1.0f : 0.0f);

		if (postProcessing) {
			if (colorDepthMat == null) {
				colorDepthMat = new Material(Shader.Find("Hidden/PS1ColorDepth"));
			} else {
				if (useCanvas) {
					imgTarget.gameObject.SetActive(true);
					colorDepthMat.SetFloat("_CamDS", 0);
				} else {
					imgTarget.gameObject.SetActive(false);
					colorDepthMat.SetFloat("_CamDS", resolutionVert / resolutionFactor);
				}

				colorDepthMat.SetFloat("_ColorDepth", colorDepth);
				colorDepthMat.SetFloat("_Scanlines", scanlines ? 1 : 0);
				colorDepthMat.SetFloat("_ScanlineIntensity", (float)scanlineIntensity / 100);
				colorDepthMat.SetTexture("_DitherTex", ditherTexture);
				colorDepthMat.SetFloat("_Dithering", dithering ? 1 : 0);
				colorDepthMat.SetFloat("_DitherThreshold", ditherThreshold);
				colorDepthMat.SetFloat("_DitherIntensity", (float)ditherIntensity / 100);
				colorDepthMat.SetFloat("_FavorRed", favorRed);
				colorDepthMat.SetFloat("_SLDirection", verticalScanlines ? 1 : 0);
			}

			if (!downscale) {
				resolutionVert = Screen.height;
			}

			if (prevResFactor != resolutionVert / resolutionFactor) {
				prevResFactor = resolutionVert / resolutionFactor;
				CreateNewRendTexture();
			}

			if (screenRes.x != Screen.width || screenRes.y != Screen.height) {
				screenRes = new Vector2(Screen.width, Screen.height);
				CreateNewRendTexture();
			}
		} else {
			imgTarget.gameObject.SetActive(false);
		}

		if (limitFramerate > 0) {
			Application.targetFrameRate = limitFramerate;
		} else {
			Application.targetFrameRate = -1;
		}

		if (Application.isEditor && !Application.isPlaying && autoAdd) {
			//AddShaderToBuild("Hidden/PS1ColorDepth");
			//AddShaderToBuild("Custom/PS1Shader");
		}
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination) {
		if (!useCanvas) {
			cam = GetComponent<Camera>();
			cam.targetTexture = null;
		}

		if (postProcessing) {
			Graphics.Blit(source, destination, colorDepthMat);
		}
	}

	void CreateNewRendTexture() {
		cam = GetComponent<Camera>();
		if (useCanvas) {
			int resolution = resolutionVert / resolutionFactor;

			if (resolution > 0) {
				cam.targetTexture = new RenderTexture((int)(resolution * ((float)Screen.width / (float)Screen.height)), resolution, 32, RenderTextureFormat.ARGB32);
				cam.targetTexture.filterMode = FilterMode.Point;

				imgTarget.texture = cam.targetTexture;
			}
		} else {
			cam.targetTexture = null;
		}
	}

	/*void AddShaderToBuild(string shaderName) {
		Shader shader = Shader.Find(shaderName);

		GraphicsSettings graphics = AssetDatabase.LoadAssetAtPath<GraphicsSettings>("ProjectSettings/GraphicsSettings.asset");
		SerializedObject ser = new SerializedObject(graphics);
		SerializedProperty prop = ser.FindProperty("m_AlwaysIncludedShaders");
		bool shaderIncluded = false;
		for (int i = 0; i < prop.arraySize; i++) {
			SerializedProperty selShader = prop.GetArrayElementAtIndex(i);
			if (shader == selShader.objectReferenceValue) {
				shaderIncluded = true;
				break;
			}
		}

		if (!shaderIncluded) {
			int ind = prop.arraySize;
			prop.InsertArrayElementAtIndex(ind);
			SerializedProperty selShader = prop.GetArrayElementAtIndex(ind);
			selShader.objectReferenceValue = shader;
			ser.ApplyModifiedProperties();
			AssetDatabase.SaveAssets();
		}
	}*/
}
