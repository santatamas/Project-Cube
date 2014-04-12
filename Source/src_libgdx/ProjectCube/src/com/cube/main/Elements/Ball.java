package com.cube.main.Elements;

import com.cube.data.Animation;
import com.cube.main.CubeGame;

public class Ball extends PixelActor {

	public int directionX = 1;
	public int directionY = 1;
	
	@Override
	public void LoadAnimations() {
		Animation anim = CubeGame.AssetManager.get("data/ball.pmz", Animation.class);
		_currentAnimation = anim;
		_animations.add(anim);
	}

}
