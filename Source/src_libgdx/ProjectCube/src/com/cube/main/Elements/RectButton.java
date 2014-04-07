package com.cube.main.Elements;

import com.badlogic.gdx.graphics.g2d.TextureAtlas;
import com.badlogic.gdx.scenes.scene2d.ui.Button;
import com.badlogic.gdx.scenes.scene2d.ui.Skin;
import com.cube.common.VirtualScreen;
import com.cube.main.CubeGame;

public class RectButton extends Button {

	TextureAtlas _atlas;
	Skin _skin;
	ButtonStyle _style;
	
	public RectButton() {
		_atlas = CubeGame.AssetManager.get("data/button_rect.atlas", TextureAtlas.class);
		_skin = new Skin();
        _skin.addRegions(_atlas);
        
        _style = new ButtonStyle();
        _style.up = _skin.getDrawable("button_rect_normal");
        _style.down = _skin.getDrawable("button_rect_pressed");
        
        setStyle(_style);
        setHeight(VirtualScreen.GetRealHeight(28));
        setWidth(VirtualScreen.GetRealWidth(101));
	}
	
}
