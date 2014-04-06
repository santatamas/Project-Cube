package com.cube.main.Elements;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.g2d.TextureAtlas;
import com.badlogic.gdx.scenes.scene2d.InputEvent;
import com.badlogic.gdx.scenes.scene2d.InputListener;
import com.badlogic.gdx.scenes.scene2d.ui.Button;
import com.badlogic.gdx.scenes.scene2d.ui.Skin;
import com.cube.main.CubeGame;

public class RoundButton extends Button {

	TextureAtlas _atlas;
	Skin _skin;
	ButtonStyle _style;
	
	public RoundButton() {
		_atlas = CubeGame.AssetManager.get("data/button_round.atlas", TextureAtlas.class);
		_skin = new Skin();
        _skin.addRegions(_atlas);
        
        _style = new ButtonStyle();
        _style.up = _skin.getDrawable("button_round_normal");
        _style.down = _skin.getDrawable("button_round_pressed");
        
        setStyle(_style);
        setHeight(116);
        setWidth(116);
	}
	
//	_buttonRound.addListener(new InputListener() {
//        public boolean touchDown (InputEvent event, float x, float y, int pointer, int button) {
//                Gdx.app.log("my app", "Pressed"); //** Usually used to start Game, etc. **//
//                return true;
//        }
//                public void touchUp (InputEvent event, float x, float y, int pointer, int button) {
//                    Gdx.app.log("my app", "Released");
//            }
//        });
	
}
