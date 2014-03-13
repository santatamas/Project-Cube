package com.cube.main.Elements;

import java.awt.Point;
import java.util.ArrayList;
import java.util.List;

import com.cube.data.Animation;

public class PixelActor {

	Point _location = new Point();
	List<Animation> _characterAnimations = new ArrayList<Animation>();
	Animation _currentAnimation;
	
	public void act(float delta) {
		if(_currentAnimation == null) return;
		
		_currentAnimation.act(delta);
	}
	
	public int[][] getCurrentFrame() {
		return _currentAnimation == null ? null :_currentAnimation.getCurrentFrame().get_data();
	}	
}
