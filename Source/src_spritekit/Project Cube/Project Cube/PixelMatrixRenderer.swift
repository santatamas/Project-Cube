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
    private var _screenBuffer : [[PixelColor]] = [[PixelColor]](count: 50, repeatedValue: [PixelColor](count: 50, repeatedValue: PixelColor(alpha: 0, red: 0, green: 0, blue: 0)))
    private var _actors: Array<Actor> = []
    private var _totalDelta: Float = 0
    
    func AddActor(actor:Actor)
    {
        _actors.append(actor)
    }
    
    func Act(delta: Float) {
        if(_totalDelta >= 0.100) // frame limiter (calculation)
        {
            ResetBuffer()
            RefreshScreenBuffer(delta)
            self.texture = RenderTexture()
            _totalDelta = 0;
        }
        _totalDelta += delta
    }
    
    func RenderTexture() -> SKTexture
    {
        UIGraphicsBeginImageContext(CGSizeMake(self.frame.width, self.frame.height))
        var ctx:CGContextRef = UIGraphicsGetCurrentContext()
        
        
        // Calculate render parameters - should be separated to a one-time init
        var pixelSize: CGFloat = CGFloat((Int(self.frame.width) / _width)) // make sure we can round up to 1 pixelspace
        var spaceSize:CGFloat = 0
        var virtualPixelSize: CGFloat = pixelSize + spaceSize
        var offset: CGFloat = CGFloat(Int((self.frame.width - (virtualPixelSize * CGFloat(_width))) / 2))
        
        // #DEBUG - DRAW BACKGROUND
        //UIColor(red:255 / 255,green:255 / 255,blue:255 / 255,alpha:1).setFill()
        //CGContextFillRect(ctx, CGRectMake(0,0,self.frame.width, self.frame.height))
        // #END
        
        for var i = 0; i < _width; i++ {
            for var j = 0; j < _height; j++ {
                
                var currentPixelValue:PixelColor = _screenBuffer[i][_height - 1 - j]
                //if(IsInverted)
                //{
                //    currentPixelValue = 255 - currentPixelValue
                //}
                if(currentPixelValue.alpha == 0)
                {
                    UIColor(red:1, green:1, blue:1, alpha: 0.2).setFill()
                }
                else
                {
                    UIColor(red: CGFloat(currentPixelValue.red) / 255, green: CGFloat(currentPixelValue.green) / 255,blue: CGFloat(currentPixelValue.blue) / 255, alpha: CGFloat(currentPixelValue.alpha) / 255).setFill()
                }
                
                CGContextFillRect(ctx,
                                  CGRectMake(
                                  CGFloat(i) * virtualPixelSize + offset,
                                  CGFloat(j) * virtualPixelSize + offset,
                                  pixelSize,
                                  pixelSize));
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
            
            for var y = 0; y < frame.Height; y++
            {
                for var x = 0; x < frame.Width; x++
                {
                    if(x + actor.Location.X >= _width) {continue}
                    if(x + actor.Location.X < 0) {continue}
                    if(y + actor.Location.Y >= _height) {continue}
                    if(y + actor.Location.Y < 0) {continue}
                    
                    var flippedYaxis = frame.Height - 1 - y
                    
                    if(frame.Data[x][flippedYaxis].alpha != 0)
                    {
                        _screenBuffer[x + actor.Location.X][y + actor.Location.Y] = frame.Data[x][flippedYaxis]
                    }
                }
            }
        }
    }
    
    func ResetBuffer() {
        _screenBuffer = [[PixelColor]](count: 50, repeatedValue: [PixelColor](count: 50, repeatedValue: PixelColor(alpha: 0, red: 0, green: 0, blue: 0)))
    }
}