package com.cube.graphics;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

import com.badlogic.gdx.Gdx;
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
	private Random rnd = new Random();
	private BitmapFont font;
	private Stage _parentStage;

	private byte[][] _screenBuffer = new byte[72][72];
	private List<Animation> _animations = new ArrayList<Animation>();
	private float _totalTime = 0;

	public PixelMatrixRenderer(Stage stage) {
		_parentStage = stage;
		shapeRenderer = new ShapeRenderer();
		font = new BitmapFont();
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
			for (int x = 0; x < frameWidth; x++) {
				for (int y = 0; y < frameHeight; y++) {
					// TODO: array size check
					_screenBuffer[x][y] = frame.get_data()[x][y];
				}
			}
		}

		_totalTime += delta;
		super.act(delta);
	}

	@Override
	public void draw(SpriteBatch batch, float parentAlpha) {
		shapeRenderer.begin(ShapeType.Filled);
		for (int i = 0; i < 72; i++) {
			for (int j = 0; j < 72; j++) {
				byte currentPixelValue = _screenBuffer[i][j];

				if (currentPixelValue == 0) {
					shapeRenderer.setColor(116 / 255f, 129 / 255f, 107 / 255f,
							1);
					shapeRenderer.rect(i * 10, j * 10, 8, 8);
					continue;
				}

				shapeRenderer.setColor(53 / 255f, 53 / 255f, 53 / 255f,
						currentPixelValue / 255f);
				shapeRenderer.rect(i * 10, j * 10, 8, 8);
			}
		}
		shapeRenderer.end();
		font.draw(batch, Integer.toString(Gdx.graphics.getFramesPerSecond()),
				10, 20);
	}

	public List<Animation> get_animations() {
		return _animations;
	}

	public void set_animations(List<Animation> _animations) {
		this._animations = _animations;
	}
}
