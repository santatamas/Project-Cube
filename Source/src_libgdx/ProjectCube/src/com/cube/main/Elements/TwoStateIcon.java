package com.cube.main.Elements;

import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.scenes.scene2d.Actor;
import com.cube.main.CubeGame;

public class TwoStateIcon extends Actor {
	Texture _txStateA;
	Texture _txStateB;
	Texture _currentState;
	float _timeSum = 0;
	boolean _stateFlag = true;
	
	public TwoStateIcon(String stateA, String stateB)
	{
		_txStateA = CubeGame.AssetManager.get(stateA, Texture.class);
		_txStateB = CubeGame.AssetManager.get(stateB, Texture.class);
		setHeight(74);
		setWidth(74);
		_currentState = _txStateA;
	}
	
	@Override
	public void act(float delta) {
		_timeSum += delta;
		
		if(_timeSum > 1)
		{
			_timeSum = 0;
			_currentState = _stateFlag ? _txStateA : _txStateB;
			_stateFlag = !_stateFlag;
		}
	}
	
	@Override
	public void draw(SpriteBatch batch, float parentAlpha) {
		// TODO Auto-generated method stub
		batch.draw(_currentState, getX(),getY(),74,74);
	}
}
