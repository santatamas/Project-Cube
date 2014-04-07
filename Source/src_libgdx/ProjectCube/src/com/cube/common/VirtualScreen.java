package com.cube.common;

public class VirtualScreen {
	public static float PixelWidthRatio;
    public static float PixelHeightRatio;
    
    public static float ReferenceScreenWidth = 720;
    public static float ReferenceScreenHeight = 1280;
    
    public static float ScreenWidth;
    public static float ScreenHeight;
    
    public static float GetRealWidth(float virtualWidth)
    {
    	return virtualWidth * PixelWidthRatio;
    }
    
    public static float GetRealHeight(float virtualHeight)
    {
    	return virtualHeight * PixelHeightRatio;
    }
}
