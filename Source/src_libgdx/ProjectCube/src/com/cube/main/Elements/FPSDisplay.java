package com.cube.main.Elements;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.scenes.scene2d.Actor;

public class FPSDisplay extends Actor {

	private BitmapFont font = new BitmapFont();
	@Override
	public void draw(SpriteBatch batch, float parentAlpha) {
		// TODO Auto-generated method stub
		super.draw(batch, parentAlpha);
		font.draw(batch, Integer.toString(Gdx.graphics.getFramesPerSecond()), getX(), getY());
	}
	
}
