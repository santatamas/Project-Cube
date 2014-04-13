package com.cube.common;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input.Keys;
import com.badlogic.gdx.InputProcessorQueue;

public class GameInputProcessor extends InputProcessorQueue {
	@Override
    public boolean keyDown(int keycode) {

        if ((keycode == Keys.ESCAPE) || (keycode == Keys.BACK) )

        // Maybe perform other operations before exiting
        Gdx.app.exit();
        return false;
    }
}
