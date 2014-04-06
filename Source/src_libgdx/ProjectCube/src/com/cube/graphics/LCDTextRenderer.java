package com.cube.graphics;

import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.scenes.scene2d.Actor;
import com.cube.main.CubeGame;

public class LCDTextRenderer extends Actor {

	BitmapFont _font;
	CharSequence str = "#############";
	CharSequence str2 = "#Hello World#";
	
	public LCDTextRenderer() {
		_font = CubeGame.AssetManager.get("data/segment_14.fnt", BitmapFont.class);
		setHeight(_font.getWrappedBounds(str, 720).height);
		setWidth(_font.getWrappedBounds(str, 720).width);
	}
	
	@Override
	public void draw(SpriteBatch batch, float parentAlpha) {
		_font.setColor(116 / 255f, 129 / 255f, 107 / 255f, 0.5f);
		_font.draw(batch, str, getX(), getY() + getHeight());
		_font.setColor(53 / 255f, 53 / 255f, 53 / 255f, 255 / 255f);
		_font.draw(batch, str2, getX(), getY() + getHeight());

	}
	
}
