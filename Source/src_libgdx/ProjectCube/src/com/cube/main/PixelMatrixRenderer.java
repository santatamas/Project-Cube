package com.cube.main;

import java.util.Random;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer.ShapeType;
import com.badlogic.gdx.scenes.scene2d.Actor;
import com.badlogic.gdx.scenes.scene2d.Stage;

public class PixelMatrixRenderer extends Actor {

	private ShapeRenderer shapeRenderer;
	private Random rnd = new Random();
	private BitmapFont font;
	private Stage _parentStage;
	
	public PixelMatrixRenderer(Stage stage)
	{
		_parentStage = stage;
		shapeRenderer = new ShapeRenderer();	
		font = new BitmapFont();
	}
	
	@Override
	public void draw(SpriteBatch batch, float parentAlpha) {
		shapeRenderer.begin(ShapeType.Filled);			
			for(int i = 0;i<72;i++)
	        {
	        	for(int j = 0;j<72;j++)
	            {
	        		if(rnd.nextBoolean())
	        		{
	        			shapeRenderer.setColor(116/255f, 129/255f, 107/255f, 1);
	        			shapeRenderer.rect(i * 10, j * 10, 8, 8);
	        			continue;
	        		}
	        		
	        		shapeRenderer.setColor(53/255f, 53/255f, 53/255f, 1);
	        		shapeRenderer.rect(i * 10, j * 10, 8, 8);
	            }       	
	        }
		shapeRenderer.end();	
		font.draw(batch, Integer.toString(Gdx.graphics.getFramesPerSecond()), 10, 20);		
	}	
}
