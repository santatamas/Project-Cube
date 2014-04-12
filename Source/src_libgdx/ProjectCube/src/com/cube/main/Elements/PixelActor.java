package com.cube.main.Elements;

import java.util.ArrayList;
import java.util.List;

import com.cube.data.Animation;
import com.cube.graphics.Point;

public abstract class PixelActor {

	public PixelActor()
	{
		LoadAnimations();
	}
	
	//private Point _location = new Point(0,0);
	public int X = 0;
	public int Y = 0;
	protected List<Animation> _animations = new ArrayList<Animation>();
	Animation _currentAnimation;
	
	public abstract void LoadAnimations();
		
	public void act(float delta) {
		if(_currentAnimation == null) return;
		
		_currentAnimation.act(delta);
	}
	
	public int[][] getCurrentFrame() {
		return _currentAnimation == null ? null :_currentAnimation.getCurrentFrame().get_data();
	}

	public int get_width() {
		if(_currentAnimation == null) return 0;
		return _currentAnimation.getCurrentFrame().get_width();
	}	
	public int get_height() {
		if(_currentAnimation == null) return 0;
		return _currentAnimation.getCurrentFrame().get_height();
	}
}
