package com.cube.graphics;

import com.badlogic.gdx.graphics.Texture;
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
		
		//_font.scale(3);
	}
	
	@Override
	public void draw(SpriteBatch batch, float parentAlpha) {
		batch.begin();
		_font.setColor(116 / 255f, 129 / 255f, 107 / 255f, 0.5f);
		_font.draw(batch, str, 22, 800);
		_font.setColor(53 / 255f, 53 / 255f, 53 / 255f, 255 / 255f);
		_font.draw(batch, str2, 22, 800);

	}
	
}
