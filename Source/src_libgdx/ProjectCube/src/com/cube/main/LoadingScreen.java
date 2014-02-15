package com.cube.main;

import com.badlogic.gdx.Game;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;

public class LoadingScreen implements Screen {

	private GameScreen _gameScreen;
	private SpriteBatch batch;
	private BitmapFont font;
	
	Game parentGame;
	
	public LoadingScreen(Game parent)
	{
		parentGame = parent;
		batch = new SpriteBatch();
		font = new BitmapFont();
		LoadPrimaryAssets();		
	}
	
	private void LoadPrimaryAssets() {
		CubeGame.AssetManager.load("data/eat_icon_off.png", Texture.class);
		CubeGame.AssetManager.load("data/eat_icon_on.png", Texture.class);
	}
	
	@Override
	public void render(float delta) {
		 if(CubeGame.AssetManager.update()) {
			 if(_gameScreen == null)
			 {
				 _gameScreen = new GameScreen();
				 parentGame.setScreen(_gameScreen);	
			 }
			return;
	      }
	
	      // display loading information
	      float progress = CubeGame.AssetManager.getProgress();
	      batch.begin();
	      	font.draw(batch, Integer.toString((int)(progress * 100)), Gdx.graphics.getWidth() / 2, Gdx.graphics.getHeight() / 2);	
	      batch.end(); 
	}

	@Override
	public void resize(int width, int height) {
		// TODO Auto-generated method stub

	}

	@Override
	public void show() {
		// TODO Auto-generated method stub

	}

	@Override
	public void hide() {
		// TODO Auto-generated method stub

	}

	@Override
	public void pause() {
		// TODO Auto-generated method stub

	}

	@Override
	public void resume() {
		// TODO Auto-generated method stub

	}

	@Override
	public void dispose() {
		_gameScreen.dispose();

	}

}
