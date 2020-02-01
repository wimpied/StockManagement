using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Use StandardUtils.cs together with this script.
/// </summary>
public class FadeChildren : MonoBehaviour {

	public AnimationCurve FadeInAnimation;
	public AnimationCurve FadeOutAnimation;
	public float AnimationTime;

	private List<Material> mats = new List<Material>();
	private List<Color> colors = new List<Color>();
	private float t =1;
	private bool fadeIn;
	
	void Start () {
		foreach (Renderer ren in gameObject.GetComponentsInChildren<Renderer>())
		{
			foreach(Material m in ren.materials)
			{
				mats.Add(m);
				colors.Add(m.color);
			}
		} 
	}
	
	
	void Update () {
		if(t>=1)
			return;
			
			
		t = Mathf.Min(1, t+Time.deltaTime/AnimationTime);
				
		float eval = fadeIn?FadeInAnimation.Evaluate(t):FadeOutAnimation.Evaluate(t);
		setFade(eval);
		
		if(t==1 && fadeIn)
			switchToOpaque();
	}
	
	private void setFade(float Alpha)
	{
		for(int i = 0; i < mats.Count; i++)
		{
			Color c = colors[i];
			c.a = Alpha;
			mats[i].color = c;
		}
	}
	
	public void FadeIn()
	{
		t=0;
		fadeIn = true;
	}
	
	public void FadeOut()
	{
		switchToFade();
		t=0;
		fadeIn = false;
	}
	
	private void switchToFade()
	{
		switchShaderMode(StandardShaderUtils.BlendMode.Fade);
	}
	private void switchToOpaque()
	{		
		switchShaderMode(StandardShaderUtils.BlendMode.Opaque);		
	}
	
	private void switchShaderMode(StandardShaderUtils.BlendMode newMode)
	{
		foreach(Material m in mats)
			StandardShaderUtils.ChangeRenderMode(m,newMode);				
	}
	
	void OnDisable()
	{
		setFade(1);
		switchToOpaque();
	}
}
