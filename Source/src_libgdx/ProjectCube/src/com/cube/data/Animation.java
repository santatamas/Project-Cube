package com.cube.data;

import java.util.ArrayList;
import java.util.List;
import com.cube.common.ColorDepth;

public class Animation {
	private ColorDepth _depth;
	private List<Frame> _frames = new ArrayList<Frame>();

	private float _elapsedTime = 0;
	private int _currentFrameIndex = 0;
	private short _currentFrameDuration = 0;

	public Animation() {

	}

	public List<Frame> get_frames() {
		return _frames;
	}

	void set_frames(List<Frame> frames) {
		this._frames = frames;
		_currentFrameDuration = frames.get(0).get_duration();
	}

	ColorDepth get_depth() {
		return _depth;
	}

	void set_depth(ColorDepth depth) {
		this._depth = depth;
	}

	public void act(float delta) {
		_elapsedTime += delta * 1000;
		if (_elapsedTime >= _currentFrameDuration) {
			_elapsedTime = 0;
		

		_currentFrameIndex = _currentFrameIndex < (_frames.size() - 1) ? (_currentFrameIndex + 1) : 0;
		_currentFrameDuration = _frames.get(_currentFrameIndex).get_duration();
		}
	}

	public Frame getCurrentFrame() {
		return _frames.get(_currentFrameIndex);
	}
}
