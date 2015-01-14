//
//  PixelMatrixRenderer.swift
//  Project Cube
//
//  Created by Tamas Santa on 15/01/15.
//  Copyright (c) 2015 Tamas Santa. All rights reserved.
//

import Foundation
import SpriteKit

class PixelMatrixRenderer: SKSpriteNode {
    /*
    
    public static int Width = 72;
    public static int Height = 72;
    public boolean IsInverted = false;
    
    private ShapeRenderer shapeRenderer;
    
    private Stage _parentStage;
    
    private int[][] _screenBuffer = new int[Width][Height];
    private List<PixelActor> _actors = new ArrayList<PixelActor>();
    private float _totalTime = 0;
    private float _virtualPixelSize = 0;
    
    public PixelMatrixRenderer(Stage stage) {
    _parentStage = stage;
    shapeRenderer = new ShapeRenderer();
    setHeight(stage.getWidth());
    setWidth(stage.getWidth());
    
    _virtualPixelSize = stage.getWidth() / Width;
    }
    
    @Override
    public void act(float delta) {
    
    ResetBuffer();
    UpdateBalls(delta);
    
    Point currentActorLocation;
    PixelActor actor;
    // do first time load to buffer
    for (int i = 0; i < _actors.size(); i++) {
    actor = _actors.get(i);
    
    // update animation state
    actor.act(delta);
    // get current frame and update buffer
    int[][] frame = actor.getCurrentFrame();
    short frameWidth = (short) frame.length;
    short frameHeight = (short) frame[0].length;
    
    for (int x = 0; x < frameWidth; x++) {
				for (int y = 0; y < frameHeight; y++) {
    // TODO: array size check
    
    if(x + actor.X >= Width) continue;
    if(x + actor.X < 0) continue;
    if(y + actor.Y >= Height) continue;
    if(y + actor.Y < 0) continue;
    
    if(frame[x][y] != 0)
    _screenBuffer[x + actor.X][y + actor.Y] = frame[x][y];
				}
    }
    }
    
    _totalTime += delta;
    super.act(delta);
    }
    
    float _totalDelta = 0;
    Random _rnd = new Random();
    private void UpdateBalls(float delta) {
    _totalDelta += delta * 100;
    if(_totalDelta >= 10)
    {
    Ball actor;
    Point loc;
    for (int i = 0; i < _actors.size(); i++)
    {
				actor = (Ball)_actors.get(i);
				actor.X+= actor.directionX;
				actor.Y+= actor.directionY;
				
				if(actor.X == Width - actor.get_width())
    actor.directionX = -1;
				
				if(actor.Y == Height - actor.get_height())
    actor.directionY = -1;
				
				if(actor.X < 0)
    actor.directionX = 1;
				
				if(actor.Y < 0)
    actor.directionY = 1;
    }
    
    _totalDelta = 0;
    }
    }
    
    private void ResetBuffer() {
    for (int x = 0; x < Width; x++) {
    for (int y = 0; y < Height; y++) {
				_screenBuffer[x][y] = 0;
    }
    }
    
    }
    
    @Override
    public void draw(SpriteBatch batch, float parentAlpha) {
    batch.end();
    Gdx.gl.glEnable(GL10.GL_BLEND);
    Gdx.gl.glBlendFunc(GL10.GL_SRC_ALPHA, GL10.GL_ONE_MINUS_SRC_ALPHA);
    shapeRenderer.begin(ShapeType.Filled);
    
    for (int i = 0; i < Width; i++) {
    for (int j = 0; j < Height; j++) {
				int currentPixelValue = _screenBuffer[i][71 - j];
				if(IsInverted)
    currentPixelValue = 255 - currentPixelValue;
				
				
				if (currentPixelValue == 0) {
    shapeRenderer.setColor(116 / 255f, 129 / 255f, 107 / 255f, 0.3f);
    shapeRenderer.rect(getX() + VirtualScreen.GetRealWidth(i) * _virtualPixelSize, getY() + VirtualScreen.GetRealHeight(j)* _virtualPixelSize, _virtualPixelSize * 0.8f, _virtualPixelSize * 0.8f);
    continue;
				}
    
				shapeRenderer.setColor(53 / 255f, 53 / 255f, 53 / 255f, currentPixelValue / 255f);
				shapeRenderer.rect(getX() + VirtualScreen.GetRealWidth(i)* _virtualPixelSize, getY() + VirtualScreen.GetRealHeight(j)* _virtualPixelSize, _virtualPixelSize * 0.8f, _virtualPixelSize * 0.8f);
    }
    }
    
    shapeRenderer.end();
    Gdx.gl.glDisable(GL10.GL_BLEND);
    batch.begin();
    }
    
    public List<PixelActor> get_actors() {
    return _actors;
    }
    public void set_actors(List<PixelActor> actors) {
    this._actors = actors;
    }
    
    */
}