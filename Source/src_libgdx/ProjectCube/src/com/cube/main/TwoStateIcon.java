package com.cube.main;

import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.scenes.scene2d.Actor;

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
		batch.draw(_currentState, 10,800);
	}
}
