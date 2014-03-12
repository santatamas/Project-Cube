package com.cube.data;

import com.cube.common.ColorDepth;

public class Frame {
	
	public Frame()
	{
		//TODO: do parameterized constructor
	}

	public byte get(short x, short y) {
		return _data[x][y];
	}
	
	public byte[][] get_data() {
		return _data;
	}
	public void set_data(byte[][] data){
		_data = data;
	}

	
	public ColorDepth get_depth() {
		return _depth;
	}

	void set_depth(ColorDepth _depth) {
		this._depth = _depth;
	}

	public short get_duration() {
		return _duration;
	}
	void set_duration(short _duration) {
		this._duration = _duration;
	}

	public short get_height() {
		return _height;
	}
	void set_height(short _height) {
		this._height = _height;
	}

	public short get_width() {
		return _width;
	}
	void set_width(short _width) {
		this._width = _width;
	}	

	private short _width;
	private short _height;
	private short _duration;
	private ColorDepth _depth; 	
	private byte[][] _data;
}
