package com.cube.main;

import com.badlogic.gdx.ApplicationListener;
import com.badlogic.gdx.Game;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.assets.AssetManager;

public class CubeGame extends Game implements ApplicationListener {
	
	public static AssetManager AssetManager = new AssetManager();
	private LoadingScreen _loadingScreen;
	
	@Override
	public void create() {	
		_loadingScreen = new LoadingScreen(this);
		setScreen(_loadingScreen);
	}

	@Override
	public void dispose() {
		_loadingScreen.dispose();
		AssetManager.dispose();
	}
}
