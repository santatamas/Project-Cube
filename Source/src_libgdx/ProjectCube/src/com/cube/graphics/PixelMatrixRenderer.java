package com.cube.graphics;

import java.util.ArrayList;
import java.util.List;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.GL10;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer;
import com.badlogic.gdx.graphics.glutils.ShapeRenderer.ShapeType;
import com.badlogic.gdx.scenes.scene2d.Actor;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.cube.data.Animation;
import com.cube.data.Frame;

public class PixelMatrixRenderer extends Actor {

	private ShapeRenderer shapeRenderer;
	private BitmapFont font;
	private Stage _parentStage;

	private int[][] _screenBuffer = new int[72][72];
	private List<Animation> _animations = new ArrayList<Animation>();
	private float _totalTime = 0;

	public PixelMatrixRenderer(Stage stage) {
		_parentStage = stage;
		shapeRenderer = new ShapeRenderer();
		font = new BitmapFont();
		setHeight(720);
		setWidth(720);
	}

	@Override
	public void act(float delta) {

		// do first time load to buffer
		for (int i = 0; i < _animations.size(); i++) {
			Animation anim = _animations.get(i);

			// update animation state
			anim.act(delta);

			// get current frame and update buffer
			Frame frame = anim.getCurrentFrame();
			short frameWidth = frame.get_width();
			short frameHeight = frame.get_height();
			int[][] frameData = frame.get_data();
			for (int x = 0; x < frameWidth; x++) {
				for (int y = 0; y < frameHeight; y++) {
					// TODO: array size check
					_screenBuffer[x][y] = frameData[x][y];
				}
			}
		}

		_totalTime += delta;
		super.act(delta);
	}

	@Override
	public void draw(SpriteBatch batch, float parentAlpha) {
		batch.end();
		Gdx.gl.glEnable(GL10.GL_BLEND);
		Gdx.gl.glBlendFunc(GL10.GL_SRC_ALPHA, GL10.GL_ONE_MINUS_SRC_ALPHA);
		shapeRenderer.begin(ShapeType.Filled);

		for (int i = 0; i < 72; i++) {
			for (int j = 0; j < 72; j++) {
				int currentPixelValue = _screenBuffer[i][71 - j];

				if (currentPixelValue == 0) {
					shapeRenderer.setColor(116 / 255f, 129 / 255f, 107 / 255f, 0.3f);
					shapeRenderer.rect(getX() + i * 10, getY() + j * 10, 8, 8);
					continue;
				}

				shapeRenderer.setColor(53 / 255f, 53 / 255f, 53 / 255f, currentPixelValue / 255f);
				shapeRenderer.rect(getX() + i * 10, getY() + j * 10, 8, 8);
			}
		}

		shapeRenderer.end();
		Gdx.gl.glDisable(GL10.GL_BLEND);
		batch.begin();
		font.draw(batch, Integer.toString(Gdx.graphics.getFramesPerSecond()), 10, 20);
		//batch.end();

	}

	public List<Animation> get_animations() {
		return _animations;
	}

	public void set_animations(List<Animation> _animations) {
		this._animations = _animations;
	}
}
