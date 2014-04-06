package com.cube.main.Screens;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.audio.Music;
import com.badlogic.gdx.audio.Sound;
import com.badlogic.gdx.graphics.GL10;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.TextureRegion;
import com.badlogic.gdx.input.GestureDetector.GestureListener;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.Table;
import com.badlogic.gdx.scenes.scene2d.utils.TextureRegionDrawable;
import com.cube.data.Animation;
import com.cube.graphics.LCDTextRenderer;
import com.cube.graphics.PixelMatrixRenderer;
import com.cube.main.CubeGame;
import com.cube.main.Elements.RectButton;
import com.cube.main.Elements.RoundButton;
import com.cube.main.Elements.TwoStateIcon;

public class GameScreen implements Screen, GestureListener {

	Stage _stage;
	PixelMatrixRenderer _matrixRenderer;
	TwoStateIcon _eatIcon;
	LCDTextRenderer _textRenderer;
	float _width;
	float _height;
	boolean _rendering = true;
	Sound _switchSound;
	Music _bgMusic;

    RoundButton _buttonA; 
    RoundButton _buttonB; 
    RectButton _buttonStart; 
    RectButton _buttonSelect;
    
    Table _layoutRoot;
    Table _iconLayout;
    Table _controlLayout;
	
	public GameScreen()
	{
		_width = Gdx.graphics.getWidth();
		_height = Gdx.graphics.getHeight();
		_stage = new Stage(_width, _height, true);
		Gdx.input.setInputProcessor(_stage);

		LoadResources();
		InitializeActors(_stage);
		InitializeLayout(_stage);
	}
	

	private void LoadResources() {
		_switchSound = CubeGame.AssetManager.get("data/switch_effect.wav");
		//_bgMusic = Gdx.audio.newMusic(Gdx.files.internal("data/bg_music.mp3"));
		//_bgMusic.play();
	}

	private void InitializeActors(Stage stage) {
		_matrixRenderer = new PixelMatrixRenderer(_stage);
		_matrixRenderer.get_animations().add((Animation) CubeGame.AssetManager.get("data/zippedAnim.pmz"));
		
		_textRenderer = new LCDTextRenderer();
		_eatIcon = new TwoStateIcon("data/cake_icon_off.png", "data/cake_icon_on.png");
		_buttonA = new RoundButton();
		_buttonB = new RoundButton();
		_buttonStart = new RectButton();
		_buttonSelect = new RectButton();
	}

	private void InitializeLayout(Stage stage) {
		_layoutRoot = new Table();

		_layoutRoot.debug();
		_layoutRoot.setFillParent(true);
		_layoutRoot.top();	
		_layoutRoot.row().padTop(20).padBottom(20);
		
		for(int i = 0;i<5;i++) {
			TwoStateIcon icon = new TwoStateIcon("data/cake_icon_off.png", "data/cake_icon_on.png");
			_layoutRoot.add(icon).expandX();
		}
		_layoutRoot.row().padBottom(20);
		_layoutRoot.add(_matrixRenderer).colspan(5);
		_layoutRoot.row().padBottom(15);
		_layoutRoot.add(_textRenderer).colspan(5);
		_layoutRoot.row();
		_layoutRoot.add(new LCDTextRenderer()).colspan(5);
		_layoutRoot.row();
		
		_controlLayout = new Table();
		_controlLayout.setHeight(260);
		_controlLayout.setWidth(720);
		_controlLayout.debug();
		_controlLayout.setFillParent(false);
		_controlLayout.setBackground(new TextureRegionDrawable(new TextureRegion(CubeGame.AssetManager.get("data/buttons_bg.png", Texture.class))));
		_controlLayout.row().expandX();
		_controlLayout.add(_buttonSelect).padLeft(25);
		_controlLayout.add(_buttonStart).padRight(35);
		_controlLayout.add(_buttonB);
		_controlLayout.add(_buttonA).padBottom(70);
		
		_layoutRoot.addActor(_controlLayout);
		
		stage.addActor(_layoutRoot);		
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
		Table.drawDebug(_stage);
	}

	@Override
	public void show() {
		//Gdx.input.setInputProcessor(new GestureDetector(this));
	}

	@Override 
    public void hide() {
    	Gdx.input.setInputProcessor(null);
    }

	@Override
	public boolean touchDown(float x, float y, int pointer, int button) {
		_rendering = !_rendering;
		_switchSound.play();
		/*if(_rendering)
		{
			_bgMusic.play();
		}else
		{
			_bgMusic.pause();
		}*/
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
