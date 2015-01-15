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
    
    var IsInverted: Bool = false
    private let _width: Int = 50
    private let _height: Int = 50
    private var _screenBuffer : [[Int]] = [[Int]](count: 50, repeatedValue: [Int](count: 50, repeatedValue: 0))
    private var _actors: Array<Actor> = [
        Ball(location: Point(x: 5, y: 10)),
        Ball(location: Point(x: 0, y: 0)),
        Ball(location: Point(x: 20, y: 12))
    ]
    private var _totalDelta: Float = 0
    
    func Act(delta: Float) {
        if(_totalDelta >= 30000)
        {
            ResetBuffer()
            UpdateBalls(delta)
            RefreshScreenBuffer(delta)
            self.texture = RenderTexture()
            _totalDelta = 0;
        }
        _totalDelta += delta
    }
    
    func RenderTexture() -> SKTexture
    {
        UIGraphicsBeginImageContext(CGSizeMake(500, 500))
        var ctx:CGContextRef = UIGraphicsGetCurrentContext()
        
        for var i = 1; i < _width; i++ {
            for var j = 1; j < _height; j++ {
                
                var currentPixelValue:Int = Int(_screenBuffer[i][_height - 1 - j])
                if(IsInverted)
                {
                    currentPixelValue = 255 - currentPixelValue
                }
                
                if (currentPixelValue == 0)
                {
                    UIColor(red:116 / 255,green:129 / 255,blue:107 / 255,alpha:0.3).setFill()
                }
                else
                {
                    UIColor(red:53 / 255,green:53 / 255,blue:53 / 255,alpha: CGFloat(currentPixelValue) / 255).setFill()
                }
                CGContextFillRect(ctx, CGRectMake(CGFloat(i*7), CGFloat(j*7), 5, 5));
            }}
        
        var textureImage:UIImage = UIGraphicsGetImageFromCurrentImageContext()
        UIGraphicsEndImageContext()
        
        var texture:SKTexture = SKTexture(image: textureImage)
        texture.filteringMode = SKTextureFilteringMode.Nearest
        
        return texture
    }
    
    func GetRandomFloat() -> CGFloat{
        let arc4randoMax:Double = 0x100000000
        let upper = 255.0
        let lower = 0.0
        return CGFloat((Double(arc4random()) / arc4randoMax) * (upper - lower) + lower) / 255.0
    }
    
    func RefreshScreenBuffer(delta:Float) {
        var currentActorLocation: Point
        var actor: Actor
        
        // do first time load to buffer
        for var i = 0; i < _actors.count;i++
        {
            actor = _actors[i]
            
            // update animation state
            actor.Act(delta)
            
            // get current frame and update buffer
            var frame = actor.GetCurrentFrame()
            
            for var x = 0; x < frame.Width; x++
            {
                for var y = 0; y < frame.Height; y++
                {
                    if(x + actor.Location.X >= _width) {continue}
                    if(x + actor.Location.X < 0) {continue}
                    if(y + actor.Location.Y >= _height) {continue}
                    if(y + actor.Location.Y < 0) {continue}
                    
                    if(frame.Data[x][y] != 0)
                    {
                        _screenBuffer[x + actor.Location.X][y + actor.Location.Y] = frame.Data[x][y]
                    }
                }
            }
        }
    }
    
    func UpdateBalls(delta:Float) {

        var actor:Ball
        var loc:Point
            
        for var i = 0; i < _actors.count; i++
        {
            actor = _actors[i] as Ball
            actor.Location.X += actor.directionX
            actor.Location.Y += actor.directionY
                
            if(actor.Location.X == _width - actor.GetCurrentWidth())
            {
                actor.directionX = -1;
            }

            if(actor.Location.Y == _height - actor.GetCurrentHeight())
            {
                actor.directionY = -1;
            }

            if(actor.Location.X < 0)
            {
                actor.directionX = 1;
            }

            if(actor.Location.Y < 0)
            {
                actor.directionY = 1;
            }
        }
    }
    
    func ResetBuffer() {
        _screenBuffer = [[Int]](count: 72, repeatedValue: [Int](count: 72, repeatedValue: 0))
    }
}