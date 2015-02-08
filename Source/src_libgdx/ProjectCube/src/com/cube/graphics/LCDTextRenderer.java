package com.cube.graphics;

import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.scenes.scene2d.Actor;
import com.cube.common.VirtualScreen;
import com.cube.main.CubeGame;

public class LCDTextRenderer extends Actor {

	BitmapFont _font;
    CharSequence _str = "#############";
    CharSequence _str2 = "#Hello World#";
    public boolean DirectionLeftToRight = true;
	
	public LCDTextRenderer() {
		_font = CubeGame.AssetManager.get("data/segment_14.fnt", BitmapFont.class);
		setHeight(_font.getWrappedBounds(_str, VirtualScreen.GetRealWidth(720)).height);
		setWidth(_font.getWrappedBounds(_str, VirtualScreen.GetRealWidth(720)).width);
	}
	
	@Override
	public void draw(SpriteBatch batch, float parentAlpha) {
		_font.setColor(116 / 255f, 129 / 255f, 107 / 255f, 0.5f);
		_font.draw(batch, _str, getX(), getY() + getHeight());
		_font.setColor(53 / 255f, 53 / 255f, 53 / 255f, 255 / 255f);
		_font.draw(batch, _str2, getX(), getY() + getHeight());
	}
	
	
	float _totalTime = 0;
	@Override
	public void act(float delta) {
		_totalTime +=delta*100;
		if(_totalTime > 20)
		{
			StringBuilder sb = new StringBuilder();
			if(DirectionLeftToRight)
			{				
				sb.append(_str2.charAt(_str2.length() - 1));				
				for(int i = 0;i<_str2.length() - 1;i++)
				{
					sb.append(_str2.charAt(i));
				}
			}
			else
			{
				char first = _str2.charAt(0);
				for(int i = 1;i<_str2.length();i++)
				{
					sb.append(_str2.charAt(i));
				}
				sb.append(first);
			}
			_str2 = sb.toString();
			_totalTime = 0;
		}		
	}
}
