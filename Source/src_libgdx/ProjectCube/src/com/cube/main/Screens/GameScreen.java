package com.cube.main.Screens;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.assets.AssetManager;
import com.badlogic.gdx.audio.Music;
import com.badlogic.gdx.audio.Sound;
import com.badlogic.gdx.graphics.GL10;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.input.GestureDetector;
import com.badlogic.gdx.input.GestureDetector.GestureListener;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.cube.data.Animation;
import com.cube.graphics.PixelMatrixRenderer;
import com.cube.main.CubeGame;
import com.cube.main.Elements.TwoStateIcon;

public class GameScreen implements Screen, GestureListener {

	Stage _stage;
	PixelMatrixRenderer _matrixRenderer;
	TwoStateIcon _eatIcon;
	float _width;
	float _height;
	boolean _rendering = true;
	Sound _switchSound;
	Music _bgMusic;
	
	
	public GameScreen()
	{
		_width = Gdx.graphics.getWidth();
		_height = Gdx.graphics.getHeight();
		_stage = new Stage();
		_stage.setCamera(new OrthographicCamera(_width, _height));
		_matrixRenderer = new PixelMatrixRenderer(_stage);
		_eatIcon = new TwoStateIcon("data/eat_icon_off.png", "data/eat_icon_on.png");
		_stage.addActor(_matrixRenderer);	
		_stage.addActor(_eatIcon);
		_switchSound = CubeGame.AssetManager.get("data/switch_effect.wav");
		_matrixRenderer.get_animations().add((Animation) CubeGame.AssetManager.get("data/demo_animation.pma"));
		_bgMusic = Gdx.audio.newMusic(Gdx.files.internal("data/bg_music.mp3"));
		_bgMusic.play();
	}
	
	public void resize(int width, int height) {
	}

	@Override
	public void render(float delta) {
		if(!_rendering) return;
		
		Gdx.gl.glClearColor(125/255f, 140/255f, 115/255f, 1);
		Gdx.gl.glClear(GL10.GL_COLOR_BUFFER_BIT);
		_stage.act(delta);
		_stage.draw();
	}

	@Override
	public void show() {
		Gdx.input.setInputProcessor(new GestureDetector(this));
	}

	@Override 
    public void hide() {
    	Gdx.input.setInputProcessor(null);
    }

	@Override
	public boolean touchDown(float x, float y, int pointer, int button) {
		_rendering = !_rendering;
		_switchSound.play();
		if(_rendering)
		{
			_bgMusic.play();
		}else
		{
			_bgMusic.pause();
		}
		return true;
	}

	@Override
	public boolean tap(float x, float y, int count, int button) {
		return false;
	}

	@Override
	public boolean longPress(float x, float y) {
		return false;
	}

	@Override
	public boolean fling(float velocityX, float velocityY, int button) {
		return false;
	}

	@Override
	public boolean pan(float x, float y, float deltaX, float deltaY) {
		return false;
	}

	@Override
	public boolean panStop(float x, float y, int pointer, int button) {
		return false;
	}

	@Override
	public boolean zoom(float initialDistance, float distance) {
		return false;
	}

	@Override
	public boolean pinch(Vector2 initialPointer1, Vector2 initialPointer2,
			Vector2 pointer1, Vector2 pointer2) {
		return false;
	}

	@Override
	public void pause() {
	}

	@Override
	public void resume() {
	}

	@Override
	public void dispose() {
	}

}
